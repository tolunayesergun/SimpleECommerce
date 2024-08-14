using AutoMapper;
using Core.Data.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Stock.Api.Models.DTOs;
using Stock.Api.Models.Entities;

namespace Stock.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ILogger<StockController> _logger;
        private readonly IGenericRepository<StockModel> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public StockController(ILogger<StockController> logger, IGenericRepository<StockModel> repository, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("CreateStock")]
        public async Task<IActionResult> CreateStock(StockDetailDTO stockDetail)
        {

            var existStock = _repository.FindBy(x => x.ProductId == stockDetail.ProductId).FirstOrDefault();

            if (existStock != null)
            {
                existStock.TotalCount += stockDetail.TotalCount;
                existStock.UpdatedDate = DateTime.Now;
                _repository.Update(existStock);
            }
            else
            {
                var stockModel = new StockModel
                {
                    ProductId = stockDetail.ProductId,
                    TotalCount = stockDetail.TotalCount,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                };

                _repository.Add(stockModel);
            }

            await _unitOfWork.CommitAsync();

            return Ok("Stok Eklendi");
        }
    }
}
