using SharpDX.Direct2D1;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;

namespace Captura.Windows.DesktopDuplication
{
    public interface IPointerShape : IDisposable
    {
        void Update(Texture2D DesktopTexture, OutputDuplicatePointerPosition PointerPosition);

        Bitmap GetBitmap();
    }
}