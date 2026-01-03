using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicMicroservice.MusicRating.Domain.MongoEntities;

namespace MusicMicroservice.MusicRating.Application.Common.Interfaces.Persistence
{
    public interface IMusicRatingRepository
    {
        Task<MusicRatingReview?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<MusicRatingReview>> GetAllAsync(CancellationToken cancellationToken = default);

        Task AddAsync(MusicRatingReview rating, CancellationToken cancellationToken = default);
        Task<IEnumerable<MusicRatingReview>> GetByMusicIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<bool> DeleteByIdAsync(string id, CancellationToken cancellationToken = default);
    }
}