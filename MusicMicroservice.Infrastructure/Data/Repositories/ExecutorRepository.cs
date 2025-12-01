using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicMicroservice.Application.Common.Interfaces.Persistance;
using MusicMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MusicMicroservice.Infrastructure.Data.Repositories;

public class ExecutorRepository : IExecutorRepository
{

    private readonly ApplicationDbContext _context;

    public ExecutorRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(Executor Executor, CancellationToken cancellationToken = default)
    {
        await _context.Executors.AddAsync(Executor, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Executor Executor, CancellationToken cancellationToken = default)
    {
        _context.Executors.Remove(Executor);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Executor>> GetAllAsync(bool includeMusics, CancellationToken cancellationToken = default)
    {
        var query = _context.Executors.AsQueryable();

        if (includeMusics)
        {
            query = query.Include(e => e.Musics);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Executor> GetByIdAsync(Guid id, bool includeMusics, CancellationToken cancellationToken = default)
    {
        var query = _context.Executors.AsQueryable();

        if (includeMusics)
        {
            query = query.Include(e => e.Musics);
        }

        return await query.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Executor>> GetRangeExecutorAsync(List<Guid> ExecutorIds, CancellationToken cancellationToken = default)
    {
        return await _context.Executors
            .Where(e => ExecutorIds.Contains(e.Id))
            .ToListAsync(cancellationToken);
    }

    public Task UpdateAsync(Executor Executor, CancellationToken cancellationToken = default)
    {
        _context.Executors.Update(Executor);

        return _context.SaveChangesAsync(cancellationToken);
    }
}