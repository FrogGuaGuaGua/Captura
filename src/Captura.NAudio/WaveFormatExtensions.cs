using Wf = NAudio.Wave.WaveFormat;
using WfEnc = NAudio.Wave.WaveFormatEncoding;

namespace Captura.Audio
{
    static class WaveFormatExtensions
    {
        public static WaveFormat ToCaptura(this Wf Wf)
        {
            return Wf.Encoding == WfEnc.IeeeFloat
                ? WaveFormat.CreateIeeeFloatWaveFormat(Wf.SampleRate, (short)Wf.Channels)
                : new WaveFormat(Wf.SampleRate, (short)Wf.BitsPerSample, (short)Wf.Channels);
        }
    }
}
