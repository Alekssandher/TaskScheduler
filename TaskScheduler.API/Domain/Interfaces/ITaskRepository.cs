using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskScheduler.API.Domain.DTOs;
using TaskScheduler.API.Domain.Enums;
using TaskScheduler.API.Domain.Models;

namespace TaskScheduler.API.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task AddTask(MyTask myTask);
        Task<List<MyTask>> GetTasksByFilters(TaskFilter taskFilter);
        Task<MyTask?> GetTaskById(int id);
        Task<List<MyTask>> GetAllTasks();
        Task UpdateEntireTask(MyTask myTask);
        Task UpdateStatus(int taskId, MyTaskStatus status);
        Task UpdateTitle(int taskId, string title);
        Task UpdateDescription(int taskId, string description);
    }
}