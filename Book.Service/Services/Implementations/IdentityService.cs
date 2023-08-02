using Book.Service.Dtos.Accounts;
using Book.Service.Responses;
using Book.Service.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Book.Service.Services.Implementations
{
    public class IdentityService:IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public IdentityService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<ApiResponse> Login(LoginDto dto)
        {
            IdentityUser? user = await _userManager.FindByNameAsync(dto.Username);

            if (user == null)
            {
                return new ApiResponse { StatusCode = 404, Description = "Username or password is not correct" };
            }

            if (!await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                return new ApiResponse { StatusCode = 404, Description = "Username or password is not correct" };
            }
            string keyStr = _configuration["Jwt:SecretKey"];
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyStr));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            JwtSecurityToken Jwttoken = new JwtSecurityToken(
                expires: DateTime.Now.AddDays(3),
               issuer: _configuration["Jwt:Issuer"],
               audience: _configuration["Jwt:Audience"],
               claims: claims,
               signingCredentials: credentials
                );
            string token = new JwtSecurityTokenHandler().WriteToken(Jwttoken);

            return new ApiResponse { StatusCode = 200, items = new { token = token } };
        }

        public async Task<ApiResponse> Register(RegisterDto dto)
        {
            IdentityUser user = new IdentityUser()
            {
                UserName = dto.UserName,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                return new ApiResponse { StatusCode = 400, items = result.Errors };
            }

            await _userManager.AddToRoleAsync(user, "Admin");
            return new ApiResponse { StatusCode = 200 };
        }
    }
}
