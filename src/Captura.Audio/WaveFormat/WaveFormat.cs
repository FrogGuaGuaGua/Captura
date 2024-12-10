using System;
using System.IO;

namespace Captura.Audio
{
    /// <summary>
    /// Represents a Wave file format
    /// </summary>
    public class WaveFormat
    {
        /// <summary>
        /// Creates a new PCM 44.1Khz stereo 16 bit format
        /// </summary>
        public WaveFormat() : this(44100, 16, 2) { }

        /// <summary>
        /// Creates a new 16 bit wave format with the specified sample
        /// rate and channel count
        /// </summary>
        /// <param name="SampleRate">Sample Rate</param>
        /// <param name="Channels">Number of channels</param>
        public WaveFormat(int SampleRate, short Channels) : this(SampleRate, 16, Channels) { }

        /// <summary>
        /// Creates a new PCM format with the specified sample rate, bit depth and channels
        /// </summary>
        public WaveFormat(int SampleRate, short BitsPerSample, short Channels)
        {
            if (Channels < 1)
                throw new ArgumentOutOfRangeException(nameof(Channels), "Channels must be 1 or greater");

            // minimum 16 bytes, sometimes 18 for PCM
            Encoding = WaveFormatEncoding.Pcm;
            this.Channels = Channels;
            this.SampleRate = SampleRate;
            this.BitsPerSample = BitsPerSample;
            ExtraSize = 0;

            BlockAlign = (short)(Channels * (BitsPerSample >> 3));
            AverageBytesPerSecond = this.SampleRate * BlockAlign;
        }

        /// <summary>
        /// Creates a new 32 bit IEEE floating point wave format
        /// </summary>
        /// <param name="SampleRate">sample rate</param>
        /// <param name="Channels">number of channels</param>
        public static WaveFormat CreateIeeeFloatWaveFormat(int SampleRate, short Channels)
        {
            return new WaveFormat
            {
                Encoding = WaveFormatEncoding.Float,
                Channels = Channels,
                SampleRate = SampleRate,
                BitsPerSample = 32,
                ExtraSize = 0,
                BlockAlign = (short)(Channels << 2),
                AverageBytesPerSecond = SampleRate * (Channels << 2)
            };
        }

        /// <summary>
        /// Returns the encoding type used
        /// </summary>
        public WaveFormatEncoding Encoding { get; set; }

        /// <summary>
        /// Writes this WaveFormat object to a stream
        /// </summary>
        /// <param name="Writer">the output stream</param>
        public virtual void Serialize(BinaryWriter Writer)
        {
            Writer.Write((short)Encoding);
            Writer.Write(Channels);
            Writer.Write(SampleRate);
            Writer.Write(AverageBytesPerSecond);
            Writer.Write(BlockAlign);
            Writer.Write(BitsPerSample);
            Writer.Write((short)ExtraSize);
        }

        /// <summary>
        /// Returns the number of channels (1=mono,2=stereo etc)
        /// </summary>
        public short Channels { get; set; }

        /// <summary>
        /// Returns the sample rate (samples per second)
        /// </summary>
        public int SampleRate { get; set; }

        /// <summary>
        /// Returns the average number of bytes used per second
        /// </summary>
        public int AverageBytesPerSecond { get; set; }

        /// <summary>
        /// Returns the block alignment
        /// </summary>
        public short BlockAlign { get; set; }

        /// <summary>
        /// Returns the number of bits per sample (usually 16 or 32, sometimes 24 or 8)
        /// Can be 0 for some codecs
        /// </summary>
        public short BitsPerSample { get; set; }

        /// <summary>
        /// Returns the number of extra bytes used by this waveformat.
        /// Often 0, except for compressed formats which store extra data after the WAVEFORMATEX header
        /// </summary>
        public int ExtraSize { get; set; }
    }
}
