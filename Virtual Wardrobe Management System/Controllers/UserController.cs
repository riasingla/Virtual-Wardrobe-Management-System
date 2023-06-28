using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using Virtual_Wardrobe_Management_System.Business_Logic.RepositoryInterfaces;
using Virtual_Wardrobe_Management_System.Data_Layer.Entities.Authentication___Authorization;

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

        // Endpoint for user signup
        [HttpPost("signup")]
        [EnableCors("AllowLocalhost")]
        public IActionResult Signup(Users user)
        {
            // Validate the password
            var passwordValidationResult = ValidatePassword(user.Password);
            if (!passwordValidationResult.IsValid)
            {
                return BadRequest(passwordValidationResult.ErrorMessages);
            }
            try
            {
                // Call the repository to sign up the user
                _userRepository.SignUp(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint for user login
        [HttpPost("login")]
        [EnableCors("AllowLocalhost")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                // Call the repository to perform user login
                var user = _userRepository.Login(loginRequest);

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Jwt:Key"));

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.FirstName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                    new Claim("UserId", user.UserId.ToString())
                };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(_configuration.GetValue<int>("Jwt:ExpiryInHours")),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                // Generate the JWT token
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                // Return the UserId along with the Token and Role
                return Ok(new { Token = tokenString, Role = user.Role, UserId = user.UserId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Helper method to validate password
        private PasswordValidationResult ValidatePassword(string password)
        {
            var result = new PasswordValidationResult();

            if (password.Length < 8)
            {
                result.ErrorMessages.Add("Password should be at least 8 characters long.");
            }

            if (!Regex.IsMatch(password, @"[A-Z]"))
            {
                result.ErrorMessages.Add("Password should contain at least one capital letter.");
            }

            if (!Regex.IsMatch(password, @"\d"))
            {
                result.ErrorMessages.Add("Password should contain at least one number.");
            }

            if (!Regex.IsMatch(password, @"[\W_]"))
            {
                result.ErrorMessages.Add("Password should contain at least one special character.");
            }

            result.IsValid = result.ErrorMessages.Count == 0;
            return result;
        }

        // Helper class for password validation result
        public class PasswordValidationResult
        {
            public bool IsValid { get; set; }
            public List<string> ErrorMessages { get; }

            public PasswordValidationResult()
            {
                IsValid = false;
                ErrorMessages = new List<string>();
            }
        }

    }
}
