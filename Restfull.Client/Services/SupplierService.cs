using System.Net.Http.Json;
using Restfull.Shared.DTOs;

namespace Restfull.Client.Services
{
    public class SupplierService
    {
        private readonly HttpClient _httpClient;

        public SupplierService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SupplierDto>> GetSuppliersAsync()
        {
            try
            {
                // Временная заглушка
                return new List<SupplierDto>
                {
                    new() { Id = 1, Name = "Tech Supplier Inc.", ContactInfo = "contact@tech.com" },
                    new() { Id = 2, Name = "Office Supplies Ltd.", ContactInfo = "info@office.com" }
                };
            }
            catch
            {
                return new List<SupplierDto>();
            }
        }
    }
}