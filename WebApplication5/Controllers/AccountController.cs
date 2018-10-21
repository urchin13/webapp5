using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using WebApplication5.EF;
using WebApplication5.Models;
using WebApplication5.Models.Auth;
using WebApplication5.Services;

namespace WebApplication5.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationContext _context;

        private readonly AccountService _account;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, AccountService account, ApplicationContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _account = account;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return Ok("Вы успешно зарегистрированы");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return Ok(ModelState);
        }

        [HttpPost]
        public async Task Login([FromBody]LoginModel model)
        {
            var identity = await GetIdentity(model.Login, model.Password);
            if (identity == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Invalid username or password.");
                return;
            }

            var encodedJwt = GenerateToken(identity, model.RememberMe);

            var response = new
            {
                Token = encodedJwt,
                Name = model.Login
            };

            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        private async Task<ClaimsIdentity> GetIdentity(string email = "", string password = "")
        {
            User user;
            user = _context.Users.FirstOrDefault(x => x.Email == email);

            if (user != null)
            {
                if (!await _userManager.CheckPasswordAsync(user, password)) return null;


                var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                new Claim("UserId", user.Id),
                new Claim("UserEmail", user.Email)
            };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }

        private string GenerateToken(ClaimsIdentity identity, bool isRemember)
        {
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: DateTime.UtcNow.AddMinutes(-5),
                claims:identity.Claims,
                expires: isRemember ? DateTime.UtcNow.AddDays(3) : DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}