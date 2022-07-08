using System;
using System.Collections.Generic;
using System.Linq;

namespace ColorSequenceGenerator
{
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
        public static RGB FromString(string color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));
            try
            {
                color = color.Trim();
                if (color.StartsWith("#"))
                {
                    if (color.Length == 7) // "#aabbcc"
                    {
                        byte[] colors = StringToByteArrayFastest(color.Substring(1));
                        return new RGB(colors[0], colors[1], colors[2]);
                    } else
                    {
                        throw new ArgumentException("Wrong Format");
                    }
                } else if (color.StartsWith("rgb", StringComparison.OrdinalIgnoreCase))
                {
                    var trimmed = color.Trim('r', 'g', 'b', 'a', '(', ')');
                    var rgb = trimmed.Split(',', ' ').Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => byte.Parse(s)).ToArray();
                    return new RGB(rgb[0], rgb[1], rgb[2]);
                }
                throw new ArgumentException("Wrong Format");
            } catch
            {
                throw new ArgumentException($"Cannot parse {color}, should be either #AABBCC or rgb(123, 16, 180) or rgba(123, 16, 180, xx)");
            }
        }

        public static implicit operator string(RGB rgb) => rgb.ToString();
        public static implicit operator RGB(string rgbs) => FromString(rgbs);

        private static byte[] StringToByteArrayFastest(string hex)
        {
            if (hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits");

            byte[] arr = new byte[hex.Length >> 1];

            for (int i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            }

            return arr;
        }

        private static int GetHexVal(char hex)
        {
            int val = (int)hex;
            //For uppercase A-F letters:
            //return val - (val < 58 ? 48 : 55);
            //For lowercase a-f letters:
            //return val - (val < 58 ? 48 : 87);
            //Or the two combined, but a bit slower:
            return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }
    }
}
