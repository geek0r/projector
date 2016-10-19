namespace CustomSoft.Projector
{
  using System;
  using System.ComponentModel;

  /// <summary>
  /// Define how external fields are mapped into fields of the database
  /// </summary>
  public class FilterMappingDefinition
  {
    #region Ctor
    public FilterMappingDefinition()
    {
      this.Type = typeof(String);
      this.DefaultFilterValue = null;
    }

    /// <summary>
    /// Add a new mapping
    /// </summary>
    /// <param name="field">The external fieldname</param>
    /// <param name="destination">The internal fieldname mapped against the datasource</param>
    /// <param name="t">The type of the field. Nullable types need to be specified</param>
    public FilterMappingDefinition(string field, string destination, Type t)
    {
      this.Field = field;
      this.Destination = destination;
      this.Type = t;
      this.DefaultFilterValue = null;
      this.DefaultFilterComparison = null;
    }

    /// <summary>
    /// Add a new mapping
    /// </summary>
    /// <param name="field">The external fieldname</param>
    /// <param name="destination">The internal fieldname mapped against the datasource</param>
    /// <param name="t">The type of the field. Nullable types need to be specified</param>
    /// <param name="defaultValue">The default value to use for filtering if not specified by the input</param>
    /// <param name="defaultComparison">The default comparison operator if not specified be the input</param>
    public FilterMappingDefinition(string field, string destination, Type t, object defaultValue, string defaultComparison)
    {
      this.Field = field;
      this.Destination = destination;
      this.Type = t;
      this.DefaultFilterValue = defaultValue;
      this.DefaultFilterComparison = defaultComparison;
    }
    #endregion

    #region Properties
    /// <summary>
    /// The field in the which is used in the "ui layer"
    /// </summary>
    public String Field { get; set; }

    /// <summary>
    /// The name of the destination property to filter against
    /// </summary>
    public String Destination { get; set; }

    /// <summary>
    /// The type to use
    /// </summary>
    public Type Type { get; set; }

    /// <summary>
    /// The default filter value to use
    /// </summary>
    public object DefaultFilterValue { get; set; }

    /// <summary>
    /// The default comparison operation
    /// </summary>
    public String DefaultFilterComparison { get; set; }
    #endregion

    #region Static methods
    /// <summary>
    /// Cast the given value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="o"></param>
    /// <returns></returns>
    public static T Cast<T>(object o)
    {
      var conv = TypeDescriptor.GetConverter(typeof(T));

      return (T)conv.ConvertFrom(o);
    }
    #endregion
  }
}
