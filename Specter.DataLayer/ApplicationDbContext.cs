using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Specter.DataLayer.Entities;

namespace Specter.DataLayer
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Template>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Name).IsRequired();
                entity.Property(t => t.Data).IsRequired();
                entity.Property(t => t.CreatedAt).IsRequired();
                entity.Property(t => t.Visibility).IsRequired();
                entity.Property(t => t.ForkId).HasDefaultValue(null);
                entity.Property(t => t.Deleted).HasDefaultValue(false);
                     
                entity.HasOne(t => t.CreatedByUser)
                      .WithMany(t => t.Templates)
                      .HasForeignKey(t => t.CreatedBy)
                      .IsRequired();

                entity.HasOne(t => t.ForkTemplate)
                      .WithMany(t => t.Forks)
                      .HasForeignKey(t => t.ForkId);

                entity.HasMany(t => t.Edits)
                      .WithOne(t => t.Template)
                      .HasForeignKey(t => t.TemplateId)
                      .IsRequired();
            });

            builder.Entity<TemplateHistory>(entity => 
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Name).IsRequired();
                entity.Property(t => t.Data).IsRequired();
                entity.Property(t => t.EditedAt).IsRequired();
                entity.Property(t => t.Reason);
                     
                entity.HasOne(t => t.Template)
                      .WithMany(t => t.Edits)
                      .HasForeignKey(t => t.TemplateId)
                      .IsRequired();
            });

            builder.Entity<Timesheet>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Name).IsRequired();
                entity.Property(t => t.Description);
                entity.Property(t => t.Date).IsRequired();
                entity.Property(t => t.Time).IsRequired();

                entity.HasOne(t => t.User)
                      .WithMany(t => t.Timesheets)
                      .HasForeignKey(t => t.UserId);
            });
        }
    }
}
