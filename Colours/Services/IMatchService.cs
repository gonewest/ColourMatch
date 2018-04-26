
using System;
using System.Collections.Generic;
using Colours.Models;
using SixLabors.ImageSharp.PixelFormats;

namespace Colours.Services
{
    
    public interface IMatchService
    {
        Rgba32 GetNearestColour(Rgba32 pixel);
        ColourMatch GetColourMatch(Rgba32 key);
    }

}