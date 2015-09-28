namespace CustomSoft.Projector
{
  using System;
  using System.Collections.Generic;

  /// <summary>
  /// Configuration for the mapping of sorting and filtering
  /// </summary>
  public static class ProjectionConfiguration
  {
    #region Private fields
    private static Object lockObj = new Object();
    private static Dictionary<Type, SortMapping> SortMappings = null;
    private static Dictionary<Type, FilterMapping> FilterMappings = null;
    #endregion

    /// <summary>
    /// The maximum number of items allowed to be used during pagination.
    /// This value will be used of the request exceeds this limit
    /// </summary>
    public static uint ItemsPerPageThreshold { get; set; } = 10000;

    /// <summary>
    /// Allow a limit-less retrieval of data of the itemsperpage setting is set to -1
    /// </summary>
    public static bool ItemsPerPageAllowNoLimit { get; set; } = true;

    #region Public methods
    /// <summary>
    /// Get available mapping configuration for the specified type
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static SortMapping GetSortMapping(Type t)
    {
      lock (lockObj)
      {
        if (null == SortMappings || !SortMappings.ContainsKey(t))
        {
          return null;
        }

        return (SortMapping)SortMappings[t];
      }
    }

    /// <summary>
    /// Get available mapping configuration for the specified type
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static FilterMapping GetFilterMapping(Type t)
    {
      lock (lockObj)
      {
        if (null == FilterMappings || !FilterMappings.ContainsKey(t))
        {
          return null;
        }

        return (FilterMapping)FilterMappings[t];
      }
    }

    /// <summary>
    /// Configure the sorting for the specified type
    /// </summary>
    /// <param name="t"></param>
    /// <param name="definitions"></param>
    public static void ConfigureSorting(Type t, IEnumerable<SortMappingDefinition> definitions)
    {
      lock (lockObj)
      {
        if (null == SortMappings)
        {
          SortMappings = new Dictionary<Type, SortMapping>();
        }

        SortMappings[t] = new SortMapping(definitions);
      }
    }

    /// <summary>
    /// Configure the filtering for the specified type
    /// </summary>
    /// <param name="t"></param>
    /// <param name="definitions"></param>
    public static void ConfigureFiltering(Type t, IEnumerable<FilterMappingDefinition> definitions)
    {
      lock (lockObj)
      {
        if (null == FilterMappings)
        {
          FilterMappings = new Dictionary<Type, FilterMapping>();
        }

        FilterMappings[t] = new FilterMapping(definitions);
      }
    }
    #endregion
  }
}