using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;

namespace AOEStatsApp
{
    public class AppData
    {
        #region EventHandlers
        public void Hyperlink_RequestNavigate(object sender, RoutedEventArgs e)
        {
            Hyperlink link = (Hyperlink)e.OriginalSource;
            Process.Start(new ProcessStartInfo()
            {
                FileName = link.NavigateUri.AbsoluteUri,
                UseShellExecute = true
            });
        }
        #endregion
    }
}
