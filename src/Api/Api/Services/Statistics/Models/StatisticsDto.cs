using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services.Statistics.Models
{
    public class StatisticsDto
    {
        public long UserId { get; set; }
        public long Place { get; set; }
        public long FinishedObjectives { get; set; }
        public long CreatedObjectives { get; set; }

        public string StatisticsType { get; set; }
    }
}
