using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecifcation<T>
    {
        Expression<Func<T,bool>> Criteria {get;}
        List<Expression<Func<T,Object>>> Includes {get;}
    }
}