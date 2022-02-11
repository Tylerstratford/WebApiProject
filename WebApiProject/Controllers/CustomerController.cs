﻿#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProject.Data;
using WebApiProject.Models.AddressModels;
using WebApiProject.Models.CustomerModels;
using WebApiProject.Models.Entities;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly SqlContext _context;

        public CustomerController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerModel>>> GetCustomer()
        {
            var items = new List<CustomerModel>();
            foreach (var i in await _context.Customer.Include(x => x.Address).ToListAsync())
                items.Add(new CustomerModel(
                    i.Id,
                    i.FirstName,
                    i.LastName,
                    i.Email,
                    i.TelephoneNumber,
                    i.DateCreated,
                    i.AddressId,
                    new AddressModel(
                        i.Address.Id,
                        i.Address.StreetName,
                        i.Address.PostalCode,
                        i.Address.City,
                        i.Address.Country)));

            return items;
        }



        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerEntity>> GetCustomerEntity(int id)
        {
            var customerEntity = await _context.Customer.FindAsync(id);

            if (customerEntity == null)
            {
                return NotFound();
            }

            return customerEntity;
        }

        // PUT: api/Customer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerEntity(int id, CustomerUpdateModel model)
        {

            if (id != model.id)
            {
                return BadRequest();
            }

            var customerEntity = await _context.Customer.FindAsync(model.id);

            if (customerEntity == null)
            {
                return BadRequest();
            }

            customerEntity.FirstName = model.FirstName;
            customerEntity.LastName = model.LastName;
            customerEntity.Email = model.Email;
            customerEntity.TelephoneNumber = model.TelephoneNumber;
            customerEntity.Password = model.Password;
            customerEntity.Address = new AddressEntity(
                model.StreetName,
                model.PostalCode,
                model.City,
                model.Country);

            _context.Entry(customerEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerEntityExists(id))
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

        // POST: api/Customer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerModel>> PostCustomerEntity(CustomerCreateModel model)
        {
            if (await _context.Customer.AnyAsync(x => x.Email == model.Email))
                return Conflict("A customer with that email already exists");


            var customerEntity = new CustomerEntity(
                model.FirstName,
                model.LastName,
                model.Email,
                model.TelephoneNumber,
                model.Password,
                model.DateCreated);
                //new AddressEntity(
                //    model.StreetName,
                //    model.PostalCode,
                //    model.City,
                //    model.Country));

            var address = await _context.Address.FirstOrDefaultAsync(x => x.StreetName == model.StreetName && x.PostalCode == model.PostalCode);
            if (address != null)
            {
                customerEntity.AddressId = address.Id;
            } else
            {
                customerEntity.Address = new AddressEntity(model.StreetName, model.PostalCode,model.City,model.Country);
            }

            _context.Customer.Add(customerEntity);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCustomerEntity", new { id = customerEntity.Id }, new CustomerModel(
                customerEntity.Id,
                customerEntity.FirstName,
                customerEntity.LastName,
                customerEntity.Email,
                customerEntity.TelephoneNumber,
                customerEntity.DateCreated, 
                customerEntity.AddressId,
                new AddressModel(
                    customerEntity.Address.Id,
                    customerEntity.Address.StreetName,
                    customerEntity.Address.PostalCode,
                    customerEntity.Address.City,
                    customerEntity.Address.Country)));
        }




        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerEntity(int id)
        {
            var customerEntity = await _context.Customer.FindAsync(id);
            if (customerEntity == null)
            {
                return NotFound();
            }

            customerEntity.FirstName = "Deleted";
            customerEntity.LastName = "Deleted";
            customerEntity.Email = "Deleted";
            customerEntity.TelephoneNumber = "Deleted";
            customerEntity.Password = "Deleted";
            customerEntity.DateUpdated = DateTime.Now;  
        

            _context.Entry(customerEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerEntityExists(int id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }
    }
}
