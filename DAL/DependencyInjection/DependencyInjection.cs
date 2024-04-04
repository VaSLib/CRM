using DAL.Entity;
using DAL.Repositories.Interfaces;
using DAL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace DAL.DependencyInjection;

public static class DependencyInjection
{
    public static void AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opts =>
                 opts.UseSqlServer("Server=.; Database=CRM; Trusted_Connection=true;Encrypt=Optional"));

        services.InitRepositories();
        services.AddAuthentication().AddCookie("cookie");

    }

    private static void InitRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
        services.AddScoped<IBaseRepository<Contact>, BaseRepository<Contact>>();
        services.AddScoped<IBaseRepository<Sale>, BaseRepository<Sale>>();
        services.AddScoped<IBaseRepository<Lead>, BaseRepository<Lead>>();
    }
}