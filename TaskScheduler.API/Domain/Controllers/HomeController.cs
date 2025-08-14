using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TaskScheduler.API.Domain.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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