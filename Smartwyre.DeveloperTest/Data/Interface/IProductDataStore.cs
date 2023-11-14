using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Data.Interface
{
    public interface IProductDataStore
    {
        void AddProduct(Product product);
        Product GetProduct(string productIdentifier);
    }
}
