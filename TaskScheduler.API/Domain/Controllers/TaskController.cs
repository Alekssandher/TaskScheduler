using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskScheduler.API.Domain.DTOs;
using TaskScheduler.API.Domain.Interfaces;
using TaskScheduler.API.Domain.Models;
using TaskScheduler.API.ModelViews;

namespace TaskScheduler.API.Domain.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType(typeof(InternalError), StatusCodes.Status500InternalServerError, "application/problem+json")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        private readonly NoContentResponse noContentResponse = new();
        private readonly CreatedResponse createdResponse = new();

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        [Consumes("application/json")]
        [EndpointName("Get Tasks")]
        [EndpointSummary("GetTasks")]
        [ProducesResponseType(typeof(OkResponse<IReadOnlyList<MyTaskResponse>>), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> GetAllTasks([FromQuery] TaskFilter taskFilter)
        {
            var res = await _taskService.GetAllTasks(taskFilter);

            return Ok(new OkResponse<List<MyTaskResponse>>("Task retrieved", "Task fetched successfully", res));
        }

        [HttpPost]
        [Consumes("application/json")]
        [EndpointName("Create Task")]
        [EndpointSummary("CreateTask")]
        [ProducesResponseType(typeof(CreatedResponse), StatusCodes.Status201Created, "application/json")]
        public async Task<IActionResult> CreateTask([FromBody] MyTaskRequestDto dto)
        {
            await _taskService.CreateTask(dto);

            return StatusCode(201, createdResponse);
        }

        [HttpPut]
        [Consumes("application/json")]
        [EndpointName("Update Task")]
        [EndpointSummary("UpdateTask")]
        [ProducesResponseType(typeof(NoContentResponse), StatusCodes.Status204NoContent, "application/json")]
        public async Task<IActionResult> UpdateTask([FromBody] MyTaskUpdateDto dto)
        {
            await _taskService.UpdateTask(dto);

            return StatusCode(204, noContentResponse);
        }
    }
}