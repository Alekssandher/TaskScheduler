using TaskScheduler.API.Domain.DTOs;

namespace TaskScheduler.API.Domain.Interfaces
{
    public interface ITaskService
    {
        Task<List<MyTaskResponse>> GetAllTasks();
    }
}