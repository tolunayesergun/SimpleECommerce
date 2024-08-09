using Core.Models.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Order.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IPublishEndpoint _publish;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IPublishEndpoint publish)
        {
            _logger = logger;
            _publish = publish;
        }

        [HttpGet]
        public async Task<IActionResult> Post()
        {

            var order = new CreatedOrderModel { Id = 1, Name = "deneme" };


            await _publish.Publish<CreatedOrderModel>(order);
            return Ok("Gönderdiðiniz ürün tarafýmýza ulaþmýþ ve gerekli iþlemler gerçekleþtirilmiþtir. Ýlginiz için teþekkür ederiz.");
        }
    }
}
