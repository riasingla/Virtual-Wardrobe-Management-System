using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using Virtual_Wardrobe_Management_System.Business_Logic.RepositoryInterfaces;
using Virtual_Wardrobe_Management_System.Data_Layer.Entities;
using Virtual_Wardrobe_Management_System.Data_Layer.Entities.Authentication___Authorization;
using Virtual_Wardrobe_Management_System.Data_Layer.Repositories;

namespace Virtual_Wardrobe_Management_System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public UserController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpPost("signup")]
        public IActionResult Signup(Users user)
        {
            try
            {
                _userRepository.SignUp(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var u = _userRepository.Login(loginRequest);

                var tokenHandler = new JwtSecurityTokenHandler();
                var Key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Jwt:Key"));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, u.FirstName),
                        new Claim(ClaimTypes.Email, u.Email),
                        new Claim(ClaimTypes.Role, u.Role.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(_configuration.GetValue<int>("Jwt:ExpiryInHours")),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new { Token = tokenString });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
