namespace CustomSoft.Projector
{
  using System;

  public class SortMappingDefinition
  {
    #region Ctor
    public SortMappingDefinition(string field, string destination)
    {
      this.Field = field;
      this.Destination = destination;
      this.DefaultDirection = "ASC";
    }

    public SortMappingDefinition(string field, string destination, string defaultDirection)
    {
      this.Field = field;
      this.Destination = destination;
      this.DefaultDirection = defaultDirection;
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
    /// The default sorting direction
    /// </summary>
    public String DefaultDirection { get; set; }
    #endregion
  }
}
