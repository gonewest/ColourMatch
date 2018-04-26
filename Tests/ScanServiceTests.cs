using Colours.Models;
using Colours.Services;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net.Http;
using Xunit;

namespace Tests
{
    public class ScanService
    {
        static readonly HttpClient Client = new HttpClient();

        ColourMatches matches = new ColourMatches
        {
            new ColourMatch( key: "grey", red: 123, green: 123, blue: 123 ),
            new ColourMatch( key: "black", red: 0, green: 0, blue: 0 ),
            new ColourMatch( key: "teal", red: 0, green: 128, blue: 128 ),
            new ColourMatch( key: "navy", red: 0, green: 0, blue: 128 ),
        };


        [Theory]
        [InlineData("black", "./Resources/sample-black.png")]
        [InlineData("grey", "./Resources/sample-grey.png")]
        [InlineData("teal", "./Resources/sample-teal.png")]
        [InlineData("navy", "./Resources/sample-navy.png")]
        public void FindsDominant(string expected, string file)
        {
            var filename = file;
            using (var stream = File.OpenRead(filename))
            {
                var options = Options.Create(matches);
                var matchService = new Colours.Services.EuclidMatchService(options);
                var scan = new ColourScanService(matchService);

                var ans = scan.GetDominantColour(stream);
                Assert.True(expected == ans.Key, $"failed: {ans.Rgb} {ans.Key}");
                Assert.Equal(expected, ans.Key);
            }
        }
    }
}
