using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicMicroservice.Application.Common.Interfaces.Persistance;
using MusicMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MusicMicroservice.Infrastructure.Data.Repositories;

public class ExecuterRepository : IExecuterRepository
{

    private readonly ApplicationDbContext _context;

    public ExecuterRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(Executor executer, CancellationToken cancellationToken = default)
    {
        await _context.Executors.AddAsync(executer, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Executor executer, CancellationToken cancellationToken = default)
    {
        _context.Executors.Remove(executer);

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

    public async Task<IEnumerable<Executor>> GetRangeExecuterAsync(List<Guid> executerIds, CancellationToken cancellationToken = default)
    {
        return await _context.Executors
            .Where(e => executerIds.Contains(e.Id))
            .ToListAsync(cancellationToken);
    }

    public Task UpdateAsync(Executor executer, CancellationToken cancellationToken = default)
    {
        _context.Executors.Update(executer);

        return _context.SaveChangesAsync(cancellationToken);
    }
}