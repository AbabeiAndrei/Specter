using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using Specter.Api.Data.Entities;

namespace Specter.Api.Data
{
    public interface IApplicationContext : IDbContext
    {
        DbSet<ApplicationUser> Users {get;}
        DbSet<Template> Templates {get;}
        DbSet<TemplateHistory> TemplatesHistory {get;}
        DbSet<Timesheet> Timesheets {get;}
        DbSet<Category> Categories {get;}
        DbSet<Delivery> Deliveries { get; }
        DbSet<Project> Projects { get; }
        DbSet<Role> Roles { get; }
        DbSet<IdentityUserClaim<Guid>> IdentityUserClaims { get; }
        DbSet<UserTeamRole> UserTeamRoles{ get; }
        DbSet<Team> Teams { get; }

    }
}