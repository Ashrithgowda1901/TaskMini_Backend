using Microsoft.EntityFrameworkCore;
using TaskMini.Data;
using TaskMini.DTO.User;
using TaskMini.Entities;
using TaskMini.Exceptions;

namespace TaskMini.Services
{
    public class UserService : IUserService
    {
        private readonly TaskMiniDbContext _dbContext;
        public UserService(TaskMiniDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<GetUserDto> CreateUserAsync(CreateUserDto userDto)
        {


            if (userDto == null)
            {
                throw new BadRequestException("Invalid Input");
            }

            var emailExists = await _dbContext.Users.AnyAsync(user => user.Email == userDto.Email);

            if (emailExists)
            {
                throw new BadRequestException("Email already exists");
            }

            User user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
            };


            await _dbContext.Users.AddAsync(user);

            var result = await _dbContext.SaveChangesAsync();

            if (result == 0)
            {
                throw new InternalServerException("Not able to create user");
            }
            return new GetUserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
            };


        }

        public async Task<List<GetUserDto>> GetUsersAsync()
        {
            var result = await _dbContext.Users.AsNoTracking()
              .Select(m => new GetUserDto
              {
                  Id = m.Id,
                  Name = m.Name,
                  Email = m.Email,
                  TaskSummarys = m.TaskItems.Select(taskItem => new TaskSummaryDto
                  {
                      Id = taskItem.Id,
                      Title = taskItem.Title,
                      Status = taskItem.Status
                  }).ToList()
              }).ToListAsync();

            return result;


        }

        public async Task<GetUserDto> GetUserByIdAsync(int id)
        {

            var result = await _dbContext.Users.AsNoTracking().Where(u => u.Id == id).Select(user => new GetUserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                TaskSummarys = user.TaskItems.Select(taskItem => new TaskSummaryDto
                {
                    Id = taskItem.Id,
                    Title = taskItem.Title,
                    Status = taskItem.Status
                }).ToList()
            }).FirstOrDefaultAsync();

            return result ?? throw new NotFoundException($"User with user Id : {id} doesn't exist ");


        }
    }
}
