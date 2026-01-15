using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MusicMicroservice.MusicRating.Domain.MongoEntities;

namespace MusicMicroservice.MusicRating.Infrastructure.Configuration
{
    public static class MusicRatingConfiguration
    {
        public static void Configure()
        {
            if(BsonClassMap.IsClassMapRegistered(typeof(MusicRatingReview)))
                return;

            BsonClassMap.RegisterClassMap<MusicRatingReview>(cm =>
            {
                cm.MapIdMember(c => c.Id)
                    .SetIdGenerator(GuidGenerator.Instance);

                // Находим приватный (или protected) пустой конструктор
                var constructor = typeof(MusicRatingReview).GetConstructor(
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, 
                    null, 
                    Type.EmptyTypes, 
                    null);

                if (constructor != null)
                {
                    // Говорим Mongo использовать именно этот конструктор для создания объекта
                    cm.MapConstructor(constructor);
                }

                cm.MapMember(c => c.MusicId);
                cm.MapMember(c => c.Rating);
                cm.MapMember(c => c.CreatedAt);
            });
        }
    }
}