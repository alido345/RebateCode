using Smartwyre.DeveloperTest.Data.Interface;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Data.Calculators
{
    public class FixedRateRebateCalculator : IIncentiveCalculator
    {
        public bool CanCalculate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate.Incentive == IncentiveType.FixedRateRebate &&
                   product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate) &&
                   rebate.Percentage != 0 && product.Price != 0 && request.Volume != 0;
        }

        public decimal Calculate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return product.Price * rebate.Percentage * request.Volume;
        }
    }
}
