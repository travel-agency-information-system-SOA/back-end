using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Internal
{
    public interface IInternalTourPointRequestService
    {
        Result<TourPointRequestDto> AcceptRequest(int id,string comment);
    }
}
