using AutoMapper;
using DAL.Entity;
using Domain.Contracts.Contact;
using Domain.Contracts.Sale;

namespace Domain.Mapping;

public class SaleMapping : Profile
{
    public SaleMapping()
    {
        CreateMap<Sale, SaleDto>().ReverseMap();
        CreateMap<Sale, SaleCreateDto>().ReverseMap();
    }
}
