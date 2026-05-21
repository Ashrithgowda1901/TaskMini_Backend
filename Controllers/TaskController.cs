using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskMini.Common;
using TaskMini.DTO.Task;
using TaskMini.Interfaces;

namespace TaskMini.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetTasks([FromQuery] RequestTaskDto request)
        {
            var result=await _taskService.GetTasksAsync(request);

            return Ok(new ApiResponse<List<GetTaskDto>>
            {
                Success = true,
                Data = result,
                Message="Data fetched successfully",
            });
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var result =await _taskService.GetTaskByIdAsync(id);

            
            return Ok(new ApiResponse<GetTaskDto>
            {
                Success = true,
                Data = result,
                Message = "Data fetched successfully",
            });

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateTaskDto dto)
        {
            var result=await _taskService.CreateTaskAsync(dto);

            if(result==null)
            {
                return BadRequest();
            }

            return CreatedAtAction(
                nameof(GetTaskById),
                new { id = result.Id },
                new ApiResponse<GetTaskDto>
                {
                    Success = true,
                    Data = result,
                    Message = "Data fetched successfully",
                });
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateTaskStatus(
            UpdateTaskStatusDto dto)
        {
            var result = await _taskService
                .UpdateTaskStatusAsync(dto);
            
            return Ok(new ApiResponse<GetTaskDto> { Success = true, Data = result, Message = "Data fetched successfully" });
        }

    }
}
