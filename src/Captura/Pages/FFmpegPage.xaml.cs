﻿using Captura.ViewModels;
using Captura.Views;
using System.Windows;
using System.Windows.Input;

namespace Captura
{
    public partial class FFmpegPage
    {
        void FFmpegDownload(object Sender, RoutedEventArgs E)
        {
            FFmpegDownloaderWindow.ShowInstance();
        }

        void SelectFFmpegFolder(object Sender, MouseButtonEventArgs E)
        {
            if (DataContext is MainViewModel vm)
            {
                vm.SelectFFmpegFolderCommand.ExecuteIfCan();
            }
        }
    }
}
