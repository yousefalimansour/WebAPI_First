using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI_First.DTO;
using WebAPI_First.Models;

namespace WebAPI_First.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountController(UserManager<ApplicationUser> applicationUser,IConfiguration configuration)
        {
            ApplicationUser = applicationUser;
            Configuration = configuration;
        }

        public UserManager<ApplicationUser> ApplicationUser { get; }
        public IConfiguration Configuration { get; }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO Userfromrequset)
        {
            if (ModelState.IsValid)
            {
                //Save in DB
                ApplicationUser userfromdb = new ApplicationUser();
                userfromdb.UserName = Userfromrequset.UserName;
                userfromdb.Email = Userfromrequset.Email;
                IdentityResult result =
                   await ApplicationUser.CreateAsync(userfromdb,Userfromrequset.Password);
                if (result.Succeeded)
                {
                    return Ok("Created");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("passord", item.Description);
                }
            }
            return BadRequest(ModelState);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO logfromrequset)
        {
            if(ModelState.IsValid)
            {
                //Check
                ApplicationUser userfromdb =
                  await ApplicationUser.FindByNameAsync(logfromrequset.UserName);
                if (userfromdb != null)
                { 
                    bool found =
                       await ApplicationUser.CheckPasswordAsync(userfromdb, logfromrequset.Password);
                    if (found)
                    {
                        //generate Token
                         
                        List<Claim> UserClaims = new List<Claim>();
                        UserClaims.Add(new Claim(ClaimTypes.NameIdentifier, userfromdb.Id));
                        UserClaims.Add(new Claim(ClaimTypes.Name, userfromdb.UserName));
                        UserClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        var UserRole = await ApplicationUser.GetRolesAsync(userfromdb);
                        foreach (var Rolename in UserRole)
                        {
                            UserClaims.Add(new Claim(ClaimTypes.Role, Rolename));
                        }
                        var signinkey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes("asdshdohjlsdjholah@@_^&*69909880"));

                        SigningCredentials signingCredentials =
                            new SigningCredentials(signinkey, SecurityAlgorithms.HmacSha256);

                        // Design Token 
                        JwtSecurityToken token = new JwtSecurityToken
                            (
                                issuer: Configuration["jwt:IssuerIP"] ,
                                audience: Configuration["jwt:AudienceIP"],
                                expires: DateTime.Now.AddHours(1),
                                claims: UserClaims,
                                signingCredentials: signingCredentials

                            );
                        // Generate Token response 
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = DateTime.Now.AddHours(1)
                        });






                    }
                }
                ModelState.AddModelError("UserName", "Password Or UserName Invalid");
            }
            return BadRequest(ModelState);
        }
    }
}
