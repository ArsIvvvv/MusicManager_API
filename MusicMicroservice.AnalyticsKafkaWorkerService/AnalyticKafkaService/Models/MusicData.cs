using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyticKafkaService.Models
{
    public class MusicData
    {
        public Guid Id { get; set; }
        public Guid MusicId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Style { get; set; } = string.Empty;
    }
}