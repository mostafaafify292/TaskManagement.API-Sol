using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Repository.Data
{
    public class TaskManagementDbContext : IdentityDbContext
    {
        public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options) : base(options) 
        {
            
        }
        public DbSet<TaskModel> Tasks { get; set; } 
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
