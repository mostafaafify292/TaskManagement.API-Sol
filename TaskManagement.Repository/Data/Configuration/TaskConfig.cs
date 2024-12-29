using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Repository.Data.Configuration
{
    public class TaskConfig : IEntityTypeConfiguration<TaskModel>
    {
        public void Configure(EntityTypeBuilder<TaskModel> builder)
        {
            builder.HasOne(p => p.User)
                   .WithMany()
                   .HasForeignKey(f => f.UserId);
                   

            builder.HasOne(t => t.Category)
                   .WithMany(c => c.Tasks)
                   .HasForeignKey(t => t.CategoryId);

        }
    }
}
