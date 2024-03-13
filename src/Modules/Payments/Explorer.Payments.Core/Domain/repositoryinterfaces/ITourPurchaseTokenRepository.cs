using Explorer.Payments.Core.Domain.ShoppingCarts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.repositoryinterfaces
{
    public interface ITourPurchaseTokenRepository
    {
        List<TourPurchaseToken> GetAll();

    }
}
