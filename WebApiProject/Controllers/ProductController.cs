#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProject.Data;
using WebApiProject.Models.CategoryModels;
using WebApiProject.Models.Entities;
using WebApiProject.Models.ProductModels;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly SqlContext _context;

        public ProductController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts()
        {
           var items = new List<ProductModel>();
            foreach (var i in await _context.Products.Include(x => x.Category).ToListAsync())
                items.Add(new ProductModel(
                    i.Id,
                    i.ProductName,
                    i.ArticleNumber,
                    i.Price,
                    i.Description,
                    i.Created,
                    i.Updated,
                    i.CategoryId,
                    new CategoryModel(
                        i.Category.Id,
                        i.Category.CategoryName)));

            return items;
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetProductEntity(int id)
        {
            var productEntity = await _context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);  

            if (productEntity == null)
            {
                return NotFound();
            }

            return new ProductModel(
                productEntity.Id,
                productEntity.ProductName,
                productEntity.ArticleNumber,
                productEntity.Price,
                productEntity.Description,
                productEntity.Created,
                productEntity.Updated,
                productEntity.CategoryId,
                new CategoryModel(
                    productEntity.Category.Id,
                    productEntity.Category.CategoryName));
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductEntity(int id, ProductUpdateModel model)
        {
            if (id != model.Id)
            {
                return BadRequest("No product with that ID was found...");
            }

            var productEntity = await _context.Products.FindAsync(model.Id);
            var category = await _context.Category.FirstOrDefaultAsync(x => x.CategoryName == model.CategoryName);

            if (productEntity == null)
            {
                return BadRequest("No ID entered...");
            }

            if (category != null)
            {
                productEntity.CategoryId = category.Id;
            }
            else
            {
                productEntity.Category = new CategoryEntity(model.CategoryName);
            }
            if (await _context.Products.AnyAsync(x => x.ProductName == model.ProductName))
                return Conflict("A product with this name already exists");

            productEntity.ProductName = model.ProductName;
            productEntity.Price = model.Price;
            productEntity.Description = model.Description;
            productEntity.Updated = DateTime.Now;
            productEntity.Category = new CategoryEntity(
                model.CategoryName);

            _context.Entry(productEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductEntityExists(id))
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

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductModel>> PostProductEntity(ProductCreateModel model)
        {
            if (await _context.Products.AnyAsync(x => x.ProductName == model.ProductName))
            {
                return Conflict("A product with this name or article number already exists...");
            }

            var productEntity = new ProductEntity(
                model.ProductName,
                model.Price,
                model.Description,
                model.Created);
            
            var category = await _context.Category.FirstOrDefaultAsync(x=> x.CategoryName == model.CategoryName);

            if (category != null)
            {
                productEntity.CategoryId = category.Id;
            } else
            {
                productEntity.Category = new CategoryEntity(model.CategoryName);
            }

            _context.Products.Add(productEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductEntity", new { id = productEntity.Id }, new ProductModel(
                productEntity.Id,
                productEntity.ProductName,
                productEntity.Price,
                productEntity.Description,
                productEntity.Created,
                productEntity.CategoryId,
                 new CategoryModel(productEntity.Category.Id, productEntity.Category.CategoryName)));
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductEntity(int id)
        {
            var productEntity = await _context.Products.FindAsync(id);
            if (productEntity == null)
            {
                return NotFound();
            }

            productEntity.ProductName = "Deleted";
            productEntity.ArticleNumber = "Deleted";
            productEntity.Price = 0;
            productEntity.Description = "Deleted";
            productEntity.Updated = DateTime.Now;

            _context.Products.Remove(productEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductEntityExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
