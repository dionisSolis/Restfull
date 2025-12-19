using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restfull.Domain.Entities;
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

        // GET: api/Resources - READ (получить все)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Resource>>> GetResources()
        {
            return await _context.Resources
                .Include(r => r.Supplier)
                .Include(r => r.Location)
                .ToListAsync();
        }

        // GET: api/Resources/5 - READ (получить по ID)
        [HttpGet("{id}")]
        public async Task<ActionResult<Resource>> GetResource(int id)
        {
            var resource = await _context.Resources
                .Include(r => r.Supplier)
                .Include(r => r.Location)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (resource == null)
            {
                return NotFound();
            }

            return resource;
        }

        // POST: api/Resources - CREATE (создать)
        [HttpPost]
        public async Task<ActionResult<Resource>> PostResource(Resource resource)
        {
            _context.Resources.Add(resource);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResource", new { id = resource.Id }, resource);
        }

        // PUT: api/Resources/5 - UPDATE (обновить)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResource(int id, Resource resource)
        {
            if (id != resource.Id)
            {
                return BadRequest();
            }

            _context.Entry(resource).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResourceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Resources/5 - DELETE (удалить)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResource(int id)
        {
            var resource = await _context.Resources.FindAsync(id);
            if (resource == null)
            {
                return NotFound();
            }

            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ResourceExists(int id)
        {
            return _context.Resources.Any(e => e.Id == id);
        }
    }
}