using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
  public class BaseSpecification<T> : ISpecifcation<T>
  {
    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
      Criteria = criteria;
    }

    public BaseSpecification()
    {
    }

    public Expression<Func<T, bool>> Criteria {get;}

    

   public  List<Expression<Func<T, object>>> Includes {get;} = new List<Expression<Func<T, object>>>();

    protected void AddInclude(Expression<Func<T, object>> include){
     Includes.Add(include);
    }
  }
}