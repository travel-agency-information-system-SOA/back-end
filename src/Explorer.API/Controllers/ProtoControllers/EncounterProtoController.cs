using System;
using System.Net.Security;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceTranscoding;
using Microsoft.AspNetCore.Mvc;
using Encounter = GrpcServiceTranscoding.Encounter;


public class EncounterProtoController : Encounter.EncounterBase
{
    private readonly ILogger<EncounterProtoController> _logger;

    public EncounterProtoController(ILogger<EncounterProtoController> logger)
    {
        _logger = logger;
    }

    public override async Task<SocialEnc> CreateSocialEncounter(SocialEnc request, ServerCallContext context)
    {
        Console.WriteLine("USAO OVDE BEK");
        Console.WriteLine("REQUEST:");
        Console.WriteLine(request);
        Console.WriteLine("Name:", request.Name);
        Console.WriteLine("Descrpiton:", request.Description);
        Console.WriteLine("XpPoints:", request.XpPoints);

        var httpHandler = new HttpClientHandler();
        httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        
        var channel = GrpcChannel.ForAddress("http://encounters:4000", new GrpcChannelOptions { HttpHandler = httpHandler });

        var client = new Encounter.EncounterClient(channel);
       
        var baseEncounterResponse = await client.CreateSocialEncounterAsync(request);

        Console.WriteLine("Response received:");
        Console.WriteLine(baseEncounterResponse);

        return await Task.FromResult(baseEncounterResponse); //ovi koji imaju listu
    }

    public override async Task<HiddenLocationEnc> CreateHiddenLocationEncounter(HiddenLocationEnc request, ServerCallContext context)
    {
        try
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            var channel = GrpcChannel.ForAddress("http://encounters:4000", new GrpcChannelOptions { HttpHandler = httpHandler });
            var client = new Encounter.EncounterClient(channel);

            var response = await client.CreateHiddenLocationEncounterAsync(request);

            _logger.LogInformation("Response received: {Response}", response);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating hidden location encounter");
            Console.WriteLine("EXCEPTION:::: " + ex);
            return null;
        }
    }

    public override async Task<ListEnc> GetAllEncounters(PageRequest request, ServerCallContext context)
    {
        Console.WriteLine("USAO GET ALL ENC");
        Console.WriteLine("Request:");
        Console.WriteLine(request);

        var httpHandler = new HttpClientHandler();
        httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        var channel = GrpcChannel.ForAddress("http://encounters:4000", new GrpcChannelOptions { HttpHandler = httpHandler });

        var client = new Encounter.EncounterClient(channel);

        var response = await client.GetAllEncountersAsync(request);

        Console.WriteLine("Response:");
        Console.WriteLine(response);

        var encounters = new List<Enc>();

        foreach (var enc in response.Results)
        {
            encounters.Add(new Enc
            {
                Id = enc.Id,
                Name = enc.Name,
                Description = enc.Description,
                XpPoints = enc.XpPoints,
                Type = enc.Type,
                Latitude = enc.Latitude,
                Longitude = enc.Longitude,
                ShouldBeApproved = enc.ShouldBeApproved,
            });
        }

        var totalCount = encounters.Count;

        var ListEnc = new ListEnc
        {
            Results = { encounters },
            TotalCount = totalCount
        };

        return ListEnc;
    }
 
}