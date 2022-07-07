using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialeNet.Core.Domain.Settings;
using SocialNet.Core.Application.Interfaces.Service;
using SocialNet.Infrastructure.Shared.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNet.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.Configure<MailSettings>(_config.GetSection("MailSettings"));
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
