
using Colours.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.IO;

namespace Colours.Services
{
    public interface IColourScanService
    {
        ColourMatch GetDominantColour(Stream imageStream);
    }
}