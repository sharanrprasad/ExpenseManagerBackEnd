﻿using System.Threading.Tasks;
using ExpenseManagerBackEnd.Contracts;
using ExpenseManagerBackEnd.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManagerBackEnd.Repositories {
    public class UserRepository : IUserRepository {

        private readonly ExpenseManagerContext _context;

        public UserRepository(ExpenseManagerContext context) {
            _context = context;
        }

        public async Task<User> GetUser(string email) {
            return await _context.Users.SingleOrDefaultAsync(user => user.Email == email);
        }

        public async Task<User> CreateUser(User user) {

            user.UserId = Utils.TokenUtils.GetNewUserId();
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async  Task<bool> Exists(string userId) {
             return await _context.Users.AnyAsync(user => user.UserId == userId);
        }

        public async  Task<User> GetUserById(string userId) {
           
            return await _context.Users.SingleOrDefaultAsync(user => user.UserId == userId);
        }


    }
}