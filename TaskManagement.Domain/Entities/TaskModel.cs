using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TaskManagement.Domain.Entities
{
    public class TaskModel : BaseEntity
    {
        
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; } = DateTime.Now;
        public string Status { get; set; } // e.g., "Pending", "In Progress", "Completed"

        // Foreign key for the Category
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        // Foreign key for the assigned user
        public string? UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
