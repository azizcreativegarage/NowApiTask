using Microsoft.AspNetCore.Authorization;
 using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
 using Microsoft.IdentityModel.Tokens;
using NowApiTask.Auth;
using NowApiTask.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static NowApiTask.Auth.ApplicationDbContext;

namespace NowApiTask
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        
        public AuthenticateController(UserManager<ApplicationUser> userManager,IConfiguration configuration, ApplicationDbContext context)
        {
            _userManager = userManager;
             _configuration = configuration;
            _context=context;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = _context.Users.Where(u=>u.UserName==model.Email && u.ipaddress==model.ipaddress && u.device==model.device).FirstOrDefault();
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                 var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                var token = GetToken(authClaims);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                     email = user.Email,
                     firstname=user.firstname,
                     lastname=user.lastname 
                });
            }
            return Unauthorized();
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Signup")]
        public async Task<IActionResult> Signup([FromBody] RegisterModel model)
        {
            // Email use as User Name
            var userExists = await _userManager.FindByNameAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });
            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email, 
                firstname=model.firstname,
                lastname=model.lastname,
                ipaddress=model.ipaddress,
                device=model.device
                
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
                return StatusCode(StatusCodes.Status200OK);
            else
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
               return Unauthorized();
         }
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(10),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }

}
