using BLL.DTOs;
using BLL.IServices;
using DAL.IRepositories.DAL.IRepositories;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthService(IUserRepository userRepo, IJwtTokenService jwtTokenService)
        {
            _userRepo = userRepo;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<UserResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            var existing = await _userRepo.GetByEmailAsync(request.Email);
            if (existing != null)
                throw new Exception("Email already exists.");

            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                IsActive = true
            };

            var createdUser = await _userRepo.CreateAsync(user);

            return new UserResponseDto
            {
                Id = createdUser.Id,
                UserName = createdUser.UserName,
                Email = createdUser.Email
            };
        }

        public async Task<UserResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await _userRepo.GetByEmailAsync(request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new Exception("Invalid email or password.");

            var token = _jwtTokenService.GenerateToken(user);

            return new UserResponseDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Token = token
            };
        }

        public async Task<UserProfileDto> ViewProfileAsync(int userId)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null || !user.IsActive)
                throw new Exception("User not found");

            return new UserProfileDto
            {
                UserName = user.UserName,
                Email = user.Email
            };
        }

        public async Task<UserProfileDto> UpdateUsernameAsync(int userId, string newUsername)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null || !user.IsActive)
                throw new Exception("User not found");

            user.UserName = newUsername;
            await _userRepo.UpdateAsync(user);

            return new UserProfileDto
            {
                UserName = user.UserName,
                Email = user.Email
            };
        }
    }
}
