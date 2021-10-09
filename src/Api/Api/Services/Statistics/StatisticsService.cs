using Api.Data;
using Api.Services.Statistics.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services.Statistics
{
    public class StatisticsService
    {
        private readonly ApplicationContext _context;

        public StatisticsService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<StatisticsDto> GetByUserId(long userId, StatisticsType statisticsType)
        {
            StatisticsDto statistics;

            if (statisticsType == StatisticsType.ForCreator)
            {
                statistics = _context.Objectives
                    .AsNoTracking()
                    .GroupBy(e => e.CreatorId)
                    .Select(e => new { UserId = e.Key, Count = e.Count() })
                    .OrderByDescending(g => g.Count)
                    .AsEnumerable() // TODO need fix for ROW_NUMBER
                    .Select((e, idx) => new StatisticsDto { UserId = e.UserId, CreatedObjectives = e.Count, Place = idx + 1 })
                    .Where(e => e.UserId == userId)
                    .SingleOrDefault();
            }
            else
            {
                statistics = _context.Objectives
                    .AsNoTracking()
                    .Where(e => e.ExecutorId != null && e.Executed)
                    .GroupBy(e => e.ExecutorId)
                    .Select(e => new { UserId = e.Key, Count = e.Count() })
                    .OrderByDescending(g => g.Count)
                    .AsEnumerable() // TODO need fix for ROW_NUMBER
                    .Select((e, idx) => new StatisticsDto { UserId = e.UserId.Value, FinishedObjectives = e.Count, Place = idx + 1 })
                    .Where(e => e.UserId == userId)
                    .SingleOrDefault();
            }

            if (statistics == null)
                return null;

            statistics.StatisticsType = statisticsType.ToString();

            return statistics;
        }

        public async Task<List<StatisticsDto>> GetTop(int countTop, StatisticsType statisticsType)
        {
            List<StatisticsDto> top;

            if (statisticsType == StatisticsType.ForCreator)
            {
                top = _context.Objectives
                    .AsNoTracking()
                    .GroupBy(e => e.CreatorId)
                    .Select(e => new { UserId = e.Key, Count = e.Count() })
                    .OrderByDescending(g => g.Count)
                    .Take(countTop)
                    .AsEnumerable() // TODO need fix for ROW_NUMBER
                    .Select((e, idx) => new StatisticsDto { UserId = e.UserId, CreatedObjectives = e.Count, Place = idx + 1, StatisticsType = statisticsType.ToString() })
                    .ToList();
            }
            else
            {
                top = _context.Objectives
                    .AsNoTracking()
                    .Where(e => e.ExecutorId != null && e.Executed)
                    .GroupBy(e => e.ExecutorId)
                    .Select(e => new { UserId = e.Key, Count = e.Count() })
                    .OrderByDescending(g => g.Count)
                    .Take(countTop)
                    .AsEnumerable() // TODO need fix for ROW_NUMBER
                    .Select((e, idx) => new StatisticsDto { UserId = e.UserId.Value, FinishedObjectives = e.Count, Place = idx + 1, StatisticsType = statisticsType.ToString() })
                    .Take(countTop)
                    .ToList();
            }

            

            return top;
        }

    }
}
