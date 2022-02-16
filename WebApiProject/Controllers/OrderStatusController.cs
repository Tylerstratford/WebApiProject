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
using WebApiProject.Models.StatusModels;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusController : ControllerBase
    {
        private readonly SqlContext _context;

        public OrderStatusController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/OrderStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusModel>>> GetOrderStatuses()
        {
            var items = new List<StatusModel>();
            foreach (var i in await _context.OrderStatuses.ToListAsync())
                items.Add(new StatusModel(i.Id,i.Status));
            return items;
        }

        // GET: api/OrderStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StatusModel>> GetOrderStatusEntity(int id)
        {
            var orderStatusEntity = await _context.OrderStatuses.FindAsync(id);

            if (orderStatusEntity == null)
            {
                return NotFound();
            }

            return new StatusModel(orderStatusEntity.Id, orderStatusEntity.Status);
        }

        // PUT: api/OrderStatus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderStatusEntity(int id, StatusUpdateModel model)
        {
            if (id != model.Id)
            {
                return BadRequest("No ID found");
            }

            var orderStatusEntity = await _context.OrderStatuses.FindAsync(model.Id);

            if(orderStatusEntity == null)
            {
                return BadRequest("No ID enetered...");
            }

            if (await _context.OrderStatuses.AnyAsync(x => x.Status == model.Status))
                return Conflict("That status already exists");

            orderStatusEntity.Status = model.Status;

            _context.Entry(orderStatusEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderStatusEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok($"Status changed to: {model.Status}");
        }

        // POST: api/OrderStatus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StatusModel>> PostOrderStatusEntity(StatusCreateModel model)
        {
            if (await _context.OrderStatuses.AnyAsync(x => x.Status == model.Status))
                return Conflict("Status already exists");
            var orderStatusEntity = new OrderStatusEntity(model.Status);

            _context.OrderStatuses.Add(orderStatusEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderStatusEntity", new { id = orderStatusEntity.Id }, new StatusModel(orderStatusEntity.Id, orderStatusEntity.Status));
        }

        // DELETE: api/OrderStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderStatusEntity(int id)
        {
            var orderStatusEntity = await _context.OrderStatuses.FindAsync(id);
            if (orderStatusEntity == null)
            {
                return NotFound();
            }

            orderStatusEntity.Status = "Status-Deleted";

            _context.Entry(orderStatusEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderStatusEntityExists(int id)
        {
            return _context.OrderStatuses.Any(e => e.Id == id);
        }
    }
}
