using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Entities
{
    public class Category : BaseEntity
    {
        
        public string Name { get; set; }
        public ICollection<TaskModel> Tasks { get; set; } = new HashSet<TaskModel>();
    }
}
