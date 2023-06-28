using DataAccessLayer.Data;
using System.Security.Cryptography;
using System.Text;
using Virtual_Wardrobe_Management_System.Business_Logic.RepositoryInterfaces;
using Virtual_Wardrobe_Management_System.Data_Layer.Entities.Authentication___Authorization;

namespace Virtual_Wardrobe_Management_System.Data_Layer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Retrieve a user by email
        public Users GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email cannot be blank.");
            }

            var result = _context.Users.FirstOrDefault(u => u.Email == email);
            if (result == null)
            {
                throw new ArgumentException("No user found with the specified email.");
            }

            return result;
        }

        // Add a user
        public Users AddUser(Users user)
        {
            try
            {
                if (_context.Users.Any(u => u.Email == user.Email))
                {
                    throw new ArgumentException("Email is already registered.");
                }

                _context.Users.Add(user);
                _context.SaveChanges();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the user.", ex);
            }
        }

        // Sign up a user
        public void SignUp(Users user)
        {
            if (string.IsNullOrEmpty(user.Email))
            {
                throw new ArgumentException("Email cannot be blank.");
            }

            try
            {
                if (_context.Users.Any(u => u.Email == user.Email))
                {
                    throw new ArgumentException("Email is already registered.");
                }

                var hashPassword = HashPassword(user.Password);

                var userEntity = new Users
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = hashPassword,
                    ConfirmPassword = hashPassword,
                    Role = user.Role,
                };

                _context.Users.Add(userEntity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("An error occurred while saving the entity changes.", ex);
            }
        }

        // User login
        public Users Login(LoginRequest loginRequest)
        {
            var userEntity = GetByEmail(loginRequest.Email);
            if (userEntity == null)
            {
                throw new Exception("Email does not exist");
            }
            var hashedPassword = HashPassword(loginRequest.Password);
            if (userEntity.Password != hashedPassword)
            {
                throw new Exception("Invalid password");
            }

            return new Users
            {
                UserId = userEntity.UserId,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Email = userEntity.Email,
                Password = userEntity.Password,
                ConfirmPassword = userEntity.ConfirmPassword,
                Role = userEntity.Role,
            };
        }

        // Hash the password using SHA256 algorithm
        public string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }


    }

}
