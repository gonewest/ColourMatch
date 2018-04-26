using Newtonsoft.Json;
using SixLabors.ImageSharp.PixelFormats;
using System.Collections.Generic;

namespace Colours.Models
{
    public class ColourMatches : List<ColourMatch>
    {
        public ColourMatches(IEnumerable<ColourMatch> collection) : base(collection)
        {
        }
        public ColourMatches() : base()
        {

        }
    }

    public class ColourMatch
    {
        public ColourMatch(string key, byte red, byte green, byte blue)
        {
            Key = key;
            Red = red;
            Green = green;
            Blue = blue;
        }

        public ColourMatch(string key, Rgba32 rgb)
        {
            Key = key;
            Red = rgb.R;
            Green = rgb.G;
            Blue = rgb.B;
        }

        public ColourMatch()
        {

        }

        public string Key { get; set; }
        public Rgba32 Rgb => new Rgba32(Red, Green, Blue);
        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }
    }
}