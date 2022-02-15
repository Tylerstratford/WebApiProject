#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProject.Data;
using WebApiProject.Models.CustomerModels;
using WebApiProject.Models.Entities;
using WebApiProject.Models.OrderLinesModels;
using WebApiProject.Models.OrderModels;
using WebApiProject.Models.ProductModels;
using WebApiProject.Models.StatusModels;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly SqlContext _context;

        public OrdersController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderModel>>> GetOrders()
        {
            var items = new List<OrderModel>();
            var line = new List<OrderLinesModel>();

            foreach (var i in await _context.Orders.Include(x => x.Customer).Include(x => x.Lines).Include(x => x.OrderStatus).ToListAsync()) {

                foreach (var _line in i.Lines)
                {
                    var _product = await _context.Products.Where(x => x.Id == _line.ProductId).FirstOrDefaultAsync();
                    line.Add(new OrderLinesModel(
                        _line.Quantity,
                        _line.LinePrice,
                        new ProductModel(
                            _line.Product.Id,
                            _line.Product.ProductName,
                            _line.Product.ArticleNumber,
                            _line.Product.Price,
                            _line.Product.Description)));
                }
                items.Add(new OrderModel(
                    i.Id,
                    new CustomerModel(
                        i.Customer.Id),
                    new StatusModel(
                        i.OrderStatus.Id,
                        i.OrderStatus.Status),
                    line));
                    }
                return items;
        }

        // GET: api/Orders/5
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

        // PUT: api/Orders/5
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

        //POST: api/Orders
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderModel>> PostOrdersEntity(OrderCreateModel model)
        {
            List<OrderLinesEntity> Line = new();

            var _customer = await _context.Customer.FindAsync(model.CustomerId);
            var _status = await _context.OrderStatuses.FindAsync(model.StatusId);

            foreach( var lines in model.Lines)
            {
                var _product = await _context.Products.Where(x => x.Id == lines.ProductId).Include(x => x.Category).FirstOrDefaultAsync();
                //Line.Add(new OrderLinesEntity(lines.Quantity, new ProductEntity(_product.ProductName, _product.ArticleNumber, _product.Price, _product.Description, _product.CategoryId)));
                Line.Add(new OrderLinesEntity(lines.ProductId, lines.Quantity));
            }


            var orderEntity = new OrdersEntity(
                _customer,
                _status,
                Line);

            _context.Orders.Add(orderEntity);
            await _context.SaveChangesAsync();

            return Ok("Order Created");

        }

        //DELETE: api/Orders/5
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
