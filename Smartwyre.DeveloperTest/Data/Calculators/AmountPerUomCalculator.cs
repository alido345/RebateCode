using Smartwyre.DeveloperTest.Data.Interface;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Data.Calculators
{
    
    public class AmountPerUomCalculator : IIncentiveCalculator
    {
        public bool CanCalculate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate.Incentive == IncentiveType.AmountPerUom &&
                   product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom) &&
                   rebate.Amount != 0 && request.Volume != 0;
        }

        public decimal Calculate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate.Amount * request.Volume;
        }
    }


}
