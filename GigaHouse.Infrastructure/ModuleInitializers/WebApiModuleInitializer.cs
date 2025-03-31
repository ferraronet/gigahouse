using GigaHouse.Core.Common.Security;
using GigaHouse.Infrastructure.Interfaces.Events;
using GigaHouse.Infrastructure.Interfaces.Services;
using GigaHouse.Infrastructure.MongoDB;
using GigaHouse.Infrastructure.RabbitMq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GigaHouse.Infrastructure.ModuleInitializers
{
    public class WebApiModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHealthChecks();
        }
    }
}
