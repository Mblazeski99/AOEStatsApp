using AOEStatsApp.Stores;
using AOEStatsApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AOEStatsApp.Services;
using System;
using AOEStatsApp.Services.Interfaces;

namespace AOEStatsApp.HostBuilders
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<Func<UnitStatsItemListingViewModel>>((s) => () => s.GetRequiredService<UnitStatsItemListingViewModel>());

                services.AddTransient<CreateOrEditUnitStatsItemViewModel>();
                services.AddSingleton<Func<CreateOrEditUnitStatsItemViewModel>>((s) => () => s.GetRequiredService<CreateOrEditUnitStatsItemViewModel>());

                services.AddSingleton<MainViewModel>();
            });

            return hostBuilder;
        }

        public static IHostBuilder AddServices(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<IUnitStatsItemService, UnitStatsItemService>();
            });

            return hostBuilder;
        }

        public static IHostBuilder AddStores(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<UnitStatsStore>();
                services.AddSingleton<NavigationStore>();
                services.AddSingleton<NotificationsStore>();
            });

            return hostBuilder;
        }
    }
}
