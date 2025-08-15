using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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
    [ProducesResponseType(typeof(UnauthorizedHttpResult), StatusCodes.Status401Unauthorized, "application/problem+json")]
    [ProducesResponseType(typeof(ModelViews.BadRequest), StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType(typeof(InternalError), StatusCodes.Status500InternalServerError, "application/problem+json")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        [EndpointName("Get Tasks")]
        [EndpointSummary("GetTasks")]
        [ProducesResponseType(typeof(OkResponse<IReadOnlyList<MyTaskResponse>>), StatusCodes.Status200OK, "application/json")]
        [EndpointDescription("Retrieves a list of user tasks based on the provided query parameters.")]
        public async Task<IActionResult> GetAllTasks([FromQuery] TaskFilter taskFilter)
        {
            var res = await _taskService.GetAllTasks(taskFilter);

            return Ok(new OkResponse<List<MyTaskResponse>>("Task retrieved", "Task fetched successfully", res));
        }

        [HttpPost]
        [Consumes("application/json")]
        [EndpointName("Create Task")]
        [EndpointSummary("CreateTask")]
        [EndpointDescription("Create a task based on the provided scheme.")]
        [ProducesResponseType(typeof(Created), StatusCodes.Status201Created, "application/json")]
        public async Task<IActionResult> CreateTask([FromBody] MyTaskRequestDto dto)
        {
            await _taskService.CreateTask(dto);

            return Created();
        }

        [HttpPut]
        [Consumes("application/json")]
        [EndpointName("Update Task")]
        [EndpointSummary("UpdateTask")]
        [ProducesResponseType(typeof(NoContent), StatusCodes.Status204NoContent, "application/json")]
        [EndpointDescription("Update a task from the provided scheme.")]
        public async Task<IActionResult> UpdateTask([FromBody] MyTaskUpdateDto dto)
        {
            await _taskService.UpdateTask(dto);

            return NoContent();
        }

        [HttpDelete("{taskId}")]
        [EndpointName("Delete Task")]
        [EndpointSummary("DeleteTask")]
        [ProducesResponseType(typeof(NoContent), StatusCodes.Status204NoContent, "application/json")]
        [EndpointDescription("Delete a task by its id.")]
        public async Task<IActionResult> DeleteTask([FromRoute] int taskId)
        {
            await _taskService.DeleteTask(taskId);

            return NoContent();
        }
    }
}