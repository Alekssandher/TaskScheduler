using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskScheduler.API.Domain.Interfaces;
using TaskScheduler.API.Domain.Models;
using TaskScheduler.API.Infrastructure.Db;

namespace TaskScheduler.API.Domain.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext _context;

        public UserRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task Add(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);

        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}