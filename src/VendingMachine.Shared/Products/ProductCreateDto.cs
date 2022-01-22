using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Shared.Products
{
    public class ProductDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
    public class ProductCreateDto : ProductDto
    {
    }

    public class ProductUpdateDto : ProductDto
    {

    }

    public class ProductListDto : ProductDto
    {
    }

    public class ProductDetalsDto : ProductDto
    {

    }
}
