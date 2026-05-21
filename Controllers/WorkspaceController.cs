using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskMini.Common;
using TaskMini.DTO.Workspace;
using TaskMini.Interfaces;

namespace TaskMini.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkspaceController : ControllerBase
    {
        private readonly IWorkspaceService _workspaceService;

        public WorkspaceController(IWorkspaceService workspaceService)
        {
            _workspaceService = workspaceService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetWorkspaces()
        {
            var result = await _workspaceService.GetAllWorkspacesAsync();

            return Ok(new ApiResponse<List<GetWorkspaceDto>>
            {
                Success = true,
                Data = result,
                Message="Data Fetched Successfully"
            });
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkspaceById(int id)
        {
            var result = await _workspaceService.GetWorkspaceByIdAsync(id);

            return Ok(new ApiResponse<GetWorkspaceDto>
            {
                Success = true,
                Data = result,
                Message = "Data Fetched Successfully"
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateWorkspace([FromBody] CreateWorkspaceDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _workspaceService.CreateWorkspaceAsync(dto);

              
            return CreatedAtAction(
                nameof(GetWorkspaceById),
                new { id = result.Id },
                new ApiResponse<GetWorkspaceDto>
                {
                    Success = true,
                    Data = result,
                    Message = "Data Fetched Successfully"
                });
        }
    }
}