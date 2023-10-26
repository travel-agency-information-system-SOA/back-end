using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourEquipmentService : BaseService<TourEquipmentDto, TourEquipment>, ITourEquipmentService
    {
        private readonly ICrudRepository<TourEquipment> _tourEquipmentRepository;
        private readonly IMapper _mapper;

        public TourEquipmentService(ICrudRepository<TourEquipment> tourEquipmentRepository, IMapper mapper) : base(mapper)
        {
            _tourEquipmentRepository = tourEquipmentRepository;
            _mapper = mapper;
        }

        public async Task<Result> AddEquipmentToTourAsync(long tourId, long equipmentId)
        {
            var allTourEquipment = _tourEquipmentRepository.GetPaged(1, int.MaxValue);

            // Proverimo da li veza već postoji
            bool connectionExists = allTourEquipment.Results.Any(te => te.TourId == tourId && te.EquipmentId == equipmentId);

            if (!connectionExists)
            {
                // Ako ne postoji veza, kreirajte novu vezu između ture i opreme
                var newTourEquipment = new TourEquipment
                {
                    TourId = tourId,
                    EquipmentId = equipmentId
                };
                _tourEquipmentRepository.Create(newTourEquipment);
            }

            return Result.Ok();
        }

        public async Task<Result<List<TourEquipmentDto>>> GetTourEquipmentAsync(long tourId)
        {
            var allTourEquipment = _tourEquipmentRepository.GetPaged(1, int.MaxValue).Results;

            var tourEquipmentForTour = allTourEquipment.Where(te => te.TourId == tourId).ToList();

            var tourEquipmentDtos = _mapper.Map<List<TourEquipmentDto>>(tourEquipmentForTour);

            return Result.Ok(tourEquipmentDtos);
        }




        public async Task<Result> RemoveEquipmentFromTourAsync(long tourId, long equipmentId)
        {
            var existingTourEquipment = _tourEquipmentRepository.GetPaged(1, int.MaxValue);
            foreach (var tourEquipment in existingTourEquipment.Results)
            {
                if (tourEquipment.TourId == tourId && tourEquipment.EquipmentId == equipmentId)
                {
                    _tourEquipmentRepository.Delete(tourEquipment.Id);
                    break;
                }
               
            }






          

            return Result.Ok();
        }


    }
}
