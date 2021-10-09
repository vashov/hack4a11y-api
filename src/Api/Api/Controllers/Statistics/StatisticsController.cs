using Api.Infrastructure;
using Api.Infrastructure.Constants;
using Api.Services.Statistics;
using Api.Services.Statistics.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers.Statistics
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StatisticsController : ControllerBase
    {
        private readonly StatisticsService _statisticsService;

        public StatisticsController(StatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet("{userId}")]
        [ResponseCache(Duration = 30)]
        public async Task<ApiResult<StatisticsDto>> Get([FromRoute] long userId)
        {
            StatisticsType statisticsType = User.IsInRole(Roles.Creator)
                ? StatisticsType.ForCreator : StatisticsType.ForExecutor;

            var stat = await _statisticsService.GetByUserId(userId, statisticsType);

            return ApiResult<StatisticsDto>.Ok(stat);
        }

        [HttpGet("[action]")]
        [ResponseCache(Duration = 30)]
        public async Task<ApiResult<List<StatisticsDto>>> Top()
        {
            StatisticsType statisticsType = User.IsInRole(Roles.Creator)
                ? StatisticsType.ForCreator : StatisticsType.ForExecutor;

            List<StatisticsDto> stat = await _statisticsService.GetTop(countTop: 10, statisticsType);

            return ApiResult<List<StatisticsDto>>.Ok(stat);
        }
    }
}
