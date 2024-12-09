using Captura.ViewModels;
using System.Windows.Input;

namespace Captura
{
    public partial class OutputFolderControl
    {
        public OutputFolderControl()
        {
            InitializeComponent();
        }

        void SelectTargetFolder(object Sender, MouseButtonEventArgs E)
        {
            if (DataContext is MainViewModel vm)
            {
                vm.SelectOutputFolderCommand.ExecuteIfCan();
            }
        }
    }
}
