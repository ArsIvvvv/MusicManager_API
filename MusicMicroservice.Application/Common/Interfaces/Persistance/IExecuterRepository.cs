using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicMicroservice.Domain.Entities;

namespace MusicMicroservice.Application.Common.Interfaces.Persistance
{
    public interface IExecuterRepository
    {
        Task<IEnumerable<Executor>> GetAllAsync(bool includeMusics, CancellationToken cancellationToken = default);
        Task<Executor> GetByIdAsync(Guid id, bool includeMusics, CancellationToken cancellationToken = default);
        Task<IEnumerable<Executor>> GetRangeExecuterAsync(List<Guid> executerIds, CancellationToken cancellationToken = default);
        Task AddAsync(Executor executer, CancellationToken cancellationToken = default);
        Task UpdateAsync(Executor executer, CancellationToken cancellationToken = default);
        Task DeleteAsync(Executor executer, CancellationToken cancellationToken = default); 
    }
}