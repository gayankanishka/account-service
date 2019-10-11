using System;
using System.Collections.Generic;
using System.Fabric;
using System.Fabric.Description;
using System.Threading;
using System.Threading.Tasks;
using Account.Service.Business;
using Account.Service.Core;
using Account.Service.Core.Models;
using Account.Service.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace Account.Service.Engine
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class Engine : StatelessService
    {
        private ServiceProvider _serviceProvider;

        public Engine(StatelessServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            ConfigurationSection azureConfigurationSection = FabricRuntime.GetActivationContext()?
                .GetConfigurationPackageObject("Config")?
                .Settings.Sections["AzureStorageConfigs"];

            string storageAccountKey = azureConfigurationSection?.Parameters["StorageConnectionString"]?.Value;

            ICloudStorage cloudStorage = new CloudStorage(storageAccountKey);
            IRepository<AccountDto> accountRepository = 
                new AccountRepository(@"Server=localhost\SQLEXPRESS;Database=PlayGround;User Id=sa;Password=1qaz2wsx;");

            serviceCollection
                .AddSingleton(cloudStorage)
                .AddSingleton(accountRepository)
                .AddScoped<Processor, Processor>()
                .AddTransient<IAccountBusiness, AccountBusiness>();

            _serviceProvider = serviceCollection.BuildServiceProvider();

            return new ServiceInstanceListener[0];
        }

        /// <summary>
        /// This is the main entry point for your service instance.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service instance.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            Processor processor = _serviceProvider.GetService<Processor>();

            while (true)
            {
                try
                {
                    await processor.ProcessMessages();
                }
                catch (Exception)
                {
                    // Not throwing in order to run the service infinity
                }

                cancellationToken.ThrowIfCancellationRequested();

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }
    }
}
