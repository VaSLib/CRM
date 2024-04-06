using DAL.Enum;
using Domain.Contracts.User;
using Domain.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CRM.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }   
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LoginUser(UserLoginDto userLoginDto)
    {
        var result = await _userService.LoginUser(userLoginDto);
        if (result.Data != null)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    [Authorize(Roles = nameof(Roles.Admin))]
    [HttpPost("register")]
    public async Task<IActionResult> CreateUserAsync(UserCreateDto userCreateDto)
    {
        var result = await _userService.CreateUserAsync(userCreateDto);
        if (result.Data != null)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [Authorize(Roles = nameof(Roles.Admin))]
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUserByIdAsync(int userId)
    {
        var result = await _userService.DeleteUserByIdAsync(userId);
        if (result.Data != null)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var result = await _userService.GetCurrentUserAsync();
        if (result.Data != null)
        {
            return Ok(result);
        }
        return NotFound(result);
    }

    [Authorize(Roles = nameof(Roles.Admin))]
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _userService.GetAllUsersAsync();
        if (result.Data != null)
        {
            return Ok(result);
        }
        return NotFound(result);
    }

    [Authorize(Roles = nameof(Roles.Admin))]
    [HttpPost("{userId}/block")]
    public async Task<IActionResult> BlockUserAsync(int userId)
    {
        var result = await _userService.BlockUserAsync(userId);
        if (result.Data != null)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [Authorize(Roles = nameof(Roles.Admin))]
    [HttpPost("{userId}/unblock")]
    public async Task<IActionResult> UnblockUserAsync(int userId)
    {
        var result = await _userService.UnblockUserAsync(userId);
        if (result.Data != null)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [Authorize(Roles = nameof(Roles.Admin))]
    [HttpPost("{userId}/change-role")]
    public async Task<IActionResult> ChangeUserRoleAsync(int userId, Roles role)
    {
        var result = await _userService.ChangeUserRoleAsync(userId, role);
        if (result.Data != null)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpPost("{userId}/change-password")]
    public async Task<IActionResult> ChangeUserPasswordAsync(string oldPassword, string newPassword )
    {

        var result = await _userService.ChangeUserPasswordAsync(oldPassword, newPassword);
        if (result.Data != null)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}
