using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskMini.Common;
using TaskMini.DTO.Project;
using TaskMini.Interfaces;

namespace TaskMini.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetProjectsAsync()
        {
            var result= await _projectService.GetProjectsAsync();

            return Ok(new ApiResponse<List<GetProjectDto>>{
                Success=true,
                Data=result,
                Message="Data fetched successfully"
            });
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var result = await _projectService.GetProjectByIdAsync(id);

            return Ok(new ApiResponse<GetProjectDto>
            {
                Success = true,
                Data = result,
                Message = "Data fetched successfully"
            });
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateProjectAsync([FromBody] CreateProjectDto dto)
        {
            var result= await _projectService.CreateProjectAsync(dto);
         
            return CreatedAtAction(
                nameof(GetProjectById),
                new {id=result.Id},
                new ApiResponse<GetProjectDto>
                {
                    Success = true,
                    Data = result,
                    Message = "Project created successfully"
                });

        }
    }
}
