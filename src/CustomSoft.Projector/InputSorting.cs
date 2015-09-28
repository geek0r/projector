namespace CustomSoft.Projector
{
  using Newtonsoft.Json;
  using System;
  using System.Collections.Generic;

  /// <summary>
  /// Object representation for the Ext JS sorting notation
  /// </summary>
  public class InputSorting
  {
    public String Property { get; set; }

    public String Direction { get; set; }

    public String DefaultDirection
    {
      get
      {
        return "ASC";
      }
    }

    #region Static methods
    public static IEnumerable<InputSorting> FromInput(string input)
    {
      if (String.IsNullOrWhiteSpace(input))
      {
        return new List<InputSorting>();
      }

      return JsonConvert.DeserializeObject<IEnumerable<InputSorting>>(input);
    }
    #endregion
  }
}