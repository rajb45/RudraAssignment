using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SBIShopify.Data;
using SBIShopify.Identity;
using SBIShopify.Models;
using SBIShopify.Models.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace SBIShopify.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly SBIShopifyDBContext _SBIShopifyDBContext;
        private readonly IConfiguration _configuration;

        public CommonController(SBIShopifyDBContext SBIShopifyDBContext,
            IConfiguration configuration)
        {
            _SBIShopifyDBContext = SBIShopifyDBContext;
            _configuration = configuration;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<LoginDTO> AuthenticateUser([FromBody] LoginDTO login)
        {
            LoginDTO loginDTO = null;
            var Userdata =await  _SBIShopifyDBContext.Customers.FirstOrDefaultAsync(x => x.UserId == login.UserId);
            if(Userdata == null)
            {
                return loginDTO;
            }
            if (Userdata.Password == login.Password && Userdata.IsAdmin)
            {
                loginDTO = login;
                loginDTO.Role = "Admin";
            }
            else
            {
                loginDTO = login;
                loginDTO.Role = "User";
            }
            return loginDTO;
        }
        private string GenerateToken(LoginDTO user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserId),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["JWT:Issuer"],
                _configuration["JWT:Audience"],
                claims, 
                expires: DateTime.Now.AddMinutes(1), 
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginDTO loginDTO)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(loginDTO);
            if(user != null)
            {
                var token = GenerateToken(user.Result);
                response = Ok(new { token = token });
            }
            return response;
        }

        [HttpPost]
        [Authorize(Roles = IdentityData.adminUserClaimName)]
        public async Task<IActionResult> GetAllProducts()
        {
            var productData = await _SBIShopifyDBContext.Products.ToListAsync();
            return Ok(productData);
        }
        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            var customerData = await _SBIShopifyDBContext.Customers.Where(x=>x.UserId == customer.UserId).ToListAsync();
            if (customerData.Count > 0)
            {
                return Conflict("Data Already Exists");
            }
            else
            {
                customer.Id = new Guid();
                    await _SBIShopifyDBContext.Customers.AddAsync(customer);
                //    var cartData = new Cart
                //    {
                //        CustomerId = customer.Id
                //    };
                //await _SBIShopifyDBContext.Carts.AddAsync(cartData);
                await _SBIShopifyDBContext.SaveChangesAsync();

                return Ok(customer);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDTO cart)
        {
            var userData = await _SBIShopifyDBContext.Customers.FirstOrDefaultAsync(x=> x.UserId == cart.CustomerUserId);
            if (userData != null)
            {
                var cartObj = new Cart
                {
                    CustomerId = userData.Id,
                    Quantity = cart.Quantity,
                    ProductId = cart.ProductId
                };
                await _SBIShopifyDBContext.Carts.AddAsync(cartObj);
                await _SBIShopifyDBContext.SaveChangesAsync();
                return Ok(cart);
            }
            else
            {
                return NotFound("User Not Found");
            }
            
        }

    }
}
