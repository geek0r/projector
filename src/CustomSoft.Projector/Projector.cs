namespace CustomSoft.Projector
{
  using System.Linq;

  /// <summary>
  /// Project the given request onto the specified data source. This allows sorting, filtering and pagination
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public static class Projector<T>
  {
    /// <summary>
    /// Apply the projection onto a deferred query
    /// </summary>
    /// <param name="query"></param>
    /// <param name="offset"></param>
    /// <param name="limit"></param>
    /// <param name="filter"></param>
    /// <param name="sort"></param>
    /// <param name="initiallySorted">Is the passed-in collection already sorted?</param>
    /// <returns></returns>
    public static ProjectedResult<T> ApplyProjection(IQueryable<T> query, int offset, int limit, string filter, string sort, bool initiallySorted)
    {
      var sorting = InputSorting.FromInput(sort);
      var filtering = InputFilter.FromInput(filter);

      query = SortMapping.ApplySorting<T>(ProjectionConfiguration.GetSortMapping(typeof(T)), sorting, query, initiallySorted);
      query = FilterMapping.ApplyFilter(ProjectionConfiguration.GetFilterMapping(typeof(T)), filtering, query);

      if (offset < 0)
      {
        offset = 0;
      }

      var result = new ProjectedResult<T>()
      {
        BaseQuery = query,
        Query = query.Skip(offset)
      };

      // If the limit is negative ... do not limit the data
      if (-1 == limit)
      {
        if (!ProjectionConfiguration.ItemsPerPageAllowNoLimit)
        {
          limit = (int)ProjectionConfiguration.ItemsPerPageThreshold;
        }
      }
      else
      {
        if (limit > (int)ProjectionConfiguration.ItemsPerPageThreshold)
        {
          limit = (int)ProjectionConfiguration.ItemsPerPageThreshold;
        }

        result.Query = result.Query.Take(limit);
      }

      return result;
    }
  }
}
