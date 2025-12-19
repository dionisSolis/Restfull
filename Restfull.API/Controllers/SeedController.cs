using Microsoft.AspNetCore.Mvc;
using Restfull.Domain.Entities;
using Restfull.Domain.Enums;
using Restfull.Infrastructure.Data;

namespace Restfull.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeedController : ControllerBase
    {
        private readonly Context _context;

        public SeedController(Context context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Seed()
        {
            // Очистка базы
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            // Создание тестовых поставщиков
            var supplier1 = new Supplier { Name = "Microsoft", ContactInfo = "sales@microsoft.com" };
            var supplier2 = new Supplier { Name = "Dell", ContactInfo = "support@dell.com" };

            _context.Suppliers.AddRange(supplier1, supplier2);
            await _context.SaveChangesAsync();

            // Создание тестовых местоположений
            var location1 = new Location { Description = "Кабинет 301" };
            var location2 = new Location { Description = "Серверная комната" };

            _context.Locations.AddRange(location1, location2);
            await _context.SaveChangesAsync();

            // Создание тестового ПО
            var software = new Software
            {
                Name = "Visual Studio 2022",
                Status = ResourceStatus.Available,
                SupplierId = supplier1.Id,
                LocationId = location1.Id,
                Version = "17.8",
                LicenseKey = "VS2022-PRO-12345",
                LicenseExpiryDate = DateTime.Now.AddYears(1)
            };

            // Создание тестового оборудования
            var equipment = new Equipment
            {
                Name = "Сервер Dell PowerEdge",
                Status = ResourceStatus.Reserved,
                SupplierId = supplier2.Id,
                LocationId = location2.Id,
                Model = "R740",
                SerialNumber = "SN789456123"
            };

            _context.Software.Add(software);
            _context.Equipment.Add(equipment);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "База данных заполнена тестовыми данными",
                softwareCount = _context.Software.Count(),
                equipmentCount = _context.Equipment.Count()
            });
        }

        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            return Ok(new
            {
                database = _context.Database.CanConnect() ? "Connected" : "Not connected",
                resourcesCount = _context.Resources.Count(),
                softwareCount = _context.Software.Count(),
                equipmentCount = _context.Equipment.Count()
            });
        }
    }
}