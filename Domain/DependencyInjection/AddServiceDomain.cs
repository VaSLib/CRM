using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Domain.Interfaces.Service;
using Domain.Services;
using Microsoft.AspNetCore.Http;

namespace Domain.DependencyInjection;

public static class DependencyInjection
{
    public static void AddServiceDomain(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddTransient<IUserService, UserService>();
    }
}
