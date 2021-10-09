using Api.Controllers.Objectives.Models;
using Api.Infrastructure;
using Api.Infrastructure.Constants;
using Api.Infrastructure.Extensions;
using Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers.Objectives
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ObjectivesController : ControllerBase
    {
        private readonly ObjectiveService _objectiveService;
        private readonly IMapper _mapper;

        public ObjectivesController(ObjectiveService objectiveService, IMapper mapper)
        {
            _objectiveService = objectiveService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ResponseCache(Duration = 30)]
        public async Task<ActionResult<GetObjectiveRequest>> Get([FromRoute] long id)
        {
            var objective = await _objectiveService.GetById(id);
            if (objective == null)
                return BadRequest("Not found");

            var res = _mapper.Map<GetObjectiveRequest>(objective);

            return res;
        }

        [HttpGet()]
        [ResponseCache(Duration = 30)]
        public async Task<ActionResult<List<GetObjectiveRequest>>> GetAll([FromQuery] GetAllObjectivesRequest model)
        {
            long? userIdFilter = model.OnlyMy.HasValue ? User.GetUserId() : null;

            var objectives = await _objectiveService.GetAll(userId: userIdFilter);

            var res = _mapper.Map<List<GetObjectiveRequest>>(objectives);

            return res;
        }

        [HttpPost("[action]")]
        [Authorize(Roles = Roles.Creator)]
        public async Task<ActionResult> Create([FromBody] CreateRequest model)
        {
            var objective = await _objectiveService.Create(
                userId: User.GetUserId(),
                description: model.Desctiption,
                latitude: model.Latitude,
                longitude: model.Longitude);

            if (objective == null)
                return BadRequest(ApiErrors.FAIL_CREATE);

            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize(Roles = Roles.Executor)]
        public async Task<ActionResult> Execute([FromBody] ExecuteRequest model)
        {
            var updated = await _objectiveService.SetExecutor(
                objectiveId: model.ObjectiveId,
                executorId: User.GetUserId());

            if (!updated)
                return BadRequest(ApiErrors.FAIL_UPDATE);

            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> Finish([FromBody] FinishRequest model)
        {
            var updated = await _objectiveService.Finish(model.ObjectiveId);
            if (!updated)
                return BadRequest(ApiErrors.FAIL_UPDATE);

            return Ok();
        }
    }
}
