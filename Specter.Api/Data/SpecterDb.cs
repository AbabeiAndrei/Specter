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

        public virtual DbSet<UserTeamRole> UserTeamRoles { get; set; }

        public virtual DbSet<IdentityUserClaim<Guid>> IdentityUserClaims { get; set; }

        public virtual DbSet<Role> Roles{ get; set; }

        public virtual DbSet<Team> Teams { get; set; }

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=specter;Integrated Security=True;");
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

            builder.Entity<Role>(entity => 
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Name).IsRequired();
                entity.Property(r => r.NormalizedName);
                entity.Property(r => r.ConcurrencyStamp).HasDefaultValueSql("NEWID()");

                entity.HasMany(r => r.Users)
                      .WithOne(u => u.Role)
                      .HasForeignKey(u => u.RoleId);
            });

            builder.Entity<UserTeamRole>(entity => 
            {
                entity.HasKey(p => new { p.UserId, p.RoleId });

                entity.HasOne(p => p.User)
                      .WithMany(p => p.Roles)
                      .HasForeignKey(p => p.UserId);

                entity.HasOne(p => p.Role)
                      .WithMany(p => p.Users)
                      .HasForeignKey(p => p.RoleId);

                entity.HasOne(p => p.Team)
                      .WithMany(p => p.Users)
                      .HasForeignKey(p => p.TeamId)
                      .IsRequired(false);
            });

            builder.Entity<IdentityUserClaim<Guid>>().HasKey(p => p.Id);

            builder.Entity<ApplicationUser>(entity => 
            {
                entity.Property(p => p.FirstName);
                entity.Property(p => p.LastName);

                entity.HasMany(p => p.Roles)
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

                entity.HasMany(p => p.Roles)
                      .WithOne(p => p.User)
                      .HasForeignKey(p => p.UserId);
            });

            builder.Entity<Team>(entity => 
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Name).IsRequired();
                entity.Property(t => t.Description);
                entity.Property(t => t.Removed).HasDefaultValue(false);
                
                entity.HasMany(t => t.Users)
                      .WithOne(t => t.Team)
                      .HasForeignKey(t => t.TeamId);

                entity.HasMany(t => t.Projects)
                      .WithOne(t => t.Team)
                      .HasForeignKey(t => t.ProjectId);
            });

            builder.Entity<ProjectTeam>(entity => 
            {
                entity.HasKey(pt => new {pt.ProjectId, pt.TeamId});

                entity.HasOne(pt => pt.Project)
                      .WithMany(pt => pt.Teams)
                      .HasForeignKey(pt => pt.ProjectId);

                entity.HasOne(pt => pt.Team)
                      .WithMany(pt => pt.Projects)
                      .HasForeignKey(pt => pt.TeamId);
            });

            builder.Entity<Category>(p => 
            {
                p.HasKey(c => c.Id);
                p.Property(c => c.Name).IsRequired();
                p.Property(c => c.Description);
                p.Property(c => c.Removed).IsRequired().HasDefaultValue(false);
            });

            builder.Entity<Project>(entity => 
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Name).IsRequired();
                entity.Property(d => d.Code).IsRequired().HasMaxLength(12);
                entity.Property(d => d.Description);
                entity.Property(d => d.WorkItemIdPrefix);
                entity.Property(d => d.Removed).IsRequired().HasDefaultValue(false);

                entity.HasMany(d => d.Deliveries)
                      .WithOne(d => d.Project)
                      .HasForeignKey(d => d.ProjectId);

                entity.HasIndex(d => d.WorkItemIdPrefix).IsUnique();
            });

            builder.Entity<Delivery>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Name).IsRequired();
                entity.Property(d => d.Description);
                entity.Property(d => d.Order);
                entity.Property(d => d.ProjectId).IsRequired();
                entity.Property(d => d.Removed).IsRequired().HasDefaultValue(false);

                entity.HasOne(d => d.Project)
                      .WithMany(d => d.Deliveries)
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