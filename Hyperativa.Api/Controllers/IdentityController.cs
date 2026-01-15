using Hyperativa.Core.Controller;
using Microsoft.AspNetCore.Mvc;
using Hyperativa.Api.Models.Dto;
using Hyperativa.Api.Services;

namespace Hyperativa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController(
    IIdentityService service) : ControllerBase
    {
        private readonly IIdentityService _service = service;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var response = await _service.Login(request);
            return this.ToActionResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto request)
        {
            var response = await _service.RegisterUser(request);
            return this.ToActionResult(response);
        }
    }
}
