using AutoMapper;
using Infrastructure.EventServices;
using Infrastructure.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProductService.API;
using SharedLibrary.Domain.Models;
using SharedLibrary.Domain.Repositories;
using SharedLibrary.DomainLayer.ModelDTO;
using SharedLibrary.SharedItems.Shared;

namespace banreservaTecnicalTest.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private IRepositoryWrapper _repository { get; set; }
    private IMapper _mapper { get; set; }
    private IRateService _rateService { get; set; }
    private IProductService _productService { get; set; }
    private IMemoryCache _cache;
    private RabbitEventsService _rabbitService { get; set; }


    public ProductController(IRepositoryWrapper repo, IRateService rateService,
        IMapper mapper, IMemoryCache cache,IProductService productService,
        RabbitEventsService rabbit)
    {
        _mapper = mapper;
        _rateService = rateService;
        _repository = repo;
        _productService = productService;
        _cache = cache;
        _rabbitService = rabbit;
    }


    [HttpGet("all")]
    [ClaimCheck("VIEW_PRODUCTS")]

    public async Task<ActionResult> GetProducts([FromQuery] string? moneda = null)
    {
        var response = new ResponseGeneric<ProductResponse>();
        try
        {
            response.Items = await _productService.GetAllProductsCachedAsync(moneda);

            response.Result.SuccessGetResult();
        }
        catch (Exception ex)
        {
            response.Result.FailedResultServer(ex);

        }

        return StatusCode(response.Result.Code, response);
    }

    [HttpGet("{category}")]
    [ClaimCheck("VIEW_PRODUCTS")]

    public async Task<ActionResult> GetProductsByCategory(string category, [FromQuery] string? moneda = null)
    {
        var response = new ResponseGeneric<ProductResponse>();

        try
        {
            response.Items = await _productService.GetProductsByCategoryCachedAsync(category,moneda);

            response.Result.SuccessGetResult();
        }
        catch(Exception ex)
        {
            response.Result.FailedResultServer(ex);

        }

        return StatusCode(response.Result.Code, response);

    }


    [HttpGet("{id}")]
    [ClaimCheck("VIEW_PRODUCTS")]

    public async Task<ActionResult> GetProductsById(long id, [FromQuery] string? moneda)
    {
        var response = new ResponseGeneric<ProductResponse>();
        try
        {
            var product = await _repository.Products.FindByIdAsync(id);

            response.Data = _mapper.Map<ProductResponse>(product);

            if (!string.IsNullOrEmpty(moneda))
            {
                var rate = await _rateService.GetCurrencyValueAsync(moneda);
                response.Data.ConvertedPrice = _rateService.GetPriceConverted(rate, product.CurrentPrice);
            }

            response.Result.SuccessGetResult();

        }
        catch(Exception ex)
        {
            response.Result.FailedResultServer(ex);
        }
        return StatusCode(response.Result.Code, response);

    }


    [HttpPost("add")]
    [ClaimCheck("CREATE_PRODUCTS")]

    public async Task<ActionResult> AddProduct(ProductDTO model)
    {
        var response = new ResponseGeneric<ProductDTO>();
        try
        {
            var product = _mapper.Map<ProductDTO, Product>(model);

            await _repository.Products.CreateAsync(product);

            await _repository.SaveAsync();


            response.Data = _mapper.Map<ProductDTO>(product);

            response.Result.SuccessSaveResult();

            await _rabbitService.Publish(product);

            _cache.Remove("products_all");
             _cache.Remove($"products_category_{model.Category}");

            await _rabbitService.Publish(new ProductEventsDTO { Active = product.Active, WasCreated = true,Category = product.Category,
            Name = product.Name,SKU = product.SKU,Description = product.Description,LastModifiedDate = product.LastModifiedDate,
            EventId = Guid.NewGuid()}); 


        }
        catch (Exception ex)
        {
            response.Result.FailedResultServer(ex);

        }

        return StatusCode(response.Result.Code, response);
    }



    [HttpPut("update")]
    [ClaimCheck("UPDATE_PRODUCTS")]

    public async Task<ActionResult> UpdateProduct(ProductUpdateDTO model)
    {

        var response = new ResponseGeneric<ProductUpdateDTO>();
        try
        {
            var product = await _repository.Products.FindByCondition(t => t.Id == model.Id).FirstOrDefaultAsync();

            if(product == null)
            {
                response.Result.FailedBadRequest(new List<string>() { "El producto que intenta editar no existe" });
                return StatusCode(response.Result.Code, response);
            }
            product = _mapper.Map(model, product);
            _repository.Products.Update(product);
            await _repository.SaveAsync();


            //REGISTER A NEW PRICE IF THE LAST PRICE IS DIFERENT TO THE CURRENT PRICE SENDED IN THE UPDATE
            var lastPrice = await _repository.ProductPrices.GetLastProductPriceByProductAndAmountAsync(product.Id, product.CurrentPrice);
            if(lastPrice == null)
            {
                var price = new ProductPrices() { Price = product.CurrentPrice, ProductFk = product.Id };
                await _repository.ProductPrices.CreateAsync(price);
                await _repository.SaveAsync();
            }

            var result = _mapper.Map<ProductUpdateDTO>(product);
            response.Data = result;

            response.Result.SuccessSaveResult();
            await _rabbitService.Publish(new ProductEventsDTO
            {
                Active = product.Active,
                WasCreated = true,
                Category = product.Category,
                Name = product.Name,
                SKU = product.SKU,
                Description = product.Description,
                LastModifiedDate = product.LastModifiedDate,
                EventId = Guid.NewGuid()
            });

            _cache.Remove("products_all");
            _cache.Remove($"products_category_{model.Category}");

        }
        catch (Exception ex)
        {
            response.Result.FailedResultServer(ex);

        }

        return StatusCode(response.Result.Code, response);

    }

    [HttpDelete("delete/{id}")]
    [ClaimCheck("DELETE_PRODUCTS")]

    public async Task<ActionResult> DeleteProduct(long id)
    {
        var response = new ResponseGeneric<ProductDTO>();
        try
        {
            var product = await _repository.Products.FindByCondition(t => t.Id == id).FirstOrDefaultAsync();

            if(product == null)
            {
                response.Result.FailedBadRequest(new List<string>() { "El producto que intenta eliminar no existe" });
                return StatusCode(response.Result.Code, response);
            }

            _repository.Products.Delete(product);
            await _repository.SaveAsync();

            product.Deleted = true;
            response.Data = _mapper.Map<ProductDTO>(product);
            response.Result.Deleted();

            _cache.Remove("products_all");
            _cache.Remove($"products_category_{product.Category}");

            await _rabbitService.Publish(new ProductEventsDTO
            {
                Active = product.Active,
                WasCreated = true,
                Category = product.Category,
                Name = product.Name,
                SKU = product.SKU,
                Description = product.Description,
                LastModifiedDate = product.LastModifiedDate,
                EventId = Guid.NewGuid()

            });


        }
        catch (Exception ex)
        {
            response.Result.FailedResultServer(ex);
        }

        return StatusCode(response.Result.Code, response);
    }


}

