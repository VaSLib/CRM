using AutoMapper;
using DAL.Entity;
using DAL.Enum;
using DAL.Repositories.Interfaces;
using Domain.Contracts.Lead;
using Domain.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace Domain.Services;

public class LeadService : ILeadService
{
    private readonly IBaseRepository<Lead> _leadRepository;
    private readonly IBaseRepository<Contact> _contactRepository;
    private readonly HttpContext _httpContext;
    private readonly IMapper _mapper;

    public LeadService(IMapper mapper, IHttpContextAccessor accessor, IBaseRepository<Lead> leadRepository)
    {
        _mapper = mapper;
        if (accessor.HttpContext == null)
        {
            throw new ArgumentException(nameof(accessor.HttpContext));
        }
        _httpContext = accessor.HttpContext;

        _leadRepository = leadRepository;
    }

    public async Task<CollectionResult<LeadDto>> GetAllYourLeadsAsync()
    {
        LeadDto[] leads;
        try
        {
            int? userIdValue = null;

            var claim = _httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null && int.TryParse(claim.Value, out int userId))
            {
                userIdValue = userId;
            }

            leads = await _leadRepository.GetAll()
                .Where(l => l.SalerId == userIdValue)
                .Select(l => _mapper.Map<LeadDto>(l))
                .ToArrayAsync();
            if (!leads.Any())
            {
                return new CollectionResult<LeadDto>()
                {
                    ErrorMessage = "Lead not found",
                };
            }
            return new CollectionResult<LeadDto>()
            {
                Data = leads,
                Count = leads.Length

            };
        }
        catch (Exception ex)
        {
            return new CollectionResult<LeadDto>()
            {
                ErrorMessage = ex.Message,
            };
        }
    }
    public async Task<BaseResult<LeadDto>> CreateLeadAsync(LeadCreateDto leadCreateDto)
    {
        try
        {
            var lead = _mapper.Map<Lead>(leadCreateDto);

            int? salerId = null;

            var claim = _httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null && int.TryParse(claim.Value, out int userId))
            {
                salerId = userId;
            }

            lead.SalerId = salerId;

            var contact = await _contactRepository.GetAll()
                .FirstOrDefaultAsync(c => c.Id == lead.ContactId);

            if (contact != null)
            {
                return new BaseResult<LeadDto>()
                {
                    ErrorMessage = "Contact not found",
                };
            }

            contact.Status = ContactStatus.Lead;
            await _leadRepository.UpdateAsync(lead);
            await _contactRepository.UpdateAsync(contact);

            return new BaseResult<LeadDto>()
            {
                Data = _mapper.Map<LeadDto>(lead)
            };
        }
        catch (Exception ex)
        {
            return new BaseResult<LeadDto>()
            {
                ErrorMessage = ex.Message,
            };
        }
    }

    public async Task<BaseResult<LeadDto>> ChangeLeadStatusAsync(int leadId, LeadStatus status)
    {
        try
        {
            var lead = _leadRepository.GetAll().FirstOrDefault(l => l.Id == leadId);
            if (lead == null)
            {
                return new BaseResult<LeadDto>()
                {
                    ErrorMessage = "Lead not found",
                };
            }



            lead.Status = status;
            await _leadRepository.UpdateAsync(lead);
            return new BaseResult<LeadDto>()
            {
                Data = _mapper.Map<LeadDto>(lead)
            };

        }
        catch (Exception ex)
        {
            return new BaseResult<LeadDto>()
            {
                ErrorMessage = ex.Message,
            };
        }
    }


}
