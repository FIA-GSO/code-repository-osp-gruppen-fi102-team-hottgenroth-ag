using Logisitcs.BLL.Interfaces;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Logistics.API.Controllers
{
   [Route("[controller]")]
   [ApiController]
   public class LoginController : Controller
   {
      private IConfiguration _config;
      private ILoginBLL _BLL;

      public LoginController(IConfiguration config, ILoginBLL bll)
      {
         _config = config;
         _BLL = bll;
      }

      [AllowAnonymous]
      [HttpPost]
      public async Task<IActionResult> Login([FromBody] ILoginData login)
      {
         IActionResult response = Unauthorized();
         var user = await _BLL.Login(login);

         if (user != null)
         {
            var tokenString = GenerateJSONWebToken(user);
            response = Ok(new { token = tokenString });
         }

         return response;
      }

      [AllowAnonymous]
      [HttpPost("Register")]
      public async Task<IActionResult> Register([FromBody] ILoginData login)
      {
         IActionResult response = Unauthorized();
         var user = await _BLL.Register(login);

         if (user != null)
         {
            response = Ok();
         }

         return response;
      }

      [Authorize]
      [HttpPut]
      public async Task<IActionResult> UpdateRole([FromBody] IUserData user)
      {
         IActionResult response = Unauthorized();
         var role = GetUserRole(await HttpContext.GetTokenAsync("access_token"));

         //user ist z.b Lagerist und darf die rolle von anderen leuten ändern
         if (role == "")
         {
            var updated = await _BLL.UpdateRole(user);
            response = Ok();
         }

         return response;
      }

      private string GenerateJSONWebToken(IUserData userInfo)
      {
         var claims = GetValidClaims(userInfo);
         var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
         var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

         var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);
         return new JwtSecurityTokenHandler().WriteToken(token);
      }

      private string GetUserRole(string token) 
      {
         var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
         var claim = decodedToken.Claims.First(c => c.Type == "Role").Value;
         return claim;
      }

      private List<Claim> GetValidClaims(IUserData user)
      {
         IdentityOptions _options = new IdentityOptions();
         var claims = new List<Claim>
         {
            new Claim("UserId", user.UserId.ToString()),
            new Claim("UserEmail", user.UserEmail),
            new Claim("Role", user.Role)
         };
         return claims;
      }
   }
}
