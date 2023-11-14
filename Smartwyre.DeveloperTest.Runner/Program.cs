using Smartwyre.DeveloperTest.Data.Calculators;
using Smartwyre.DeveloperTest.Data.Data_Store;
using Smartwyre.DeveloperTest.Data.Interface;
using Smartwyre.DeveloperTest.Data.Repository;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        // Setup in-memory data stores
        var productDataStore = new ProductDataStore();
        var rebateDataStore = new RebateDataStore();
        var calculationDataStore = new RebateCalculationDataStore();

        // Inject in-memory data stores into the service
        var rebateService = new RebateService(
            rebateDataStore,
            productDataStore,
            calculationDataStore,
           new List<IIncentiveCalculator>
            {
                new FixedCashAmountCalculator(),
                new FixedRateRebateCalculator(),
                new AmountPerUomCalculator()
            }
        );

        // Add sample product
        var product = new Product
        {
            Id=1,
            Identifier = "P1",
            Price = 100,
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate | SupportedIncentiveType.AmountPerUom
        };

        productDataStore.AddProduct(product);

        // Add sample rebate
        var rebate = new Rebate
        {
            Identifier = "R1",
            Incentive = IncentiveType.FixedRateRebate,
            Percentage = 0.1m
        };

        rebateDataStore.AddRebate(rebate);

        // Calculate rebate
        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "R1",
            ProductIdentifier = "P1",
            Volume = 5
        };

        var result = rebateService.Calculate(request);

        Console.WriteLine($"Calculation Success: {result.Success}");

    }
}

