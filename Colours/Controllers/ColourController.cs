using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Colours.Services;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;

namespace Colours.Controllers
{
    [Route("api/[controller]")]
    public class ColourController : Controller
    {
        static readonly HttpClient Client = new HttpClient();
        private IColourScanService _scanService;
        public ColourController(
            IMatchService matchService,
            IColourScanService scanService
        )
        {

            var b = matchService;
            _scanService = scanService;
        }

        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery()] string url)
        {
            var result = await ReadImage(url);
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]string url)
        {
            var result = await ReadImage(url);
            return result;
        }

        private async Task<IActionResult> ReadImage(string url)
        {
            try
            {
                using (var stream = await Client.GetStreamAsync(url))
                {
                    var match = _scanService.GetDominantColour(stream);
                    if (match.Key == null)
                    {
                        return BadRequest($"Image dominant colour {match.Rgb} does not match a colour key.");
                    }
                    return Ok(match.Key);
                }
            }
            catch (ArgumentException e)
            {
                return BadRequest($"{e.GetType()} Check source url is correct.  Source: {url}");
            }
        }

    }
}
