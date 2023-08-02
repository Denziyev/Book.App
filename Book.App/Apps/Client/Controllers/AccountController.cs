using Book.Service.Dtos.Accounts;
using Book.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace WebApplication1.Apps.Client.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IIdentityService _identityService;

        public AccountController(IIdentityService identityService, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _identityService = identityService;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var result = await _identityService.Register(dto);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _identityService.Login(dto);
            return StatusCode(result.StatusCode, result);
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateRole()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "Member" });
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });

        //    return Ok();
        //}
    }
}
