#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProject.Data;
using WebApiProject.Models.Entities;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly SqlContext _context;

        public CategoryController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryEntity>>> GetCategory()
        {
            return await _context.Category.ToListAsync();
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryEntity>> GetCategoryEntity(int id)
        {
            var categoryEntity = await _context.Category.FindAsync(id);

            if (categoryEntity == null)
            {
                return NotFound();
            }

            return categoryEntity;
        }

        // PUT: api/Category/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoryEntity(int id, CategoryEntity categoryEntity)
        {
            if (id != categoryEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(categoryEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryEntityExists(id))
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

        // POST: api/Category
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoryEntity>> PostCategoryEntity(CategoryEntity categoryEntity)
        {
            _context.Category.Add(categoryEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoryEntity", new { id = categoryEntity.Id }, categoryEntity);
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryEntity(int id)
        {
            var categoryEntity = await _context.Category.FindAsync(id);
            if (categoryEntity == null)
            {
                return NotFound();
            }

            _context.Category.Remove(categoryEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryEntityExists(int id)
        {
            return _context.Category.Any(e => e.Id == id);
        }
    }
}
