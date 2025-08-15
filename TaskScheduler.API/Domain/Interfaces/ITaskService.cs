using TaskScheduler.API.Domain.DTOs;
using TaskScheduler.API.Domain.Models;

namespace TaskScheduler.API.Domain.Interfaces
{
    public interface ITaskService
    {
        Task<List<MyTaskResponse>> GetAllTasks(TaskFilter taskFilter);
        Task CreateTask(MyTaskRequestDto dto);
        Task UpdateTask(MyTaskUpdateDto dto);
        Task DeleteTask(int taskId);
    }
}