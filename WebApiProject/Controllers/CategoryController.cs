#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProject.Data;
using WebApiProject.Filters;
using WebApiProject.Models.CategoryModels;
using WebApiProject.Models.Entities;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [UseApiKey]
    [UseAdminApiKey]
    public class CategoryController : ControllerBase
    {
        private readonly SqlContext _context;

        public CategoryController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetCategory()
        {
            var items = new List<CategoryModel>();
            foreach (var i in await _context.Category.ToListAsync())
            {
                items.Add(new CategoryModel(i.Id , i.CategoryName));
            }
            return items;
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModel>> GetCategoryEntity(int id)
        {
            var categoryEntity = await _context.Category.FindAsync(id);

            if (categoryEntity == null)
            {
                return NotFound();
            }

            return new CategoryModel(categoryEntity.Id , categoryEntity.CategoryName);
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoryEntity(int id, CategoryUpdateModel model)
        {
            if (id != model.Id)
            {
                return BadRequest("No cateogry with enetered ID was found...");
            }
            var categoryEntity = await _context.Category.FindAsync(model.Id);

            if(categoryEntity == null)
            {
                return BadRequest("No ID entered...");
            }

            if (await _context.Category.AnyAsync(x => x.CategoryName == model.CategoryName))
                return Conflict("This category already exists");

            categoryEntity.CategoryName = model.CategoryName;

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

            return Ok($"Category changed to: {model.CategoryName}");
        }

        // POST: api/Category
        [HttpPost]
        public async Task<ActionResult<CategoryModel>> PostCategoryEntity(CategoryCreateModel model)
        {

            if (await _context.Category.AnyAsync(x => x.CategoryName == model.CategoryName))
                return Conflict("A category with that name already exists");

            var categoryEntity = new CategoryEntity(model.CategoryName);

            _context.Category.Add(categoryEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoryEntity", new { id = categoryEntity.Id }, new CategoryModel(categoryEntity.Id, categoryEntity.CategoryName));
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

            categoryEntity.CategoryName = "Category deleted";

            _context.Entry(categoryEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryEntityExists(int id)
        {
            return _context.Category.Any(e => e.Id == id);
        }
    }
}
