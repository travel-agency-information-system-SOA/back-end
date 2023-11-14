using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IUserPositionRepository
    {
        public UserPosition GetByUserId(int id,int page,int pageSize);
    }
}
