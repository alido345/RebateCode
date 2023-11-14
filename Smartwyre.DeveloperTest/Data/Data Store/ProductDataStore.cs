using Smartwyre.DeveloperTest.Data.Interface;
using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;
using System.Linq;

namespace Smartwyre.DeveloperTest.Data.Repository;

public class ProductDataStore : IProductDataStore
{
    private readonly List<Product> _products = new List<Product>();

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public Product GetProduct(string productIdentifier)
    {
        return _products.FirstOrDefault(p => p.Identifier == productIdentifier);
    }
}
