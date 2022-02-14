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
    public class OrderLinesController : ControllerBase
    {
        private readonly SqlContext _context;

        public OrderLinesController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/OrderLines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderLinesEntity>>> GetOrderLines()
        {
            return await _context.OrderLines.ToListAsync();
        }

        // GET: api/OrderLines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderLinesEntity>> GetOrderLinesEntity(int id)
        {
            var orderLinesEntity = await _context.OrderLines.FindAsync(id);

            if (orderLinesEntity == null)
            {
                return NotFound();
            }

            return orderLinesEntity;
        }

        // PUT: api/OrderLines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderLinesEntity(int id, OrderLinesEntity orderLinesEntity)
        {
            if (id != orderLinesEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderLinesEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderLinesEntityExists(id))
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

        // POST: api/OrderLines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderLinesEntity>> PostOrderLinesEntity(OrderLinesEntity orderLinesEntity)
        {
            _context.OrderLines.Add(orderLinesEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderLinesEntity", new { id = orderLinesEntity.Id }, orderLinesEntity);
        }

        // DELETE: api/OrderLines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderLinesEntity(int id)
        {
            var orderLinesEntity = await _context.OrderLines.FindAsync(id);
            if (orderLinesEntity == null)
            {
                return NotFound();
            }

            _context.OrderLines.Remove(orderLinesEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderLinesEntityExists(int id)
        {
            return _context.OrderLines.Any(e => e.Id == id);
        }
    }
}
