using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GigaHouse.Infrastructure;

public interface IModuleInitializer
{
    void Initialize(IServiceCollection services);
}
