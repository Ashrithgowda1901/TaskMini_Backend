using Microsoft.EntityFrameworkCore;
using TaskMini.Common.Enum;
using TaskMini.Data;
using TaskMini.DTO.Task;
using TaskMini.Entities;
using TaskMini.Exceptions;
using TaskMini.Interfaces;

namespace TaskMini.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskMiniDbContext _dbContext;

        public TaskService(TaskMiniDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetTaskDto> CreateTaskAsync(CreateTaskDto task)
        {


            //if (task == null || !userExists || !projectExists)
            if (task == null)
            {
                throw new BadRequestException("Invalid input");
            }
            var userExists = await _dbContext.Users.AnyAsync(x => x.Id == task.UserId);

            if (!userExists)
            {
                throw new NotFoundException($"User with userId {task.UserId} doesn't exist ");
            }

            var projectExists = await _dbContext.Projects.AnyAsync(x => x.Id == task.ProjectId);

            if (!projectExists)
            {
                throw new NotFoundException($"Project with ProjectId {task.ProjectId} doesn't exist ");
            }


            var newTask = new TaskItem
            {
                Title = task.Title,
                ProjectId = task.ProjectId,
                UserId = task.UserId,
                Status = TaskItemStatus.Defined
            };
            await _dbContext.Tasks.AddAsync(newTask);
            var result = await _dbContext.SaveChangesAsync();

            if (result == 0)
            {
                throw new InternalServerException("Not able to Create Task");
            }

            return new GetTaskDto
            {
                Id = newTask.Id,
                Title = newTask.Title,
                ProjectId = newTask.ProjectId,
                UserId = newTask.UserId,
                Status = newTask.Status
            };



        }

        public async Task<List<GetTaskDto>> GetTasksAsync(RequestTaskDto request)
        {
            var query = _dbContext.Tasks.AsNoTracking();

            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(task => task.Title.Contains(request.Search));
            }

            if (request.Status.HasValue)
            {
                query = query.Where(task => task.Status == request.Status.Value);
            }

            return await query
        .Select(task => new GetTaskDto
        {
            Id = task.Id,
            Title = task.Title,
            ProjectId = task.ProjectId,
            UserId = task.UserId,
            Status = task.Status
        })
        .Skip((request.PageNumber - 1)
            * request.PageSize)
        .Take(request.PageSize)
        .ToListAsync();
           
        }

        public async Task<GetTaskDto> GetTaskByIdAsync(int id)
        {
            return await _dbContext.Tasks.AsNoTracking().Where(t => t.Id == id).Select(task => new GetTaskDto
            {
                Id = task.Id,
                Title = task.Title,
                ProjectId = task.ProjectId,
                UserId = task.UserId,
                Status = task.Status
            }).FirstOrDefaultAsync() ?? throw new NotFoundException($"Task with task id : {id} doesn't exist");
        }
        public async Task<GetTaskDto> UpdateTaskStatusAsync(UpdateTaskStatusDto task)
        {
            if (task == null)
            {
                throw new BadRequestException("Invalid Input");
            }

            var existingTask = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == task.Id);

            if (existingTask == null)
            {
                throw new NotFoundException($"Task with taskID {task.Id} doesn't exist");
            }

            existingTask.Status = task.Status;

            var result = await _dbContext.SaveChangesAsync();

            if (result == 0)
            {
                throw new InternalServerException("Not able to update Task");
            }

            return new GetTaskDto
            {
                Id = existingTask.Id,
                Title = existingTask.Title,
                ProjectId = existingTask.ProjectId,
                UserId = existingTask.UserId,
                Status = existingTask.Status
            };
        }
    }
}
