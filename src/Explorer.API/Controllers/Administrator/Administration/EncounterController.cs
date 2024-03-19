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

//[Authorize(Policy = "administratorPolicy")]
[Route("api/encounters")]
public class EncounterController : BaseApiController
{
    private readonly IEncounterService _encounterService;
    private readonly IHiddenLocationEncounterService _hiddenLocationEncounterService;

    private readonly ISocialEncounterService _socialEncounterService;

    private readonly HttpClient _httpClient = new HttpClient();
    public EncounterController(IEncounterService encounterService, ISocialEncounterService socialEncounterService, IHiddenLocationEncounterService hiddenLocationEncounterService)
    {
        _encounterService = encounterService;
        _socialEncounterService = socialEncounterService;
        _hiddenLocationEncounterService = hiddenLocationEncounterService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<EncounterDto>>> GetAllEncounters([FromQuery] int page, [FromQuery] int pageSize)
    {
        // Poziv mikroservisa za dobavljanje svih susreta
        var response = await _httpClient.GetAsync($"http://localhost:4000/encounters?page={page}&pageSize={pageSize}");

        // Provera statusa odgovora
        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();

            // Deserializujete odgovor u List<EncounterDto>
            List<EncounterDto> encounters = JsonConvert.DeserializeObject<List<EncounterDto>>(responseContent);

            // Kreiranje PagedResult objekta
            PagedResult<EncounterDto> pagedResult = new PagedResult<EncounterDto>(encounters, encounters.Count);

            // Vraćanje PagedResult<EncounterDto> kao rezultat akcije.
            return Ok(pagedResult);
        }
        else
        {
            // Vraćanje odgovarajućeg statusa u slučaju greške
            return StatusCode((int)response.StatusCode, "Error occurred while fetching encounters.");
        }
    }

