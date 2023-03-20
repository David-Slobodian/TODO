using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TODO.BL.Authentication;
using TODO.BL.AuthService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TODO.BL.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace TODO.BL.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<UserAuth> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IConfiguration _config;

        public AuthService(UserManager<UserAuth> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
        }
        public async Task<string> Login(LoginModel login)
        {
            var entity = await _userManager.FindByNameAsync(login.UserName);

            if (entity != null && await _userManager.CheckPasswordAsync(entity, login.Password))
            {
                var roles = await _userManager.GetRolesAsync(entity);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, entity.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                foreach (var userRole in roles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
                var expiresAt = Convert.ToDouble(_config["JWT:LifetimeInSeconds"]);

                var token = new JwtSecurityToken(
                    issuer: _config["JWT:Issuer"],
                    audience: _config["JWT:Audience"],
                    expires: DateTime.UtcNow.Add(
                            TimeSpan.FromSeconds(expiresAt)),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.Sha256)
                );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            throw new UnauthorizedAccessException();
        }

        public async Task<Response> Registration(RegistrationModel model)
        {
            if (model.UserName.Contains("teacher"))
            {
                return await RegisterTeacher(model);
            }


            if (await _userManager.FindByNameAsync(model.UserName) != null)
            {
                return new Response
                {
                    Status = "Failed",
                    Message = "The account already exists"
                };
            }
            var result = await _userManager.CreateAsync(new UserAuth()
            {
                UserName = model.UserName,
                Id = new Guid().ToString()
            },
            model.Password);

            if (!result.Succeeded)
            {
                return new Response
                {
                    Status = "Failed",
                    Message = "An error occured while creating an account"
                };
            }
            return new Response
            {
                Status = "Success",
                Message = "Account has been successfully registered!"
            };
        }
        private async Task<Response> RegisterTeacher(RegistrationModel model)
        {
            if (await _userManager.FindByNameAsync(model.UserName) != null)
            {
                return new Response
                {
                    Status = "Failed",
                    Message = "The account already exists"
                };
            }
            UserAuth user = new()
            {
                UserName = model.UserName,
                Id = new Guid().ToString()
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return new Response { Status = "Failed", Message = "An error occured while creating an account" };

            if (!await _roleManager.RoleExistsAsync(Roles.Teacher))
                await _roleManager.CreateAsync(new IdentityRole(Roles.Teacher));

            if (await _roleManager.RoleExistsAsync(Roles.Teacher))
            {
                await _userManager.AddToRoleAsync(user, Roles.Teacher);
            }

            return new Response { Status = "Success", Message = "Successfully registered" };
        }
    }
}
