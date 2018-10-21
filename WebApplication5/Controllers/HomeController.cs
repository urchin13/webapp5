using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using WebApplication5.Services;

namespace WebApplication5.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly CatalogService _catalogService;

        public HomeController(CatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var result = _catalogService.GetAllPhones();

            return Ok("Хуй");
        }
    }
}