using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using TaskManagement.Domain.Entities;

namespace TaskManagement.API.Dtos
{
    public class TaskModelDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime DueDate { get; set; } = DateTime.Now;
        [Required]
        public string Status { get; set; } // e.g., "Pending", "In Progress", "Completed"
        
        public int CategoryId { get; set; }
        //public string? CategoryName { get; set; }

        // Foreign key for the assigned user
        public string? AspNetUsersId { get; set; }
       // public string? AssignedToUserEmail { get; set; }
    }
}
