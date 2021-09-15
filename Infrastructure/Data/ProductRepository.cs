using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
  public class ProductRepository : IProductRepository
  {
    private readonly StoreContext _ctx;
    public ProductRepository(StoreContext ctx)
    {
      _ctx = ctx;
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
      return await _ctx.Products.
      Include(p=>p.ProductBrand)
      .Include(p=>p.ProductType)
      .ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
      Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Product, ProductType> includableQueryable = _ctx.Products.
            Include(p => p.ProductBrand)
            .Include(p => p.ProductType);
      return await includableQueryable
      .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IReadOnlyList<ProductBrand>> GetProductsBrandAsync()
    {
      return await  _ctx.ProductBrands.
      ToListAsync();
    }

    public async Task<IReadOnlyList<ProductType>> GetProductsTypeAsync()
    {
      return await _ctx.ProductTypes.ToListAsync();
    }
  }
}