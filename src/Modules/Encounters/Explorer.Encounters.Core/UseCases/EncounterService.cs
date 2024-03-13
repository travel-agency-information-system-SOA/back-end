using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.API.Internal;
using FluentResults;

namespace Explorer.Encounters.Core.UseCases;

public class EncounterService : CrudService<EncounterDto, Encounter>, IEncounterService
{
    public EncounterService(ICrudRepository<Encounter> crudRepository, IMapper mapper) : base(crudRepository, mapper)
    {

    }

    public EncounterDto GetEncounter(int encounterId)
    {
        List<Encounter> encounters = new List<Encounter>();
        encounters = CrudRepository.GetPaged(0, 0).Results.ToList();

        List<EncounterDto> encountersDto = new List<EncounterDto>();
        foreach (var encounter in encounters)
        {
            if(encounter.Id == encounterId)
            {
                return MapToDto(encounter);
            }
        }
        return null;
    }

    public Result<EncounterDto> GetEncounterById(int encounterId)
    {
        List<Encounter> encounters = new List<Encounter>();
        encounters = CrudRepository.GetPaged(0, 0).Results.ToList();

        List<EncounterDto> encountersDto = new List<EncounterDto>();
        foreach (var encounter in encounters)
        {
            if (encounter.Id == encounterId)
            {
                return MapToDto(encounter);
            }
        }
        return null;
    }
    public Result<List<EncounterDto>> GetAll()
    {
        List<Encounter> encounters = new List<Encounter>();
        encounters = CrudRepository.GetPaged(0, 0).Results.ToList();

        
        return MapToDto(encounters);
    }

    public double CalculateDistance(double userLat, double userLon, double pointLat, double pointLon)
    {
        double EarthRadiusKm = 6371.0;
        userLat = DegreesToRadians(userLat);
        userLon = DegreesToRadians(userLon);
        pointLat = DegreesToRadians(pointLat);
        pointLon = DegreesToRadians(pointLon);

        double dLat = pointLat - userLat;
        double dLon = pointLon - userLon;

        double a = Math.Sin(dLat / 2.0) * Math.Sin(dLat / 2.0) +
                   Math.Cos(userLat) * Math.Cos(pointLat) *
                   Math.Sin(dLon / 2.0) * Math.Sin(dLon / 2.0);

        double c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        double distance = EarthRadiusKm * c * 1000;

        return distance;
    }

    private double DegreesToRadians(double degrees)
    {
        return degrees * Math.PI / 180.0;
    }
}
