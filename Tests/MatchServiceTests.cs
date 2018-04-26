using Colours.Models;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests
{
    public class MatchService
    {

        ColourMatches matches = new ColourMatches
        {
            new ColourMatch( key: "grey", red: 123, green: 123, blue: 123 ),
            new ColourMatch( key: "black", red: 0, green: 0, blue: 0 ),
            new ColourMatch( key: "teal", red: 0, green: 128, blue: 128 ),
            new ColourMatch( key: "navy", red: 0, green: 0, blue: 128 ),
        };


        [Theory]
        [InlineData(56, 70, 87)]
        [InlineData(123, 123, 123)]
        [InlineData(122, 110, 133)]
        [InlineData(110 ,110 ,110 )]
        public void GetsNearestGrey(byte r, byte g, byte b)
        {
            Rgba32 expected = new Rgba32(123, 123, 123);
            Rgba32 sample = new Rgba32(r, g, b);
            var options = Options.Create(matches);
            var matchService = new Colours.Services.EuclidMatchService(options);

            var ans = matchService.GetNearestColour(sample);

            Assert.Equal(expected, ans);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(10, 20, 40)]
        [InlineData(50, 50, 50)]
        public void GetsNearestBlack(byte r, byte g, byte b)
        {
            Rgba32 expected = new Rgba32(0, 0, 0);
            Rgba32 sample = new Rgba32(r, g, b);
            var options = Options.Create(matches);
            var matchService = new Colours.Services.EuclidMatchService(options);

            var ans = matchService.GetNearestColour(sample);

            Assert.Equal(expected, ans);
        }
    }
}
