using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers.Objectives.Models
{
    public class GetObjectiveRequest
    {
        public long Id { get; set; }
        public string Desctiption { get; set; }

        public DateTime CreatedAt { get; set; }

        public long CreatorId { get; set; }

        public long? ExecutorId { get; set; }

        public bool Executed { get; set; }
        public DateTime? ExecutedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
