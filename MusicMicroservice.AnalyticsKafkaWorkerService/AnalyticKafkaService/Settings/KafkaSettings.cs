using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyticKafkaService.Settings
{
    public class KafkaSettings
    {   
        [Required]
        public string BootstrapServers { get; set; } = string.Empty;

        [Required]
        public string Topic { get; set; } = string.Empty;
        
        [Required]
        public string GroupId { get; set; } = string.Empty;
    }
}