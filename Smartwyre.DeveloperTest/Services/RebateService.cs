using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Data.Interface;
using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly IRebateDataStore _rebateDataStore;
    private readonly IProductDataStore _productDataStore;
    private readonly IRebateCalculationDataStore _rebateCalculationDataStore;
    private readonly IEnumerable<IIncentiveCalculator> _calculators;

    public RebateService(
        IRebateDataStore rebateDataStore,
        IProductDataStore productDataStore,
        IRebateCalculationDataStore rebateCalculationDataStore,
        IEnumerable<IIncentiveCalculator> calculators)
    {
        _rebateDataStore = rebateDataStore;
        _productDataStore = productDataStore;
        _rebateCalculationDataStore = rebateCalculationDataStore;
        _calculators = calculators;
    }

    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        var rebate = _rebateDataStore.GetRebate(request.RebateIdentifier);
        var product = _productDataStore.GetProduct(request.ProductIdentifier);

        var result = new CalculateRebateResult();

        foreach (var calculator in _calculators)
        {
            if (calculator.CanCalculate(rebate, product, request))
            {
                var rebateAmount = calculator.Calculate(rebate, product, request);
                var calculation = new RebateCalculation
                {
                    Identifier = rebate.Identifier,
                    RebateIdentifier = rebate.Identifier,
                    IncentiveType = rebate.Incentive,
                    Amount = rebateAmount
                };

                _rebateCalculationDataStore.StoreCalculationResult(calculation);

                result.Success = true;
                break;

            }
        }

        return result;
    }
}

//public class RebateService : IRebateService
//{
//    public CalculateRebateResult Calculate(CalculateRebateRequest request)
//    {
//        var rebateDataStore = new RebateDataStore();
//        var productDataStore = new ProductDataStore();

//        Rebate rebate = rebateDataStore.GetRebate(request.RebateIdentifier);
//        Product product = productDataStore.GetProduct(request.ProductIdentifier);

//        var result = new CalculateRebateResult();

//        var rebateAmount = 0m;

//        switch (rebate.Incentive)
//        {
//            case IncentiveType.FixedCashAmount:
//                if (rebate == null)
//                {
//                    result.Success = false;
//                }
//                else if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount))
//                {
//                    result.Success = false;
//                }
//                else if (rebate.Amount == 0)
//                {
//                    result.Success = false;
//                }
//                else
//                {
//                    rebateAmount = rebate.Amount;
//                    result.Success = true;
//                }
//                break;

//            case IncentiveType.FixedRateRebate:
//                if (rebate == null)
//                {
//                    result.Success = false;
//                }
//                else if (product == null)
//                {
//                    result.Success = false;
//                }
//                else if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate))
//                {
//                    result.Success = false;
//                }
//                else if (rebate.Percentage == 0 || product.Price == 0 || request.Volume == 0)
//                {
//                    result.Success = false;
//                }
//                else
//                {
//                    rebateAmount += product.Price * rebate.Percentage * request.Volume;
//                    result.Success = true;
//                }
//                break;

//            case IncentiveType.AmountPerUom:
//                if (rebate == null)
//                {
//                    result.Success = false;
//                }
//                else if (product == null)
//                {
//                    result.Success = false;
//                }
//                else if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom))
//                {
//                    result.Success = false;
//                }
//                else if (rebate.Amount == 0 || request.Volume == 0)
//                {
//                    result.Success = false;
//                }
//                else
//                {
//                    rebateAmount += rebate.Amount * request.Volume;
//                    result.Success = true;
//                }
//                break;
//        }

//        if (result.Success)
//        {
//            var storeRebateDataStore = new RebateDataStore();
//            storeRebateDataStore.StoreCalculationResult(rebate, rebateAmount);
//        }

//        return result;
//    }
//}
