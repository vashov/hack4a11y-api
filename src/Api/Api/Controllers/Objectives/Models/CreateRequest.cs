using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers.Objectives.Models
{
    public class CreateRequest
    {
        [Required]
        [MaxLength(512)]
        public string Desctiption { get; set; }

        [Required]
        public decimal Latitude { get; set; }

        [Required]
        public decimal Longitude { get; set; }
    }
}
