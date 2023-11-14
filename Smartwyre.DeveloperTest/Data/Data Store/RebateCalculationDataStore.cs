using Smartwyre.DeveloperTest.Data.Interface;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Data.Data_Store
{
    public class RebateCalculationDataStore : IRebateCalculationDataStore
    {
        private readonly List<RebateCalculation> _calculations = new List<RebateCalculation>();

        public void StoreCalculationResult(RebateCalculation calculation)
        {
            _calculations.Add(calculation);
        }
    }
}
