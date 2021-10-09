using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers.Objectives.Models
{
    public class GetAllObjectivesRequest
    {
        public bool? OnlyMy { get; set; }
    }
}
