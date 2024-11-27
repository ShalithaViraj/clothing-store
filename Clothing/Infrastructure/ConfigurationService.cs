
using Clothing.Application.Common.Interface;
using Clothing.Domain.Repository.Command;
using Clothing.Domain.Repository.Command.Common;
using Clothing.Domain.Repository.Query;
using Clothing.Domain.Repository.Query.Common;
using Clothing.Infrastructure.Data;
using Clothing.Infrastructure.Interceptor;
using Clothing.Infrastructure.Repository.Command;
using Clothing.Infrastructure.Repository.Command.Common;
using Clothing.Infrastructure.Repository.Query;
using Clothing.Infrastructure.Repository.Query.Common;
using Clothing.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigurationService
    {
        public static IServiceCollection AddClothingInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuditableEntitySaveChangerInterceptor>();

            services.AddDbContext<ClothingDBContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            services.AddTransient<IClothingDBContext>(provider => provider.GetRequiredService<ClothingDBContext>());

            services.AddTransient<ClothingDbContextInitilizer>();

            services.AddTransient<IDateTime, DateTimeService>();


            services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));
            services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));

            services.AddTransient<IUserQueryRepository, UserQueryRepository>();
            services.AddTransient<IUserCommandRepository, UserCommandRepository>();

            services.AddTransient<IUserHistoryCommandRepository, UserHistoryCommandRepository>();
            
            services.AddTransient<IAuthenticationService, AuthenticationService>();

            return services;
        }

    }
}
