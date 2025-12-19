using System.Net.Http.Json;
using Restfull.Shared.DTOs;

namespace Restfull.Client.Services
{
    public class ResourceService
    {
        private readonly HttpClient _httpClient;

        public ResourceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResourceDto>> GetResourcesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Resources");

                if (response.IsSuccessStatusCode)
                {
                    // Читаем как строку для дебага
                    var json = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API Response: {json}");

                    // Пытаемся десериализовать
                    var resources = await response.Content.ReadFromJsonAsync<List<ResourceDto>>();
                    return resources ?? new List<ResourceDto>();
                }
                else
                {
                    Console.WriteLine($"API Error: {response.StatusCode}");
                    return new List<ResourceDto>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new List<ResourceDto>();
            }
        }

        public async Task<ResourceDto?> GetResourceByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ResourceDto>($"api/Resources/{id}");
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> CreateResourceAsync(CreateResourceDto resource)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Resources", resource);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateResourceAsync(int id, CreateResourceDto resource)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Resources/{id}", resource);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteResourceAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Resources/{id}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SeedDatabaseAsync()
        {
            try
            {
                var response = await _httpClient.PostAsync("api/Resources/seed", null);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}