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
                services.AddTransient((s) => CreateUnitStatsItemListingViewModel(s));
                services.AddSingleton<Func<UnitStatsItemListingViewModel>>((s) => () => s.GetRequiredService<UnitStatsItemListingViewModel>());
                services.AddSingleton<NavigationService<UnitStatsItemListingViewModel>>();

                services.AddTransient<CreateOrEditUnitStatsItemViewModel>();
                services.AddSingleton<Func<CreateOrEditUnitStatsItemViewModel>>((s) => () => s.GetRequiredService<CreateOrEditUnitStatsItemViewModel>());
                services.AddSingleton<NavigationService<CreateOrEditUnitStatsItemViewModel>>();

                services.AddSingleton<MainViewModel>();
            });

            return hostBuilder;
        }

        private static UnitStatsItemListingViewModel CreateUnitStatsItemListingViewModel(IServiceProvider services)
        {
            return UnitStatsItemListingViewModel.LoadViewModel(
                services.GetRequiredService<UnitStatsStore>(),
                services.GetRequiredService<NavigationService<CreateOrEditUnitStatsItemViewModel>>(),
                services.GetRequiredService<NotificationsStore>());
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
