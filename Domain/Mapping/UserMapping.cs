using AutoMapper;
using DAL.Entity;
using Domain.Contacts;
using Domain.Contracts.User;

namespace Domain.Mapping;

public class UserMapping:Profile
{
    public UserMapping()
    {
        CreateMap<User, UserCreateDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
    }
}
