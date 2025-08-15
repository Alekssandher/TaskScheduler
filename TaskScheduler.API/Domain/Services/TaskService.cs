using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TaskScheduler.API.Domain.DTOs;
using TaskScheduler.API.Domain.Interfaces;
using TaskScheduler.API.Domain.Mappers;
using TaskScheduler.API.Domain.Repositories;

namespace TaskScheduler.API.Domain.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtRepository _jwtRepository;
        public TaskService(ITaskRepository taskRepository, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, IJwtRepository jwtRepository)
        {
            _taskRepository = taskRepository;
            _httpContextAccessor = httpContextAccessor;
            _jwtRepository = jwtRepository;
            _userRepository = userRepository;
        }

        public string? GetToken()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null) return null;

            var authHeader = httpContext.Request.Headers.Authorization.FirstOrDefault();

            if (string.IsNullOrEmpty(authHeader)) return null;

            if (authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                return authHeader["Bearer ".Length..].Trim();
            }

            return null;
        }

        public async Task<List<MyTaskResponse>> GetAllTasks()
        {
            var token = GetToken() ?? throw new Exception("Token Missing?");

            var email = _jwtRepository.ExtractEmail(token);

            var user = await _userRepository.GetByEmail(email) ?? throw new Exception();

            var res = await _taskRepository.GetAllTasks(user.Id);

            return res.ToListResult();
        }

        public async Task CreateTask(MyTaskRequestDto dto)
        {
            var token = GetToken() ?? throw new Exception("Token Missing?");

            var email = _jwtRepository.ExtractEmail(token);

            var user = await _userRepository.GetByEmail(email) ?? throw new Exception();

            await _taskRepository.AddTask(dto.ToTaskModel(user.Id));
        }
    }
}