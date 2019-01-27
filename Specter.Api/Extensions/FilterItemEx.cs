using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

using Specter.Api.Services.Filtering;

namespace Specter.Api.Extensions 
{
    public static class FiltereItemEx
    {
        public static Expression<Func<T, bool>> ToExpression<T>(this IFilterItem filter, 
                                                                Func<string, Operation?, Expression<Func<T, bool>>> selector)
        {
            if(filter == null)
                throw new ArgumentNullException(nameof(filter));

            if(filter.Values == null)
                return PredicateBuilder.True<T>();

            return filter.Values.ToExpression(selector);
        }

        public static Expression<Func<T, bool>> ToExpression<T>(this IEnumerable<IFilterValue> values, 
                                                                Func<string, Operation?, Expression<Func<T, bool>>> selector)
        {
            var lvalues = values.OrderBy(v => v.Order).ToList();

            if(lvalues.Count == 0)
                return PredicateBuilder.True<T>();


            var last = lvalues[0];
            var baseExpr = CalculateExpression(last, last.Operation, selector);

            if (lvalues.Count == 1)
                return baseExpr;

            foreach(var value in lvalues.Skip(1))
            {
                if(!last.Operation.HasValue)
                    throw new ArgumentException("Invalid filter", nameof(values));

                var newExpr = CalculateExpression(value, value.Operation, selector);

                switch(last.Operation.Value)
                {
                    case Operation.And:
                    case Operation.Until:
                        baseExpr = PredicateBuilder.And(baseExpr, newExpr);
                        break;
                    case Operation.Or:
                        baseExpr = PredicateBuilder.Or(baseExpr, newExpr);
                        break;
                    default:
                        throw new NotImplementedException();
                };

                last = value;
            }

            return baseExpr;
        }

        private static Expression<Func<T, bool>> CalculateExpression<T>(IFilterValue value, 
                                                                        Operation? operation,
                                                                        Func<string, Operation?, Expression<Func<T, bool>>> selector)
        {
            return value.Complex != null
                        ? value.Complex.ToExpression(selector)
                        : selector(value.Value, operation);
        }
    }

    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T> () => t => true;  
        public static Expression<Func<T, bool>> False<T> () => t => false; 

        public static Expression<Func<T, bool>> Or<T> (this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke (expr2, expr1.Parameters.Cast<Expression> ());
            return Expression.Lambda<Func<T, bool>>
                (Expression.OrElse (expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T> (this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke (expr2, expr1.Parameters.Cast<Expression> ());
            return Expression.Lambda<Func<T, bool>>
                (Expression.AndAlso (expr1.Body, invokedExpr), expr1.Parameters);
        }
    }
}