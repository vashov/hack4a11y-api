using Api.Data;
using Api.Data.Entities;
using Api.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public class ObjectiveService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<ObjectiveService> _logger;

        public ObjectiveService(ApplicationContext context, ILogger<ObjectiveService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Objective> GetById(long id)
        {
            var objective = await _context.Objectives.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);

            return objective;
        }

        public async Task<List<Objective>> GetAll(
            long? userId = null,
            long? executorId = null, 
            long? creatorId = null)
        {

            var query = _context.Objectives.AsNoTracking();

            if (userId.HasValue)
            {
                query = query.Where(e => e.ExecutorId == userId.Value || e.CreatorId == userId.Value);
            }

            if (executorId.HasValue)
            {
                query = query.Where(e => e.ExecutorId == userId.Value);
            }

            if (creatorId.HasValue)
            {
                query = query.Where(e => e.CreatorId == userId.Value);
            }

            var objectives = await query.ToListAsync();

            return objectives;
        }

        public async Task<Objective> Create(
            long userId,
            string description,
            decimal latitude,
            decimal longitude)
        {
            var objective = new Objective
            {
                CreatorId = userId,
                Description = description,
                CreatedAt = DateTime.UtcNow,
                Latitude = latitude,
                Longitude = longitude
            };

            _context.Add(objective);
            await _context.SaveChangesAsync();

            _context.Entry(objective).State = EntityState.Detached;

            return objective;
        }

        public async Task<bool> SetExecutor(long objectiveId, long executorId)
        {
            var objective = await _context.Objectives
                .Where(e => e.Id == objectiveId && e.ExecutorId == null)
                .SingleOrDefaultAsync();
            
            if (objective == null)
            {
                _logger.LogWarning($"Objective {objectiveId} not exists or has executor");
                return false;
            }

            var userCan = await _context.Users
                .AnyAsync(u => u.Id == executorId && u.Roles.Any(r => r.NormalizedName == Roles.Executor.ToUpper()));
            if (!userCan)
            {
                _logger.LogWarning($"User {executorId} cant take objective {objectiveId}");
                return false;
            }

            objective.ExecutorId = executorId;
            objective.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                _logger.LogWarning($"Objective {objectiveId} error: {e}");
                return false;
            }

            _context.Entry(objective).State = EntityState.Detached;

            return true;
        }

        public async Task<bool> Finish(long objectiveId)
        {
            var objective = await _context.Objectives
                .Where(e => e.Id == objectiveId && !e.Executed)
                .SingleOrDefaultAsync();

            if (objective == null)
            {
                _logger.LogWarning($"Objective {objectiveId} not exists or already executed");
                return false;
            }

            objective.Executed = true;
            objective.ExecutedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Objective {objectiveId} error: {e}");
                return false;
            }

            _context.Entry(objective).State = EntityState.Detached;

            return true;
        }
    }
}
