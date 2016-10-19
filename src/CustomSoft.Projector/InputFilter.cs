namespace CustomSoft.Projector
{
  using Newtonsoft.Json;
  using System;
  using System.Collections.Generic;
  using System.Linq;

  /// <summary>
  /// The representation of the Ext JS json-encoded filter objects
  /// </summary>
  public class InputFilter
  {
    public string Property { get; set; }

    public string Value { get; set; }

    public string Operator { get; set; }

    /// <summary>
    /// Is there a valid value to filter for?
    /// </summary>
    public bool HasValidValue
    {
      get
      {
        return !String.IsNullOrWhiteSpace(this.Value);
      }
    }

    #region Public static methods
    /// <summary>
    /// Generate the collection of objects from a json-input-string
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static IEnumerable<InputFilter> FromInput(string input)
    {
      if (null == input)
      {
        return new List<InputFilter>();
      }

      return JsonConvert.DeserializeObject<IEnumerable<InputFilter>>(input);
    }

    /// <summary>
    /// Create a JSON represenation of the given filters
    /// </summary>
    /// <param name="filter">Collection of internal filters to represent as json</param>
    /// <returns></returns>
    public static String FromInteral(IEnumerable<InputFilter> filter)
    {
      if (null == filter || 0 == filter.Count())
      {
        return String.Empty;
      }

      return JsonConvert.SerializeObject(filter);
    }
    #endregion
  }

  /// <summary>
  /// Extension methods for the input filter stuff
  /// </summary>
  public static class InputFilterExtensions
  {
    /// <summary>
    /// Is the given filter key existent within the parsed filter collection
    /// </summary>
    /// <param name="data"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool Exists(this IEnumerable<InputFilter> data, string key)
    {
      return null != data.Where(x => x.Property == key).FirstOrDefault();
    }

    /// <summary>
    /// Get the specific filter key
    /// </summary>
    /// <param name="data"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static InputFilter Get(this IEnumerable<InputFilter> data, string key)
    {
      if (null == data)
      {
        return null;
      }

      return data.Where(x => x.Property == key).FirstOrDefault();
    }

    /// <summary>
    /// Check if the given filter key has valid data.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool IsNotEmpty(this IEnumerable<InputFilter> data, string key)
    {
      return !String.IsNullOrWhiteSpace(data.GetValueWithDefault(key, null));
    }

    /// <summary>
    /// Get the specified value of the filter or the default value specified
    /// </summary>
    /// <param name="data"></param>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static string GetValueWithDefault(this IEnumerable<InputFilter> data, string key, string defaultValue)
    {
      if (null == data)
      {
        return defaultValue;
      }

      if (data.Get(key) == null)
      {
        return defaultValue;
      }

      return data.Get(key).Value;
    }
  }
}