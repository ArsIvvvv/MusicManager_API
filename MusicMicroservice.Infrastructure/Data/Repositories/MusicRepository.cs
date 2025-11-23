using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicMicroservice.Application.Common.Interfaces.Persistance;
using MusicMicroservice.Domain.Entities;

namespace MusicMicroservice.Infrastructure.Data.Repositories
{

    public class MusicRepository : IMusicRepository
    {
        private readonly ApplicationDbContext _context;

        public MusicRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Music music, CancellationToken cancellationToken = default)
        {
            await _context.Musics.AddAsync(music, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Music music, CancellationToken cancellationToken = default)
        {
             _context.Musics.Remove(music);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Music>> GetAllAsync(bool includeExecuters, CancellationToken cancellationToken = default)
        {
            var query = _context.Musics.AsQueryable();

            if (includeExecuters)
            {
                query = query.Include(m => m.Executors);
            }

            return await query.ToListAsync(cancellationToken);    
        }

        public async Task<Music> GetByIdAsync(Guid id, bool includeExecuters, CancellationToken cancellationToken = default)
        {
            var query = _context.Musics.AsQueryable();

            if (includeExecuters)
            {
                query = query.Include(m => m.Executors);
            }

            return await query.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Music>> GetRangeBookAsync(List<Guid> musicIds, CancellationToken cancellationToken = default)
        {
            return await _context.Musics
                .Where(m => musicIds.Contains(m.Id))
                .ToListAsync(cancellationToken);
        }

        public Task UpdateAsync(Music music, CancellationToken cancellationToken = default)
        {
            _context.Musics.Update(music);

            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}