﻿using Captura.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace Captura
{
    public partial class SoundsPage
    {
        public SoundsPage()
        {
            InitializeComponent();
        }

        void SetFile(object Sender, MouseButtonEventArgs E)
        {
            if (Sender is FrameworkElement element && element.DataContext is SoundsViewModelItem vm)
            {
                vm.SetCommand.ExecuteIfCan();
            }
        }
    }
}
