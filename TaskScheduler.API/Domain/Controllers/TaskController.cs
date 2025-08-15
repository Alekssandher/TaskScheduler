using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskScheduler.API.Domain.DTOs;
using TaskScheduler.API.Domain.Interfaces;
using TaskScheduler.API.ModelViews;

namespace TaskScheduler.API.Domain.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        [Consumes("application/json")]
        [EndpointName("Get Tasks")]
        [EndpointSummary("GetTasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            var res = await _taskService.GetAllTasks();

            return Ok(new OkResponse<List<MyTaskResponse>>("Task retrieved", "Task fetched successfully", res));
        }

        [HttpPost]
        [Consumes("application/json")]
        [EndpointName("Create Task")]
        [EndpointSummary("CreateTask")]
        public async Task<IActionResult> CreateTask([FromBody] MyTaskRequestDto dto)
        {
            await _taskService.CreateTask(dto);

            return Created();
        }

        [HttpPut]
        [Consumes("application/json")]
        [EndpointName("Update Task")]
        [EndpointSummary("UpdateTask")]
        public async Task<IActionResult> UpdateTask([FromBody] MyTaskUpdateDto dto)
        {
            await _taskService.UpdateTask(dto);

            return NoContent();
        }
    }
}