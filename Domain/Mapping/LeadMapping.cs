using AutoMapper;
using DAL.Entity;
using Domain.Contracts.Contact;
using Domain.Contracts.Lead;

namespace Domain.Mapping;

public class LeadMapping : Profile
{
    public LeadMapping()
    {
        CreateMap<Lead, LeadDto>().ReverseMap();
        CreateMap<Lead, LeadCreateDto>().ReverseMap();
    }
}

