using AOEStatsApp.ViewModels;
using AOEStatsApp.HostBuilders;
using DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Position;
using ToastNotifications.Lifetime;
using System;
using AOEStatsApp.Stores;
using Domain.Models;
using Domain.Enums;
using ToastNotifications.Messages;
using ToastNotifications.Core;
using System.Windows.Documents;

namespace AOEStatsApp
{
    public partial class App : Application
    {
        private readonly IHost _host;
        private Notifier _notifier;
        private AppData _appData;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .AddServices()
                .AddStores()
                .AddViewModels()
                .ConfigureServices((hostContext, services) =>
                {
                    string connectionString = hostContext.Configuration.GetConnectionString("Default");
                    services.AddSingleton(new AOEStatsDbContextFactory(connectionString));

                    services.AddSingleton(s => new MainWindow()
                    {
                        DataContext = s.GetRequiredService<MainViewModel>()
                    });
                }).Build();

            _appData = new AppData();
            InitializeNotifier();
            InitializeGlobalEvents();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            AOEStatsDbContextFactory dbContextFactory = _host.Services.GetRequiredService<AOEStatsDbContextFactory>();
            using (AOEStatsDbContext dbContext = dbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

            var navigationStore = _host.Services.GetRequiredService<NavigationStore>();
            navigationStore.CurrentViewModel = new HomeViewModel();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        private void InitializeNotifier()
        {
            var notifierMessageOptions = new MessageOptions() { UnfreezeOnMouseLeave = true };

            _notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Current.MainWindow,
                    corner: Corner.TopLeft,
                    offsetX: 0,
                    offsetY: 0);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(5),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = Current.Dispatcher;
            });

            var notificationStore = _host.Services.GetRequiredService<NotificationsStore>();
            notificationStore.NotificationsChanged += (currentNotification, args) =>
            {
                var notification = currentNotification as Notification;

                switch (notification?.MessageType)
                {
                    case MessageType.Error:
                        _notifier.ShowError(notification.Message, notifierMessageOptions);
                        break;

                    case MessageType.Status:
                        _notifier.ShowInformation(notification.Message, notifierMessageOptions);
                        break;

                    case MessageType.Success:
                        _notifier.ShowSuccess(notification.Message, notifierMessageOptions);
                        break;
                }
            };
        }

        private void InitializeGlobalEvents()
        {
            EventManager.RegisterClassHandler(typeof(Hyperlink),
                Hyperlink.ClickEvent,
                new RoutedEventHandler(_appData.Hyperlink_RequestNavigate));
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();
            base.OnExit(e);
        }
    }
}