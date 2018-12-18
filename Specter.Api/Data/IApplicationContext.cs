using Microsoft.EntityFrameworkCore;

namespace Specter.Api.Data
{
    public interface IApplicationContext
    {
        DbSet<ApplicationUser> Users {get;}
        DbSet<Template> Tempaltes {get;}
        DbSet<TemplateHistory> TempalteHistory {get;}
        DbSet<Timesheet> Timesheets {get;}

    }
}