using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Application.Repositories;
using VendingMachine.Application.Services;
using VendingMachine.Domain.Core;
using VendingMachine.Domain.Entities;
using VendingMachine.Shared.Products;

namespace VendingMachine.Infrastructure.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public IResultTemplate Create(ProductCreateDto dto)
        {
            if (dto is null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var entity = new Product
            {
                Id = dto.Id,
                Name = dto.Name,
                Price = dto.Price,
                Quantity = dto.Quantity
            };

            _productRepository.Create(entity);
            _productRepository.SaveChanges();

            return ResultTemplate.SucceededResult("Product {0} created successfully", entity.Name);
        }

        public IResultTemplate DeleteById(long id)
        {
            var entity = _productRepository.GetQuerryable().FirstOrDefault(t => t.Id == id);
            if (entity == null) return ResultTemplate.FailedResult("Product at id = {0} not found", id);
            _productRepository.Delete(entity);
            _productRepository.SaveChanges();
            return ResultTemplate.SucceededResult("Product with id = {0} deleted successfully", id);
        }

        public IResultTemplate<ProductDetalsDto> GetById(long id)
        {
            var query = _productRepository.GetQuerryable().Where(t => t.Id == id)
                .Select(t => new ProductDetalsDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Price = t.Price,
                    Quantity = t.Quantity - t.OrderProducts.Where(t => t.IsDisposed).Count(),
                });
            var result = query.FirstOrDefault();
            if (result == null) return ResultTemplate<ProductDetalsDto>.FailedResult("Product not found!");
            return ResultTemplate<ProductDetalsDto>.SucceededResult(result);
        }


        public IResultTemplate<IEnumerable<ProductListDto>> List()
        {
            var query = _productRepository.GetQuerryable()
                .Select(t => new ProductListDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Price = t.Price,
                    Quantity = t.Quantity - t.OrderProducts.Where(t => t.IsDisposed).Count(),
                });

            return ResultTemplate<IEnumerable<ProductListDto>>.SucceededResult(query);
        }

        public IResultTemplate Update(ProductUpdateDto dto)
        {
            if (dto is null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var entity = _productRepository.GetQuerryable().FirstOrDefault(t => t.Id == dto.Id);
            if (entity == null) return ResultTemplate.FailedResult("Can't find order with id = {0}", dto.Id);
            entity.Name = dto.Name;
            entity.Price = dto.Price;
            entity.Quantity = dto.Quantity;

            _productRepository.Update(entity);
            return ResultTemplate.SucceededResult("Product at id = {0} updated successfully!", entity.Id);
        }
    }
}
