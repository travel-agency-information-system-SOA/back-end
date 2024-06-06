using System;
using System.Net.Security;
using Explorer.BuildingBlocks.Core.UseCases;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceTranscoding;
using Microsoft.AspNetCore.Mvc;
using Explorer.API.Protos;
//using Tour = GrpcServiceTranscoding.Tour;

namespace Explorer.API.Controllers.ProtoControllers
{
    public class TourProtoController : Tour.TourBase
    {

        private readonly ILogger<TourProtoController> _logger;

        public TourProtoController(ILogger<TourProtoController> logger)
        {
            _logger = logger;
        }

           public override async Task<TourDto> Create(TourDto message,
           ServerCallContext context)
        {
            Console.WriteLine("USAO CREATE:");
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://tours:3200", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new Tour.TourClient(channel);
            var response = await client.CreateAsync(message);
            Console.WriteLine("OVDE JE RESPONSE ZA CREATE:");
            Console.WriteLine(response);

            var newTourDto = new TourDto
            {
                Id = response.Id,
                Name = response.Name,
                PublishedDateTime = response.PublishedDateTime, //proveri date, string?
                ArchivedDateTime = response.ArchivedDateTime,
                Description = response.Description,
                DifficultyLevel = response.DifficultyLevel,
                Tags = { response.Tags },
                Price = response.Price,
                Status = response.Status,
                UserId = response.UserId,
                TourPoints = { response.TourPoints },
                TourCharacteristics = { response.TourCharacteristics },
                TourReviews = { response.TourReviews }
            };

            return newTourDto;
        }


        public override async Task<TourListResponse> GetByUserId(PageRequestTour request, ServerCallContext context)
        {
            Console.WriteLine("USAO GET BY USER ID:");
            Console.WriteLine("Request:");
            Console.WriteLine(request);
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://tours:3200", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new Tour.TourClient(channel);
            var response = await client.GetByUserIdAsync(request);
            Console.WriteLine("Response:");
            Console.WriteLine(response);
            // Mapiranje vrednosti iz response-a na listu TourDto objekata
            var tours = new List<TourDto>();
            foreach (var tour in response.Results)
            {
                tours.Add(new TourDto
                {
                    Id = tour.Id,
                    Name = tour.Name,
                    PublishedDateTime = tour.PublishedDateTime,
                    ArchivedDateTime = tour.ArchivedDateTime,
                    Description = tour.Description,
                    DifficultyLevel = tour.DifficultyLevel,
                    Tags = { tour.Tags },
                    Price = tour.Price,
                    Status = tour.Status,
                    UserId = tour.UserId,
                    TourPoints = { tour.TourPoints },
                    TourCharacteristics = { tour.TourCharacteristics },
                    TourReviews = { tour.TourReviews }
                });
            }

            // Setovanje total_count na osnovu broja elemenata u listi tours
            var totalCount = tours.Count;

            // Kreiranje TourListResponse objekta sa učitanim tura objektima i totalCount vrednošću
            var tourListResponse = new TourListResponse
            {
                Results = { tours },
                TotalCount = totalCount
            };

            return tourListResponse;
        }


       public override async Task<TourDto> Publish(TourPublishRequest request, ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://tours:3200", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new Tour.TourClient(channel);
            var response = await client.PublishAsync(request);

            var publishedTourDto = new TourDto
            {
                    Id = response.Id,
                    Name = response.Name,
                    PublishedDateTime = response.PublishedDateTime,
                    ArchivedDateTime = response.ArchivedDateTime,
                    Description = response.Description,
                    DifficultyLevel = response.DifficultyLevel,
                    Tags = { response.Tags },
                    Price = response.Price,
                    Status = response.Status,
                    UserId = response.UserId,
                    TourPoints = { response.TourPoints },
                    TourCharacteristics = { response.TourCharacteristics },
                    TourReviews = { response.TourReviews }
            };
            return publishedTourDto;
        }

        public override async Task<TourDto> Archive(TourPublishRequest request, ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://tours:3200", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new Tour.TourClient(channel);
            var response = await client.ArchiveAsync(request);

            var publishedTourDto = new TourDto
            {
                Id = response.Id,
                Name = response.Name,
                PublishedDateTime = response.PublishedDateTime,
                ArchivedDateTime = response.ArchivedDateTime,
                Description = response.Description,
                DifficultyLevel = response.DifficultyLevel,
                Tags = { response.Tags },
                Price = response.Price,
                Status = response.Status,
                UserId = response.UserId,
                TourPoints = { response.TourPoints },
                TourCharacteristics = { response.TourCharacteristics },
                TourReviews = { response.TourReviews }
            };
            return publishedTourDto;
        }

        public override async Task<TourDto> Delete(TourIdRequest request, ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://tours:3200", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new Tour.TourClient(channel);

            var response = await client.DeleteAsync(request);
            Console.WriteLine("Uspesno obrisana tura:");
            Console.WriteLine(response);
           
            // Vraćanje obrisane ture kao odgovor - provera 
            var deletedTourDto = new TourDto
            {
                Id = response.Id,
                Name = response.Name,
                PublishedDateTime = response.PublishedDateTime,
                ArchivedDateTime = response.ArchivedDateTime,
                Description = response.Description,
                DifficultyLevel = response.DifficultyLevel,
                Tags = { response.Tags },
                Price = response.Price,
                Status = response.Status,
                UserId = response.UserId,
                TourPoints = { response.TourPoints },
                TourCharacteristics = { response.TourCharacteristics },
                TourReviews = { response.TourReviews }
            };

            return deletedTourDto;
        }



    }
}
