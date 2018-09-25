using Microsoft.EntityFrameworkCore;
using prjmng.Models;

namespace prjmng
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Assignee> Assignees { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
    }
}