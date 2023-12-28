using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
    public class CouponService : CrudService<CouponDto, Coupon>, ICouponService
    {
        private readonly ICouponRepository _repository;
        public CouponService(ICrudRepository<Coupon> repository, ICouponRepository couponRepository, IMapper mapper) : base(repository, mapper)
        {
            _repository = couponRepository;
        }

        /*public Result<CouponDto> GetByCodeAndTourId(string code, int tourId)
        {
            try
            {
                var result = _repository.GetByCodeAndTourId(code, tourId);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }*/

        public Result<CouponDto> GetByCode(string code)
        {
            try
            {
                var result = _repository.GetByCode(code);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<List<CouponDto>> GetByAuthorId(int authorId)
        {
            try
            {
                var result = _repository.GetByAuthorId(authorId);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public override Result<CouponDto> Create(CouponDto coupon)
        {
            try
            {
                var result = _repository.Create(MapToDomain(coupon));
                return MapToDto(result);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
    }
}
