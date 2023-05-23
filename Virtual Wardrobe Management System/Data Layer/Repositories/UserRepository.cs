﻿using DataAccessLayer.Data;
using System.Security.Cryptography;
using System.Text;
using Virtual_Wardrobe_Management_System.Business_Logic.RepositoryInterfaces;
using Virtual_Wardrobe_Management_System.Data_Layer.Entities;
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

        public Users GetByEmail(string email)
        {
            var result = _context.Users.FirstOrDefault(u => u.Email == email);
            if (result == null)
            {
                throw new ArgumentException("Email cannot be blank.");
            }
            return result;
        }

        public Users AddUser(Users user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
        public void SignUp(Users user)
        {
            var data = GetByEmail(user.Email);
            if (data != null)
            {
                throw new Exception("Email Already Exits");
            }
            var hashPassword = HashPassword(user.Password);
            var userEntity = new Users
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = hashPassword,
                ConfirmPassword = user.ConfirmPassword,
                Role = user.Role,
                DateOfBirth = user.DateOfBirth

            };
            AddUser(userEntity);
        }
        public Users Login(LoginRequest loginRequest)
        {
            var userEntity = GetByEmail(loginRequest.Email);
            if (userEntity == null)
            {
                throw new Exception("Email does not exist");
            }
            var hashedPasword = HashPassword(loginRequest.Password);
            if (userEntity.Password != hashedPasword)
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

        public string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

    }

}