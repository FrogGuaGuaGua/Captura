using Captura.Views;
using Microsoft.Win32;
using System.Windows;

namespace Captura
{
    public partial class AboutPage
    {
        void OpenAudioVideoTrimmer(object Sender, RoutedEventArgs E)
        {
            new TrimmerWindow().ShowAndFocus();
        }
    }
}
