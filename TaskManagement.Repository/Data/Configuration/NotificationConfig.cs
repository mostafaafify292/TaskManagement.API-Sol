using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Repository.Data.Configuration
{
    public class NotificationConfig : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasOne(n => n.User)
                   .WithMany()
                   .HasForeignKey(n => n.UserId);
            builder.HasOne(n => n.Task)
                   .WithMany()
                   .HasForeignKey(n => n.TaskId);
        }
    }
}
