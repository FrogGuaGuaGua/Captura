﻿using Captura.Loc;
using Captura.Models;
using Captura.Video;
using Captura.Webcam;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Captura.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ScreenShotViewModel : ViewModelBase
    {
        public DiskWriter DiskWriter { get; }
        public ClipboardWriter ClipboardWriter { get; }

        public ScreenShotViewModel(ILocalizationProvider Loc,
            Settings Settings,
            DiskWriter DiskWriter,
            ClipboardWriter ClipboardWriter,
            ScreenShotModel ScreenShotModel,
            VideoSourcesViewModel VideoSourcesViewModel,
            WebcamModel WebcamModel,
            IPlatformServices PlatformServices) : base(Settings, Loc)
        {
            this.DiskWriter = DiskWriter;
            this.ClipboardWriter = ClipboardWriter;

            ScreenShotCommand = new[]
                {
                    VideoSourcesViewModel
                        .ObserveProperty(M => M.SelectedVideoSourceKind)
                        .Select(M => M is NoVideoSourceProvider),
                    VideoSourcesViewModel
                        .ObserveProperty(M => M.SelectedVideoSourceKind)
                        .Select(M => M is WebcamSourceProvider),
                    WebcamModel
                        .ObserveProperty(M => M.SelectedCam)
                        .Select(M => M is NoWebcamItem)
                }
                .CombineLatest(M =>
                {
                    bool noVideo = M[0];
                    bool webcamMode = M[1];
                    bool noWebcam = M[2];

                    if (webcamMode)
                        return !noWebcam;

                    return !noVideo;
                })
                .ToReactiveCommand()
                .WithSubscribe(async M =>
                {
                    var bmp = await ScreenShotModel.GetScreenShot(VideoSourcesViewModel.SelectedVideoSourceKind);

                    await ScreenShotModel.SaveScreenShot(bmp);
                });

            async Task ScreenShotWindow(IWindow Window)
            {
                var img = ScreenShotModel.ScreenShotWindow(Window);

                await ScreenShotModel.SaveScreenShot(img);
            }

            ScreenShotActiveCommand = new ReactiveCommand()
                .WithSubscribe(async () => await ScreenShotWindow(PlatformServices.ForegroundWindow));

            ScreenShotDesktopCommand = new ReactiveCommand()
                .WithSubscribe(async () =>
                {
                    await Task.Delay(300);

                    await ScreenShotWindow(PlatformServices.DesktopWindow);
                });

            ScreenshotRegionCommand = new ReactiveCommand()
                .WithSubscribe(async () =>
                {
                    await Task.Delay(300);

                    await ScreenShotModel.ScreenshotRegion();
                });

            ScreenshotWindowCommand = new ReactiveCommand()
                .WithSubscribe(async () =>
                {
                    await Task.Delay(300);

                    await ScreenShotModel.ScreenshotWindow();
                });

            ScreenshotScreenCommand = new ReactiveCommand()
                .WithSubscribe(async () =>
                {
                    await Task.Delay(300);

                    await ScreenShotModel.ScreenshotScreen();
                });
        }

        public ICommand ScreenShotCommand { get; }
        public ICommand ScreenShotActiveCommand { get; }
        public ICommand ScreenShotDesktopCommand { get; }
        public ICommand ScreenshotRegionCommand { get; }
        public ICommand ScreenshotWindowCommand { get; }
        public ICommand ScreenshotScreenCommand { get; }

        public IEnumerable<ImageFormats> ScreenShotImageFormats { get; } = Enum
            .GetValues(typeof(ImageFormats))
            .Cast<ImageFormats>();
    }
}