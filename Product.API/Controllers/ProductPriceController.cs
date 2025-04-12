using System;
using AutoMapper;
using DomainLayer.ModelsDTO;
using Infrastructure.IServices;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Domain.Models;
using SharedLibrary.Domain.Repositories;
using SharedLibrary.SharedItems.Shared;

namespace ProductService.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ProductPriceController:ControllerBase
	{

		private IRepositoryWrapper _repository;
		private IMapper _mapper;
		private IRateService _rateService;
		private IProductService _productService;

		public ProductPriceController(IRepositoryWrapper repository, IMapper mapper,
			IRateService rateService, IProductService productService)
		{
			_mapper = mapper;
			_repository = repository;
			_rateService = rateService;
			_productService = productService;
		}


		[HttpGet("{productId}")]
        [ClaimCheck("VIEW_PRICES")]

        public async Task<ActionResult> GetProductPrices(long productId, [FromQuery] string? moneda = null)
		{
			var response = new ResponseGeneric<ProductPricesDTO>();

			try
			{
				response.Items = await _productService.GetProductsPricesCachedAsync(productId,moneda);
                response.Result.SuccessGetResult();
            }
            catch (Exception ex)
			{
				response.Result.FailedResultServer(ex);
			}

			return StatusCode(response.Result.Code, response);

		}
	}
}

