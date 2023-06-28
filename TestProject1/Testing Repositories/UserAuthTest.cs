using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using Virtual_Wardrobe_Management_System.Business_Logic.RepositoryInterfaces;
using Virtual_Wardrobe_Management_System.Data_Layer.Entities.Authentication___Authorization;

namespace Virtual_Wardrobe_Management_System.Data_Layer.Repositories.Tests
{
    public class UserRepositoryTests
    {
        private Mock<ApplicationDbContext> _contextMock;
        private IUserRepository _userRepository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var dbContext = new ApplicationDbContext(options);
            _userRepository = new UserRepository(dbContext);
        }

        [Test]
        public void GetByEmail_ValidEmail_ReturnsUser()
        {
            // Arrange
            string email = "test@example.com";
            var user = new Users
            {
                UserId = 1001,
                FirstName = "John",
                LastName = "Doe",
                Email = email,
                Password = "password",
                ConfirmPassword = "password",
                Role = (RoleType)1
            };
            _userRepository.AddUser(user);

            // Act
            var result = _userRepository.GetByEmail(email);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Email, Is.EqualTo(email));
        }

        [Test]
        public void GetByEmail_EmptyEmail_ThrowsArgumentException()
        {
            // Arrange
            string email = string.Empty;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _userRepository.GetByEmail(email));
        }

        [Test]
        public void GetByEmail_UserNotFound_ThrowsArgumentException()
        {
            // Arrange
            string email = "nonexistent@example.com";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _userRepository.GetByEmail(email));
        }

        [Test]
        public void AddUser_ValidUser_ReturnsUser()
        {
            // Arrange
            var user = new Users
            {
                UserId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "test@example.com",
                Password = "password",
                ConfirmPassword = "password",
                Role = (RoleType)1,
            };

            // Act
            var result = _userRepository.AddUser(user);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.UserId, Is.EqualTo(user.UserId));
            Assert.That(result.FirstName, Is.EqualTo(user.FirstName));
            Assert.That(result.LastName, Is.EqualTo(user.LastName));
            Assert.That(result.Email, Is.EqualTo(user.Email));
        }

        [Test]
        public void AddUser_ExceptionOccurs_ThrowsException()
        {
            // Arrange
            var user = new Users
            {
                UserId = 1003,
                FirstName = "John",
                LastName = "Doe",
                Email = "test1@example.com",
                Password = "password",
                ConfirmPassword = "password",
                Role = (RoleType)1
            };

            // Act & Assert
            Assert.Throws<Exception>(() => _userRepository.AddUser(user));
        }

        [Test]
        
        public void SignUp_ValidUser_ReturnsUser()
        {
            // Arrange
            var user = new Users
            {
                UserId = 1111,
                FirstName = "Johnny",
                LastName = "Doe",
                Email = "test3@example.com",
                Password = "password",
                ConfirmPassword = "password",
                Role = (RoleType)1
            };

            // Act & Assert
            Assert.DoesNotThrow(() => _userRepository.SignUp(user));
        }


        [Test]
        public void SignUp_EmptyEmail_ThrowsArgumentException()
        {
            // Arrange
            var user = new Users
            {
                UserId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = string.Empty,
                Password = "password",
                ConfirmPassword = "password",
                Role = (RoleType)1
            };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _userRepository.SignUp(user));
        }

        [Test]
        public void SignUp_EmailAlreadyRegistered_ThrowsArgumentException()
        {
            // Arrange
            string email = "test@example.com";
            var existingUser = new Users
            {
                UserId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = email,
                Password = "password",
                ConfirmPassword = "password",
                Role = (RoleType)1
            };
            _userRepository.AddUser(existingUser);

            var newUser = new Users
            {
                UserId = 2,
                FirstName = "Jane",
                LastName = "Smith",
                Email = email,
                Password = "password",
                ConfirmPassword = "password",
                Role = (RoleType)2
            };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _userRepository.SignUp(newUser));
        }

        [Test]
        public void Login_ValidCredentials_ReturnsUser()
        {
            // Arrange
            string email = "test@example.com";
            string password = "password";
            var user = new Users
            {
                UserId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = email,
                Password = _userRepository.HashPassword(password),
                ConfirmPassword = _userRepository.HashPassword(password),
                Role = (RoleType)1
            };
            _userRepository.AddUser(user);

            var loginRequest = new LoginRequest
            {
                Email = email,
                Password = password
            };

            // Act
            var result = _userRepository.Login(loginRequest);

            // Assert
            Assert.NotNull(result);
            Assert.That( result.UserId, Is.EqualTo(user.UserId));
            Assert.That(result.FirstName, Is.EqualTo(user.FirstName));
            Assert.That(result.LastName, Is.EqualTo(user.LastName));
            Assert.That(result.Email, Is.EqualTo(user.Email));
        }

        [Test]
        public void Login_InvalidEmail_ThrowsException()
        {
            // Arrange
            string email = "test@example.com";
            string password = "password";
            var user = new Users
            {
                UserId = 1002,
                FirstName = "John",
                LastName = "Doe",
                Email = email,
                Password = _userRepository.HashPassword(password),
                ConfirmPassword = _userRepository.HashPassword(password),
                Role = (RoleType)1
            };
            _userRepository.AddUser(user);

            var loginRequest = new LoginRequest
            {
                Email = "nonexistent@example.com",
                Password = password
            };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _userRepository.Login(loginRequest));
        }

        [Test]
        public void Login_InvalidPassword_ThrowsException()
        {
            // Arrange
            string email = "test@example.com";
            string password = "password";
            var user = new Users
            {
                UserId = 1002,
                FirstName = "John",
                LastName = "Doe",
                Email = email,
                Password = _userRepository.HashPassword(password),
                ConfirmPassword = _userRepository.HashPassword(password),
                Role = (RoleType)1
            };
            _userRepository.AddUser(user);

            var loginRequest = new LoginRequest
            {
                Email = email,
                Password = "invalidpassword"
            };

            // Act & Assert
            Assert.Throws<Exception>(() => _userRepository.Login(loginRequest));
        }

        [Test]
        public void HashPassword_ValidPassword_ReturnsHashedPassword()
        {
            // Arrange
            string password = "password";
            var expectedHash = _userRepository.HashPassword(password);

            // Act
            var result = _userRepository.HashPassword(password);

            // Assert
            Assert.That( result, Is.EqualTo(expectedHash));
        }
    }
}
