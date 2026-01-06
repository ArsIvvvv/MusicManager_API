using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MusicMicroservice.MusicRating.Application.Common.Interfaces.Persistence;
using MusicMicroservice.MusicRating.Application.Common.Settings;
using MusicMicroservice.MusicRating.Domain.MongoEntities;

namespace MusicMicroservice.MusicRating.Infrastructure.Repositories
{
    public class MusicRatingRepository : IMusicRatingRepository
    {
        private readonly IMongoCollection<MusicRatingReview> _collection;

        private readonly MongoSettings _settings;

        public MusicRatingRepository(IMongoClient mongoClient, IOptions<MongoSettings> options)
        {
            _settings = options.Value;

            _collection = mongoClient
                .GetDatabase(_settings.DatabaseName)
                .GetCollection<MusicRatingReview>("musicRatings");
        }
        public async Task AddAsync(MusicRatingReview rating, CancellationToken cancellationToken = default)
        {
            await _collection.InsertOneAsync(rating, new InsertOneOptions(), cancellationToken);
        }

        public async Task<bool> DeleteByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var result = await _collection.DeleteOneAsync(r => r.Id.ToString() == id, cancellationToken);

            return result.DeletedCount > 0;
        }

        public async Task<IEnumerable<MusicRatingReview>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _collection.Find(new BsonDocument()).ToListAsync(cancellationToken);
        }

        public async Task<MusicRatingReview?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var filter = Builders<MusicRatingReview>.Filter.Eq(r => r.Id, id);
            return await _collection.Find(filter)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<MusicRatingReview>> GetByMusicIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var filter = Builders<MusicRatingReview>.Filter.Eq(r => r.MusicId, id);
            return await _collection.Find(filter)
                .ToListAsync(cancellationToken);
        }
    }
}