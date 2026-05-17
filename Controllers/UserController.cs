using Microsoft.AspNetCore.Mvc;
using TaskMini.Common;
using TaskMini.DTO.User;
using TaskMini.Interfaces;

namespace TaskMini.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userService.GetUsersAsync();

            return Ok(new ApiResponse<List<GetUserDto>>
            {
                Success = true,
                Data = result,
                Message="Data Fetched Successfully"
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _userService.GetUserByIdAsync(id);


            return Ok(new ApiResponse<GetUserDto>
            {
                Success = true,
                Data = result,
                Message = "Data Fetched Successfully"
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.CreateUserAsync(dto);

            
            
            return CreatedAtAction(
                nameof(GetUserById),
                new { id = result.Id },
                new ApiResponse<GetUserDto>
                {
                    Success = true,
                    Data = result,
                    Message = "Data Fetched Successfully"
                });
        }
    }
}