using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TourAPI.Models.Request;
using TourAPI.Services;
using User.Manage.API.Models;

namespace TourAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IEmailService emailService;
        private IConfiguration configuration;

        public AuthenticationController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager
            , IConfiguration configuration, IEmailService emailService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
            this.emailService = emailService;
        }

        [HttpPost("Registration")]
        public async Task<ActionResult> Registration([FromForm] RegisterRequest request, string role)
        {
            var user = (IdentityUser)request;
            var existUser = await userManager.FindByEmailAsync(user.Email);
            if (existUser is null)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (roleExist)
                {
                    var createResult = await userManager.CreateAsync(user, request.Password);
                    if (createResult.Succeeded)
                    {
                        createResult = await userManager.AddToRoleAsync(user, role);
                        var token = await userManager.GenerateChangeEmailTokenAsync(user, user.Email);

                        var confirmLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { token, email = user.Email }, Request.Scheme);
                        var message = new Message
                        (
                            new string[] { user.Email },
                            "Confirm Email",
                            confirmLink
                        );
                        var sendEmailResult = await emailService.CreateMailMessage(message);
                        if (sendEmailResult is not null)
                        {
                            return Ok(new
                            {
                                Status = "Success",
                                Message = $"User Created & Confirm your Email: {user.Email}"
                            });
                        }
                        return BadRequest(new
                        {
                            Status = "Error",
                            Message = "Email is not exist",
                        });
                    }
                    return BadRequest(new
                    {
                        Status = "Error",
                        Message = createResult.Errors.Select(x => x.Description).ToList()
                    });
                }
                return BadRequest(new
                {
                    Status = "Error",
                    Message = "Role is not exist!"
                });
            }
            return BadRequest(new
            {
                Status = "Error",
                Message = "Email is already exist!"
            });
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return Ok(new
                    {
                        Status = "Success",
                        Message = "Email Verified Successfully",
                    });
                }
                return BadRequest(new
                {
                    Status = "Error",
                    Message = "Invalid Token"
                });
            }
            return BadRequest(new
            {
                Status = "Error",
                Message = "User does not exist"
            });
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user is not null)
            {
                var passwordResult = await userManager.CheckPasswordAsync(user, password);
                if (passwordResult)
                {
                    //Get Roles
                    var roles = await userManager.GetRolesAsync(user);
                    //Generate claims
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, user.Email)
                    };
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Email, role));
                    }
                    //Generate Token
                    var token = GenerateToken(claims);

                    return Ok(new
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Expires = token.ValidTo
                    });



                }
                return BadRequest(new
                {
                    Status = "Error",
                    Message = "Password is incorrect"
                });
            }
            return NotFound(new
            {
                Status = "Error",
                Message = "Email is not exist!"
            });
        }

        private JwtSecurityToken GenerateToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                audience: configuration["Jwt:Audience"],
                issuer: configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: credential
            );

            return token;
        }
    }
}
