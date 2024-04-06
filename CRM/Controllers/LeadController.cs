using DAL.Enum;
using Domain.Contracts.Lead;
using Domain.Result;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadController : ControllerBase
    {
        private readonly LeadService _leadService;

        public LeadController(LeadService leadService)
        {
            _leadService = leadService;
        }

        [Authorize(Roles = $" {nameof(Roles.Sales)}")]
        [HttpGet]
        public async Task<ActionResult<CollectionResult<LeadDto>>> GetAllYourLeads()
        {
            var result = await _leadService.GetAllYourLeadsAsync();
            if (result.Data == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Authorize(Roles = $" {nameof(Roles.Sales)}")]
        [HttpPost]
        public async Task<ActionResult<BaseResult<LeadDto>>> CreateLead(LeadCreateDto leadCreateDto)
        {
            var result = await _leadService.CreateLeadAsync(leadCreateDto);
            if (result.Data == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Authorize(Roles = $" {nameof(Roles.Sales)}")]
        [HttpPatch("{leadId}/status")]
        public async Task<ActionResult<BaseResult<LeadDto>>> ChangeLeadStatus(int leadId, [FromBody] LeadStatus status)
        {
            var result = await _leadService.ChangeLeadStatusAsync(leadId, status);
            if (result.Data == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
