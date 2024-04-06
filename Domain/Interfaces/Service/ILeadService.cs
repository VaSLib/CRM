using DAL.Enum;
using Domain.Contracts.Lead;
using Domain.Result;

namespace Domain.Services
{
    public interface ILeadService
    {
        Task<BaseResult<LeadDto>> ChangeLeadStatusAsync(int leadId, LeadStatus status);
        Task<BaseResult<LeadDto>> CreateLeadAsync(LeadCreateDto leadCreateDto);
        Task<CollectionResult<LeadDto>> GetAllYourLeadsAsync();
    }
}