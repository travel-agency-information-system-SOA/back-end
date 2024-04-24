using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Payments.API.Dtos;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Packaging;
using NuGet.Protocol;
using System.Net.Http;
using System.Text;


namespace Explorer.API.Controllers
{

    [Route("api/follower")]
    public class FollowerController : BaseApiController
    {
        private readonly IFollowerService _followerService;
        private readonly IUserService _userService;
        private readonly IBlogPostService _blogPostService;

        private readonly HttpClient _httpClient = new HttpClient();

        public FollowerController(IFollowerService followerService, IUserService userService, IBlogPostService blogPostService)
        {
            _followerService = followerService;
            _userService = userService;
            _blogPostService = blogPostService;
        }


        [HttpPost]
        public ActionResult<FollowerDto> Create([FromBody] FollowerDto follower)
        {
            var result = _followerService.Create(follower);

            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _followerService.Delete(id);
            return CreateResponse(result);
        }


        [HttpPut("{id:int}")]
        public ActionResult<FollowerDto> Update([FromBody] FollowerDto followerDto)
        {
            var result = _followerService.Update(followerDto);
            return CreateResponse(result);
        }

        [HttpGet("{userId:int}")]
        public ActionResult<List<FollowerDto>> GetByUserId(int userId)
        {
            var result = _followerService.GetByUserId(userId);
            return CreateResponse(result);
        }


        //create followers, izmeni povratnu vrednost za front?
        [HttpPost("/followers/{userId:int}/{followerId:int}")]
        public async Task<ActionResult<NeoFollowerDto>> Create(int userId, int followerId)
        {
            var content = new StringContent("", Encoding.UTF8, "application/json");
            var requestUri = $"http://localhost:8090/followers/{userId}/{followerId}";

            try
            {
                var response = await _httpClient.PostAsync(requestUri, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var createdFollower = JsonConvert.DeserializeObject<NeoFollowerDto>(responseContent);
                    return Ok(createdFollower);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Error occurred while creating NeoFollowerDto.");
                }
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error occurred while sending request: {ex.Message}");
            }
        }

        //get followings iskombinuj sa blogovima sa beka 
        [HttpGet("/getFollowings/{userId:int}")]
        public async Task<ActionResult<List<BlogPostDto>>> GetUserFollowings(int userId)
        {
            var requestUri = $"http://followers:8090/followers/followings/{userId}";

            try
            {
                var response = await _httpClient.GetAsync(requestUri);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var followers = JsonConvert.DeserializeObject<List<NeoUserDto>>(responseContent);

                    var blogPosts = new List<BlogPostDto>();
                    foreach (var follower in followers)
                    { 
                        //konfuzija nema smisla??
                        //var blogs = _blogPostService.GetAllByAuthorIds(follower.Id);
                        //blogPosts.Add(blogs);
                    }

                    return Ok(blogPosts);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Error occurred while getting followers for user.");
                }
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error occurred while sending request: {ex.Message}");
            }
        }


        //get all recommendations
        [HttpGet("/getAllRecomodations/{userId:int}")]
        public async Task<ActionResult<List<BlogPostDto>>> GetUserRecommodations(int userId)
        {
            var requestUri = $"http://followers:8090/followers/followings/{userId}";

            try
            {
                var response = await _httpClient.GetAsync(requestUri);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var followers = JsonConvert.DeserializeObject<List<NeoUserDto>>(responseContent);

                    var allBlogPosts = new List<BlogPostDto>(); 

                    foreach (var follower in followers)
                    {
                        var blogsResult = _blogPostService.GetAllByAuthorIds(follower.Id);

                        if (blogsResult.IsSuccess) //proveriti
                        {
                            var blogs = blogsResult.Value;
                            allBlogPosts.AddRange(blogs); 
                        }
                        else
                        {
                            // Ukoliko operacija nije uspela, možete postaviti odgovarajući status koda ili obavestiti korisnika
                            return StatusCode(500, "Error occurred while getting blog posts.");
                        }
                    }

                    return Ok(allBlogPosts); // Vraćamo jednu listu koja sadrži sve blogove
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Error occurred while getting followers for user.");
                }
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error occurred while sending request: {ex.Message}");
            }
        }





    }
}
