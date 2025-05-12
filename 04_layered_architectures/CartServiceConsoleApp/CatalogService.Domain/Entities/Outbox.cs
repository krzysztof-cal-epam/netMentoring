using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Entities
{
    public class Outbox
    {
        public int Id { get; set; }

        public string EventType { get; set; }
        public string Payload { get; set; }
        public bool IsProcessed { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ProcessedAt { get; set; }
    }
}
