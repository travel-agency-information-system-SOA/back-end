using System;
using System.Net.Security;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.Core.Domain;
using Explorer.Tours.API.Dtos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceTranscoding;
using Microsoft.AspNetCore.Mvc;
using Tour = GrpcServiceTranscoding.Tour;

namespace Explorer.API.Controllers.ProtoControllers
{
    public class TourProtoController : Tour.TourBase
    {
        private readonly ILogger<TourProtoController> _logger;

        public TourProtoController(ILogger<TourProtoController> logger)
        {
            _logger = logger;
        }

 
        public override async Task<TourDto> Create(GrpcServiceTranscoding.TourDto message,
           ServerCallContext context)
        {
            Console.WriteLine("USAO CREATE:");
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://tours:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

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


        public override async Task<TourListResponse> GetByUserId(PageRequest request, ServerCallContext context)
        {
            Console.WriteLine("USAO GET BY USER ID:");
            Console.WriteLine("Request:");
            Console.WriteLine(request);
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://tours:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

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
            var channel = GrpcChannel.ForAddress("http://tours:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

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










        //TourIdRequest, napraviti dto ?
        /*
             public override async Task<ActionResult> Delete(TourIdRequest request, ServerCallContext context)
             {
                 var httpHandler = new HttpClientHandler();
                 httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                 var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

                 var client = new Tour.TourClient(channel);

                 var response = await client.DeleteTourAsync(request);//izmena ?? // Koristimo metodu za brisanje ture iz generisanog gRPC klijenta

                 if (response.Success) 
                 {
                     return Ok("Tour deleted successfully."); 
                 }
                 else
                 {
                     return StatusCode(500, response.Error);
                 }
             }

             //AddCaracteristics metoda - PROVERITI, ovo je json.. zajebano

             //TourIdRequest izmena!

             public override async Task<ActionResult> Archive(TourIdRequest request, ServerCallContext context)
             {
                 var httpHandler = new HttpClientHandler();
                 httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                 var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

                 var client = new Tour.TourClient(channel);

                 var response = await client.ArchiveTourAsync(request); // Koristimo metodu za arhiviranje ture iz generisanog gRPC klijenta

                 if (response.Success) 
                 {
                     return Ok("Tour archived successfully."); 
                 }
                 else
                 {
                     return StatusCode(500, response.Error); 
                 }
             }*/
    }
}
