namespace CustomSoft.Projector
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Linq.Expressions;

  public class FilterMapping
  {
    #region Properties
    public IEnumerable<FilterMappingDefinition> Definition { get; private set; }
    #endregion

    #region Ctor
    public FilterMapping(IEnumerable<FilterMappingDefinition> def)
    {
      this.Definition = def;
    }
    #endregion

    #region Constants
    /// <summary>
    /// The default text comparison operator to use
    /// </summary>
    public const string DefaultTextOperator = "ct";
    #endregion

    #region Readonly properties
    /// <summary>
    /// Get the default filter if defined or else null
    /// </summary>
    public FilterMappingDefinition DefaultFilter
    {
      get
      {
        if (null == this.Definition)
        {
          return null;
        }

        return this.Definition.Where(x => x.DefaultFilterValue != null).FirstOrDefault();
      }
    }
    #endregion

    #region Static methods
    /// <summary>
    /// Apply the given filter to the given source "query"
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IQueryable<T> ApplyFilter<T>(FilterMapping mapping, IEnumerable<InputFilter> filters, IQueryable<T> source)
    {
      if (mapping == null)
      {
        return source;
      }

      foreach (var def in mapping.Definition)
      {
        if (!filters.Exists(def.Field))
        {
          continue;
        }

        // Yes there is a filter so apply it
        //foreach (var f in filters.Get(def.Field))
        //{
        var f = filters.Get(def.Field);
        MemberExpression left = null;
        var param = Expression.Parameter(typeof(T), "f");

        try
        {
          left = Expression.Property(param, typeof(T).GetProperty(def.Destination));
        }
        catch (Exception err)
        {
          throw new ArgumentOutOfRangeException(String.Format("Error during field mapping for {0}:{1}", f.Property, def.Destination), err);
        }

        var pred = FilterMapping.GetOperatorLambda(def, f, param, left);
        var expr = Expression.Call(typeof(Queryable), "Where", new Type[] { typeof(T) }, Expression.Constant(source), pred);

        source = source.Provider.CreateQuery<T>(expr);
        //}
      }

      return source;
    }

    /// <summary>
    /// Get the expression which "does" the comparison of the specified operator and its value
    /// </summary>
    /// <param name="def"></param>
    /// <param name="filter"></param>
    /// <param name="paramEx"></param>
    /// <param name="left"></param>
    /// <returns></returns>
    private static LambdaExpression GetOperatorLambda(FilterMappingDefinition def, InputFilter filter, ParameterExpression paramEx, MemberExpression left)
    {
      // Check for valid comparison operator or set the default ones
      var compOperator = filter.Operator;
      if (String.IsNullOrEmpty(compOperator) && !String.IsNullOrEmpty(def.DefaultFilterComparison))
      {
        compOperator = def.DefaultFilterComparison;
      }
      else if (String.IsNullOrEmpty(compOperator))
      {
        compOperator = "eq"; // Filter.DefaultComparisonOperator;
      }

      // Dynamically cast the value
      var castMethod = typeof(FilterMappingDefinition).GetMethod("Cast").MakeGenericMethod(def.Type);
      var casted = castMethod.Invoke(null, new object[] { filter.Value });

      if (compOperator.Equals("eq"))
      {
        return Expression.Lambda(Expression.Equal(left, Expression.Constant(casted, def.Type)), paramEx);
      }
      if (compOperator.Equals("lt"))
      {
        return Expression.Lambda(Expression.LessThan(left, Expression.Constant(casted, def.Type)), paramEx);
      }
      if (compOperator.Equals("lte"))
      {
        return Expression.Lambda(Expression.LessThanOrEqual(left, Expression.Constant(casted, def.Type)), paramEx);
      }
      if (compOperator.Equals("gt"))
      {
        return Expression.Lambda(Expression.GreaterThan(left, Expression.Constant(casted, def.Type)), paramEx);
      }
      if (compOperator.Equals("gte"))
      {
        return Expression.Lambda(Expression.GreaterThanOrEqual(left, Expression.Constant(casted, def.Type)), paramEx);
      }
      if (compOperator.Equals("sw"))
      {
        var startsWithEx = Expression.Call(left, typeof(string).GetMethod("StartsWith", new[] { typeof(string) }),
          Expression.Constant(casted));

        return Expression.Lambda(startsWithEx, paramEx);
      }
      if (compOperator.Equals("ct") || compOperator.Equals("like"))
      {
        var startsWithEx = Expression.Call(left, typeof(string).GetMethod("Contains", new[] { typeof(string) }),
          Expression.Constant(casted));

        return Expression.Lambda(startsWithEx, paramEx);
      }
      if (compOperator.Equals("in"))
      {
        var inExpr = Expression.Call(left, typeof(string).GetMethod("Contains", new[] { typeof(string) }),
          Expression.Constant(casted));

        return Expression.Lambda(inExpr, paramEx);
      }

      return Expression.Lambda(Expression.Equal(left, Expression.Constant(casted)), paramEx);
    }
    #endregion
  }
}
