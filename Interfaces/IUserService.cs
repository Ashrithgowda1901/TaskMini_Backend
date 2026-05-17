using TaskMini.DTO.User;

public interface IUserService
{
    Task<GetUserDto> CreateUserAsync(CreateUserDto userDto);

    Task<List<GetUserDto>> GetUsersAsync();

    Task<GetUserDto> GetUserByIdAsync(int id);
}