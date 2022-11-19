using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace Dotnet7Yenilikler.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableRateLimiting("Basic")]
    public class ValuesController : ControllerBase
    {
        [HttpGet("/Get")]
        public IActionResult Get()
        {
            return Ok();
        }
        [HttpGet("/Get2")]
        public async Task<IActionResult> Get2()
        {
            await Task.Delay(20000);
            return Ok();
        }


    }
    class CustomRateLimitPolicy : IRateLimiterPolicy<string>
    {
        public Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected =>
            (Context, CancellationToken) =>
            {
                return new();
            };

        public RateLimitPartition<string> GetPartition(HttpContext httpContext)
        {
            return RateLimitPartition.GetFixedWindowLimiter("" _ => new(){
                PermitLimit =4,
                Window =TimeSpan.FromSeconds(12),
                QueueLimit=2,
                QueueProcessingOrder =QueueProcessingOrder.OldestFirst
            });
        }
    }
}
