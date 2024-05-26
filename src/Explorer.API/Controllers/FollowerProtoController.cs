using Explorer.API.FollowerDtos;
using Explorer.Blog.API.Public;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using GrpcServiceTranscoding;
using Explorer.Blog.Core.UseCases;
using NuGet.Packaging;
using System.Xml.Linq;
using System;

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
			Console.WriteLine("USAO JE U CREATE NEW FOLLOWER");
			var httpHandler = new HttpClientHandler();
			httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
			var channel = GrpcChannel.ForAddress("http://followers:8090", new GrpcChannelOptions { HttpHandler = httpHandler });

			var client = new Follower.FollowerClient(channel);
			var response = await client.CreateNewFollowingAsync(following);
            Console.WriteLine("OVO JE RESPONSEEE");
            Console.WriteLine(response);

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
			Console.WriteLine("Usao je GetFollowingsWithBlogs!!!!!!!!!!!");
			var httpHandler = new HttpClientHandler();
			httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
			var channel = GrpcChannel.ForAddress("http://followers:8090", new GrpcChannelOptions { HttpHandler = httpHandler });

			var client = new Follower.FollowerClient(channel);
			var followers = await client.FindUserFollowingsAsync(id); 
			var allBlogPosts = new List<GrpcServiceTranscoding.BlogPostDto>();

			foreach (var follower in followers.ResponseList)
			{
				var blogsResult = _blogPostService.GetAllByAuthorIds(follower.Id);
                Console.WriteLine("valueeeee");
                Console.WriteLine(blogsResult.Value);

				//if (blogsResult.IsSuccess) //proveriti
				//{
					var blogs = blogsResult.Value;

					foreach (var blogPost in blogs)
					{
						var newBlogPost = new GrpcServiceTranscoding.BlogPostDto
						{
							Id = blogPost.Id,
							AuthorId = blogPost.AuthorId,
							TourId = blogPost.TourId,
                            AuthorUsername = blogPost.AuthorUsername ?? "",
                            Title = blogPost.Title,
							Description = blogPost.Description,
							CreationDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(blogPost.CreationDate), // Convert DateTime to Timestamp
																														 //ImageURLs = new List<string>(blogPost.ImageURLs), // Create a new List to make it mutable
																														 //Ratings = new List<BlogPostRatingDto>(blogPost.Ratings), // Create a new List to make it mutable
																														 //Comments = new List<BlogPostCommentDto>(blogPost.Comments), // Create a new List to make it mutable
							Status = blogPost.Status,
						};
						allBlogPosts.Add(newBlogPost);
					}
				//}
			}

            Console.WriteLine("BLOG POSTTT");
            Console.WriteLine(allBlogPosts);

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
