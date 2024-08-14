using AutoMapper;
using Core.Data.Abstracts;
using Core.Models.Events.Order;
using Core.Models.Product;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Order.Api.Models.DTOs;
using Order.Api.Models.Entities;

namespace Order.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly IPublishEndpoint _publish;
        private readonly ILogger<OrderController> _logger;
        private readonly IGenericRepository<OrderModel> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderController(ILogger<OrderController> logger, IPublishEndpoint publish, IGenericRepository<OrderModel> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _publish = publish;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(OrderDetailDTO orderDetail)
        {

            var orderModel = new OrderModel
            {
                CustomerId = orderDetail.CustomerId,
                CreatedDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
            };

            _repository.Add(orderModel);

            await _unitOfWork.CommitAsync();

            var order = new OrderCreated
            {
                OrderId = orderModel.Id,
                Products = _mapper.Map<List<ProductDetail>>(orderDetail.productDetails)
            };

            await _publish.Publish<OrderCreated>(order);

            return Ok("Siparişleriniz Sepete Eklendi");
        }

        [HttpGet("ConfirmOrder")]
        public async Task<IActionResult> ConfirmOrder()
        {

            //Stock Reserve edildiyse order'ı confirmle

            var order = new OrderConfirmed
            {
                OrderId = 1
            };

            await _publish.Publish<OrderConfirmed>(order);

            return Ok("Gönderdiğiniz ürün tarafımıza ulaşmış ve gerekli işlemler gerçekleştirilmiştir. İlginiz için teşekkür ederiz.");
        }
    }
}
