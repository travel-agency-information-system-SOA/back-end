using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Unisplash
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/unisplash")]
    public class UnisplashController : BaseApiController
    {
        private readonly string _unsplashAccessKey = "oeQrSj9gSOv3VdTwc080-RnD7swYcxbMrYPOsLrT30I";

        [HttpGet("search")]
        public async Task<ActionResult> SearchImages([FromQuery] string query)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Client-ID {_unsplashAccessKey}");

                var response = await client.GetAsync($"https://api.unsplash.com/search/photos?query={query}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return Ok(result);
                }

                return BadRequest("Error fetching images from Unsplash");
            }
        }
    }
}
