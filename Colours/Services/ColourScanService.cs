using Colours.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Colours.Services
{
    public class ColourScanService : IColourScanService
    {
        IMatchService _matchService;
        public ColourScanService(IMatchService matchService)
        {
            _matchService = matchService;
        }

        public ColourMatch GetDominantColour(Stream imageStream)
        {
            var image = Image.Load(imageStream);
            var bins = GetColourFrequencies(image);

            bins = GroupByNearestColours(bins);

            var seed = default(KeyValuePair<Rgba32, int>);
            var dominant = bins.Aggregate(seed, (best, bin) => bin.Value > best.Value ? bin : best);

            return _matchService.GetColourMatch(dominant.Key);
        }

        private static Dictionary<Rgba32, int> GetColourFrequencies(Image<Rgba32> image)
        {
            Dictionary<Rgba32, int> bins = new Dictionary<Rgba32, int>();
            int height = image.Height;
            int width = image.Width;
            int xskip = width < 1000 ? 1 : 10;
            int yskip = height < 1000 ? 1 : 10;

            for (int y = 0; y < height; y += yskip)
            {
                for (int x = 0; x < width; x += xskip)
                {
                    var pixel = image[x, y];
                    bins.TryGetValue(pixel, out int val);
                    bins[pixel] = (++val);
                }
            }
            return bins;
        }

        private Dictionary<Rgba32, int> GroupByNearestColours(Dictionary<Rgba32, int> bins)
        {
            Dictionary<Rgba32, int> matchedBins = new Dictionary<Rgba32, int>();
            foreach (var bin in bins)
            {
                var nearest = _matchService.GetNearestColour(bin.Key);
                matchedBins.TryGetValue(nearest, out int val);
                matchedBins[nearest] = val + bin.Value;
            }
            return matchedBins;
        }
    }
}