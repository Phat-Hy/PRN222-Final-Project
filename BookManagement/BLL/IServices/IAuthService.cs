using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface IAuthService
    {
        Task<UserResponseDto> RegisterAsync(RegisterRequestDto request);
        Task<UserResponseDto> LoginAsync(LoginRequestDto request);
        Task<UserProfileDto> ViewProfileAsync(int userId);
        Task<UserProfileDto> UpdateUsernameAsync(int userId, string newUsername);
    }
}
