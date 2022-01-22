using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Domain.Core;
using VendingMachine.Shared.Products;

namespace VendingMachine.Application.Services
{
    public interface IProductService
    {
        public IResultTemplate Create(ProductCreateDto dto);
        public IResultTemplate Update(ProductUpdateDto dto);
        public IResultTemplate DeleteById(long id);
        public IResultTemplate<IEnumerable<ProductListDto>> List();
        public IResultTemplate<ProductDetalsDto> GetById(long id);
    }
}
