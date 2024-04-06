using DAL.Enum;
using Domain.Contracts.Sale;
using Domain.Result;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly SaleService _saleService;

        public SaleController(SaleService saleService)
        {
            _saleService = saleService;
        }

        [Authorize(Roles = $" {nameof(Roles.Admin)}")]
        [HttpGet]
        public async Task<ActionResult<CollectionResult<SaleDto>>> GetAllSales()
        {
            var result = await _saleService.GetAllSalesAsync();
            if (result.Data == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Authorize(Roles = $" {nameof(Roles.Sales)}")]
        [HttpGet("your")]
        public async Task<ActionResult<CollectionResult<SaleDto>>> GetAllYourSales()
        {
            var result = await _saleService.GetAllYourSalesAsync();
            if (result.Data == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Authorize(Roles = $" {nameof(Roles.Sales)}")]
        [HttpPost]
        public async Task<ActionResult<BaseResult<SaleDto>>> CreateSale(SaleCreateDto saleCreateDto)
        {
            var result = await _saleService.CreateSaleAsync(saleCreateDto);
            if (result.Data == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
