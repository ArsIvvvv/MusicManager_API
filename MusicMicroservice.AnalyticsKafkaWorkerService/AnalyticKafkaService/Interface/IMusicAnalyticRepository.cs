using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnalyticKafkaService.Models;

namespace AnalyticKafkaService.Interface
{
    public interface IMusicAnalyticRepository
    {
        Task AddAsync(MusicData musicData, CancellationToken cancellationToken);
    }
}