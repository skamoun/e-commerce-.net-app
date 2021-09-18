using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
  public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
  {
    private readonly StoreContext _storeContext;
    public GenericRepository(StoreContext storeContext)
    {
      _storeContext = storeContext;
    }

    public  async Task<IReadOnlyList<T>> GetAllAsync()
    {
      return await _storeContext.Set<T>().ToListAsync();
    }

    public  async Task<T> GetByIdAsync(int id)
    {
      return await  _storeContext.Set<T>().FindAsync(id);
    }

    public  async Task<T> GetEntityWithSpec(ISpecifcation<T> spec)
    {
      return await ApplySpecif(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> ListAsync(ISpecifcation<T> spec)
    {
      return await ApplySpecif(spec).ToListAsync();
    }
    private IQueryable<T> ApplySpecif(ISpecifcation<T> spec){
      return SpecificationEvaluator<T>.GetQueryFromSpecification(_storeContext.Set<T>(),spec);
    }
  }
}