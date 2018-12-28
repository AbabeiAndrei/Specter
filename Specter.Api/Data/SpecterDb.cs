using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Specter.Api.Data.Entities;

namespace Specter.Api.Data
{
    public class SpecterDb : DbContext, IApplicationContext
    {
        public virtual DbSet<ApplicationUser> Users { get; set; }

        public virtual DbSet<Template> Templates { get; set; }

        public virtual DbSet<TemplateHistory> TemplatesHistory { get; set; }

        public virtual DbSet<Timesheet> Timesheets { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Delivery> Deliveries { get; set; }

        public virtual DbSet<Project> Projects { get; set; }

        public virtual DbSet<UserProject> UserProjects { get; set; }

        public virtual DbSet<IdentityUserRole<string>> IdentityUserRoles { get; set; }

        public virtual DbSet<IdentityUserClaim<string>> IdentityUserClaims { get; set; }

        public virtual DbSet<IdentityRole> IdentityRoles{ get; set; }

        public SpecterDb()
        {

        }
        
        public SpecterDb(DbContextOptions<SpecterDb> options)
            :base(options)
        {

        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=D:\\specter.db");
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
                entity.Property(t => t.CreatedBy).IsRequired();
                /* 
                entity.HasOne(t => t.CreatedByUser)
                      .WithMany(t => t.Templates)
                      .HasForeignKey(t => t.CreatedBy)
                      .IsRequired();
                */
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
                entity.Property(t => t.UserId).IsRequired();
                entity.Property(t => t.CategoryId).IsRequired();
                entity.Property(t => t.DeliveryId).IsRequired();
                entity.Property(t => t.Removed).HasDefaultValue(false);
                /*      
                entity.HasOne(t => t.User)
                      .WithMany(t => t.Timesheets)
                      .HasForeignKey(t => t.UserId);
                */
            });

            builder.Entity<ApplicationUser>(entity => 
            {
                entity.Property(p => p.FirstName);
                entity.Property(p => p.LastName);
            });

            builder.Entity<IdentityRole>().HasKey(p => p.Id);

            builder.Entity<IdentityUserRole<string>>().HasKey(p => new { p.UserId, p.RoleId });
            builder.Entity<IdentityUserClaim<string>>().HasKey(p => p.Id);

            builder.Entity<Category>(p => 
            {
                p.HasKey(c => c.Id);
                p.Property(c => c.Name);
                p.Property(c => c.Description);
                p.Property(c => c.Removed);
            });

            builder.Entity<Project>(p => 
            {
                p.HasKey(d => d.Id);
                p.Property(d => d.Name);
                p.Property(d => d.Description);
                p.Property(d => d.Removed);
            });

            builder.Entity<Delivery>(p =>
            {
                p.HasKey(d => d.Id);
                p.Property(d => d.Name);
                p.Property(d => d.Description);
                p.Property(d => d.Order);
                p.Property(d => d.ProjectId);
                p.Property(d => d.Removed);
            });

            builder.Entity<UserProject>(p => 
            {
                p.HasKey(up => new { up.UserId, up.ProjectId, up.RoleId });
                p.Property(up => up.Removed);
            });
        }

        void IDbContext.SaveChanges()
        {
            SaveChanges();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }
    }
}