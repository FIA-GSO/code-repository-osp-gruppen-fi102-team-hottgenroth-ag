using Logisitcs.BLL.Interfaces;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
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
        private readonly ILoginBll bll;

        public LoginController(IConfiguration config, ILoginBll bll)
        {
            _config = config;
            this.bll = bll;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] ILoginData login)
        {
            IActionResult response = Unauthorized();
            var user = await bll.Login(login);

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

            try
            {
                var user = await bll.Register(login);

                if (user != null)
                {
                    response = Ok();
                }

                return response;
            }
            catch (DuplicateNameException ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateRole([FromBody] IUserData user)
        {
            IActionResult response = Unauthorized();
            var role = GetUserRole(await HttpContext.GetTokenAsync("access_token"));

            //user ist z.b Lagerist und darf die rolle von anderen leuten ändern
            if (ValidateUserRole(role))
            {
                bool updated = await bll.UpdateRole(user);
                response = Ok(updated);
            }

            return response;
        }

        private static bool ValidateUserRole(string role)
        {
            return role == "Admin" || role == "Storekeeper" || role == "TeamLeader";
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