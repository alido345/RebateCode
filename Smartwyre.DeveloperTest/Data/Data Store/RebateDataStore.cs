using Smartwyre.DeveloperTest.Data.Interface;
using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;
using System.Linq;

namespace Smartwyre.DeveloperTest.Data.Repository;

public class RebateDataStore : IRebateDataStore
{
    private readonly List<Rebate> _rebates = new List<Rebate>();

    public void AddRebate(Rebate rebate)
    {
        _rebates.Add(rebate);
    }

    public Rebate GetRebate(string rebateIdentifier)
    {
        return _rebates.FirstOrDefault(r => r.Identifier == rebateIdentifier);
    }
}
