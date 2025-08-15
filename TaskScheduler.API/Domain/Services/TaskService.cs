using TaskScheduler.API.Domain.DTOs;
using TaskScheduler.API.Domain.Interfaces;
using TaskScheduler.API.Domain.Mappers;

namespace TaskScheduler.API.Domain.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<List<MyTaskResponse>> GetAllTasks()
        {
            var res = await _taskRepository.GetAllTasks();

            return res.ToListResult();
        }
    }
}