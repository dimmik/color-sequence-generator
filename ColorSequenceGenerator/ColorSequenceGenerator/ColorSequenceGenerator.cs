using System;
using System.Collections.Generic;

namespace ColorSequenceGenerator
{
    public class ColorSequenceGenerator
    {
        public RGB Seed { get; set; } = new RGB(0x35, 0x66, 0xee);
        public double Magic { get; set; } = 1.63;
        public int MaxSeqLength { get; set; } = 1000000;

        public ColorSequenceGenerator()
        {
        }
        public ColorSequenceGenerator(RGB seed, double magic)
        {
            Seed = seed ?? throw new ArgumentNullException(nameof(seed));
            Magic = magic;
        }
        public static readonly ColorSequenceGenerator Instance = new ColorSequenceGenerator(new RGB(0x35, 0x66, 0xee), 1.63);

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
    public class RGB
    {
        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }

        public RGB()
        {
        }
        public RGB(byte red, byte green, byte blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }
        private readonly SortedDictionary<string, Func<RGB, string>> formats = new SortedDictionary<string, Func<RGB, string>>()
        {
            { "rgb", (r) => $"rgb({r.Red}, {r.Green}, {r.Blue})" },
            { "#", (r) => $"#{r.Red:X2}{r.Green:X2}{r.Blue:X2}" },
        };
        public string ToString(string fmt)
        {
            if (!formats.ContainsKey(fmt)) throw new ArgumentException($"Format should be one of ('{string.Join("', '", formats.Keys)}')");
            return formats[fmt](this);
        }
        public override string ToString()
        {
            return ToString("#");
        }
    }
}
