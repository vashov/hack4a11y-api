using Api.Data;
using Api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public class UserService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<UserService> _logger;

        public UserService(ApplicationContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<User> Create(long phone, string passwordHash, string role)
        {
            var roleEntity = await _context.Roles.AsNoTracking()
                .SingleOrDefaultAsync(r => r.NormalizedName == role.ToUpper());

            if (roleEntity == null)
            {
                _logger.LogWarning($"There is no role: {role}");
                return null;
            }

            var user = new User
            {
                PhoneNumber = phone,
                PasswordHash = passwordHash,
                CreatedAt = DateTime.UtcNow,
                Roles = new List<Role> { roleEntity }
            };

            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                _logger.LogWarning($"{e}");
                return null;
            }

            _context.Entry(user).State = EntityState.Detached;
            return user;
        }

        public async Task<User> GetByPhone(long phone)
        {
            var user = await _context.Users.AsNoTracking()
                .SingleOrDefaultAsync(u => u.PhoneNumber == phone);

            return user;
        }

        public async Task<User> GetById(long userId)
        {
            var user = await _context.Users.AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == userId);

            return user;
        }

        public async Task LoadRoles(User user)
        {
            var roles = await _context.Users.AsNoTracking()
                .Where(u => u.Id == user.Id)
                .SelectMany(u => u.Roles)
                .ToListAsync();

            user.Roles = roles;
        }
    }
}
