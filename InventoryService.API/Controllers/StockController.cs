using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.IServices;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SharedLibrary.Domain.Models;
using SharedLibrary.Domain.Repositories;
using SharedLibrary.DomainLayer.ModelDTO;
using SharedLibrary.SharedItems.Shared;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryService.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {

        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private IStockServiceCached _stockService;
        private IMemoryCache _memoryCache; 

        public StockController(IRepositoryWrapper repo, IMapper mapper,
            IStockServiceCached stockService, IMemoryCache memoryCache)
        {
            _repository = repo;
            _mapper = mapper;
            _stockService = stockService;
            _memoryCache = memoryCache;
        }


        [HttpPost("refresh")]
        [ClaimCheck("UPDATE_STOCK")]

        public async Task<ActionResult> RefreshStock(StockUpdateDTO model)
        {
            var response = new ResponseGeneric<StockDTO>();

            try
            {
                var stock = _mapper.Map<Stock>(model);
                await  _repository.Stocks.CreateAsync(stock);
                await _repository.SaveAsync();

                response.Data = _mapper.Map<StockDTO>(stock);

                response.Result.SuccessSaveResult();
                _memoryCache.Remove($"stocks_product_history_{model.ProductFk}");
            }
            catch(Exception ex)
            {
                response.Result.FailedResultServer(ex);
            }

            return StatusCode(response.Result.Code, response);
        }


        [HttpGet("quantity/{productId}")]
        [ClaimCheck("VIEW_PRODUCTS")]

        public async Task<ActionResult> ProductQuantity(long productId)
        {
            var response = new ResponseGenericStruct<long>();

            try
            {
                response.Data = _repository.Stocks.GetProductQuantity(productId);
                response.Result.SuccessGetResult();
            }
            catch(Exception ex)
            {
                response.Result.FailedResultServer(ex);

            }

            return StatusCode(response.Result.Code, response);

        }

        [HttpGet("history/{productId}")]
        [ClaimCheck("VIEW_PRODUCTS")]

        public async Task<ActionResult> ProductStockHistory(long productId)
        {
            var response = new ResponseGeneric<StockDTO>();

            try
            {
                var stocks = await _stockService.GetProductStockCachedAsync(productId);
                response.Items = _mapper.Map<List<StockDTO>>(stocks);

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

