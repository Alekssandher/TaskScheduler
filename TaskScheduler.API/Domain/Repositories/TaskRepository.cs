using Microsoft.EntityFrameworkCore;
using TaskScheduler.API.Domain.Enums;
using TaskScheduler.API.Domain.Interfaces;
using TaskScheduler.API.Domain.Models;
using TaskScheduler.API.Infrastructure.Db;

namespace TaskScheduler.API.Domain.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly MyDbContext _context;

        public TaskRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<MyTask>> GetAllTasks(int id)
        {
            return await _context.Tasks
            .Where(u => u.UserId == id)
            .ToListAsync();
                
        }

        public async Task<List<MyTask>> GetTasksByFilters(int id, TaskFilter taskFilter)
        {
            var query = _context.Tasks.AsQueryable();

            query = query.Where(u => u.UserId == id);
            if (taskFilter.Status.HasValue)
                query = query.Where(t => t.Status == taskFilter.Status.Value);

            if (taskFilter.FromDate.HasValue)
                query = query.Where(t => t.ScheduledAt >= taskFilter.FromDate.Value);

            if (taskFilter.ToDate.HasValue)
                query = query.Where(t => t.ScheduledAt <= taskFilter.ToDate.Value);

            if (!string.IsNullOrEmpty(taskFilter.TitleContains))
                query = query.Where(t => t.Title.Contains(taskFilter.TitleContains));

            query = query.OrderBy(t => t.ScheduledAt);

            query = query
                .Skip((taskFilter.Page - 1) * taskFilter.PageSize)
                .Take(taskFilter.PageSize);

            return await query.ToListAsync();
        }
        
        public async Task<MyTask?> GetTaskById(int id)
        {
            return await _context.Tasks
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task UpdateEntireTask(MyTask myTask)
        {
            var affected = await _context.Tasks
            .Where(t => t.Id == myTask.Id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(t => t.Title, myTask.Title)
                .SetProperty(t => t.Description, myTask.Description)
                .SetProperty(t => t.Status, myTask.Status)
                .SetProperty(t => t.FinishDate, myTask.FinishDate)
                .SetProperty(t => t.UpdatedAt, DateTime.UtcNow)
            );

            if (affected == 0)
                throw new Exception("Task not found");

            await _context.SaveChangesAsync();
        }

        public async Task UpdateStatus(int taskId, MyTaskStatus status)
        {
            var affected = await _context.Tasks
            .Where(t => t.Id == taskId)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(t => t.Status, status)
                .SetProperty(t => t.UpdatedAt, DateTime.UtcNow)
            );

            if (affected == 0)
                throw new Exception("Task not found");
        }

        public async Task UpdateTitle(int taskId, string title)
        {
            var affected = await _context.Tasks
            .Where(t => t.Id == taskId)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(t => t.Title, title)
                .SetProperty(t => t.UpdatedAt, DateTime.UtcNow)
            );

            if (affected == 0)
                throw new Exception("Task not found");
        }

        public async Task UpdateDescription(int taskId, string description)
        {
            var affected = await _context.Tasks
            .Where(t => t.Id == taskId)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(t => t.Description, description)
                .SetProperty(t => t.UpdatedAt, DateTime.UtcNow)
            );

            if (affected == 0)
                throw new Exception("Task not found");
        }

        public async Task AddTask(MyTask myTask)
        {
            await _context.Tasks.AddAsync(myTask);
            await _context.SaveChangesAsync();
        }
    }
}