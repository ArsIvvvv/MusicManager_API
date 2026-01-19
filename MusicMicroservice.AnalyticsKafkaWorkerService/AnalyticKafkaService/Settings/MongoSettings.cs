using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyticKafkaService.Settings
{
    public class MongoSettings
    {
        [Required]
        public string ConnectionString { get; set; } = string.Empty;

        [Required]
        public string DatabaseName { get; set; } = string.Empty;
    }
}