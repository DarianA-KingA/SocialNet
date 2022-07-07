using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNet.Core.Application.Interfaces.Repository;
using SocialNet.Infrastructure.Persistence.Context;
using SocialNet.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNet.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Context
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<SocialNetContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<SocialNetContext>(options => {
                    options.EnableSensitiveDataLogging();
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    m => m.MigrationsAssembly(typeof(SocialNetContext).Assembly.FullName));
                });
            }
            #endregion

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IComentaryRepository, ComentaryRepository>();
            services.AddTransient<IFriendRepository, FriendRepository>();
            services.AddTransient<IPictureRepository, PictureRepository>();
            services.AddTransient<IPublicationRepository, PublicationRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            #endregion
        }
    }
}
