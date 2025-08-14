using TaskScheduler.API.Domain.DTOs;

namespace TaskScheduler.API.Domain.Interfaces
{
    public interface IAccountService
    {
        public Task Register(RegisterDto registerDto);
        public Task<string> Login(LoginDto loginDto);
    }
}