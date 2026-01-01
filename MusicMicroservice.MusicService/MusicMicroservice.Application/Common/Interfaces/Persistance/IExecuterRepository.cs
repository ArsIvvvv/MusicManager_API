using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicMicroservice.Domain.Entities;

namespace MusicMicroservice.Application.Common.Interfaces.Persistance;

public interface IExecutorRepository
{
    Task<IEnumerable<Executor>> GetAllAsync(bool includeMusics, CancellationToken cancellationToken = default);
    Task<Executor> GetByIdAsync(Guid id, bool includeMusics, CancellationToken cancellationToken = default);
    Task<IEnumerable<Executor>> GetRangeExecutorAsync(List<Guid> ExecutorIds, CancellationToken cancellationToken = default);
    Task AddAsync(Executor Executor, CancellationToken cancellationToken = default);
    Task UpdateAsync(Executor Executor, CancellationToken cancellationToken = default);
    Task DeleteAsync(Executor Executor, CancellationToken cancellationToken = default); 
}