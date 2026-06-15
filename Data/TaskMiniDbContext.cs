using Microsoft.EntityFrameworkCore;
using TaskMini.Entities;

namespace TaskMini.Data
{
    public class TaskMiniDbContext : DbContext
    {
        public TaskMiniDbContext(DbContextOptions<TaskMiniDbContext> options)
           : base(options)
        {
        }


        public DbSet<Workspace> Workspaces { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }

        public DbSet<Role> Roles
        {
            get; set;

        }
    }
}
