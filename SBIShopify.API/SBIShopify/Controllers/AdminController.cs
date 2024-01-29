using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SBIShopify.Data;
using SBIShopify.Identity;
using SBIShopify.Models;

namespace SBIShopify.Controllers
{
    [Authorize(Roles = IdentityData.adminUserClaimName)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private SBIShopifyDBContext _SBIShopifyDBContext;
        public AdminController(SBIShopifyDBContext SBIShopifyDBContext)
        {
            _SBIShopifyDBContext = SBIShopifyDBContext;
        }
        [HttpPost]
        public async Task<IActionResult> AddProducts([FromBody] Products products)
        {

            products.Id = new Guid();
            await _SBIShopifyDBContext.Products.AddAsync(products);
            await _SBIShopifyDBContext.SaveChangesAsync();
            return Ok(products);
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {

            await _SBIShopifyDBContext.Categories.AddAsync(category);
            await _SBIShopifyDBContext.SaveChangesAsync();
            return Ok(category);
        }

    }
}
