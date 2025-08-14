using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using TaskScheduler.API.Domain.DTOs;
using TaskScheduler.API.Domain.Interfaces;
using TaskScheduler.API.Domain.Models;
using TaskScheduler.API.Domain.Repositories;

namespace TaskScheduler.API.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordRepository _passwordRepository;
        public AccountService(IUserRepository userRepository, IPasswordRepository passwordRepository)
        {
            _userRepository = userRepository;
            _passwordRepository = passwordRepository;
        }

        public async Task<string> Login(LoginDto loginDto)
        {
            var res = await _userRepository.GetByEmail(loginDto.Email) ?? throw new Exception("NotFound");

            
            if (_passwordRepository.VerifyPassword(loginDto.Password, res.PasswordHash))
            {
                return "token";
            }

            throw new Exception("NotFound");
        }

        public async Task Register(RegisterDto registerDto)
        {
            var res = await _userRepository.GetByEmail(registerDto.Email);
            if (res != null)
            {
                throw new Exception();
            }

            var user = new User
            {
                Email = registerDto.Email,
                Username = registerDto.Email,
                PasswordHash = _passwordRepository.HashPassword(registerDto.Password)
            };
            await _userRepository.Add(user);

            return;
        }
    }
}