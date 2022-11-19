using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace OutputCaching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("/Get")]
        [OutputCache(PolicyName ="Custom")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
