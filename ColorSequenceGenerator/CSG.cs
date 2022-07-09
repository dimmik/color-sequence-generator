using System;
using System.Collections.Generic;

namespace ColorSequenceGenerator
{
    public class CSG : IColorSequenceGenerator
    {
        public RGB Seed { get; set; } = new RGB(0x35, 0x66, 0xee);
        public double Magic { get; set; } = 1.63;
        public int MaxSeqLength { get; set; } = 1000000;

        public CSG()
        {
        }
        public CSG(RGB seed, double magic)
        {
            Seed = seed ?? throw new ArgumentNullException(nameof(seed));
            Magic = magic;
        }
        public static readonly CSG Instance = new CSG(new RGB(0x35, 0x66, 0xee), 1.63);

        public IEnumerable<RGB> ColorSequence()
        {
            return ColorSequence(Seed, Magic, MaxSeqLength);
        }
        public IEnumerable<RGB> ColorSequence(RGB seed)
        {
            return ColorSequence(seed, Magic, MaxSeqLength);
        }
        public IEnumerable<RGB> ColorSequence(double magic)
        {
            return ColorSequence(Seed, magic, MaxSeqLength);
        }
        public static IEnumerable<RGB> ColorSequence(RGB seed, double magic, int maxLength)
        {
            int i = 0;
            RGB c = seed;
            while (i < maxLength)
            {
                yield return c;
                c = NextColor(c, magic);
                i++;
            }
        }
        private static RGB NextColor(RGB c, double magic)
        {
            int colorN = BitConverter.ToInt32(new byte[] { c.Blue, c.Green, c.Red, 0 }, 0);
            colorN = (int)(colorN * magic);
            var bytes = BitConverter.GetBytes(colorN);
            return new RGB() { Red = bytes[2], Green = bytes[1], Blue = bytes[0] };
        }
    }
}
