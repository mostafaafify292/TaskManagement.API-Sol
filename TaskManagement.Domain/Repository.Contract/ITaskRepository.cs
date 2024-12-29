using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Repository.Contract
{
    public interface ITaskRepository : IGenericRepository<TaskModel>
    {
        public Task<IReadOnlyList<TaskModel>> GetTasksByStatusAsync(string status);
    }
}
