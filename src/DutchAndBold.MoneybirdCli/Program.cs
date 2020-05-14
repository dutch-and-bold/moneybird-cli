using System.IO;
using CommandLine;
using DutchAndBold.MoneybirdCli.Commands;
using DutchAndBold.MoneybirdCli.Configurations;
using DutchAndBold.MoneybirdSdk.Extensions.Microsoft.DependencyInjection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DutchAndBold.MoneybirdCli
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = BuildConfiguration();
            var collection = new ServiceCollection();

            Startup(configuration, collection);

            var services = collection.BuildServiceProvider();

            Parser.Default
                .ParseArguments<LoginCommand, TestConnectionCommand>(args)
                .WithParsedAsync(
                    async c =>
                    {
                        using var scope = services.CreateScope();
                        await scope.ServiceProvider.GetService<IMediator>().Send(c);
                    })
                .Wait();
        }

        private static IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .AddEnvironmentVariables("MoneybirdCli_")
                .Build();
        }

        private static void Startup(IConfiguration configuration, IServiceCollection services)
        {
            services
                .AddLogging(b => b.AddConsole())
                .AddLocalization()
                .AddMediatR(typeof(Program).Assembly);
            
            var apiConfiguration = new MoneybirdApiConfiguration();
            configuration.Bind("Moneybird:Api", apiConfiguration);

            services.AddSingleton(apiConfiguration);

            services
                .AddMoneybirdSdk(apiConfiguration.EndpointUrl)
                .AddFileTokenStore(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "token.json");

            if (apiConfiguration.ClientId != null && apiConfiguration.ClientSecret != null)
            {
                services.AddMoneybirdMachineToMachineAuthentication(
                    apiConfiguration.AuthorityUrl,
                    apiConfiguration.ClientId,
                    apiConfiguration.ClientSecret);
            }
        }
    }
}