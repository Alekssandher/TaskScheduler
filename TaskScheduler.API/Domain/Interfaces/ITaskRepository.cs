using TaskScheduler.API.Domain.Enums;
using TaskScheduler.API.Domain.Models;

namespace TaskScheduler.API.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task AddTask(MyTask myTask);
        Task<bool> DeleteTask(int taskId, int userId);
        Task<List<MyTask>> GetTasksByFilters(int id, TaskFilter taskFilter);
        Task<MyTask?> GetTaskById(int id);
        Task<List<MyTask>> GetAllTasks(int id);
        Task<bool> UpdateEntireTask(MyTask myTask);

    }
}