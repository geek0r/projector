namespace CustomSoft.Projector
{
  using System.Linq;

  public class ProjectedResult<T>
  {
    /// <summary>
    /// The query without skip and take
    /// </summary>
    public IQueryable<T> BaseQuery { get; set; }

    /// <summary>
    /// The query with skip and take applied
    /// </summary>
    public IQueryable<T> Query { get; set; }
  }
}
