
using System;
using System.Linq;
using Colours.Models;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp.PixelFormats;

namespace Colours.Services
{
    /// <summary>
    /// Calculate the euclidean distance between colours to pick nearest
    /// </summary>
    public class EuclidMatchService : IMatchService
    {
        ColourMatches _matches;

        public EuclidMatchService(IOptions<ColourMatches> matches)
        {
            _matches = new ColourMatches(matches.Value);

        }

        public ColourMatch GetColourMatch(Rgba32 colour)
        {
            return _matches.FirstOrDefault(m => m.Rgb == colour) ?? new ColourMatch(key:null, rgb:colour);
        }

        public Rgba32 GetNearestColour(Rgba32 pixel)
        {
            const double tolerance = 110 * 110;
            double nearestDistance = 255 * 255;
            Rgba32 match = Rgba32.White;

            foreach (var target in _matches)
            {
                var targetColour = target.Rgb;
                double distance = GetDistance(pixel, targetColour);
                if (distance == 0)
                {
                    return targetColour;
                }
                else if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    match = targetColour;
                }
            }
            //if the distance is too great add this colour as a new match entry
            if (nearestDistance > tolerance)  
            {
                _matches.Add(new ColourMatch(key: null, rgb: pixel));
                return pixel;
            }
            return match;
        }

        double GetDistance(Rgba32 a, Rgba32 z)
        {
            int r = a.R - z.R;
            int g = a.G - z.G;
            int b = a.B - z.B;
            /*weighting values to adjust for human perception*/
            const double rweight = 0.2126;
            const double gweight = 0.7152;
            const double bweight = 0.0722;
            return (r * r * rweight) + (g * g * gweight) + (b * b * bweight);
        }
    }
}
