using TaskScheduler.API.Domain.DTOs;
using TaskScheduler.API.Domain.Models;

namespace TaskScheduler.API.Domain.Mappers
{
    public static class MyTaskMapper
    {
        public static List<MyTaskResponse> ToListResult(this List<MyTask> myTasks)
        {
            if (myTasks == null || myTasks.Count == 0)
                return [];
                
            return [.. myTasks.Select(task => new MyTaskResponse
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                ScheduledAt = task.ScheduledAt,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt
            })];
        }

        public static MyTask ToTaskModel(this MyTaskRequestDto dto, int userId)
        {
            return new MyTask
            {
                Title = dto.Title,
                Description = dto.Description,
                FinishDate = dto.FinishDate,
                Status = dto.Status,
                UserId = userId
            };
        }
        public static MyTaskResponse ToResult(this MyTask myTask)
        {
            return new MyTaskResponse
            {
                Id = myTask.Id,
                Title = myTask.Title,
                Description = myTask.Description,
                Status = myTask.Status,
                ScheduledAt = myTask.ScheduledAt,
                CreatedAt = myTask.CreatedAt,
                UpdatedAt = myTask.UpdatedAt
            };
        }
    }
}