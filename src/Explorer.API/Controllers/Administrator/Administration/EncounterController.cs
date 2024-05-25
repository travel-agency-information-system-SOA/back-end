using Azure;
using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics.Metrics;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Explorer.API.Controllers.Administrator.Administration;

/*

//[Authorize(Policy = "administratorPolicy")]
//[Route("api/encounters")]
public class EncounterController : BaseApiController
{
    private readonly IEncounterService _encounterService;
    private readonly IHiddenLocationEncounterService _hiddenLocationEncounterService;
    private readonly ISocialEncounterService _socialEncounterService;

    //koirstimo ga za komunikaciju sa mikroservisima putem http zahteva
    private readonly HttpClient _httpClient = new HttpClient();

    public EncounterController(IEncounterService encounterService, ISocialEncounterService socialEncounterService, IHiddenLocationEncounterService hiddenLocationEncounterService)
    {
        _encounterService = encounterService;
        _socialEncounterService = socialEncounterService;
        _hiddenLocationEncounterService = hiddenLocationEncounterService;
    }

    //PREBACENO
    //[HttpGet]
    public async Task<ActionResult<PagedResult<EncounterMongoDto>>> GetAllEncounters([FromQuery] int page, [FromQuery] int pageSize)
    {
        //async Task< > - potrpis metode da ce biti asinhrono izvrsavanje

        //poziv mikroservisa za dobavljanje svih susreta
        //saljemo get zahtev ka url koji je prosledjen kao agrument
        var response = await _httpClient.GetAsync($"http://encounters:4000/encounters?page={page}&pageSize={pageSize}");

        if (response.IsSuccessStatusCode)
        {
            //ceka na asinhrono citanje sadrzaja http odgovora (kao string)
            var responseContent = await response.Content.ReadAsStringAsync();

            //DESERIJALIZUJE odgovor u List<EncounterDto>
            //mi dobijemo JSON string - sadrzaj http odgovora (kao odgovor od mikroservisa), a hocemo da ga deserijalizujemo u .Net objekte
            List<EncounterMongoDto> encounters = JsonConvert.DeserializeObject<List<EncounterMongoDto>>(responseContent);

            //kreiranje pagedResult objekta
            PagedResult<EncounterMongoDto> pagedResult = new PagedResult<EncounterMongoDto>(encounters, encounters.Count);

            return Ok(pagedResult);
        }
        else
        {
            return StatusCode((int)response.StatusCode, "Error occurred while fetching encounters.");
        }
    }

    //NE TREBA
    [HttpGet("social")]
    public async Task<ActionResult<PagedResult<SocialEncounterDto>>> GetAllSocialEncounters([FromQuery] int page, [FromQuery] int pageSize)
    {
        var response = await _httpClient.GetAsync($"http://encounters:4000/socialEncounters?page={page}&pageSize={pageSize}");

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();

            List<SocialEncounterDto> encounters = JsonConvert.DeserializeObject<List<SocialEncounterDto>>(responseContent);

            PagedResult<SocialEncounterDto> pagedResult = new PagedResult<SocialEncounterDto>(encounters, encounters.Count);

            return Ok(pagedResult);
        }
        else
        {
            return StatusCode((int)response.StatusCode, "Error occurred while fetching encounters.");
        }
    }

    //NE TREBA
    [HttpGet("hiddenLocation")]
    public async Task<ActionResult<PagedResult<HiddenLocationEncounterDto>>> GetAllHiddenLocationEncounters([FromQuery] int page, [FromQuery] int pageSize)
    {
        var response = await _httpClient.GetAsync($"http://encounters:4000/hiddenLocationEncounters?page={page}&pageSize={pageSize}");

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();

            List<HiddenLocationEncounterDto> encounters = JsonConvert.DeserializeObject<List<HiddenLocationEncounterDto>>(responseContent);

            PagedResult<HiddenLocationEncounterDto> pagedResult = new PagedResult<HiddenLocationEncounterDto>(encounters, encounters.Count);

            return Ok(pagedResult);
        }
        else
        {
            return StatusCode((int)response.StatusCode, "Error occurred while fetching encounters.");
        }
    }

    //NE TREBA
    [HttpPost]
    public async Task<ActionResult<EncounterDto>> Create([FromBody] EncounterDto encounter)
    {
        //serijalizujemo objekat u json, kako bi smo ga mogli poslati mikroservisu putem http zahteva
        string json = JsonConvert.SerializeObject(encounter);

        //koristi se za sadrzaj razlicitih zahteva
        //mime tip - tip sadrzaja http zahteva (poslednje) - oznacava da je json format 
        //omogucava slanje podataka u json formatu na server
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            //HttpResponseMessage - odgovor koji ce biti vracen nakon slanja http zahteva (statusni kod, sadrzaj, zaglavlje...)
            HttpResponseMessage response = await _httpClient.PostAsync("http://encounters:4000/encounters/create", content);

            if (response.IsSuccessStatusCode)
            {
                //citamo sadrzaj odgovora
                string responseContent = await response.Content.ReadAsStringAsync();

                EncounterDto createdEncounter = JsonConvert.DeserializeObject<EncounterDto>(responseContent);

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

    //PREBACENO
    //[HttpPost("hiddenLocation")]
    public async Task<ActionResult<WholeHiddenLocationEncounterMongoDto>> Create([FromBody] WholeHiddenLocationEncounterMongoDto wholeEncounter)
    {
        var encounterDto = new EncounterMongoDto
        {
            Name = wholeEncounter.Name,
            Description = wholeEncounter.Description,
            XpPoints = wholeEncounter.XpPoints,
            Status = wholeEncounter.Status,
            Type = wholeEncounter.Type,
            Longitude = wholeEncounter.Longitude,
            Latitude = wholeEncounter.Latitude,
            ShouldBeApproved = wholeEncounter.ShouldBeApproved
        };

        //PRVO KREIRALI OBICAN ENCOUNTER (BASE ENCOUNTER)
        //PostAsync: zahteva kreiranje content objekta, moze sadrzati bilo sta ne samo json objekte vec taj content
        //PostAsJsonAsync: omogucava slanje objekta kao jsona, objekat ce biti automatski serijalizovan kao json pre slanja
        var baseEncounterResponse = await _httpClient.PostAsJsonAsync("http://encounters:4000/encounters/create", encounterDto);

        if (!baseEncounterResponse.IsSuccessStatusCode)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, "Error occurred while creating encounter.");
        }

        //deserijalizacija odgovora u EncounterDto
        //cita sadrzaj http odgovora i deserijalizuje ga u odgovarajuci objekat iz .Net
        var createdEncounter = await baseEncounterResponse.Content.ReadFromJsonAsync<EncounterMongoDto>();

        //NA OSNOVU OBICNOG KREIRAJU HIDDEN LOCATION ENCOUNTER
        var hiddenLocationEncounterDto = new HiddenLocationEncounterMongoDto
        {
            Encounter = createdEncounter,
            ImageLatitude = wholeEncounter.ImageLatitude,
            ImageLongitude = wholeEncounter.ImageLongitude,
            ImageURL = wholeEncounter.ImageURL,
            DistanceTreshold = wholeEncounter.DistanceTreshold
        };

        var hiddenLocationEncounterResponse = await _httpClient.PostAsJsonAsync("http://encounters:4000/encounters/createHiddenLocationEncounter", hiddenLocationEncounterDto);

        if (!hiddenLocationEncounterResponse.IsSuccessStatusCode)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, "Error occurred while creating hidden location encounter.");
        }

        var createdHiddenLocationEncounter = await hiddenLocationEncounterResponse.Content.ReadFromJsonAsync<WholeHiddenLocationEncounterMongoDto>();

        return StatusCode((int)HttpStatusCode.Created, createdHiddenLocationEncounter);
    }

    //ne treba
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
     
    //PREBACENO
    //SOCIAL ENCOUNTER CEO
    //[HttpPost("social")]
    public async Task<ActionResult<WholeSocialEncounterMongoDto>> CreateSocialEncounter([FromBody] WholeSocialEncounterMongoDto socialEncounter)
    {
        EncounterMongoDto encounterDto = new EncounterMongoDto
        {
            Name = socialEncounter.Name,
            Description = socialEncounter.Description,
            XpPoints = socialEncounter.XpPoints,
            Status = socialEncounter.Status,
            Type = socialEncounter.Type,
            Longitude = socialEncounter.Longitude,
            Latitude = socialEncounter.Latitude,
            ShouldBeApproved = socialEncounter.ShouldBeApproved
        };

        //kreiranje encountera
        var baseEncounterResponse = await CreateBaseEncounterAsync(encounterDto);

        var baseEncounter = (OkObjectResult)baseEncounterResponse.Result;
        var createdEncounter = (EncounterMongoDto)baseEncounter.Value;

        Console.WriteLine("Kreiran encounter: " + createdEncounter);

        Console.WriteLine("ID of created encounter: " + createdEncounter.Id);

        SocialEncounterMongoDto socialEncounterDto = new SocialEncounterMongoDto
        {
            //EncounterId = createdEncounter.Id.ToString(),
            Encounter = createdEncounter,
            TouristsRequiredForCompletion = socialEncounter.TouristsRequiredForCompletion,
            DistanceTreshold = socialEncounter.DistanceTreshold,
            TouristIDs = socialEncounter.TouristIDs
        };

        // Pozivamo mikroservis za kreiranje socijalnog sastanka (SocialEncounter)
        var result = await CreateSocialEncounterAsync(socialEncounterDto);

        

        var wholeSocialEncounterMongoDto = new WholeSocialEncounterMongoDto
        {
            EncounterId = createdEncounter.Id,
            Name = socialEncounter.Name,
            Description = socialEncounter.Description,
            XpPoints = socialEncounter.XpPoints,
            Status = socialEncounter.Status,
            Type = socialEncounter.Type,
            Latitude = socialEncounter.Latitude,
            Longitude = socialEncounter.Longitude,
            //Id = result.Value.Id, //PROBAJ OVO DA OTKOMENTARISES
            TouristsRequiredForCompletion = socialEncounter.TouristsRequiredForCompletion,
            DistanceTreshold = socialEncounter.DistanceTreshold,
            TouristIDs = socialEncounter.TouristIDs,
            ShouldBeApproved = socialEncounter.ShouldBeApproved
        };

        return StatusCode((int)HttpStatusCode.Created, wholeSocialEncounterMongoDto);
    }

    //ne treba 
    //SOCIAL ENCOUNTER
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

    //... NE TREBA VRV unutar metode za update hidden location ili social updatovati i ovo
    [HttpPut]
    public async Task<ActionResult<EncounterDto>> Update([FromBody] EncounterDto encounter)
    {
        string json = JsonConvert.SerializeObject(encounter);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage response = await _httpClient.PutAsync("http://encounters:4000/encounters/update", content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                EncounterDto updatedEncounter = JsonConvert.DeserializeObject<EncounterDto>(responseContent);

                return Ok(updatedEncounter);
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Error occurred while updating encounter.");
            }
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Error occurred while sending request: {ex.Message}");
        }
    }

/////////////////ODAVDE NASTAVI

    [HttpPut("hiddenLocation")]
    public async Task<ActionResult<HiddenLocationEncounterDto>> Update([FromBody] WholeHiddenLocationEncounterDto wholeEncounter)
    {
        var encounterDto = new EncounterDto
        {
            Id = wholeEncounter.EncounterId,
            Name = wholeEncounter.Name,
            Description = wholeEncounter.Description,
            XpPoints = wholeEncounter.XpPoints,
            Status = wholeEncounter.Status,
            Type = wholeEncounter.Type,
            Longitude = wholeEncounter.Longitude,
            Latitude = wholeEncounter.Latitude,
            ShouldBeApproved = wholeEncounter.ShouldBeApproved
        };

        try
        {
            string encounterJson = JsonConvert.SerializeObject(encounterDto);
            HttpContent encounterContent = new StringContent(encounterJson, Encoding.UTF8, "application/json");

            HttpResponseMessage encounterResponse = await _httpClient.PutAsync("http://encounters:4000/encounters/update", encounterContent);

            if (!encounterResponse.IsSuccessStatusCode)
            {
                return StatusCode((int)encounterResponse.StatusCode, "Error occurred while updating encounter.");
            }

            var hiddenLocationEncounterDto = new HiddenLocationEncounterDto
            {
                Id = wholeEncounter.Id,
                EncounterId = encounterDto.Id,
                ImageLatitude = wholeEncounter.ImageLatitude,
                ImageLongitude = wholeEncounter.ImageLongitude,
                ImageURL = wholeEncounter.ImageURL,
                DistanceTreshold = wholeEncounter.DistanceTreshold
            };

            string hiddenLocationEncounterJson = JsonConvert.SerializeObject(hiddenLocationEncounterDto);
            HttpContent hiddenLocationEncounterContent = new StringContent(hiddenLocationEncounterJson, Encoding.UTF8, "application/json");

            HttpResponseMessage hiddenLocationEncounterResponse = await _httpClient.PutAsync("http://encounters:4000/encounters/updateHiddenLocationEncounter", hiddenLocationEncounterContent);

            if (!hiddenLocationEncounterResponse.IsSuccessStatusCode)
            {
                return StatusCode((int)hiddenLocationEncounterResponse.StatusCode, "Error occurred while updating hidden location encounter.");
            }

            return StatusCode((int)HttpStatusCode.NoContent);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Error occurred while sending request: {ex.Message}");
        }
    }


    [HttpPut("social")]
    public async Task<ActionResult<WholeSocialEncounterDto>> UpdateSocialEncounter([FromBody] WholeSocialEncounterDto socialEncounter)
    {
        var encounterDto = new EncounterDto
        {
            Id = socialEncounter.EncounterId,
            Name = socialEncounter.Name,
            Description = socialEncounter.Description,
            XpPoints = socialEncounter.XpPoints,
            Status = socialEncounter.Status,
            Type = socialEncounter.Type,
            Longitude = socialEncounter.Longitude,
            Latitude = socialEncounter.Latitude,
            ShouldBeApproved = socialEncounter.ShouldBeApproved
        };

        try
        {
            string json = JsonConvert.SerializeObject(encounterDto);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage encounterResponse = await _httpClient.PutAsync("http://encounters:4000/encounters/update", content);

            if (!encounterResponse.IsSuccessStatusCode)
            {
                return StatusCode((int)encounterResponse.StatusCode, "Error occurred while updating encounter.");
            }

            string encounterResponseContent = await encounterResponse.Content.ReadAsStringAsync();

            EncounterDto updatedEncounter = JsonConvert.DeserializeObject<EncounterDto>(encounterResponseContent);

            var socialEncounterDto = new SocialEncounterDto
            {
                Id = socialEncounter.Id,
                EncounterId = updatedEncounter.Id,
                TouristsRequiredForCompletion = socialEncounter.TouristsRequiredForCompletion,
                DistanceTreshold = socialEncounter.DistanceTreshold,
                TouristIDs = socialEncounter.TouristIDs
            };

            HttpResponseMessage socialEncounterResponse = await _httpClient.PutAsync("http://encounters:4000/encounters/updateSocialEncounter", content);

            if (!socialEncounterResponse.IsSuccessStatusCode)
            {
                return StatusCode((int)socialEncounterResponse.StatusCode, "Error occurred while updating social encounter.");
            }

            return StatusCode((int)HttpStatusCode.NoContent, socialEncounterDto);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Error occurred while sending request: {ex.Message}");
        }
    }
///////////////////////////////////////////////////////

    //ne treba
    [HttpGet("getEncounter/{encounterId:int}")]
    public async Task<ActionResult<EncounterDto>> GetEncounter(int encounterId)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"http://encounters:4000/encounters/getEncounterById/{encounterId}");

        if (response.IsSuccessStatusCode)
        {
            //procita odgovor http zahteva kao json i odmah ga deserijalizuje u odgovarajuci objekat
            var encounter = await response.Content.ReadAsAsync<EncounterDto>();
            return encounter;
        }
        else
        {
            throw new HttpRequestException($"Failed to retrieve data from microservice. Status code: {response.StatusCode}");
        }
    }

    //videti da li i delete
    [HttpDelete("{baseEncounterId:int}")]
    public async Task<ActionResult> DeleteEncounter(int baseEncounterId)
    {
        var baseEncounterResponse = await DeleteEncounterAsync(baseEncounterId);

        if (baseEncounterResponse.IsSuccessStatusCode || baseEncounterResponse.StatusCode == HttpStatusCode.NoContent)
        {
            var socialEncounterIdResponse = await GetSocialEncounterIdAsync(baseEncounterId);

            //ovo sam radila jer bi mi bio potreban dodatni dto 
            //citamo odgovor kao json string
            string jsonResponse1 = await socialEncounterIdResponse.Content.ReadAsStringAsync();
            //json string konvertujemo u json objekat
            JObject jsonObject1 = JObject.Parse(jsonResponse1);
            //odavde (iz json objekta) izvlacimo vrednost polja socialEncounterId 
            int socialEncounterId = (int)jsonObject1["socialEncounterId"];

            var hiddenLocationEncounterIdResponse = await GetHiddenLocationEncounterIdAsync(baseEncounterId);

            string jsonResponse2 = await hiddenLocationEncounterIdResponse.Content.ReadAsStringAsync();
            JObject jsonObject2 = JObject.Parse(jsonResponse2);
            int hiddenLocationEncounterId = (int)jsonObject2["hiddenLocationEncounterId"];

            if (socialEncounterId != -1)
            {
                var socialEncounterResponse = await DeleteSocialEncounterAsync(socialEncounterId);
                return CreateResponse(socialEncounterResponse);
            }
            else if (hiddenLocationEncounterId != -1)
            {
                var hiddenLocationEncounterResponse = await DeleteHiddenLocationEncounterAsync(hiddenLocationEncounterId);
                return CreateResponse(hiddenLocationEncounterResponse);
            }
        }

        return CreateResponse(baseEncounterResponse);
    }

    private async Task<HttpResponseMessage> DeleteEncounterAsync(int baseEncounterId)
    {
        return await _httpClient.DeleteAsync($"http://encounters:4000/encounters/deleteEncounter/{baseEncounterId}");
    }

    private async Task<HttpResponseMessage> GetSocialEncounterIdAsync(int baseEncounterId)
    {
        return await _httpClient.GetAsync($"http://encounters:4000/encounters/getSocialEncounterId/{baseEncounterId}");
    }

    private async Task<HttpResponseMessage> GetHiddenLocationEncounterIdAsync(int baseEncounterId)
    {
        return await _httpClient.GetAsync($"http://encounters:4000/encounters/getHiddenLocationEncounterId/{baseEncounterId}");
    }

    private async Task<HttpResponseMessage> DeleteSocialEncounterAsync(long socialEncounterId)
    {
        return await _httpClient.DeleteAsync($"http://encounters:4000/encounters/deleteSocialEncounter/{socialEncounterId}");
    }

    private async Task<HttpResponseMessage> DeleteHiddenLocationEncounterAsync(long hiddenLocationEncounterId)
    {
        return await _httpClient.DeleteAsync($"http://encounters:4000/encounters/deleteHiddenLocationEncounter/{hiddenLocationEncounterId}");
    }

    private ActionResult CreateResponse(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return Ok("Encounter deleted successfully.");
        }
        else
        {
            return StatusCode((int)response.StatusCode, "Failed to delete encounter.");
        }
    }

    [HttpGet("hiddenLocation/{encounterId:int}")]
    public async Task<ActionResult<HiddenLocationEncounterDto>> GetHiddenLocationEncounterByEncounterId(int encounterId)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"http://encounters:4000/encounters/getHiddenLocationEncounter/{encounterId}");

        if (response.IsSuccessStatusCode)
        {
            var hiddenLocationEncounter = await response.Content.ReadAsAsync<HiddenLocationEncounterDto>();
            return hiddenLocationEncounter;
        }
        else
        {
            throw new HttpRequestException($"Failed to retrieve data from microservice. Status code: {response.StatusCode}");
        }
    }
}
*/