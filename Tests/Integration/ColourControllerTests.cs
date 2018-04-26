using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;
using Xunit;

namespace Tests.Controller
{
    public class Main : IClassFixture<TestFixture>
    {
        private readonly HttpClient _client;

        public Main(TestFixture fixture)
        {
            _client = fixture.Client;
        }


        [Theory]
        [InlineData("https://pwintyimages.blob.core.windows.net/samples/stars/sample-black.png", "black")]
        [InlineData("https://pwintyimages.blob.core.windows.net/samples/stars/sample-grey.png", "grey")]
        [InlineData("https://pwintyimages.blob.core.windows.net/samples/stars/sample-teal.png", "teal")]
        [InlineData("https://pwintyimages.blob.core.windows.net/samples/stars/sample-navy.png", "navy")]
        public async void EndpointReturnsOk(string url, string expected)
        {        
            var response = await _client.GetAsync($"/api/colour?url={url}");
            response.EnsureSuccessStatusCode();

            string actual = await response.Content.ReadAsStringAsync();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("http://www.pngpix.com/wp-content/uploads/2016/06/PNGPIX-COM-Ford-Mustang-Red-Car-PNG-Image.png")]
        public async void EndpointReturnsBadRequest(string url)
        {
            //returns 400 if colour not matched
            var response = await _client.GetAsync($"/api/colour?url={url}");

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

    }
}
