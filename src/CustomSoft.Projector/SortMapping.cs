namespace CustomSoft.Projector
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Linq.Expressions;

  /// <summary>
  /// Mapping for sorting against a datasource
  /// </summary>
  public class SortMapping
  {
    #region Ctor
    public SortMapping()
    {
      this.Definition = new List<SortMappingDefinition>();
    }

    public SortMapping(IEnumerable<SortMappingDefinition> definition)
    {
      this.Definition = definition;
    }
    #endregion

    #region Properties
    public IEnumerable<SortMappingDefinition> Definition { get; set; }
    #endregion

    #region Static methods
    /// <summary>
    /// Apply the sorting to the queryable collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="mapping"></param>
    /// <param name="filter"></param>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IQueryable<T> ApplySorting<T>(SortMapping mapping, IEnumerable<InputSorting> sortings, IQueryable<T> source, bool initiallySorted)
    {
      if (null == mapping)
      {
        throw new ArgumentNullException("mapping", String.Format("No sorting mapping specified for {0}", typeof(T)));
      }

      if (0 == sortings.Count())
      {
        // Add the first field as default
        var first = mapping.Definition.FirstOrDefault();
        if (first == null)
        {
          throw new ArgumentNullException("first", "No default sorting field defined!");
        }

        sortings = new List<InputSorting>()
        {
          new InputSorting()
          {
            Property = first.Field,
            Direction = first.DefaultDirection
          }
        };
      }

      var sortingApplied = initiallySorted;
      foreach (var sort in sortings)
      {
        var sortMapping = mapping.Definition.Where(x => x.Field.Equals(sort.Property)).FirstOrDefault();

        if (sortMapping == null)
        {
          throw new Exception(String.Format("Sorting configuration for '{0}' not found.", sort.Property));
        }

        // The property on the mapping-object
        var prop = typeof(T).GetProperty(sortMapping.Destination);

        // Yes there is a sorting so apply it
        var param = Expression.Parameter(typeof(T), "s");
        var sortEx = Expression.Property(param, prop);
        var pred = Expression.Lambda(sortEx, param);

        var s = sortings.FirstOrDefault();
        var method = (s.Direction.Equals("ASC") ? "OrderBy" : "OrderByDescending");
        if (sortingApplied)
        {
          method = (s.Direction.Equals("ASC") ? "ThenBy" : "ThenByDescending");
        }

        var expr = Expression.Call(typeof(Queryable), method, new Type[] { typeof(T), prop.PropertyType }, source.Expression, pred);

        source = source.Provider.CreateQuery<T>(expr);
        sortingApplied = true;
      }

      return source;
    }
    #endregion
  }
}

