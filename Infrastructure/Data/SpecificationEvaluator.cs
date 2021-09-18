using System.Linq;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity:BaseEntity  
    {
      public static IQueryable<TEntity> GetQueryFromSpecification(IQueryable<TEntity> inputQuery,ISpecifcation<TEntity> spec){
        var query = inputQuery;
       if(spec.Criteria!=null){
           query=query.Where(spec.Criteria);
       }
       query = spec.Includes.Aggregate(query,(current,include)=>current.Include(include));
       return query;
      } 
    }
}