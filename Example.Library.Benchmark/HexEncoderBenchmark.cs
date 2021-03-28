namespace Example.Library.Benchmark
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using BenchmarkDotNet.Attributes;

    using Example.Library.Text;

    [Config(typeof(BenchmarkConfig))]
    public class HexEncoderBenchmark
    {
        [AllowNull]
        private byte[] bytes;

        [AllowNull]
        private string text;

        [AllowNull]
        private byte[] byteBuffer;

        [AllowNull]
        private char[] charBuffer;

        [Params(4, 16, 64, 256)]
        public int Size { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            bytes = Enumerable.Range(0, Size).Select(x => (byte)x).ToArray();
            text = BitConverter.ToString(bytes).Replace("-", string.Empty, StringComparison.Ordinal);
            byteBuffer = new byte[Size];
            charBuffer = new char[Size * 2];
        }

        [Benchmark]
        public string Encode() => HexEncoder.Encode(bytes);

        [Benchmark]
        public int EncodeToBuffer() => HexEncoder.Encode(bytes, charBuffer);

        [Benchmark]
        public byte[] Decode() => HexEncoder.Decode(text);

        [Benchmark]
        public int DecodeToBuffer() => HexEncoder.Decode(text, byteBuffer);
    }
}
