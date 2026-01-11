using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MusicMicroservice.MusicRating.Application.Common.Interfaces.Persistence;
using MusicMicroservice.MusicRating.Application.Common.Settings;
using MusicMicroservice.MusicRating.Domain.MongoEntities;

namespace MusicMicroservice.MusicRating.Infrastructure.Background
{
    public class RatingCountBackgroundService : BackgroundService
    {
        private readonly ILogger<RatingCountBackgroundService> _logger;

        private readonly IMongoCollection<MusicRatingReview> _collection;

        private readonly MongoSettings _settings;

        private readonly IDistributedCache _distributedCache;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        private readonly TimeSpan _period = TimeSpan.FromMinutes(5);

        public RatingCountBackgroundService(
            ILogger<RatingCountBackgroundService> logger,
            IMongoClient mongoClient, 
            IOptions<MongoSettings> options,
            IDistributedCache distributedCache)
        {
            _logger = logger;
            _settings = options.Value;
            _collection = mongoClient
                .GetDatabase(_settings.DatabaseName)
                .GetCollection<MusicRatingReview>("musicRatings");
            _distributedCache = distributedCache;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("RatingCountBackgroundService is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("RatingCountBackgroundService is doing background work.");

                var ratingCount = await _collection.CountDocumentsAsync(_ => true, cancellationToken: stoppingToken);

                var ratingCountString = JsonSerializer.Serialize(ratingCount.ToString(), _jsonSerializerOptions);

                await _distributedCache.SetStringAsync("RatingCount", ratingCountString, stoppingToken);

                _logger.LogInformation("RatingCountBackgroundService has updated the rating count to {RatingCount}.", ratingCount);

                await Task.Delay(_period, stoppingToken);
            }

            _logger.LogInformation("RatingCountBackgroundService is stopping.");
        } 
    }
}