using Grpc.Core;
using GrpcGreeterClient;
using Microsoft.AspNetCore.Mvc;

namespace TestGrpcWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly Greeter.GreeterClient _client;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
                                        Greeter.GreeterClient client)
        {
            _logger = logger;
            _client=client;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {

            try
            {
                //³]©w´Á­­
                var reply =await _client.SayHelloAsync(
                                  new HelloRequest { Name = "GreeterClient" }
                                                      );
                Console.WriteLine("Greeting: " + reply.Message);
            }
            catch (RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.DeadlineExceeded)
            {
                Console.WriteLine("Greeting timeout.");
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine("Greeting cancel.");
            }
                

    return Ok();    
        }
    }
}
