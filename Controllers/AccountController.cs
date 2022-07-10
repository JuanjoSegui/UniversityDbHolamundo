using Holamundo.DataAccess;
using Holamundo.Helpers;
using Holamundo.Models.DataModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using System.Net;

namespace Holamundo.Controllers
{
    [Route("api/[controller]/[action")]
    [ApiController]
    public class AccountController : ControllerBase
    {
         private readonly UniversityContext _context;

        private readonly JwtSettings _jwtSettings;
        public AccountController(UniversityContext context, JwtSettings jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings;
        }

        private IEnumerable<User> Logins = new List<User>()
        {
            new User()
            {
                Id = 1,
                Email = "juanjo@holamundo.es",
                Name = "Admin",
                Password = "Admin",
            },
            new User()
            {

                Id = 2,
                Email = "juan@holamundo.es",
                Name = "User1",
                Password = "Juan",
            }


        };

        [HttpPost]
        public IActionResult GetToken(UserLogins userLogins)
        {
            try
            {
                var Token = new UserTokens();

                var Valid = Logins.Any(user => user.Name.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));

                if (Valid)
                {
                    var user = Logins.FirstOrDefault(user => user.Name.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));

                    Token = JwtHelpers.GetTokenKey(new UserTokens()
                    {
                        UserName = user.Name,
                        EmailId = user.Email,
                        Id = user.Id,
                        GuidId = Guid.NewGuid(),

                    },_jwtSettings);

                }

                else
                {
                    return BadRequest("Wrong Password");

                }
                return Ok(Token);



            }catch (Exception ex)
            {
                throw new Exception("GetToken Error", ex);

            }

        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults, Roles = "Administrator")]
        public IActionResult GetUserList()
        {
            return Ok(Logins);

        }

    }

}
