using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain.Competitions;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.UseCases.Authoring;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Services
{
	public class CompetitionApplyService : CrudService<CompetitionApplyDto, CompetitionApply>, ICompetitionApplyService
	{
		public CompetitionApplyService(ICrudRepository<CompetitionApply> crudRepository, IMapper mapper) : base(crudRepository, mapper)
		{
		}


        public Result<PagedResult<CompetitionApplyDto>> GetAppliesByCompId(int comId)
        {
            var allApplies = CrudRepository.GetPaged(1, int.MaxValue);

            var filteredApplies = allApplies.Results.Where(apply => apply.CompetitionId == comId);

            var filteredPagedResult = new PagedResult<CompetitionApply>(filteredApplies.ToList(), filteredApplies.Count());
            return MapToDto(filteredPagedResult);
        }



        public Result<PagedResult<CompetitionApplyDto>> GetWinnerByCompId(int comId)
        {
            var winnerApply = new List<CompetitionApply>();
            var allApplies = CrudRepository.GetPaged(1, int.MaxValue);

            var filteredApplies = allApplies.Results.Where(apply => apply.CompetitionId == comId).ToList();

            // Sort the filteredApplies in descending order based on NumLikes
            filteredApplies.Sort((a1, a2) => a2.NumLikes.CompareTo(a1.NumLikes));

            int n = filteredApplies.Count;

            if (n > 0)
            {
                // Add the entry with the maximum number of likes to the winnerApply list
                winnerApply.Add(filteredApplies[0]);

                // Iterate through the sorted list to find and add entries with the same maximum number of likes
                for (int i = 1; i < n; i++)
                {
                    if (filteredApplies[i].NumLikes == filteredApplies[0].NumLikes)
                    {
                        winnerApply.Add(filteredApplies[i]);
                    }
                    else
                    {
                        // Break the loop if a different number of likes is encountered (as the list is sorted)
                        break;
                    }
                }
            }

            var filteredPagedResult = new PagedResult<CompetitionApply>(winnerApply, winnerApply.Count);
            return MapToDto(filteredPagedResult);
        }


    }
}
