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

    }
}