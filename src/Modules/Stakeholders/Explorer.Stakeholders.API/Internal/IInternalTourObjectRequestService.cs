using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Explorer.Stakeholders.API.Internal
{
    public interface IInternalTourObjectRequestService
    {
        Result<TourObjectRequestDto> AcceptRequest(int id, string comment);
    }
}
