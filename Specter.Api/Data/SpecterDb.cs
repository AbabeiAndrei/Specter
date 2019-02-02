using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Specter.Api.Data.Entities;

namespace Specter.Api.Data
{
    public class SpecterDb : DbContext, IApplicationContext
    {
        private static readonly JsonSerializerSettings _conversionSerializeSettings;
        private static readonly ValueConverter<ApplicatioUserPreferences, string> _applicatioUserPreferenceConverter;

        public virtual DbSet<ApplicationUser> Users { get; set; }

        public virtual DbSet<Template> Templates { get; set; }

        public virtual DbSet<TemplateHistory> TemplatesHistory { get; set; }

        public virtual DbSet<Timesheet> Timesheets { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Delivery> Deliveries { get; set; }

        public virtual DbSet<Project> Projects { get; set; }

        public virtual DbSet<UserProject> UserProjects { get; set; }

        public virtual DbSet<IdentityUserRole<Guid>> IdentityUserRoles { get; set; }

        public virtual DbSet<IdentityUserClaim<Guid>> IdentityUserClaims { get; set; }

        public virtual DbSet<IdentityRole<Guid>> IdentityRoles{ get; set; }

        public SpecterDb()
        {

        }
        
        public SpecterDb(DbContextOptions<SpecterDb> options)
            :base(options)
        {

        }
        
        static SpecterDb()
        {
            _conversionSerializeSettings = new JsonSerializerSettings 
            { 
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            _applicatioUserPreferenceConverter = new ValueConverter<ApplicatioUserPreferences, string>
            (
                p => JsonConvert.SerializeObject(p, _conversionSerializeSettings),
                p => JsonConvert.DeserializeObject<ApplicatioUserPreferences>(p, _conversionSerializeSettings)
            );
        }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=specter;Integrated Security=True;");
        // }

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
                entity.Property(t => t.InternalId).IsRequired();
                entity.Property(t => t.Name).IsRequired();
                entity.Property(t => t.Description);
                entity.Property(t => t.Created).HasDefaultValueSql("GETDATE()");
                entity.Property(t => t.Date).IsRequired();
                entity.Property(t => t.Time).IsRequired();
                entity.HasIndex(t => t.InternalId);
                      
                entity.HasOne(t => t.User)
                      .WithMany(t => t.Timesheets)
                      .HasForeignKey(t => t.UserId)
                      .IsRequired();

                entity.HasOne(t => t.Delivery)
                      .WithMany(t => t.Timesheets)
                      .HasForeignKey(t => t.DeliveryId);

                entity.HasOne(t => t.Project)
                      .WithMany(t => t.Timesheets)
                      .HasForeignKey(t => t.ProjectId)
                      .IsRequired();

                entity.HasOne(t => t.Category)
                      .WithMany(t => t.Timesheets)
                      .HasForeignKey(t => t.CategoryId)
                      .IsRequired();

                entity.Property(t => t.Locked).HasDefaultValue(false);
                entity.Property(t => t.Removed).HasDefaultValue(false);                
            });

            builder.Entity<ApplicationUser>(entity => 
            {
                entity.Property(p => p.FirstName);
                entity.Property(p => p.LastName);

                entity.HasMany(p => p.Projects)
                      .WithOne(p => p.User)
                      .HasForeignKey(p => p.UserId);

                entity.HasMany(p => p.Timesheets)
                      .WithOne(p => p.User)
                      .HasForeignKey(p => p.UserId);
                    
                entity.HasMany(p => p.Templates)
                      .WithOne(p => p.CreatedByUser)
                      .HasForeignKey(p => p.CreatedBy);

                entity.Property(p => p.Preferences)
                      .HasConversion(_applicatioUserPreferenceConverter)
                      .HasDefaultValue(ApplicatioUserPreferences.Default);
            });

            builder.Entity<IdentityRole<Guid>>().HasKey(p => p.Id);

            builder.Entity<IdentityUserRole<Guid>>().HasKey(p => new { p.UserId, p.RoleId });
            builder.Entity<IdentityUserClaim<Guid>>().HasKey(p => p.Id);

            builder.Entity<Category>(p => 
            {
                p.HasKey(c => c.Id);
                p.Property(c => c.Name).IsRequired();
                p.Property(c => c.Description);
                p.Property(c => c.Removed).IsRequired().HasDefaultValue(false);
            });

            builder.Entity<Project>(p => 
            {
                p.HasKey(d => d.Id);
                p.Property(d => d.Name).IsRequired();
                p.Property(d => d.Code).IsRequired().HasMaxLength(12);
                p.Property(d => d.Description);
                p.Property(d => d.WorkItemIdPrefix);
                p.Property(d => d.Removed).IsRequired().HasDefaultValue(false);

                p.HasMany(d => d.Deliveries)
                 .WithOne(d => d.Project)
                 .HasForeignKey(d => d.ProjectId);

                p.HasIndex(d => d.WorkItemIdPrefix).IsUnique();
            });

            builder.Entity<Delivery>(p =>
            {
                p.HasKey(d => d.Id);
                p.Property(d => d.Name).IsRequired();
                p.Property(d => d.Description);
                p.Property(d => d.Order);
                p.Property(d => d.ProjectId).IsRequired();
                p.Property(d => d.Removed).IsRequired().HasDefaultValue(false);

                p.HasOne(d => d.Project)
                 .WithMany(d => d.Deliveries)
                 .HasForeignKey(d => d.ProjectId);
            });

            builder.Entity<UserProject>(p => 
            {
                p.HasKey(up => new { up.UserId, up.ProjectId, up.RoleId });
                p.Property(up => up.Removed);
            
                p.HasOne(d => d.User)
                 .WithMany(d => d.Projects)
                 .HasForeignKey(d => d.UserId);
            
                p.HasOne(d => d.Project)
                 .WithMany(d => d.Users)
                 .HasForeignKey(d => d.ProjectId);
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