using Microsoft.EntityFrameworkCore;
using TaskMini.Data;
using TaskMini.DTO.Project;
using TaskMini.DTO.User;
using TaskMini.Entities;
using TaskMini.Exceptions;
using TaskMini.Interfaces;

namespace TaskMini.Services
{
    public class ProjectService:IProjectService
    {
        private readonly TaskMiniDbContext _dbContext;

        public ProjectService(TaskMiniDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetProjectDto> CreateProjectAsync(CreateProjectDto dto)
        {           

            if (dto == null)
                throw new BadRequestException("Invalid input");

            var workspaceExists = await _dbContext.Workspaces.AnyAsync(x => x.Id == dto.WorkspaceId);

            if(!workspaceExists)
            {
                throw new NotFoundException("Workspace Not found");
            }

            Project project = new()
            {
                Title = dto.Title,
                WorkspaceId = dto.WorkspaceId,
            };

            await _dbContext.Projects.AddAsync(project);
            var result = await _dbContext.SaveChangesAsync();

            if (result == 0)
            {
                throw new InternalServerException("Failed to create project");
                
            }

            return new GetProjectDto
            {
                Id = project.Id,
                Title = project.Title,
                WorkspaceId = project.WorkspaceId,
                TaskItems = new()
            };
        }

        public async Task<List<GetProjectDto>> GetProjectsAsync()
        {
            var projects = await _dbContext.Projects.AsNoTracking().Select(p => new GetProjectDto
            {
                Id = p.Id,
                Title = p.Title,
                WorkspaceId = p.WorkspaceId,
                TaskItems = p.TaskItems.Select(t => new TaskSummaryDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Status = t.Status
                }).ToList()
            }).ToListAsync();
            return projects;
        }

        public async Task<GetProjectDto> GetProjectByIdAsync(int Id)
        {
            var project = await _dbContext.Projects.AsNoTracking().Where(p=>p.Id==Id).Select(p => new GetProjectDto
            {
                Id = p.Id,
                Title = p.Title,
                WorkspaceId = p.WorkspaceId,
                TaskItems = p.TaskItems.Select(t => new TaskSummaryDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Status = t.Status
                }).ToList()
            }).FirstOrDefaultAsync() ?? throw new NotFoundException($"Project with project id : {Id} doesn't exist ");

            
           
            return project;
        }
    }
}
