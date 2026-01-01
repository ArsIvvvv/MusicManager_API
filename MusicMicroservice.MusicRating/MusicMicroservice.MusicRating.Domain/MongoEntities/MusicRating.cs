using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MusicMicroservice.MusicRating.Domain.Exceptions;

namespace MusicMicroservice.MusicRating.Domain.MongoEntities
{
    public class MusicRating
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; private set; }

        [BsonRepresentation(BsonType.String)]
        public Guid MusicId { get; private set; }

        [BsonElement("rating")]
        public int Rating { get; private set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; private set; }

        [BsonConstructor]
        private MusicRating() { }

        public static MusicRating Create(Guid musicId, int rating)
        {
            if (rating < 0 || rating > 5)
                throw new MongoEntityException("Rating must be between 0 and 5.");

            return new MusicRating
            {
                Id = Guid.NewGuid(),
                MusicId = musicId,
                Rating = rating,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}