using System.Linq.Expressions;
using System.Reflection;
using FinalProject.Common;
using FinalProject.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.QueryServices;

public class DynamicQueryServices
{
    enum ComparisonMethod
    {
        Equal,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual
    }
    private static Expression<Func<T, bool>> CreateFilterExpression<T>(string propertyName, object constantValue, string operation)
    {
        if (Enum.IsDefined(typeof(ComparisonMethod), operation))
        {
            var parameter = Expression.Parameter(typeof(T), "prop");
            var property = Expression.Property(parameter, propertyName);
            var constant = Expression.Constant(constantValue);
            
            var comparisonMethod = typeof(Expression).GetMethod(operation,new[] { typeof(Expression) , typeof(Expression)});
            if (comparisonMethod == null) throw new InvalidOperationException("Invalid operation: " + operation);
            var exp = (Expression?) comparisonMethod.Invoke(null, new object[] {property, constant});
            if (exp == null) throw new InvalidOperationException("Invalid operation: " + operation);

            var lambda = Expression.Lambda<Func<T, bool>>(exp, parameter);
            
            return lambda;
        }

        throw new InvalidOperationException("Invalid operation: " + operation);
    }
    
    public static IQueryable<TEntity> Where<TEntity>(IQueryable<TEntity> source, string propertyName, object constantValue, string operation)
    {
        var lambda = CreateFilterExpression<TEntity>(propertyName, constantValue, operation);
        var result = source.Where(lambda);
        return result;
    }
    
    public static IQueryable<TEntity> OrderBy<TEntity>(IQueryable<TEntity> source, string orderByProperty, bool desc)
    {
        string command = desc ? "OrderByDescending" : "OrderBy";
        var type = typeof(TEntity);
        var property = type.GetProperty(orderByProperty);
        if (property == null) throw new ArgumentException();
        var parameter = Expression.Parameter(type, "p");
        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        var orderByExpression = Expression.Lambda(propertyAccess, parameter);
        var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
            source.Expression, Expression.Quote(orderByExpression));
        return source.Provider.CreateQuery<TEntity>(resultExpression);
    }
    
}