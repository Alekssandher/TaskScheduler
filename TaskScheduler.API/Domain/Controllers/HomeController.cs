using Microsoft.AspNetCore.Mvc;

namespace TaskScheduler.API.Domain.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]
        [EndpointName("Home")]
        [EndpointSummary("Base Endpoint")]
        [EndpointDescription("Redirects to API documentation.")]
        public IActionResult Get()
        {
            return Redirect("/docs");
        }
    }
}