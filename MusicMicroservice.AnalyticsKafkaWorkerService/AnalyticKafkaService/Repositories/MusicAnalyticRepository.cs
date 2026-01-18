using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnalyticKafkaService.Interface;
using AnalyticKafkaService.Models;
using AnalyticKafkaService.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AnalyticKafkaService.Repositories
{
    public class MusicAnalyticRepository : IMusicAnalyticRepository
    {
        private readonly IMongoCollection<MusicData> _musicCollection;
        private readonly MongoSettings _mongoSettings;

        public MusicAnalyticRepository(IMongoClient mongoClient, IOptions<MongoSettings> options)
        {
            _mongoSettings = options.Value;

            _musicCollection = mongoClient
                .GetDatabase(_mongoSettings.DatabaseName)
                .GetCollection<MusicData>("musicAnalytics");

        }
        public async Task AddAsync(MusicData musicData, CancellationToken cancellationToken)
        {
            await _musicCollection.InsertOneAsync(musicData, new InsertOneOptions(), cancellationToken);
        }
    }
}