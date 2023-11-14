using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Smartwyre.DeveloperTest;
using Smartwyre.DeveloperTest.Data.Interface;
using Smartwyre.DeveloperTest.Types;
using Smartwyre.DeveloperTest.Services;

namespace Smartwyre.DeveloperTest.Tests
{
    public class RebateServiceTests
    {
        [Fact]
        public void Calculate_FixedCashAmount_Success()
        {
            // Arrange
            var rebateDataStoreMock = new Mock<IRebateDataStore>();
            rebateDataStoreMock.Setup(r => r.GetRebate(It.IsAny<string>()))
                .Returns(new Rebate { Incentive = IncentiveType.FixedCashAmount, Amount = 10 });

            var productDataStoreMock = new Mock<IProductDataStore>();
            productDataStoreMock.Setup(p => p.GetProduct(It.IsAny<string>()))
                .Returns(new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount });

            var rebateCalculationDataStoreMock = new Mock<IRebateCalculationDataStore>();

            var calculatorMock = new Mock<IIncentiveCalculator>();
            calculatorMock.Setup(calculator => calculator.CanCalculate(It.IsAny<Rebate>(), It.IsAny<Product>(), It.IsAny<CalculateRebateRequest>()))
                .Returns(true);
            calculatorMock.Setup(calculator => calculator.Calculate(It.IsAny<Rebate>(), It.IsAny<Product>(), It.IsAny<CalculateRebateRequest>()))
                .Returns(10); 

            var rebateService = new RebateService(
                rebateDataStoreMock.Object,
                productDataStoreMock.Object,
                rebateCalculationDataStoreMock.Object,
                new List<IIncentiveCalculator> { calculatorMock.Object });

            var request = new CalculateRebateRequest { RebateIdentifier = "rebate1", ProductIdentifier = "product1", Volume = 5 };

            // Act
            var result = rebateService.Calculate(request);

            // Assert
            Assert.True(result.Success);
            rebateCalculationDataStoreMock.Verify(r => r.StoreCalculationResult(It.IsAny<RebateCalculation>()), Times.Once);
        }

        [Fact]
        public void Calculate_FixedCashAmount_Failure()
        {
            // Arrange
            var rebateDataStoreMock = new Mock<IRebateDataStore>();
            var productDataStoreMock = new Mock<IProductDataStore>();
            var rebateCalculationDataStoreMock = new Mock<IRebateCalculationDataStore>();

            var calculatorMock = new Mock<IIncentiveCalculator>();
            calculatorMock.Setup(calculator => calculator.CanCalculate(It.IsAny<Rebate>(), It.IsAny<Product>(), It.IsAny<CalculateRebateRequest>()))
                .Returns(false);

            var rebateService = new RebateService(
                rebateDataStoreMock.Object,
                productDataStoreMock.Object,
                rebateCalculationDataStoreMock.Object,
                new List<IIncentiveCalculator> { calculatorMock.Object });

            var request = new CalculateRebateRequest { RebateIdentifier = "rebate1", ProductIdentifier = "product1", Volume = 5 };

            // Act
            var result = rebateService.Calculate(request);

            // Assert
            Assert.False(result.Success);
            rebateCalculationDataStoreMock.Verify(r => r.StoreCalculationResult(It.IsAny<RebateCalculation>()), Times.Never);
        }
    }


}





