using Explorer.API.FollowerDtos;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Stakeholders.API.Dtos;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using GrpcServiceTranscoding;

namespace Explorer.API.Controllers
{
	public class FollowerProtoController : Follower.FollowerBase
	{
		private readonly ILogger<FollowerProtoController> _logger;
		public FollowerProtoController(ILogger<FollowerProtoController> logger)
		{
			_logger = logger;
		}

		public override async Task<GrpcServiceTranscoding.NeoFollowerDto> CreateNewFollowing(GrpcServiceTranscoding.UserFollowingDto following,
			ServerCallContext context)
		{
			var httpHandler = new HttpClientHandler();
			httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
			var channel = GrpcChannel.ForAddress("http://localhost:8090", new GrpcChannelOptions { HttpHandler = httpHandler });

			var client = new Follower.FollowerClient(channel);
			var response = await client.CreateNewFollowingAsync(following);

			// Console.WriteLine(response.AccessToken);

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
			var channel = GrpcChannel.ForAddress("http://localhost:8090", new GrpcChannelOptions { HttpHandler = httpHandler });

			var client = new Follower.FollowerClient(channel);
			//var response = await client.GetUserRecommendations(id);

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
			var channel = GrpcChannel.ForAddress("http://localhost:8090", new GrpcChannelOptions { HttpHandler = httpHandler });

			var client = new Follower.FollowerClient(channel);
			//var response = await client.GetFollowingsWithBlogs(id);

			// Console.WriteLine(response.AccessToken);

			return await Task.FromResult(new ListBlogPostDto
			{
				ResponseList = { response.ResponseList }
			});
		}

		public override async Task<ListNeoUserDto> FindUserFollowings(id id,
			   ServerCallContext context)
		{
			var httpHandler = new HttpClientHandler();
			httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
			var channel = GrpcChannel.ForAddress("http://localhost:8090", new GrpcChannelOptions { HttpHandler = httpHandler });

			var client = new Follower.FollowerClient(channel);
			//var response = await client.GetFollowersAsync(id);

			// Console.WriteLine(response.AccessToken);

			return await Task.FromResult(new ListNeoUserDto
			{
				ResponseList = { response.ResponseList }
			});
		}

	}
}
