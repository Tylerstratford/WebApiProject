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
    public class OrdersStatusController : ControllerBase
    {
        private readonly SqlContext _context;

        public OrdersStatusController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/OrdersStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdersEntity>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        // GET: api/OrdersStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdersEntity>> GetOrdersEntity(int id)
        {
            var ordersEntity = await _context.Orders.FindAsync(id);

            if (ordersEntity == null)
            {
                return NotFound();
            }

            return ordersEntity;
        }

        // PUT: api/OrdersStatus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrdersEntity(int id, OrdersEntity ordersEntity)
        {
            if (id != ordersEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(ordersEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdersEntityExists(id))
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

        // POST: api/OrdersStatus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrdersEntity>> PostOrdersEntity(OrdersEntity ordersEntity)
        {
            _context.Orders.Add(ordersEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrdersEntity", new { id = ordersEntity.Id }, ordersEntity);
        }

        // DELETE: api/OrdersStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdersEntity(int id)
        {
            var ordersEntity = await _context.Orders.FindAsync(id);
            if (ordersEntity == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(ordersEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdersEntityExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
