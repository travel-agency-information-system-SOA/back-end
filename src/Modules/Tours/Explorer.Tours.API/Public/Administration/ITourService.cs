using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Administration
{
	public interface ITourService
	{
		Result<TourDTO> Create(TourDTO tourDto);
		
		Result<List<TourDTO>> GetByUserId(int userId, int page, int pageSize);
	}
}
