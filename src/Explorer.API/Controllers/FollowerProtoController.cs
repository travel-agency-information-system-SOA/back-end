using Explorer.API.FollowerDtos;
using Explorer.Blog.API.Public;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using GrpcServiceTranscoding;
using Explorer.Blog.Core.UseCases;
using NuGet.Packaging;

namespace Explorer.API.Controllers
{
	public class FollowerProtoController : Follower.FollowerBase
	{
		private readonly ILogger<FollowerProtoController> _logger;
		private readonly IBlogPostService _blogPostService;
		public FollowerProtoController(ILogger<FollowerProtoController> logger, IBlogPostService blogPostService)
		{
			_logger = logger;
			_blogPostService = blogPostService;
		}

		public override async Task<GrpcServiceTranscoding.NeoFollowerDto> CreateNewFollowing(GrpcServiceTranscoding.UserFollowingDto following,
			ServerCallContext context)
		{
			var httpHandler = new HttpClientHandler();
			httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
			var channel = GrpcChannel.ForAddress("http://followers:8090", new GrpcChannelOptions { HttpHandler = httpHandler });

			var client = new Follower.FollowerClient(channel);
			var response = await client.CreateNewFollowingAsync(following);

			return await Task.FromResult(new GrpcServiceTranscoding.NeoFollowerDto
			{
				UserId = response.UserId,
				Username = response.Username,
				FollowingUserId = response.FollowingUserId,
				FollowingUsername = response.FollowingUsername
			});
		}


		public override async Task<ListNeoUserDto> GetUserRecommendations(id id,
			   ServerCallContext context)
		{
			var httpHandler = new HttpClientHandler();
			httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
			var channel = GrpcChannel.ForAddress("http://followers:8090", new GrpcChannelOptions { HttpHandler = httpHandler });

			var client = new Follower.FollowerClient(channel);
			var response = await client.GetUserRecommendationsAsync(id);

			return await Task.FromResult(new ListNeoUserDto
			{
				ResponseList = { response.ResponseList }
			});
		}


		public override async Task<ListBlogPostDto> GetFollowingsWithBlogs(id id,
		 ServerCallContext context)
		{
			var httpHandler = new HttpClientHandler();
			httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
			var channel = GrpcChannel.ForAddress("http://followers:8090", new GrpcChannelOptions { HttpHandler = httpHandler });

			var client = new Follower.FollowerClient(channel);
			var followers = await client.FindUserFollowingsAsync(id); 
			var allBlogPosts = new List<GrpcServiceTranscoding.BlogPostDto>();

			foreach (var follower in followers.ResponseList)
			{
				var blogsResult = _blogPostService.GetAllByAuthorIds(follower.Id);

				if (blogsResult.IsSuccess) //proveriti
				{
					var blogs = blogsResult.Value;
					allBlogPosts.AddRange((IEnumerable<BlogPostDto>)blogs);
				}
			}

			return await Task.FromResult(new ListBlogPostDto
			{
				ResponseList = { allBlogPosts }
			});
		}

		public override async Task<ListNeoUserDto> FindUserFollowings(id id,
			   ServerCallContext context)
		{

			Console.WriteLine("USAO JE FindUserFollowings!!!!");
			var httpHandler = new HttpClientHandler();
			httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
			var channel = GrpcChannel.ForAddress("http://followers:8090", new GrpcChannelOptions { HttpHandler = httpHandler });

			var client = new Follower.FollowerClient(channel);
			var response = await client.FindUserFollowingsAsync(id);
            Console.WriteLine("OVO JE RESPONSE!!!!");
            Console.WriteLine(response);

            return await Task.FromResult(new ListNeoUserDto
			{
				ResponseList = { response.ResponseList }
			});
		}

	}
}
