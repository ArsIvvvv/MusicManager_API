using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMicroservice.Application.Common.Settings
{
    public class JwtSettings
    {
        [Required]
        public string Issuer { get; set; } = string.Empty;

        [Required]
        public string Audience { get; set; } = string.Empty;

        [Required]
        public string Secret { get; set; } = string.Empty;  

        [Required]
        public int ExpiryMinutes { get; set; }
    }
}