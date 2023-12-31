﻿using WebApplication1.Data;
using WebApplication1.Entities;

namespace WebApplication1.Services
{
    public class UserAuthenticationService
    {
        private readonly DatabaseContext _db;

        public UserAuthenticationService(DatabaseContext context)
        {
            _db = context;
        }

        public User? Authenticate(string username, string password)
        {
            var user = _db.Users.SingleOrDefault(u => u.Username == username);
            if (user == null)
                return null;

            var isPasswordValid = VerifyPassword(password, user.Password!);
            if (!isPasswordValid)
                return null;

            return user;
        }

        private bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            //dummy comparison WITHOUT HASHING
            return enteredPassword == storedHashedPassword;
        }
    }
}
