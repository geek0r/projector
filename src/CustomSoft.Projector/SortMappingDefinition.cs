namespace CustomSoft.Projector
{
  using System;

  /// <summary>
  /// The configuration for sorting fields
  /// </summary>
  public class SortMappingDefinition
  {
    public const string DIRECTION_ASCENDING = "ASC";

    public const string DIRECTION_DESCENDING = "DESC";

    #region Ctor
    /// <summary>
    /// Configure sorting where source and destination are equal.
    /// purchaseOrderId -> purchaseOrderId
    /// </summary>
    /// <param name="field"></param>
    public SortMappingDefinition(string field)
    {
      this.Field = field;
      this.Destination = field;
      this.DefaultDirection = DIRECTION_ASCENDING;
    }

    /// <summary>
    /// Configure sorting where field is mapped to destination.
    /// purchaseOrderId -> bes_bestell_nummer
    /// </summary>
    /// <param name="field"></param>
    /// <param name="destination"></param>
    public SortMappingDefinition(string field, string destination)
    {
      this.Field = field;
      this.Destination = destination;
      this.DefaultDirection = DIRECTION_ASCENDING;
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
