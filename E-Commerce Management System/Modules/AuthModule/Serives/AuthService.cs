using E_Commerce_Management_System.Modules.AuthModule.Dtos;
using E_Commerce_Management_System.Modules.AuthModule.Interfaces;
using E_Commerce_Management_System.Shared.Entities;
using E_Commerce_Management_System.Shared.Helpers;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce_Management_System.Modules.AuthModule.Serives
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtTokenHelper _jwtHelper;

        public AuthService(UserManager<ApplicationUser> userManager, JwtTokenHelper jwtHelper)
        {
            _userManager = userManager;
            _jwtHelper = jwtHelper;
        }

        public async Task<ApiResponse<AuthResponseDto>> RegisterCustomerAsync(RegisterRequestDto request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser is not null)
                return ApiResponse<AuthResponseDto>.Fail("A user with this email already exists.");

            var newUser = new ApplicationUser
            {
                FullName = request.FullName,
                UserName = request.Email,
                Email = request.Email,
                EmailConfirmed = true
            };

            var createResult = await _userManager.CreateAsync(newUser, request.Password);
            if (!createResult.Succeeded)
            {
                var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                return ApiResponse<AuthResponseDto>.Fail(errors);
            }

            await _userManager.AddToRoleAsync(newUser, Roles.Customer);

            var roles = await _userManager.GetRolesAsync(newUser);
            var (token, expiresAt) = _jwtHelper.GenerateToken(newUser, roles);

            return ApiResponse<AuthResponseDto>.Ok(BuildResponse(newUser, roles, token, expiresAt),
                                                   "Registration successful.");
        }

        public async Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginRequestDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
                return ApiResponse<AuthResponseDto>.Fail("Invalid email or password.");

            var roles = await _userManager.GetRolesAsync(user);
            var (token, expiresAt) = _jwtHelper.GenerateToken(user, roles);

            return ApiResponse<AuthResponseDto>.Ok(BuildResponse(user, roles, token, expiresAt),
                                                   "Login successful.");
        }

        private static AuthResponseDto BuildResponse(
            ApplicationUser user, IList<string> roles, string token, DateTime expiresAt) => new()
            {
                Token = token,
                Email = user.Email!,
                FullName = user.FullName,
                Roles = roles,
                ExpiresAt = expiresAt
            };
    }
}
