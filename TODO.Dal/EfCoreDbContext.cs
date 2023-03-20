using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODO.BL.Authentication;
using TODO.Dal.Entities;

namespace TODO.Dal
{
    public class EfCoreDbContext : IdentityDbContext<UserAuth>
    {
        public EfCoreDbContext(DbContextOptions<EfCoreDbContext> options) : base(options)
        {

        }

        public DbSet<TodoEntity> ToDoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TodoEntity>(entity =>
            {
                entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(1);
            });

            base.OnModelCreating(builder);
        }
    }
}
