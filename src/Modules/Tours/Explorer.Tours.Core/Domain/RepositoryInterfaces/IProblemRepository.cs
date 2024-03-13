using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain.Problems;
using FluentResults;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces;

public interface IProblemRepository
{
    PagedResult<Problem> GetByTouristId(int userId, int page, int pageSize);
    PagedResult<Problem> GetByGuideId(int userId, int page, int pageSize);
    PagedResult<Problem> GetUnsolvedProblems(int page, int pageSize);
    PagedResult<Problem> GetProblemsOfUser(int userId, int page, int pageSize);
}
