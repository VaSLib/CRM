using AutoMapper;
using DAL.Entity;
using Domain.Dto;

namespace Domain.Mapping;

public class UserMapping:Profile
{
    public UserMapping()
    {
        CreateMap<User, UserCreateDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
    }
}
