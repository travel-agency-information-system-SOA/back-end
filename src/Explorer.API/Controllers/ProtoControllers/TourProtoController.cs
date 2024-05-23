using System;
using FluentResults;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Authorization;
using GrpcServiceTranscoding;

namespace Explorer.API.Controllers.ProtoControllers
{
    public class TourProtoController : Tour.TourBase
    {
        private readonly ILogger<TourProtoController> _logger;

        public TourProtoController(ILogger<TourProtoController> logger, HttpClient httpClient)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        //[Authorize(Roles = "author, tourist")] ovo mi treba?
        public override async Task<TourDto> Create(TourDto message,
           ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

            //var client = new TourService.TourServiceClient(channel); izmena?
            var client = new Tour.TourClient(channel);
            var response = await client.Create(message);

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


        //UserIdRequest, kako da uzmem iz putanje ?
        public override async Task<ActionResult<PagedResult<TourDto>>> GetByUserId(UserIdRequest request, ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });
            var client = new Tour.TourClient(channel);
            var response = await client.GetToursByUserId(request); //metoda go ?

            // Mapiranje vrednosti iz response-a na listu TourDto objekata
            var tours = new List<TourDto>();
            foreach (var tour in response.Tours)
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


            var pagedResult = new PagedResult<TourDto>(tours, tours.Count);
            return Ok(pagedResult);
        }
    }


    //TourIdRequest, napraviti dto ?

    public override async Task<ActionResult> Delete(TourIdRequest request, ServerCallContext context)
    {
        var httpHandler = new HttpClientHandler();
        httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

        var client = new Tour.TourClient(channel);

        var response = await client.DeleteTour(request);//izmena ?? // Koristimo metodu za brisanje ture iz generisanog gRPC klijenta

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

    //TourIdRequest ?? 
    public override async Task<ActionResult> Publish(TourIdRequest request, ServerCallContext context)
    {
        var httpHandler = new HttpClientHandler();
        httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

        var client = new Tour.TourClient(channel);

        var response = await client.PublishTour(request); // Koristimo metodu za objavljivanje ture iz generisanog gRPC klijenta

        if (response.Success) 
        {
            return Ok("Tour published successfully."); 
        }
        else
        {
            return StatusCode(500, response.Error);
        }
    }

    //TourIdRequest izmena!

    public override async Task<ActionResult> Archive(TourIdRequest request, ServerCallContext context)
    {
        var httpHandler = new HttpClientHandler();
        httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        var channel = GrpcChannel.ForAddress("http://localhost:3000", new GrpcChannelOptions { HttpHandler = httpHandler });

        var client = new Tour.TourClient(channel);

        var response = await client.ArchiveTour(request); // Koristimo metodu za arhiviranje ture iz generisanog gRPC klijenta

        if (response.Success) 
        {
            return Ok("Tour archived successfully."); 
        }
        else
        {
            return StatusCode(500, response.Error); 
        }
    }
}
