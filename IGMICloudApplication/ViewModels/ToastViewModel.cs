using System;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Core;
using ToastNotifications.Lifetime;
using ToastNotifications.Lifetime.Clear;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace IGMICloudApplication.ViewModels
{
    public class ToastViewModel : ViewModelBase
    {
        public readonly Notifier Notifier;

        public ToastViewModel()
        {
            Notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.TopRight,
                    offsetX: 0,
                    offsetY: 0);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(10),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(3));

                cfg.Dispatcher = Application.Current.Dispatcher;

                cfg.DisplayOptions.TopMost = true;
                cfg.DisplayOptions.Width = 300;
            });
        }

        public void OnUnloaded()
        {
            Notifier.Dispose();
        }

        public void ShowInformation(string message)
        {
            Notifier.ShowInformation(message);
        }

        public void ShowInformation(string message, MessageOptions opts)
        {
            Notifier.ShowInformation(message, opts);
        }

        public void ShowSuccess(string message)
        {
            Notifier.ShowSuccess(message);
        }

        public void ShowSuccess(string message, MessageOptions opts)
        {
            Notifier.ShowSuccess(message, opts);
        }

        public void ClearMessages(string msg)
        {
            Notifier.ClearMessages(new ClearByMessage(msg));
        }

        public void ShowWarning(string message, MessageOptions opts)
        {
            Notifier.ShowWarning(message, opts);
        }

        public void ShowError(string message)
        {
            Notifier.ShowError(message);
        }

        public void ShowError(string message, MessageOptions opts)
        {
            Notifier.ShowError(message, opts);
        }

        public void ClearAll()
        {
            Notifier.ClearMessages(new ClearAll());
        }
    }
}
