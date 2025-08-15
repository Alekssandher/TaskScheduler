using TaskScheduler.API.Domain.Enums;
using TaskScheduler.API.Domain.Models;

namespace TaskScheduler.API.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task AddTask(MyTask myTask);
        Task<List<MyTask>> GetTasksByFilters(int id, TaskFilter taskFilter);
        Task<MyTask?> GetTaskById(int id);
        Task<List<MyTask>> GetAllTasks(int id);
        Task UpdateEntireTask(MyTask myTask);
        Task UpdateStatus(int taskId, MyTaskStatus status);
        Task UpdateTitle(int taskId, string title);
        Task UpdateDescription(int taskId, string description);
    }
}