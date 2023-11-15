using AutoMapper.Configuration.Conventions;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Administration
{
    public interface ITouristEquipmentService
    {
        Result<TouristEquipmentDto> GetTouristEquipment(int touristId);
        Result<TouristEquipmentDto> AddToMyEquipment(int touristId, int equipmentId);
        Result<TouristEquipmentDto> DeleteFromMyEquipment(int touristId, int equipmentId);

        Result<TouristEquipmentDto> Create(int touristId);
    }
}