    [HttpGet("social")]
    public async Task<ActionResult<PagedResult<SocialEncounterDto>>> GetAllSocialEncounters([FromQuery] int page, [FromQuery] int pageSize)
    {
        // Poziv mikroservisa za dobavljanje svih socijalnih susreta
        var response = await _httpClient.GetAsync($"http://localhost:4000/socialEncounters?page={page}&pageSize={pageSize}");

        // Provera statusa odgovora
        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();

            // Deserializujete odgovor u List<EncounterDto>
            List<SocialEncounterDto> encounters = JsonConvert.DeserializeObject<List<SocialEncounterDto>>(responseContent);

            // Kreiranje PagedResult objekta
            PagedResult<SocialEncounterDto> pagedResult = new PagedResult<SocialEncounterDto>(encounters, encounters.Count);

            // Vraćanje PagedResult<EncounterDto> kao rezultat akcije.
            return Ok(pagedResult);
        }
        else
        {
            // Vraćanje odgovarajućeg statusa u slučaju greške
            return StatusCode((int)response.StatusCode, "Error occurred while fetching encounters.");
        }
    }

    [HttpGet("hiddenLocation")]
    public async Task<ActionResult<PagedResult<HiddenLocationEncounterDto>>> GetAllHiddenLocationEncounters([FromQuery] int page, [FromQuery] int pageSize)
    {
        // Poziv mikroservisa za dobavljanje svih susreta na skrivenim lokacijama
        var response = await _httpClient.GetAsync($"http://localhost:4000/hiddenLocationEncounters?page={page}&pageSize={pageSize}");

        // Provera statusa odgovora
        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();

            // Deserializujete odgovor u List<EncounterDto>
            List<HiddenLocationEncounterDto> encounters = JsonConvert.DeserializeObject<List<HiddenLocationEncounterDto>>(responseContent);

            // Kreiranje PagedResult objekta
            PagedResult<HiddenLocationEncounterDto> pagedResult = new PagedResult<HiddenLocationEncounterDto>(encounters, encounters.Count);

            // Vraćanje PagedResult<EncounterDto> kao rezultat akcije.
            return Ok(pagedResult);
        }
        else
        {
            // Vraćanje odgovarajućeg statusa u slučaju greške
            return StatusCode((int)response.StatusCode, "Error occurred while fetching encounters.");
        }
    }

    //mikroservisi
    [HttpPost]
    public async Task<ActionResult<EncounterDto>> Create([FromBody] EncounterDto encounter)
    {
        //var result = _encounterService.Create(encounter);
        //return CreateResponse(result);

        // Serijalizujemo objekat u JSON
        string json = JsonConvert.SerializeObject(encounter);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
        try
        {
            // Šaljemo POST zahtev na mikroservis
            HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:4000/encounters/create", content);

            // Proveravamo status odgovora
            if (response.IsSuccessStatusCode)
            {
                // Ako je odgovor uspešan, čitamo sadržaj odgovora
                string responseContent = await response.Content.ReadAsStringAsync();

                // Deserijalizujemo JSON odgovor u TourDTO objekat
                EncounterDto createdEncounter = JsonConvert.DeserializeObject<EncounterDto>(responseContent);

                // Vraćamo OK rezultat sa kreiranim turizmom
                return Ok(createdEncounter);
            }
            else
            {
                // Ako je došlo do greške, vraćamo odgovarajući HTTP status
                return StatusCode((int)response.StatusCode, "Error occurred while creating encounter.");
            }
        }
        catch (HttpRequestException ex)
        {
            // Uhvatamo eventualne greške prilikom slanja zahteva
            return StatusCode(500, $"Error occurred while sending request: {ex.Message}");
        }
    }

    //HIDDEN LOCATION ENCOUNTER

    [HttpPost("hiddenLocation")]
    public async Task<ActionResult<WholeHiddenLocationEncounterDto>> Create([FromBody] WholeHiddenLocationEncounterDto wholeEncounter)
    {
        // Kreiranje susreta
        var encounterDto = new EncounterDto
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

        // Slanje POST zahteva za kreiranje susreta
        var baseEncounterResponse = await _httpClient.PostAsJsonAsync("http://localhost:4000/encounters/create", encounterDto);

        if (!baseEncounterResponse.IsSuccessStatusCode)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, "Error occurred while creating encounter.");
        }

        // Deserijalizacija odgovora u EncounterDto
        var createdEncounter = await baseEncounterResponse.Content.ReadFromJsonAsync<EncounterDto>();

        // Kreiranje skrivenog susreta
        var hiddenLocationEncounterDto = new HiddenLocationEncounterDto
        {
            EncounterId = createdEncounter.Id,
            ImageLatitude = wholeEncounter.ImageLatitude,
            ImageLongitude = wholeEncounter.ImageLongitude,
            ImageURL = wholeEncounter.ImageURL,
            DistanceTreshold = wholeEncounter.DistanceTreshold
        };

        // Slanje POST zahteva za kreiranje skrivenog susreta
        var hiddenLocationEncounterResponse = await _httpClient.PostAsJsonAsync("http://localhost:4000/encounters/createHiddenLocationEncounter", hiddenLocationEncounterDto);

        if (!hiddenLocationEncounterResponse.IsSuccessStatusCode)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, "Error occurred while creating hidden location encounter.");
        }

        // Deserijalizacija odgovora u WholeHiddenLocationEncounterDto
        var createdHiddenLocationEncounter = await hiddenLocationEncounterResponse.Content.ReadFromJsonAsync<WholeHiddenLocationEncounterDto>();

        return StatusCode((int)HttpStatusCode.Created, createdHiddenLocationEncounter);
    }


    //ENCOUNTER
    private async Task<ActionResult<EncounterDto>> CreateBaseEncounterAsync(EncounterDto encounterDto)
    {
        string json = JsonConvert.SerializeObject(encounterDto);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
        try
        {
            HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:4000/encounters/create", content);

            if (response.IsSuccessStatusCode)
            {
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
     
    //SOCIAL ENCOUNTER CEO
    [HttpPost("social")]
    public async Task<ActionResult<WholeSocialEncounterDto>> CreateSocialEncounter([FromBody] WholeSocialEncounterDto socialEncounter)
    {
        EncounterDto encounterDto = new EncounterDto
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

        var baseEncounterResponse = await CreateBaseEncounterAsync(encounterDto); // Pozivamo metodu za kreiranje osnovnog sastanka

        /*
        if (baseEncounterResponse.Value == null)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, "Error occurred while creating encounter."); // Vraćamo BadRequest ako je rezultat null
        }
        */

        var baseEncounter = (OkObjectResult)baseEncounterResponse.Result;
        var createdEncounter = (EncounterDto)baseEncounter.Value;

        SocialEncounterDto socialEncounterDto = new SocialEncounterDto
        {
            EncounterId = createdEncounter.Id,
            TouristsRequiredForCompletion = socialEncounter.TouristsRequiredForCompletion,
            DistanceTreshold = socialEncounter.DistanceTreshold,
            TouristIDs = socialEncounter.TouristIDs
        };

        // Pozivamo mikroservis za kreiranje socijalnog sastanka (SocialEncounter)
        var result = await CreateSocialEncounterAsync(socialEncounterDto);

        /*
        if (result.Value == null)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, "Error occurred while creating social encounter."); // Vraćamo BadRequest ako je rezultat null
        }
        */

        var wholeSocialEncounterDto = new WholeSocialEncounterDto
        {
            EncounterId = createdEncounter.Id,
            Name = socialEncounter.Name,
            Description = socialEncounter.Description,
            XpPoints = socialEncounter.XpPoints,
            Status = socialEncounter.Status,
            Type = socialEncounter.Type,
            Latitude = socialEncounter.Latitude,
            Longitude = socialEncounter.Longitude,
            //Id = result.Value.Id,
            TouristsRequiredForCompletion = socialEncounter.TouristsRequiredForCompletion,
            DistanceTreshold = socialEncounter.DistanceTreshold,
            TouristIDs = socialEncounter.TouristIDs,
            ShouldBeApproved = socialEncounter.ShouldBeApproved
        };

        return StatusCode((int)HttpStatusCode.Created, wholeSocialEncounterDto);
    }

    //SOCIAL ENCOUNTER
    private async Task<ActionResult<SocialEncounterDto>> CreateSocialEncounterAsync(SocialEncounterDto socialEncounterDto)
    {
        string json = JsonConvert.SerializeObject(socialEncounterDto);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
        try
        {
            HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:4000/encounters/createSocialEncounter", content); // Promeniti URL na odgovarajući za kreiranje socijalnog sastanka

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                SocialEncounterDto createdSocialEncounter = JsonConvert.DeserializeObject<SocialEncounterDto>(responseContent);
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

    //UPDATE

    [HttpPut]
    public async Task<ActionResult<EncounterDto>> Update([FromBody] EncounterDto encounter)
    {
        // Serijalizacija objekta u JSON
        string json = JsonConvert.SerializeObject(encounter);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            // Šaljemo PUT zahtev na mikroservis
            HttpResponseMessage response = await _httpClient.PutAsync("http://localhost:4000/encounters/update", content);

            // Proveravamo status odgovora
            if (response.IsSuccessStatusCode)
            {
                // Ako je odgovor uspešan, čitamo sadržaj odgovora
                string responseContent = await response.Content.ReadAsStringAsync();

                // Deserijalizujemo JSON odgovor u EncounterDto objekat
                EncounterDto updatedEncounter = JsonConvert.DeserializeObject<EncounterDto>(responseContent);

                // Vraćamo OK rezultat sa ažuriranim susretom
                return Ok(updatedEncounter);
            }
            else
            {
                // Ako je došlo do greške, vraćamo odgovarajući HTTP status
                return StatusCode((int)response.StatusCode, "Error occurred while updating encounter.");
            }
        }
        catch (HttpRequestException ex)
        {
            // Uhvatamo eventualne greške prilikom slanja zahteva
            return StatusCode(500, $"Error occurred while sending request: {ex.Message}");
        }
    }

    //HIDDEN LOCATION ENCOUNTER UPDATE
    [HttpPut("hiddenLocation")]
    public async Task<ActionResult<HiddenLocationEncounterDto>> Update([FromBody] WholeHiddenLocationEncounterDto wholeEncounter)
    {
        // Prvo ažurirajte EncounterDto
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
            // Serijalizacija EncounterDto u JSON
            string encounterJson = JsonConvert.SerializeObject(encounterDto);
            HttpContent encounterContent = new StringContent(encounterJson, Encoding.UTF8, "application/json");

            // Slanje PUT zahteva na mikroservis za ažuriranje EncounterDto
            HttpResponseMessage encounterResponse = await _httpClient.PutAsync("http://localhost:4000/encounters/update", encounterContent);

            // Provera statusa odgovora za ažuriranje EncounterDto
            if (!encounterResponse.IsSuccessStatusCode)
            {
                // Ako nije uspelo, vraćamo odgovarajući HTTP status
                return StatusCode((int)encounterResponse.StatusCode, "Error occurred while updating encounter.");
            }

            // Sada ažurirajte HiddenLocationEncounterDto
            var hiddenLocationEncounterDto = new HiddenLocationEncounterDto
            {
                Id = wholeEncounter.Id,
                EncounterId = encounterDto.Id,
                ImageLatitude = wholeEncounter.ImageLatitude,
                ImageLongitude = wholeEncounter.ImageLongitude,
                ImageURL = wholeEncounter.ImageURL,
                DistanceTreshold = wholeEncounter.DistanceTreshold
            };

            // Serijalizacija HiddenLocationEncounterDto u JSON
            string hiddenLocationEncounterJson = JsonConvert.SerializeObject(hiddenLocationEncounterDto);
            HttpContent hiddenLocationEncounterContent = new StringContent(hiddenLocationEncounterJson, Encoding.UTF8, "application/json");

            // Slanje PUT zahteva na mikroservis za ažuriranje HiddenLocationEncounterDto
            HttpResponseMessage hiddenLocationEncounterResponse = await _httpClient.PutAsync("http://localhost:4000/encounters/updateHiddenLocationEncounter", hiddenLocationEncounterContent);

            // Provera statusa odgovora za ažuriranje HiddenLocationEncounterDto
            if (!hiddenLocationEncounterResponse.IsSuccessStatusCode)
            {
                // Ako nije uspelo, vraćamo odgovarajući HTTP status
                return StatusCode((int)hiddenLocationEncounterResponse.StatusCode, "Error occurred while updating hidden location encounter.");
            }

            // Ako je sve uspešno, vraćamo odgovarajući HTTP status bez sadržaja
            return StatusCode((int)HttpStatusCode.NoContent);
        }
        catch (HttpRequestException ex)
        {
            // Uhvatamo eventualne greške prilikom slanja zahteva i vraćamo odgovarajući HTTP status
            return StatusCode(500, $"Error occurred while sending request: {ex.Message}");
        }
    }


    [HttpPut("social")]
    //WholeSocialEncounterDto povratna vrednost cele funkcije
    public async Task<ActionResult<WholeSocialEncounterDto>> UpdateSocialEncounter([FromBody] WholeSocialEncounterDto socialEncounter)
    {
        // Prvo konvertujemo WholeSocialEncounterDto u EncounterDto
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
            // Serijalizujemo EncounterDto u JSON
            string json = JsonConvert.SerializeObject(encounterDto);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            // Šaljemo PUT zahtev na mikroservis za ažuriranje susreta
            HttpResponseMessage encounterResponse = await _httpClient.PutAsync("http://localhost:4000/encounters/update", content);

            // Proveravamo status odgovora
            if (!encounterResponse.IsSuccessStatusCode)
            {
                // Ako nije uspeo, vraćamo odgovarajući HTTP status
                return StatusCode((int)encounterResponse.StatusCode, "Error occurred while updating encounter.");
            }

            // Serijalizujemo odgovor u string
            string encounterResponseContent = await encounterResponse.Content.ReadAsStringAsync();

            // Deserijalizujemo odgovor u ažurirani EncounterDto
            EncounterDto updatedEncounter = JsonConvert.DeserializeObject<EncounterDto>(encounterResponseContent);

            // Zatim konvertujemo WholeSocialEncounterDto u SocialEncounterDto
            var socialEncounterDto = new SocialEncounterDto
            {
                Id = socialEncounter.Id,
                EncounterId = updatedEncounter.Id,
                TouristsRequiredForCompletion = socialEncounter.TouristsRequiredForCompletion,
                DistanceTreshold = socialEncounter.DistanceTreshold,
                TouristIDs = socialEncounter.TouristIDs
            };

            // Šaljemo PUT zahtev na mikroservis za ažuriranje socijalnog susreta
            HttpResponseMessage socialEncounterResponse = await _httpClient.PutAsync("http://localhost:4000/encounters/updateSocialEncounter", content);

            // Proveravamo status odgovora
            if (!socialEncounterResponse.IsSuccessStatusCode)
            {
                // Ako nije uspeo, vraćamo odgovarajući HTTP status
                return StatusCode((int)socialEncounterResponse.StatusCode, "Error occurred while updating social encounter.");
            }

            // Ako je sve uspešno, vraćamo odgovor
            return StatusCode((int)HttpStatusCode.NoContent, socialEncounterDto);
        }
        catch (HttpRequestException ex)
        {
            // U slučaju greške prilikom slanja zahteva, vraćamo odgovarajući HTTP status
            return StatusCode(500, $"Error occurred while sending request: {ex.Message}");
        }
    }

    /*
    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var result = _encounterService.Delete(id);
        return CreateResponse(result);
    }
    [HttpDelete("hiddenLocation/{baseEncounterId:int}/{hiddenLocationEncounterId:int}")]
    public ActionResult DeleteHiddenLocationEncounter(int baseEncounterId, int hiddenLocationEncounterId)
    {
        var baseEncounter = _encounterService.Delete(baseEncounterId);
        var result = _hiddenLocationEncounterService.Delete(hiddenLocationEncounterId);
        return CreateResponse(result);
    }


    [HttpDelete("social/{baseEncounterId:int}/{socialEncounterId:int}")]
    public ActionResult Delete(int baseEncounterId, int socialEncounterId)
    {
        var baseEncounter = _encounterService.Delete(baseEncounterId);
        var result = _socialEncounterService.Delete(socialEncounterId);
        return CreateResponse(result);
    }
    */
    [HttpGet("getEncounter/{encounterId:int}")]
    public ActionResult<PagedResult<EncounterDto>> GetEncounter(int encounterId)
    {
        var encounter = _encounterService.GetEncounterById(encounterId);
        return CreateResponse(encounter);
    }

    //DELETE MIKROSERVIS

    [HttpDelete("{baseEncounterId:int}")]
    public async Task<ActionResult> DeleteEncounter(int baseEncounterId)
    {
        var baseEncounterResponse = await DeleteEncounterAsync(baseEncounterId);

        if (baseEncounterResponse.IsSuccessStatusCode || baseEncounterResponse.StatusCode == HttpStatusCode.NoContent)
        {
            var socialEncounterIdResponse = await GetSocialEncounterIdAsync(baseEncounterId);

            string jsonResponse1 = await socialEncounterIdResponse.Content.ReadAsStringAsync();
            JObject jsonObject1 = JObject.Parse(jsonResponse1);
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
        return await _httpClient.DeleteAsync($"http://localhost:4000/encounters/deleteEncounter/{baseEncounterId}");
    }

    private async Task<HttpResponseMessage> GetSocialEncounterIdAsync(int baseEncounterId)
    {
        return await _httpClient.GetAsync($"http://localhost:4000/encounters/getSocialEncounterId/{baseEncounterId}");
    }

    private async Task<HttpResponseMessage> GetHiddenLocationEncounterIdAsync(int baseEncounterId)
    {
        return await _httpClient.GetAsync($"http://localhost:4000/encounters/getHiddenLocationEncounterId/{baseEncounterId}");
    }

    private async Task<HttpResponseMessage> DeleteSocialEncounterAsync(long socialEncounterId)
    {
        return await _httpClient.DeleteAsync($"http://localhost:4000/encounters/deleteSocialEncounter/{socialEncounterId}");
    }

    private async Task<HttpResponseMessage> DeleteHiddenLocationEncounterAsync(long hiddenLocationEncounterId)
    {
        return await _httpClient.DeleteAsync($"http://localhost:4000/encounters/deleteHiddenLocationEncounter/{hiddenLocationEncounterId}");
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

    /*
    [HttpGet("hiddenLocation/{encounterId:int}")]
    public ActionResult<PagedResult<HiddenLocationEncounterDto>> GetHiddenLocationEncounterByEncounterId(int encounterId)
    {
        var hiddenLocationEncounter = _hiddenLocationEncounterService.GetHiddenLocationEncounterByEncounterId(encounterId);
        return CreateResponse(hiddenLocationEncounter);
    }
    */

}
