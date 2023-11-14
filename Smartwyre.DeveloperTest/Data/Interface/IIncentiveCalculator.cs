using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Data.Interface
{
    public interface IIncentiveCalculator
    {
        bool CanCalculate(Rebate rebate, Product product, CalculateRebateRequest request);
        decimal Calculate(Rebate rebate, Product product, CalculateRebateRequest request);
    }
}
