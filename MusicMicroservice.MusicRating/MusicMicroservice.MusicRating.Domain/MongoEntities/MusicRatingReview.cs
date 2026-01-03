using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MusicMicroservice.MusicRating.Domain.Exceptions;

namespace MusicMicroservice.MusicRating.Domain.MongoEntities
{
    public class MusicRatingReview
    {
        public Guid Id { get; private set; }

        public Guid MusicId { get; private set; }

        public int Rating { get; private set; }

        public DateTime CreatedAt { get; private set; }

        private MusicRatingReview() { }

        public static MusicRatingReview Create(Guid musicId, int rating)
        {
            if (rating < 0 || rating > 5)
                throw new MongoEntityException("Rating must be between 0 and 5.");

            return new MusicRatingReview
            {
                Id = Guid.NewGuid(),
                MusicId = musicId,
                Rating = rating,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}