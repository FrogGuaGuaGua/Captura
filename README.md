需要在Configure > Video 里面勾选 Use GDI instead of DesktopDuplication, 才能避免众多报错，勉强能用，我正在尝试解决下面的问题。

问题：
```csharp
MfWriter.cs
    198行, _writer.WriteSample(VideoStreamIndex, Sample); 
        // SharpDX.SharpDXException:“HRESULT: [0x80004005], Module: [General], 
        // ApiCode: [E_FAIL/Unspecified error], Message: 未指定的错误

Direct2DEditorSession.cs
    102行, ColorConverter = new Lazy<MfColorConverter>(() => new MfColorConverter(Width, Height, Device));
        // System.MissingMethodException, HResult = 0x80131513
        // Message = Method not found: 'SharpDX.MediaFoundation.Activate[]
        // SharpDX.MediaFoundation.MediaFactory.FindTransform(System.Guid,
        // SharpDX.MediaFoundation.TransformEnumFlag, System.Nullable`1<SharpDX.MediaFoundation.TRegisterTypeInformation>,
        // System.Nullable`1<SharpDX.MediaFoundation.TRegisterTypeInformation>)'.

    public void EndDraw() 
        RenderTarget.EndDraw();
        // SharpDX.SharpDXException: HRESULT: [0x8899000C], Module: [SharpDX.Direct2D1],
        // ApiCode: [D2DERR_RECREATE_TARGET/RecreateTarget],
        // Message: 存在可以恢复的演示错误。调用方需要重新创建、重新渲染整个帧，并重新尝试显示。

    增加RenderTarget = new RenderTarget(_factory, surface, renderTargetProps);
        // SharpDX.SharpDXException:“HRESULT: [0x887A0005], Module: [SharpDX.DXGI],
        // ApiCode: [DXGI_ERROR_DEVICE_REMOVED/DeviceRemoved],
        // Message: GPU 设备实例已经暂停。使用 GetDeviceRemovedReason 以确定相应的措施。
```
&copy; [Copyright 2019](LICENSE.md) Mathew Sachin

Capture Screen, WebCam, Audio, Cursor, Mouse Clicks and Keystrokes.

<a href="docs/Screenshots"><img src="https://mathewsachin.github.io/Captura/assets/ScreenShots/Home.png" style="max-width: 200px"></a>

## Features

- Take ScreenShots
- Capture ScreenCasts (Avi/Gif/Mp4)
- Capture with/without Mouse Cursor
- Capture Specific Regions, Screens or Windows
- Capture Mouse Clicks or Keystrokes
- Mix Audio recorded from Microphone and Speaker Output
- Capture from WebCam.
- Can be used from [Command-line](https://mathewsachin.github.io/Captura/cmdline) (*BETA*).
- Available in [multiple languages](https://mathewsachin.github.io/Captura/translation)
- Configurable [Hotkeys](https://mathewsachin.github.io/Captura/hotkeys)

## Installation

[latest]: https://github.com/MathewSachin/Captura/releases/latest

Portable and Setup builds for the latest release can be downloaded from [here][latest].

### Chocolatey

```powershell
choco install captura -y
```

### Dev Builds

See the [Continuous Integration page](docs/CI.md).

## Docs
[Build Notes](docs/Build.md) | [System Requirements](docs/System-Requirements.md) | [Contributing](CONTRIBUTING.md)

[ScreenShots](docs/Screenshots) | [Command-line](docs/Cmdline/README.md) | [Hotkeys](https://mathewsachin.github.io/Captura/hotkeys)

[FAQ](docs/FAQ.md) | [Code of Conduct](CODE_OF_CONDUCT.md) | [Changelog](docs/Changelogs/README.md)

[Continuous Integration](docs/CI.md) | [FFmpeg](docs/FFmpeg.md)

## License

[MIT License](LICENSE.md)

Check [here](licenses/) for licenses of dependencies.
