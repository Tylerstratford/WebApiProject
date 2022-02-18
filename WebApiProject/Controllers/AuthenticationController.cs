using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiProject.Data;
using WebApiProject.Filters;
using WebApiProject.Models.Entities;
using WebApiProject.Models.LogIns;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly SqlContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(SqlContext context, IConfiguration configuration, ILogger<AuthenticationController> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("SignUp")]
        //Not sure if ApiKey is needed here... depending on how/what the Api is used for. If the product is free then users should be able to sign up without a API key, if the product costs money to use then they should need an API key to sign up there information
        public async Task<ActionResult> SignUp(SignUpModel m)
        {
            if (await _context.Customer.AnyAsync(x => x.Email == m.Email))
                return BadRequest();

                var customerEntity = new CustomerEntity(m.FirstName, m.LastName, m.Email, m.TelephoneNumber, m.DateCreated);
                customerEntity.CreateSecurePassword(m.Password);

                var address = await _context.Address.FirstOrDefaultAsync(x => x.StreetName == m.StreetName && x.PostalCode == m.PostalCode);

                if (address != null)
                {
                    customerEntity.AddressId = address.Id;
                }
                else
                {
                    customerEntity.Address = new AddressEntity(m.StreetName, m.PostalCode, m.City, m.Country);
                }

                _context.Customer.Add(customerEntity);
                await _context.SaveChangesAsync();

                return Ok("Customer created");
        }

        [HttpPost("SignUpAdmin")]
        [UseAdminApiKey]
        public async Task<ActionResult> SignUpAdmin(SignUpAdminModel m)
        {
            if (await _context.Admins.AnyAsync(x => x.Email == m.Email))
                return BadRequest("An admin with this email address already exists");

            var adminEntity = new AdminEntity(m.FirstName, m.LastName, m.Email);
            adminEntity.CreateSecurePassword(m.Password);

            _context.Admins.Add(adminEntity);
            await _context.SaveChangesAsync();

            return Ok("Admin created");
        }


        [HttpPost("SignInCustomer")]
        [UseApiKey]
        [UseCustomerApiKey]
        public async Task<ActionResult> SignInCustomer(SignInModel m)
        {
            if (string.IsNullOrEmpty(m.Email) || string.IsNullOrEmpty(m.Password))
                return BadRequest("Please provide an email and password");

            var customerEntity = await _context.Customer.FirstOrDefaultAsync(x => x.Email == m.Email);
            if (customerEntity == null || !customerEntity.CompareSecurePassword(m.Password))
                return BadRequest("Incorrect email address or password");

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", customerEntity.Id.ToString()),
                    new Claim(ClaimTypes.Name, customerEntity.Email),
                    new Claim("code", _configuration.GetValue<string>("ApiKey")),
                    new Claim("code", _configuration.GetValue<string>("CustomerApiKey"))
                }),
                Expires = DateTime.Now.AddMinutes(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Secret"))),
                    SecurityAlgorithms.HmacSha512Signature
                    )
            };

            return Ok(tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)));
        }

        [HttpPost("SignInAdmin")]
        [UseAdminApiKey]
        public async Task<ActionResult> SignInAdmin(SignInModel m)
        {
            if (string.IsNullOrEmpty(m.Email) || string.IsNullOrEmpty(m.Password))
                return BadRequest("Please provide an email and password");

            var adminEntity = await _context.Admins.FirstOrDefaultAsync(x => x.Email == m.Email);
            if (adminEntity == null || !adminEntity.CompareSecurePassword(m.Password))
                return BadRequest("Incorrect email address or password");

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", adminEntity.Id.ToString()),
                    new Claim(ClaimTypes.Name, adminEntity.Email),
                    new Claim("code", _configuration.GetValue<string>("ApiKey")),
                    new Claim("code", _configuration.GetValue<string>("AdminApiKey"))
                }),
                Expires = DateTime.Now.AddMinutes(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Secret"))),
                    SecurityAlgorithms.HmacSha512Signature
                    )
            };

            return Ok(tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)));
        }

    }
}
