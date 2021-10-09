using Api.Controllers.Objectives.Models;
using Api.Controllers.Users.Models;
using Api.Data.Entities;
using AutoMapper;
using System.Linq;

namespace Api.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Objective, GetObjectiveRequest>();

            CreateMap<User, UserResponse>()
                .ForMember(
                    ur => ur.Roles,
                    r => r.MapFrom(r => r.Roles.Select(r => r.Name).ToList())
                    )
                .ForMember(
                    ur => ur.Phone,
                    r => r.MapFrom(r => r.PhoneNumber)
                );
        }
    }
}
