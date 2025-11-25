using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MusicMicroservice.Contracts.Requests.Music;
using MusicMicroservice.Contracts.Responses.Music;

namespace MusicMicroservice.Application.Common.Interfaces.Services
{
    public interface IMusicService
    {
        Task<Result<IEnumerable<MusicResponse>>> GetAllMusicsAsync(CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<MusicWithExecutersResponse>>> GetAllMusicsWithExecutersAsync(CancellationToken cancellationToken = default);
        Task<Result<MusicResponse>> GetMusicByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Result<MusicWithExecutersResponse>> GetMusicWithExecutersByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Result<MusicResponse>> CreateMusicAsync(CreateMusicRequest request, CancellationToken cancellationToken = default);
        Task<Result<MusicWithExecutersResponse>> CreateMusicWithExecutersAsync(CreateMusicWithExecutersRequest request, CancellationToken cancellationToken = default);
        Task<Result> UpdateMusicAsync(UpdateMusicRequest request, CancellationToken cancellationToken = default);
        Task<Result> UpdateMusicWithExecutersAsync(UpdateMusicWithExecuters request, CancellationToken cancellationToken = default); 
        Task<Result> DeleteMusicByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}