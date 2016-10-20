using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace CustomSoft.Projector.Test
{
  [TestClass]
  public class CombinationTest
  {
    [TestMethod]
    public void Sorting_And_Filtering_Is_Applied()
    {
      ProjectionConfiguration.ConfigureSorting(typeof(PurchaseOrder),
        new List<SortMappingDefinition>()
        {
          new SortMappingDefinition("id", "Id"),
          new SortMappingDefinition("idh_num", "IdhNum"),
          new SortMappingDefinition("status", "Status")
        }
      );

      ProjectionConfiguration.ConfigureFiltering(typeof(PurchaseOrder),
        new List<FilterMappingDefinition>()
        {
          new FilterMappingDefinition("id", "Id", typeof(int)),
          new FilterMappingDefinition("idh_num", "IdhNum", typeof(string)),
          new FilterMappingDefinition("status", "Status", typeof(string), null, "sw"),
          new FilterMappingDefinition("salesPrice", "Price", typeof(decimal))
        }
      );

      var inputSort = "[{\"property\":\"status\",\"direction\":\"DESC\"},{\"property\":\"id\",\"direction\":\"DESC\"}]";
      var inputFilter = "[{\"type\":\"string\",\"value\":\"00006779\",\"field\":\"idh_num\"}]";

      var data = PurchaseOrder.Data;
      var query = data.AsQueryable()
        .Where(x => x.Price > 50.00M)
        .OrderBy(x => x.Price);

      var projected = Projector<PurchaseOrder>.ApplyProjection(
        query,
        0,
        1000,
        inputFilter,
        inputSort,
        true
      );

      Assert.AreEqual(2, projected.Query.FirstOrDefault().Id);
    }
  }
}
