using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MusicMicroservice.Contracts.Requests.Executer;
using MusicMicroservice.Contracts.Responses.Executer;

namespace MusicMicroservice.Application.Common.Interfaces.Services
{
    public interface IExecuterService
    {
        Task<Result<IEnumerable<ExecuterResponse>>> GetAllExecutersAsync(CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<ExecuterWithMusicResponse>>> GetAllExecutersWithMusicsAsync(CancellationToken cancellationToken = default);
        Task<Result<ExecuterResponse>> GetExecuterByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Result<ExecuterWithMusicResponse>> GetExecuterWithMusicsByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Result<ExecuterResponse>> CreateExecuterAsync(CreateExecuterRequest request, CancellationToken cancellationToken = default);
        Task<Result> UpdateExecuterAsync(UpdateExecuterRequest request, CancellationToken cancellationToken = default);
        Task<Result> UpdateExecuterWithMusicsAsync(UpdateExecuterMusicsRequest request, CancellationToken cancellationToken = default);
        Task<Result> DeleteExecuterByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}