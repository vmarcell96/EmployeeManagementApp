﻿using EmployeeManagementApp.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementApp.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EmployeeManagementAppContext _context;

        public UserRepository(EmployeeManagementAppContext context)
        {
            _context = context;
        }

        public async Task Add(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            _context.Users.Remove(await Get(id));
            await _context.SaveChangesAsync();
        }

        public async Task<User> Get(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(user => user.ID == id);
            return user;
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User> Update(User entity)
        {
            var userToUpdate = await Get(entity.ID);
            userToUpdate.HashedPassword = entity.HashedPassword;
            userToUpdate.FirstName = entity.FirstName;
            userToUpdate.LastName = entity.LastName;
            await _context.SaveChangesAsync();
            return userToUpdate;
        }

        public async Task<User> GetByUserName(string username)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.UserName == username);
            return user;
        }
    }
}
