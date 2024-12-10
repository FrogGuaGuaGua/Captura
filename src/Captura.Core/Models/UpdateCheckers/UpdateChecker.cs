using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace Captura.Models
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class UpdateChecker : IUpdateChecker
    {
        readonly ProxySettings _proxySettings;
        readonly Version _currentVersion;

        public UpdateChecker(ProxySettings ProxySettings)
        {
            _proxySettings = ProxySettings;

            _currentVersion = ServiceProvider.AppVersion;
        }

        const string DownloadsUrl = "https://github.com/FrogGuaGuaGua/Captura/releases";
        public void GoToDownloadsPage()
        {
            Process.Start(new ProcessStartInfo(DownloadsUrl) { UseShellExecute = true });
        }       
    }
}