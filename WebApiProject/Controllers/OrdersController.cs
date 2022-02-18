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
using WebApiProject.Models.AddressModels;
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
    [Authorize]
    [UseApiKey]
    public class OrdersController : ControllerBase
    {
        private readonly SqlContext _context;

        public OrdersController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        [UseAdminApiKey]
        public async Task<ActionResult<IEnumerable<OrderOutputModel>>> GetOrders()
        {
            var items = new List<OrderOutputModel>();
            

            foreach (var i in await _context.Orders.Include(x => x.Customer).ThenInclude(x => x.Address).Include(x => x.Lines).Include(x => x.OrderStatus).ToListAsync()) 
            {

                var line = new List<OrderLinesOutputModel>();

                foreach (var _line in i.Lines.Where(x => x.OrderId == i.Id))
                {
                    var _product = await _context.Products.Where(x => x.Id == _line.ProductId).FirstOrDefaultAsync();
                    line.Add(new OrderLinesOutputModel(
                        _line.Id,
                        _line.OrderId,
                        _line.ProductId,
                        _line.Quantity,
                        _line.LinePrice,
                        new ProductOutputModel(
                            _line.Product.Id,
                            _line.Product.ProductName,
                            _line.Product.ArticleNumber,
                            _line.Product.Price))); 
                }
                items.Add(new OrderOutputModel(
                    i.Id,
                    i.Created,
                    i.Updated,
                    new CustomerOutputModel(
                        i.Customer.Id),
                        new AddressModel(
                            i.Customer.Address.Id,
                            i.Customer.Address.StreetName,
                            i.Customer.Address.PostalCode,
                            i.Customer.Address.City,
                            i.Customer.Address.Country),
                    new StatusModel(
                        i.OrderStatus.Id,
                        i.OrderStatus.Status),
                    line,
                    i.Total));
                    }
                return items;
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        [UseAdminApiKey]
        public async Task<ActionResult<OrderOutputModel>> GetOrdersEntity(int id)
        {
            var ordersEntity = await _context.Orders.Include(x => x.Customer).ThenInclude(x => x.Address).Include(x => x.Lines).ThenInclude(x => x.Product).Include(x => x.OrderStatus).FirstOrDefaultAsync(x => x.Id == id);

            if (ordersEntity == null)
            {
                return NotFound();
            }

            var line = new List<OrderLinesOutputModel>();

            foreach (var order in ordersEntity.Lines)
            {
                line.Add(new OrderLinesOutputModel(
                    order.ProductId,
                    order.OrderId,
                    order.ProductId,
                    order.Quantity,
                    order.LinePrice,
                    new ProductOutputModel(
                        order.Product.Id,
                        order.Product.ProductName,
                        order.Product.ArticleNumber,
                        order.Product.Price)));
            }

            return new OrderOutputModel(
                    ordersEntity.Id,
                    ordersEntity.Created,
                    ordersEntity.Updated,
                    new CustomerOutputModel(
                        ordersEntity.Customer.Id),
                        new AddressModel(
                            ordersEntity.Customer.Address.Id,
                            ordersEntity.Customer.Address.StreetName,
                            ordersEntity.Customer.Address.PostalCode,
                            ordersEntity.Customer.Address.City,
                            ordersEntity.Customer.Address.Country),
                    new StatusModel(
                        ordersEntity.OrderStatus.Id,
                        ordersEntity.OrderStatus.Status),
                    line,
                    ordersEntity.Total);
        }


        // PUT: api/Orders/5
        [HttpPut("{id}")]
        [UseAdminApiKey]
        public async Task<IActionResult> PutOrdersEntity(int id, OrderUpdateModel model)
        {
            var ordersEntity = await _context.Orders.Where(x => x.Id == id).Include(x => x.OrderStatus).FirstOrDefaultAsync();

            if (id != ordersEntity.Id)
            {
                return BadRequest("No order with that ID was found...");
            }


            if (ordersEntity == null)
            {
                return BadRequest("No ID entered...");
            }

            ordersEntity.OrderStatusId = model.StatusId;
            ordersEntity.Updated = DateTime.Now;

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
        [HttpPost]
        public async Task<ActionResult<OrderModel>> PostOrdersEntity(OrderCreateModel model)
        {
            List<OrderLinesEntity> Line = new();

            var _customer = await _context.Customer.FindAsync(model.CustomerId);
            var _status = await _context.OrderStatuses.FindAsync(model.StatusId);
            
            foreach( var lines in model.Lines)
            {
                var _product = await _context.Products.Where(x => x.Id == lines.ProductId).Include(x => x.Category).FirstOrDefaultAsync();

                var _linePrice = _product.Price * lines.Quantity;
                Line.Add(new OrderLinesEntity(lines.ProductId, lines.Quantity, _linePrice));
            }

            decimal total = 0;

            foreach (var line in Line)
            {
                total += line.LinePrice;
            }

            var orderEntity = new OrdersEntity(
                _customer,
                _status,
                total,
                Line);

            _context.Orders.Add(orderEntity);
            await _context.SaveChangesAsync();

            return Ok("Order Created");

        }

        //DELETE: api/Orders/5
        [HttpDelete("{id}")]
        [UseAdminApiKey]
        public async Task<IActionResult> DeleteOrdersEntity(int id)
        {
            var ordersEntity = await _context.Orders.FindAsync(id);

            if (ordersEntity == null)
            {
                return NotFound();
            }

            var statusEntity = await _context.OrderStatuses.Where(x => x.Status == "Order deleted").FirstOrDefaultAsync();

            if (statusEntity != null)
            {
                ordersEntity.OrderStatusId = statusEntity.Id;

            }
            else
            {
                ordersEntity.OrderStatus = new OrderStatusEntity("Order deleted");
            }

            _context.Entry(ordersEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdersEntityExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
