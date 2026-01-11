using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using MusicMicroservice.Application.Common.Interfaces.HttpService;
using MusicMicroservice.Application.Common.Interfaces.Persistance;
using MusicMicroservice.Contracts.Responses.MusicRatingDetails;

namespace MusicMicroservice.Application.Services.MusicService.Queries
{
    public class GetAllDetailMusicQuery: IRequest<Result<MusicRatingDetailsResponse>>
    {
        
    }
}