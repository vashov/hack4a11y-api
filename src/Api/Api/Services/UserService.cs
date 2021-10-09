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
            var t = await _context.Roles.AsNoTracking().ToListAsync();
            var u = await _context.Users.AsNoTracking().ToListAsync();

            var roleEntity = await _context.Roles
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

            try
            {
                _context.Add(user);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogWarning($"{e}");
                return null;
            }

            _context.Entry(user).State = EntityState.Detached;
            _context.Entry(roleEntity).State = EntityState.Detached;

            return user;
        }

        public async Task<User> GetByPhone(long phone)
        {
            var user = await _context.Users
                .Include(e => e.Roles)
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.PhoneNumber == phone);

            return user;
        }

        public async Task<User> GetById(long userId)
        {
            var user = await _context.Users
                .Include(e => e.Roles)
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == userId);

            return user;
        }
    }
}
