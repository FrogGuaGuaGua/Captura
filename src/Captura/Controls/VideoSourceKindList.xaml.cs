using Captura.Video;
using System.Windows.Controls;
using System.Windows.Input;

namespace Captura
{
    public partial class VideoSourceKindList
    {
        public VideoSourceKindList()
        {
            InitializeComponent();
        }

        void OnVideoSourceReSelect(object Sender, MouseButtonEventArgs E)
        {
            if (Sender is ListViewItem item && item.IsSelected)
            {
                if (item.DataContext is IVideoSourceProvider provider)
                {
                    provider.OnSelect();
                }
            }
        }
    }
}
