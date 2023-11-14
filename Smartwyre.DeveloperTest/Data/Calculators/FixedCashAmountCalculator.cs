using Smartwyre.DeveloperTest.Data.Interface;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Data.Calculators
{
    public class FixedCashAmountCalculator : IIncentiveCalculator
    {
        public bool CanCalculate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate.Incentive == IncentiveType.FixedCashAmount
                && product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount)
                && rebate.Amount != 0;
        }

        public decimal Calculate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate.Amount;
        }
    }
}
