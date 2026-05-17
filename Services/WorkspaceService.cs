using Microsoft.EntityFrameworkCore;
using TaskMini.Data;
using TaskMini.DTO.User;
using TaskMini.DTO.Workspace;
using TaskMini.Entities;
using TaskMini.Exceptions;
using TaskMini.Interfaces;

namespace TaskMini.Services
{
    public class WorkspaceService:IWorkspaceService
    {
        private readonly TaskMiniDbContext _dbContext;

        public WorkspaceService(TaskMiniDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetWorkspaceDto> CreateWorkspaceAsync(CreateWorkspaceDto workspace)
        {
            if (workspace == null)
                throw new BadRequestException("Invalid Input");

            Workspace workSpace = new Workspace
            {
                Name = workspace.Name,

            };
            
            await _dbContext.Workspaces.AddAsync(workSpace);
            
            var result=await _dbContext.SaveChangesAsync();

            if(result==0)
            {
                throw new InternalServerException("Not able to create Workspace");
            }
            return new GetWorkspaceDto
            {     
                Id= workSpace.Id,
                Name = workspace.Name,
                Projects = []
            };

        }

        public async Task<List<GetWorkspaceDto>> GetAllWorkspacesAsync()
        {
            return await _dbContext.Workspaces
                .AsNoTracking()
                .Select(workSpace => new GetWorkspaceDto
                {
                    Id = workSpace.Id,
                    Name = workSpace.Name,

                    Projects = workSpace.Projects
                        .Select(p => new ProjectSummary
                        {
                            Id = p.Id,
                            Title = p.Title,

                            TaskItems = p.TaskItems
                                .Select(t => new TaskSummaryDto
                                {
                                    Id = t.Id,
                                    Title = t.Title,
                                    Status = t.Status,
                                })
                                .ToList()
                        })
                        .ToList()
                })
                .ToListAsync();
        }

        public async Task<GetWorkspaceDto> GetWorkspaceByIdAsync(int id)
        {
            return await _dbContext.Workspaces.AsNoTracking().Where(workSpace => workSpace.Id == id).Select(workSpace => new GetWorkspaceDto
            {
                Id = workSpace.Id,
                Name = workSpace.Name,
                Projects = workSpace.Projects.Select(p => new ProjectSummary
                {
                    Id = p.Id,
                    Title = p.Title,
                    TaskItems = p.TaskItems.Select(t => new TaskSummaryDto
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Status = t.Status,
                    }).ToList()
                }).ToList()
            }).FirstOrDefaultAsync() ?? throw new NotFoundException("workspace doesn't exist");
        }
    }
}
