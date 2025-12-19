using System.Net.Http.Json;
using Restfull.Shared.DTOs;

namespace Restfull.Client.Services
{
    public class LocationService
    {
        private readonly HttpClient _httpClient;

        public LocationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<LocationDto>> GetLocationsAsync()
        {
            try
            {
                // Временная заглушка
                return new List<LocationDto>
                {
                    new() { Id = 1, Description = "Главный офис, кабинет 301" },
                    new() { Id = 2, Description = "Серверная комната" },
                    new() { Id = 3, Description = "Конференц-зал" }
                };
            }
            catch
            {
                return new List<LocationDto>();
            }
        }
    }
}