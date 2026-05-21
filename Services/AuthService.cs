using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using TaskMini.Data;
using TaskMini.DTO.Auth;
using TaskMini.Entities;
using TaskMini.Exceptions;
using TaskMini.Interfaces;

namespace TaskMini.Services
{
    public class AuthService:IAuthService
    {
        private readonly TaskMiniDbContext _dbContext;
        private readonly IJwtService _jwtService;

        public AuthService(TaskMiniDbContext dbContext, IJwtService jwtService)
        {
            _dbContext = dbContext;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDto> RegisterUser(RegisterDto registerDto)
        {
            if (registerDto == null)
                throw new BadRequestException("Invalid Input");            

            var isUserExist=await _dbContext.Users.AsNoTracking().AnyAsync(u=>u.Email == registerDto.Email);

            if (isUserExist)
            {
                throw new BadRequestException("User already exist");
            }

            var user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
            };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return new AuthResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
            };
        }

        public async Task<AuthResponseDto> LoginUser(LoginDto loginDto)
        {
            if (loginDto == null)
                throw new BadRequestException("invalid input");

            var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if(user == null || !(BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash)))
            {
                throw new BadRequestException("Invalid email or password");
            }

            var token = _jwtService.GenerateToken(user);
            return new AuthResponseDto
            {
                Id=user.Id,
                Email = user.Email,
                Name = user.Name,
                Token = token
            };

        }

    }
}
