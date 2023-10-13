using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.Foundation.SitecoreSend.Services;

namespace Sitecore.Foundation.SitecoreSend
{
    public class ServiceConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ISitecoreSendService, SitecoreSendService>();
        }
    }
}
