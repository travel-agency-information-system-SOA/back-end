using System;
using System.Net.Security;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.Core.Domain;
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

    public override async Task<HiddenLocationEnc> CreateHiddenLocationEncounter (HiddenLocationEnc request, ServerCallContext context)
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
}