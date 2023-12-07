using Explorer.Payments.API.Dtos.BundlePayRecord;
using Explorer.Payments.API.Dtos.ShoppingCartDtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public.BundlePayRecord
{
    public interface IBundlePayRecordService
    {
        // sadrzaj metoda iz servisa
        Result<BundlePayRecordDto> BundlePurchase(int bundleIdint, int touristId);
        
        

    }
}
