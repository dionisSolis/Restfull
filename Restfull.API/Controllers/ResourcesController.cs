using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restfull.Domain.Entities;
using Restfull.Domain.Enums;
using Restfull.Infrastructure.Data;

namespace Restfull.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResourcesController : ControllerBase
    {
        private readonly Context _context;

        public ResourcesController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetResources()
        {
            try
            {
                var resources = await _context.Resources
                    .Include(r => r.Supplier)
                    .Include(r => r.Location)
                    .ToListAsync();

                var result = resources.Select(r => new
                {
                    r.Id,
                    r.Name,
                    Status = r.Status.ToString(),
                    Type = GetResourceType(r),
                    SupplierName = r.Supplier?.Name ?? "",
                    LocationDescription = r.Location?.Description ?? "",

                    // Software
                    Version = (r as Software)?.Version,
                    LicenseKey = (r as Software)?.LicenseKey,
                    LicenseExpiryDate = (r as Software)?.LicenseExpiryDate,

                    // Equipment
                    Model = (r as Equipment)?.Model,
                    SerialNumber = (r as Equipment)?.SerialNumber,

                    // Furniture
                    Material = (r as Furniture)?.Material,
                    Dimensions = (r as Furniture)?.Dimensions,

                    // Room
                    Number = (r as Room)?.Number,
                    Building = (r as Room)?.Building,
                    Capacity = (r as Room)?.Capacity,
                    RoomType = (r as Room)?.RoomType.ToString()
                }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetResource(int id)
        {
            try
            {
                var resource = await _context.Resources
                    .Include(r => r.Supplier)
                    .Include(r => r.Location)
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (resource == null)
                {
                    return NotFound(new { error = "Resource not found" });
                }

                return new
                {
                    resource.Id,
                    resource.Name,
                    Status = resource.Status.ToString(),
                    Type = GetResourceType(resource),
                    SupplierName = resource.Supplier?.Name ?? "",
                    LocationDescription = resource.Location?.Description ?? "",

                    Version = (resource as Software)?.Version,
                    LicenseKey = (resource as Software)?.LicenseKey,
                    LicenseExpiryDate = (resource as Software)?.LicenseExpiryDate,
                    Model = (resource as Equipment)?.Model,
                    SerialNumber = (resource as Equipment)?.SerialNumber,
                    Material = (resource as Furniture)?.Material,
                    Dimensions = (resource as Furniture)?.Dimensions,
                    Number = (resource as Room)?.Number,
                    Building = (resource as Room)?.Building,
                    Capacity = (resource as Room)?.Capacity,
                    RoomType = (resource as Room)?.RoomType.ToString()
                };
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<object>> PostResource([FromBody] ResourceRequest request)
        {
            try
            {
                Resource resource = request.Type switch
                {
                    "Software" => new Software
                    {
                        Name = request.Name,
                        Status = (ResourceStatus)request.Status,
                        SupplierId = request.SupplierId,
                        LocationId = request.LocationId,
                        Version = request.Version ?? "",
                        LicenseKey = request.LicenseKey ?? "",
                        LicenseExpiryDate = request.LicenseExpiryDate ?? DateTime.Now.AddYears(1)
                    },
                    "Equipment" => new Equipment
                    {
                        Name = request.Name,
                        Status = (ResourceStatus)request.Status,
                        SupplierId = request.SupplierId,
                        LocationId = request.LocationId,
                        Model = request.Model ?? "",
                        SerialNumber = request.SerialNumber ?? ""
                    },
                    "Furniture" => new Furniture
                    {
                        Name = request.Name,
                        Status = (ResourceStatus)request.Status,
                        SupplierId = request.SupplierId,
                        LocationId = request.LocationId,
                        Material = request.Material ?? "",
                        Dimensions = request.Dimensions ?? ""
                    },
                    "Room" => new Room
                    {
                        Name = request.Name,
                        Status = (ResourceStatus)request.Status,
                        SupplierId = request.SupplierId,
                        LocationId = request.LocationId,
                        Number = request.Number ?? "",
                        Building = request.Building ?? "",
                        Capacity = request.Capacity ?? 0,
                        RoomType = (RoomType)(request.RoomType ?? 0)
                    },
                    _ => throw new ArgumentException("Invalid resource type")
                };

                _context.Resources.Add(resource);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetResource), new { id = resource.Id }, new
                {
                    resource.Id,
                    resource.Name,
                    Status = resource.Status.ToString(),
                    Type = GetResourceType(resource),
                    Message = "Resource created successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutResource(int id, [FromBody] ResourceRequest request)
        {
            try
            {
                var existingResource = await _context.Resources.FindAsync(id);
                if (existingResource == null)
                {
                    return NotFound(new { error = "Resource not found" });
                }

                // Обновление базовых полей
                existingResource.Name = request.Name;
                existingResource.Status = (ResourceStatus)request.Status;
                existingResource.SupplierId = request.SupplierId;
                existingResource.LocationId = request.LocationId;

                // Обновление специфичных полей в зависимости от типа
                // (реализация зависит от вашей структуры)

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResource(int id)
        {
            try
            {
                var resource = await _context.Resources.FindAsync(id);
                if (resource == null)
                {
                    return NotFound(new { error = "Resource not found" });
                }

                _context.Resources.Remove(resource);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost("seed")]
        public async Task<ActionResult<object>> SeedDatabase()
        {
            try
            {
                // Очистка базы
                await _context.Database.EnsureDeletedAsync();
                await _context.Database.EnsureCreatedAsync();

                // Создание тестовых данных
                var supplier = new Supplier { Name = "Microsoft", ContactInfo = "sales@microsoft.com" };
                var location = new Location { Description = "Кабинет 301" };

                _context.Suppliers.Add(supplier);
                _context.Locations.Add(location);
                await _context.SaveChangesAsync();

                var software = new Software
                {
                    Name = "Visual Studio 2022",
                    Status = ResourceStatus.Available,
                    SupplierId = supplier.Id,
                    LocationId = location.Id,
                    Version = "17.8",
                    LicenseKey = "VS2022-PRO-12345",
                    LicenseExpiryDate = DateTime.Now.AddYears(1)
                };

                var equipment = new Equipment
                {
                    Name = "Сервер Dell PowerEdge",
                    Status = ResourceStatus.Reserved,
                    SupplierId = supplier.Id,
                    LocationId = location.Id,
                    Model = "R740",
                    SerialNumber = "SN789456123"
                };

                _context.Software.Add(software);
                _context.Equipment.Add(equipment);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Database seeded successfully",
                    resourcesCount = await _context.Resources.CountAsync()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        private string GetResourceType(Resource resource)
        {
            return resource switch
            {
                Software => "Software",
                Equipment => "Equipment",
                Furniture => "Furniture",
                Room => "Room",
                _ => "Unknown"
            };
        }
    }

    public class ResourceRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = "Equipment";
        public int Status { get; set; }
        public int SupplierId { get; set; }
        public int LocationId { get; set; }

        public string? Version { get; set; }
        public string? LicenseKey { get; set; }
        public DateTime? LicenseExpiryDate { get; set; }
        public string? Model { get; set; }
        public string? SerialNumber { get; set; }
        public string? Material { get; set; }
        public string? Dimensions { get; set; }
        public string? Number { get; set; }
        public string? Building { get; set; }
        public int? Capacity { get; set; }
        public int? RoomType { get; set; }
    }
}