using AutoMapper;
using DAL.Entity;
using Domain.Contacts;
using Domain.Contracts.Contact;
using Domain.Contracts.User;

namespace Domain.Mapping;

public class ContactMapping: Profile
{
    public ContactMapping()
    {
        CreateMap<Contact, ContactDto>().ReverseMap();
        CreateMap<Contact, ContactCreateDto>().ReverseMap();
        CreateMap<Contact, ContactUpdateDto>().ReverseMap();
    }
}
