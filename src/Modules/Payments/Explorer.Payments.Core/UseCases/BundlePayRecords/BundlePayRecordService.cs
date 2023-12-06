using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.BundlePayRecord;
using Explorer.Payments.API.Dtos.ShoppingCartDtos;
using Explorer.Payments.API.Public.BundlePayRecord;
using Explorer.Payments.Core.Domain.BundlePayRecords;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Explorer.Tours.Core.Domain.TourBundle;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases.BundlePayRecords
{
    public class BundlePayRecordService : CrudService<BundlePayRecordDto, BundlePayRecord>, IBundlePayRecordService
    {
                                                            //za pravljenje tokena za svaku turu iz bundle nakon kupovine bundl-a
        private readonly ICrudRepository<TourPurchaseToken> _tourPurhcaseTokenRepository;  
        //private readonly IBundlePayRecordsRepository _bundlePayRecordRepository;
        private readonly ICrudRepository<BundlePayRecord> _bundlePayRecordRepository;
        private readonly ICrudRepository<TourBundle> _tourBundleRepository;

        
        public BundlePayRecordService(ICrudRepository<BundlePayRecord> repository, ICrudRepository<TourPurchaseToken> tokenRepo, ICrudRepository<TourBundle> bundleRepository, IMapper mapper) : base(repository, mapper)
        {
            _bundlePayRecordRepository = repository;
            _tourPurhcaseTokenRepository = tokenRepo;
            _tourBundleRepository = bundleRepository;
        }


        //metode logika poslovna



        //prosledjen id bundla:  za sve ture iz njega napravi tokene, a za njega mi napravi BundlePayRecord 
        public Result<BundlePayRecordDto> BundlePurchase(int tourBundleId, int touristId) 
        {
            var tourBundle = _tourBundleRepository.Get(tourBundleId); //dobavljanje bas tog bandla koji se kupuje

            // PRAVLJENJE TOKENA ZA SVE TURE IZ BUNDLA
            var toursIds = new List<int>(tourBundle.TourIds); //lista tura iz bundla

            foreach (int tourId in toursIds)
            {
                TourPurchaseToken token = new TourPurchaseToken(touristId, tourId);
                _tourPurhcaseTokenRepository.Create(token);  
            }

            // PRAVLJENJE PAYMENT RECORDA ZA BAS OVAJ BUNDLE
            var result = new BundlePayRecord
            {
                TouristId = touristId,
                TourBundleId = tourBundleId,
                Price = tourBundle.Price,
                DateCreated = DateTime.UtcNow
            };

            _bundlePayRecordRepository.Create(result);
            /*
            // PRAVLJENJE PAYMENT RECORDA ZA BAS OVAJ BUNDLE
            var result = _bundlePayRecordRepository.PayRecordCreate(bundleId, touristId);

            // baciti neko obavestenje o kupovini 
            */

            return MapToDto(result);
        }


    }
}
