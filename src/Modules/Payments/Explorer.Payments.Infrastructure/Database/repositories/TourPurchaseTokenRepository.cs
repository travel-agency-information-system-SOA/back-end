using Explorer.Payments.Core.Domain.repositoryinterfaces;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.repositories
{
    public class TourPurchaseTokenRepository : ITourPurchaseTokenRepository
    {
        private readonly PaymentsContext _dbContext;

        public TourPurchaseTokenRepository(PaymentsContext dbContext) {
            _dbContext = dbContext;

        }


        public List<TourPurchaseToken> GetAll()
        {
            return _dbContext.TourPurchaseTokens.ToList();
        }
    }
}
