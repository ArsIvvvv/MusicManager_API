using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using MusicMicroservice.Domain.Entities;

namespace MusicMicroservice.Application.Common.Interfaces.Persistance
{
    public interface IMusicRepository
    {
        Task<IEnumerable<Music>> GetAllAsync(bool includeExecuters, CancellationToken cancellationToken = default);
        Task <Music> GetByIdAsync(Guid id, bool includeExecuters, CancellationToken cancellationToken = default);
        Task<IEnumerable<Music>> GetRangeBookAsync(List<Guid> musicIds, CancellationToken cancellationToken = default);
        Task AddAsync(Music music, CancellationToken cancellationToken = default);
        Task UpdateAsync(Music music, CancellationToken cancellationToken = default);
        Task DeleteAsync(Music music, CancellationToken cancellationToken = default);
    }
}
