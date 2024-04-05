using DAL.Enum;
using Domain.Dto;
using Domain.Result;

namespace Domain.Services
{
    public interface IUserService
    {
        Task<BaseResult<UserDto>> BlockUserAsync(int userId);
        Task<BaseResult<UserDto>> ChangeUserRoleAsync(int userId, Roles role);
        Task<BaseResult<UserDto>> ChangeUserRoleAsync(int userId, string oldPassword, string newPassword);
        Task<BaseResult<UserDto>> CreateUserAsync(UserCreateDto userCreateDto);
        Task<BaseResult<UserDto>> DeleteUserByIdAsync(int userId);
        Task<CollectionResult<UserDto>> GetAllUsersAsync(int userId);
        Task<BaseResult<UserDto>> GetCurrentUserAsync();
        Task<BaseResult<UserDto>> LoginUser(UserLoginDto userLoginDto);
        Task<BaseResult<UserDto>> UnblockUserAsync(int userId);
    }
}