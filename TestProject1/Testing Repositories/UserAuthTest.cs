using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Virtual_Wardrobe_Management_System.Business_Logic.RepositoryInterfaces;
using Virtual_Wardrobe_Management_System.Data_Layer.Entities.Authentication___Authorization;

namespace Virtual_Wardrobe_Management_System.Data_Layer.Repositories.Tests
{
    public class UserRepositoryTests
    {
        private Mock<ApplicationDbContext> _contextMock;
        private UserRepository _userRepository;

        [SetUp]
        public void Setup()
        {
            _contextMock = new Mock<ApplicationDbContext>();
            _userRepository = new UserRepository(_contextMock.Object);
        }

        [Test]
        public void GetByEmail_ValidEmail_ReturnsUser()
        {
            // Arrange
            string email = "test@example.com";
            var users = GetTestUsers();
            var usersDbSetMock = GetMockDbSet(users);
            _contextMock.Setup(c => c.Users).Returns(usersDbSetMock.Object);

            // Act
            var result = _userRepository.GetByEmail(email);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(email, result.Email);
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
            var users = GetTestUsers();
            var usersDbSetMock = GetMockDbSet(users);
            _contextMock.Setup(c => c.Users).Returns(usersDbSetMock.Object);

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
                DateOfBirth = DateTime.Now
            };

            var users = new List<Users>();
            var usersDbSetMock = GetMockDbSet(users);
            _contextMock.Setup(c => c.Users).Returns(usersDbSetMock.Object);

            // Act
            var result = _userRepository.AddUser(user);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(user.UserId, result.UserId);
            Assert.AreEqual(user.FirstName, result.FirstName);
            Assert.AreEqual(user.LastName, result.LastName);
            Assert.AreEqual(user.Email, result.Email);
        }

        [Test]
        public void AddUser_ExceptionOccurs_ThrowsException()
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
                DateOfBirth = DateTime.Now
            };

            var users = new List<Users>();
            var usersDbSetMock = GetMockDbSet(users);
            _contextMock.Setup(c => c.Users).Returns(usersDbSetMock.Object);

            _contextMock.Setup(c => c.SaveChanges()).Throws<Exception>();

            // Act & Assert
            Assert.Throws<Exception>(() => _userRepository.AddUser(user));
        }

        private List<Users> GetTestUsers()
        {
            return new List<Users>
            {
                new Users
                {
                    UserId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "test@example.com",
                    Password = "password",
                    ConfirmPassword = "password",
                    Role = (RoleType)1,
                    DateOfBirth = DateTime.Now
                }
            };
        }

        private Mock<DbSet<T>> GetMockDbSet<T>(List<T> list) where T : class
        {
            var queryable = list.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            return dbSetMock;
        }

        public string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}
