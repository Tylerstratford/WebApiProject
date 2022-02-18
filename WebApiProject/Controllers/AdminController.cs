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
using WebApiProject.Models.AdminModel;
using WebApiProject.Models.Entities;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly SqlContext _context;

        public AdminController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/Admin
        [HttpGet]
        [UseAdminApiKey]
        public async Task<ActionResult<IEnumerable<AdminModel>>> GetAdmins()
        {
           var items = new List<AdminModel>();
           foreach (var i in await _context.Admins.ToListAsync())
            {
                items.Add(new AdminModel(
                    i.Name,
                    i.LastName,
                    i.Email));
            }

            return items;
        }

        // GET: api/Admin/5
        [HttpGet("{id}")]
        [UseAdminApiKey]
        public async Task<ActionResult<AdminModel>> GetAdminEntity(int id)
        {
            var adminEntity = await _context.Admins.FindAsync(id);

            if (adminEntity == null)
            {
                return NotFound("No ID found");
            }

            return new AdminModel(adminEntity.Name,adminEntity.LastName,adminEntity.Email);
        }

        // PUT: api/Admin/5
        [HttpPut("{id}")]
        [UseAdminApiKey]
        public async Task<IActionResult> PutAdminEntity(int id, AdminModel model)
        {
            var adminEntity = await _context.Admins.FirstOrDefaultAsync(x => x.Id == id);
            if (id != adminEntity.Id)
            {
                return BadRequest("No ID found");
            }

            adminEntity.Name = model.Name;
            adminEntity.LastName = model.Lastname;
            adminEntity.Email = model.Email;

            _context.Entry(adminEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok($"Admin successfully changed:\n {adminEntity.Name} \n {adminEntity.LastName}\n {adminEntity.Email}");
        }

        // DELETE: api/Admin/5
        [HttpDelete("{id}")]
        [UseAdminApiKey]
        public async Task<IActionResult> DeleteAdminEntity(int id)
        {
            var adminEntity = await _context.Admins.FindAsync(id);
            if (adminEntity == null)
            {
                return NotFound("No ID found");
            }

            adminEntity.Name = "Admin deleted";
            adminEntity.LastName = "Admin deleted";
            adminEntity.Email = "Admin deleted";

            _context.Entry(adminEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok("Admin deleted");
        }

        private bool AdminEntityExists(int id)
        {
            return _context.Admins.Any(e => e.Id == id);
        }
    }
}
