using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers.Objectives.Models
{
    public class ExecuteRequest
    {
        [Required]
        public long ObjectiveId { get; set; }
    }
}
