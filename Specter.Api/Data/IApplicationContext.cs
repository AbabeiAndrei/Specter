using Microsoft.EntityFrameworkCore;
using Specter.Api.Data.Entities;

namespace Specter.Api.Data
{
    public interface IApplicationContext
    {
        DbSet<ApplicationUser> Users {get;}
        DbSet<Template> Templates {get;}
        DbSet<TemplateHistory> TemplatesHistory {get;}
        DbSet<Timesheet> Timesheets {get;}

    }
}