using Api.Controllers.Objectives.Models;
using Api.Data.Entities;
using AutoMapper;

namespace Api.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Objective, GetObjectiveRequest>();
        }
    }
}
