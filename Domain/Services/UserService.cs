using AutoMapper;
using DAL.Entity;
using DAL.Repositories.Interfaces;
using Domain.Dto;
using Domain.Result;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using DAL.Enum;
using Domain.Interfaces.Service;

namespace Domain.Services;

public class UserService : IUserService
{

    private readonly IBaseRepository<User> _userRepository;
    private readonly HttpContext _httpContext;
    private readonly IMapper _mapper;



    public UserService(IBaseRepository<User> userRepository, IMapper mapper, IHttpContextAccessor accessor)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        if (accessor.HttpContext == null)
        {
            throw new ArgumentException(nameof(accessor.HttpContext));
        }
        _httpContext = accessor.HttpContext;
    }

    public async Task<BaseResult<UserDto>> LoginUser(UserLoginDto userLoginDto)
    {
        try
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Email == userLoginDto.Email);


            if (user == null)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = "User not found",
                };
            }

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.Password);
            if (!isPasswordCorrect)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = "invalid password",
                };
            }

            if (user.DateOfBlocking == null)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = "User is blocked",
                };
            }

            var claims = new List<Claim>() {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Roles.ToString())};

            var claimsIdentity = new ClaimsIdentity(claims, "cookie");
            await _httpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

            return new BaseResult<UserDto>()
            {
                Data = _mapper.Map<UserDto>(user)
            };
        }
        catch (Exception ex)
        {
            return new BaseResult<UserDto>()
            {
                ErrorMessage = ex.Message
            };
        }

    }

    public async Task<BaseResult<UserDto>> CreateUserAsync(UserCreateDto userCreateDto)
    {
        try
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userCreateDto.Password, salt);

            var user = _mapper.Map<User>(userCreateDto);
            user.Password = hashedPassword;

            await _userRepository.CreateAsync(user);

            return new BaseResult<UserDto>()
            {
                Data = _mapper.Map<UserDto>(user)
            };
        }
        catch (Exception ex)
        {
            return new BaseResult<UserDto>()
            {
                ErrorMessage = ex.Message,
            };
        }
    }

    public async Task<BaseResult<UserDto>> DeleteUserByIdAsync(int userId)
    {
        try
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Id == userId);
            await _userRepository.RemoveAsync(user);

            return new BaseResult<UserDto>()
            {
                Data = _mapper.Map<UserDto>(user)
            };
        }
        catch (Exception ex)
        {
            return new BaseResult<UserDto>()
            {
                ErrorMessage = ex.Message,
            };
        }
    }

    public async Task<CollectionResult<UserDto>> GetAllUsersAsync()
    {
        UserDto[] users;
        try
        {
            users = await _userRepository.GetAll()
                .Select(u => _mapper.Map<UserDto>(u))
                .ToArrayAsync();

        }
        catch (Exception ex)
        {
            return new CollectionResult<UserDto>()
            {
                ErrorMessage = ex.Message,
            };
        }

        if (!users.Any())
        {
            return new CollectionResult<UserDto>()
            {
                ErrorMessage = "User not found",
            };
        }

        return new CollectionResult<UserDto>()
        {
            Data = users,
            Count = users.Length

        };

    }

    public async Task<BaseResult<UserDto>> GetCurrentUserAsync()
    {
        try
        {
            int? userIdValue = null;

            var claim = _httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null && int.TryParse(claim.Value, out int userId))
            {
                userIdValue = userId;
            }

            var user = _userRepository.GetAll().FirstOrDefault(u => u.Id == userIdValue);

            if (user == null)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = "User not found",
                };
            }
            return new BaseResult<UserDto>()
            {
                Data = _mapper.Map<UserDto>(user)
            };
        }
        catch (Exception ex)
        {
            return new BaseResult<UserDto>()
            {
                ErrorMessage = ex.Message,
            };
        }
    }

    public async Task<BaseResult<UserDto>> BlockUserAsync(int userId)
    {
        try
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = "User not found",
                };
            }

            user.DateOfBlocking = DateTime.Now;
            return new BaseResult<UserDto>()
            {
                Data = _mapper.Map<UserDto>(user)
            };

        }
        catch (Exception ex)
        {
            return new BaseResult<UserDto>()
            {
                ErrorMessage = ex.Message,
            };
        }
    }

    public async Task<BaseResult<UserDto>> UnblockUserAsync(int userId)
    {
        try
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = "User not found",
                };
            }

            user.DateOfBlocking = null;
            return new BaseResult<UserDto>()
            {
                Data = _mapper.Map<UserDto>(user)
            };

        }
        catch (Exception ex)
        {
            return new BaseResult<UserDto>()
            {
                ErrorMessage = ex.Message,
            };
        }
    }

    public async Task<BaseResult<UserDto>> ChangeUserRoleAsync(int userId, Roles role)
    {
        try
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = "User not found",
                };
            }

            user.Roles = role;
            return new BaseResult<UserDto>()
            {
                Data = _mapper.Map<UserDto>(user)
            };

        }
        catch (Exception ex)
        {
            return new BaseResult<UserDto>()
            {
                ErrorMessage = ex.Message,
            };
        }
    }

    public async Task<BaseResult<UserDto>> ChangeUserPasswordAsync(string oldPassword, string newPassword)
    {
        try
        {
            int? userIdValue = null;

            var claim = _httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null && int.TryParse(claim.Value, out int userId))
            {
                userIdValue = userId;
            }

            var user = _userRepository.GetAll().FirstOrDefault(u => u.Id == userIdValue);

            if (user == null)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = "User not found",
                };
            }

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(oldPassword, user.Password);

            if (!isPasswordCorrect)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = "Old password is incorrect ",
                };
            }

            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword, salt);

            user.Password = hashedPassword;
            return new BaseResult<UserDto>()
            {
                Data = _mapper.Map<UserDto>(user)
            };

        }
        catch (Exception ex)
        {
            return new BaseResult<UserDto>()
            {
                ErrorMessage = ex.Message,
            };
        }
    }


}
