using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Administration
{
    public interface IProblemService
    {
        Result<ProblemDto> Report(ProblemDto problemDto);
        Result<List<ProblemDto>> GetByUserId(int userId, int page, int pageSize);
    }
}