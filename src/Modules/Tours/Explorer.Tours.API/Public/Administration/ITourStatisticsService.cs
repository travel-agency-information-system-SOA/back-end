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
    public interface ITourStatisticsService
    {
        public int GetNumberOfPurchaseByAuthor(int authorId);

        public int GetNumberOfCompletedByAuthor(int authorId);

        public int GetNumberOfStartedByAuthor(int authorId);
    }
}
