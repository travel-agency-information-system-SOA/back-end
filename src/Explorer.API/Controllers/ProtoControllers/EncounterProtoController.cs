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

    /*
    public override async Task<GetAllEncountersResponse> GetAllEncounters(GetAllEncountersRequest request, ServerCallContext context)
    {
        var httpHandler = new HttpClientHandler();
        httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        //OVDE MOZDA NIJE TAJ LOCALHOST
        var channel = GrpcChannel.ForAddress("http://localhost:81", new GrpcChannelOptions { HttpHandler = httpHandler });

        var client = new Encounter.EncounterClient(channel);
        var response = await _httpClient.GetAsync($"http://encounters:4000/encounters?page={request.Page}&pageSize={request.PageSize}");

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var encounters = JsonConvert.DeserializeObject<List<GrpcServiceTranscoding.EncounterMongoDto>>(responseContent);

            var encounterResponses = encounters.ConvertAll(e => new GrpcServiceTranscoding.EncounterMongoDto
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                XpPoints = e.XpPoints,
                Status = e.Status,
                Type = e.Type,
                Latitude = e.Latitude,
                Longitude = e.Longitude,
                ShouldBeApproved = e.ShouldBeApproved
            });

            return new GetAllEncountersResponse
            {
                Encounters = { encounterResponses },
                TotalItems = encounters.Count
            };
        }
        else
        {
            throw new RpcException(new(StatusCode.Internal, "Error occurred while fetching encounters."));
        }
    }
    */


    /*
    public override async Task<WholeHiddenLocationEncounterResponse> CreateWholeHiddenLocationEncounter(WholeHiddenLocationEncounterRequest request, ServerCallContext context)
    {
        var encounterDto = new GrpcServiceTranscoding.EncounterMongoDto
        {
            Name = request.Name,
            Description = request.Description,
            XpPoints = request.XpPoints,
            Status = request.Status,
            Type = request.Type,
            Longitude = request.Longitude,
            Latitude = request.Latitude,
            ShouldBeApproved = request.ShouldBeApproved
        };

        var baseEncounterResponse = await _httpClient.PostAsJsonAsync("http://encounters:4000/encounters/create", encounterDto);

        if (!baseEncounterResponse.IsSuccessStatusCode)
        {
            throw new RpcException(new (StatusCode.Internal, "Error occurred while creating encounter."));
        }

        var createdEncounter = await baseEncounterResponse.Content.ReadFromJsonAsync<GrpcServiceTranscoding.EncounterMongoDto>();

        var hiddenLocationEncounterDto = new GrpcServiceTranscoding.HiddenLocationEncounterMongoDto
        {
            Encounter = createdEncounter,
            ImageLatitude = request.ImageLatitude,
            ImageLongitude = request.ImageLongitude,
            DistanceTreshold = request.DistanceTreshold
        };

        var hiddenLocationEncounterResponse = await _httpClient.PostAsJsonAsync("http://encounters:4000/encounters/createHiddenLocationEncounter", hiddenLocationEncounterDto);

        if (!hiddenLocationEncounterResponse.IsSuccessStatusCode)
        {
            throw new RpcException(new (StatusCode.Internal, "Error occurred while creating hidden location encounter."));
        }

        var createdHiddenLocationEncounter = await hiddenLocationEncounterResponse.Content.ReadFromJsonAsync<GrpcServiceTranscoding.HiddenLocationEncounterMongoDto>();

        return new WholeHiddenLocationEncounterResponse
        {
            Id = createdHiddenLocationEncounter.Id,
            ImageLatitude = createdHiddenLocationEncounter.ImageLatitude,
            ImageLongitude = createdHiddenLocationEncounter.ImageLongitude,
            DistanceTreshold = createdHiddenLocationEncounter.DistanceTreshold
        };
    }
    */

    public override async Task<GrpcServiceTranscoding.WholeSocialEncounterMongoDto> CreateSocialEncounter(GrpcServiceTranscoding.WholeSocialEncounterMongoDto request, ServerCallContext context)
    {
        Console.WriteLine("USAO OVDE BEK");
        Console.WriteLine("Name:", request.Name);
        Console.WriteLine("Descrpiton:", request.Description);
        Console.WriteLine("XpPoints:", request.XpPoints);
        var httpHandler = new HttpClientHandler();
        httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        
        var channel = GrpcChannel.ForAddress("http://encounters:4000", new GrpcChannelOptions { HttpHandler = httpHandler });

        var client = new GrpcServiceTranscoding.Encounter.EncounterClient(channel);
       
        var baseEncounterResponse = await client.CreateSocialEncounterAsync(request);

        // Print the response content
        Console.WriteLine("Response received:");
        Console.WriteLine(baseEncounterResponse);

        return await Task.FromResult(baseEncounterResponse); //ovi koji imaju listu
    }

    //SOCIAL ENCOUNTER
    /*
     * ovo verovtno nije potrebno!
    private async Task<ActionResult<SocialEncounterMongoDto>> CreateSocialEncounterAsync(SocialEncounterMongoDto socialEncounterDto)
    {
        string json = JsonConvert.SerializeObject(socialEncounterDto);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
        try
        {
            HttpResponseMessage response = await _httpClient.PostAsync("http://encounters:4000/encounters/createSocialEncounter", content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                SocialEncounterMongoDto createdSocialEncounter = JsonConvert.DeserializeObject<SocialEncounterMongoDto>(responseContent);
                return Ok(createdSocialEncounter);
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Error occurred while creating social encounter.");
            }
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Error occurred while sending request: {ex.Message}");
        }
    }
    */

    /*
     * - ni ovo verovatno nije potrebno!

    private async Task<ActionResult<EncounterMongoDto>> CreateBaseEncounterAsync(EncounterMongoDto encounterDto)
    {
        string json = JsonConvert.SerializeObject(encounterDto);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
        try
        {
            HttpResponseMessage response = await _httpClient.PostAsync("http://encounters:4000/encounters/create", content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                EncounterMongoDto createdEncounter = JsonConvert.DeserializeObject<EncounterMongoDto>(responseContent);
                return Ok(createdEncounter);
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Error occurred while creating encounter.");
            }
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Error occurred while sending request: {ex.Message}");
        }
    }
    */


}