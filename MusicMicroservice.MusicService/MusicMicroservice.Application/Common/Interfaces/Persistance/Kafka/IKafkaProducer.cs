using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicMicroservice.Contracts.Responses.Music;

namespace MusicMicroservice.Application.Common.Interfaces.Persistance.Kafka
{
    public interface IKafkaProducer: IDisposable
    {
        Task ProduceAsync<T>(MusicResponse response, CancellationToken cancellationToken = default);
    }
}