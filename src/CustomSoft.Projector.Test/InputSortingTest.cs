using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace CustomSoft.Projector.Test
{
  [TestClass]
  public class InputSortingTest
  {
    [TestMethod]
    public void Sorting_Is_Detected_When_Provided()
    {
      var input = "[{\"property\":\"status\",\"direction\":\"ASC\"},{\"property\":\"id\",\"direction\":\"DESC\"}]";
      var sortings = InputSorting.FromInput(input);

      Assert.AreEqual(2, sortings.Count());
    }

    [TestMethod]
    public void Sorting_Is_Evaluated_With_Default_Mapping()
    {
      ProjectionConfiguration.ConfigureSorting(typeof(PurchaseOrder),
        new List<SortMappingDefinition>()
        {
          new SortMappingDefinition("id", "Id"),
          new SortMappingDefinition("idh_num", "IdhNum"),
          new SortMappingDefinition("status", "Status")
        }
      );

      var input = "[{\"property\":\"status\",\"direction\":\"ASC\"},{\"property\":\"id\",\"direction\":\"DESC\"}]";
      var sortings = InputSorting.FromInput(input);

      var data = PurchaseOrder.Data;
      var query = (from d in data select d).AsQueryable();

      var projected = Projector<PurchaseOrder>.ApplyProjection(
        query,
        0,
        1000,
        string.Empty,
        input,
        false
      );

      Assert.AreEqual("AAA", projected.Query.FirstOrDefault().Status);
    }

    [TestMethod]
    public void Sorting_Is_Evaluated_With_Default_Mapping_When_Initially_Sorted()
    {
      ProjectionConfiguration.ConfigureSorting(typeof(PurchaseOrder),
        new List<SortMappingDefinition>()
        {
          new SortMappingDefinition("id", "Id"),
          new SortMappingDefinition("idh_num", "IdhNum"),
          new SortMappingDefinition("status", "Status")
        }
      );

      var input = "[{\"property\":\"status\",\"direction\":\"DESC\"},{\"property\":\"id\",\"direction\":\"DESC\"}]";
      var sortings = InputSorting.FromInput(input);

      var data = PurchaseOrder.Data;
      var query = (from d in data select d)
        .OrderBy(x => x.Price)
        .AsQueryable();

      var projected = Projector<PurchaseOrder>.ApplyProjection(
        query,
        0,
        1000,
        string.Empty,
        input,
        true
      );

      Assert.AreEqual(3, projected.Query.FirstOrDefault().Id);
    }
  }
}
