using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TaskManagement.Domain.Entities
{
    public class Notification : BaseEntity
    {
       
        public string UserId { get; set; }
        public int? TaskId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        public IdentityUser User { get; set; }
        public TaskModel Task { get; set; }
    }
}
