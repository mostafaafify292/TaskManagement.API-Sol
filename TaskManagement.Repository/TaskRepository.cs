using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repository.Contract;
using TaskManagement.Repository.Data;

namespace TaskManagement.Repository
{
    public class TaskRepository : GenericRepository<TaskModel>, ITaskRepository 
    {
        private readonly TaskManagementDbContext _context;

        public TaskRepository(TaskManagementDbContext context) : base(context) 
        {
            _context = context;
        }
        public async Task<IReadOnlyList<TaskModel>> GetTasksByStatusAsync(string status)
        {
            return await _context.Tasks.Where(t => t.Status == status).ToListAsync();
        }
    }
}
