using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
  public class ProductWithTypesAndBrandSpecif: BaseSpecification<Product>
  {
    public ProductWithTypesAndBrandSpecif() 
    {
     AddInclude(p=>p.ProductType);
     AddInclude(p=>p.ProductBrand);
    }

    public ProductWithTypesAndBrandSpecif(int id) : base(x=>x.Id==id)
    {
      AddInclude(p=>p.ProductType);
     AddInclude(p=>p.ProductBrand);
    }
  }
}