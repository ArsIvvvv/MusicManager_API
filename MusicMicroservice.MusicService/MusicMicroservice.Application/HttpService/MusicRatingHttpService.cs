using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using FluentResults;
using MusicMicroservice.Application.Common.Interfaces.HttpService;
using MusicMicroservice.Contracts.Responses.Rating;

namespace MusicMicroservice.Application.HttpService
{
    public class MusicRatingHttpService : IMusicRatingHttpService
    {
        private readonly HttpClient _httpClient;

        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public MusicRatingHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
                
        }

        public async Task<Result<IEnumerable<MusicRatingReviewResponse>>> GetAllRatingsAsync(CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync("get-all-ratings");
            if(response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<MusicRatingReviewResponse>>(_jsonSerializerOptions, cancellationToken);
                return Result.Ok(result!);
            }

            var errorResponse =  await response.Content.ReadAsStringAsync(cancellationToken);
            return Result.Fail<IEnumerable<MusicRatingReviewResponse>>(($"HTTP Error: {response.StatusCode} - {errorResponse}"));
        }
    }
}