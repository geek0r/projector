using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomSoft.Projector.Test
{
  [TestClass]
  public class ConfigurationTest
  {
    #region Data
    private static List<SortMappingDefinition> Sortings = new List<SortMappingDefinition>()
    {
      new SortMappingDefinition("foobar", "ASC")
    };

    private static List<FilterMappingDefinition> Filters = new List<FilterMappingDefinition>()
    {
      new FilterMappingDefinition("foobar", "menge", typeof(decimal?))
    };
    #endregion

    [TestMethod]
    public void Default_State_Is_As_Brain_Wants_It()
    {
      Assert.AreEqual((uint)10000, ProjectionConfiguration.ItemsPerPageThreshold);
      Assert.AreEqual(true, ProjectionConfiguration.ItemsPerPageAllowNoLimit);
    }

    [TestMethod]
    public void Sorting_Configuration_Is_Empty_When_Nothing_Was_Added()
    {
      Assert.IsNull(ProjectionConfiguration.GetSortMapping(typeof(String)));
    }

    [TestMethod]
    public void Sorting_Configuration_Exists_When_Added()
    {
      ProjectionConfiguration.ConfigureSorting(typeof(String), Sortings);

      Assert.IsNotNull(ProjectionConfiguration.GetSortMapping(typeof(String)));
    }

    [TestMethod]
    public void Sorting_Configuration_Is_Valid_When_Source_And_Destination_Equal()
    {
      ProjectionConfiguration.ConfigureSorting(typeof(String),
        new List<SortMappingDefinition>()
        {
          new SortMappingDefinition("purchaseOrderId")
        });

      Assert.IsNotNull(ProjectionConfiguration.GetSortMapping(typeof(String)));
      Assert.IsTrue(ProjectionConfiguration.GetSortMapping(typeof(String))
        .Definition
        .FirstOrDefault()
        .Destination
        .Equals("purchaseOrderId")
      );
    }

    [TestMethod]
    public void Filtering_Configuration_Is_Empty_When_Nothing_Was_Added()
    {
      Assert.IsNull(ProjectionConfiguration.GetFilterMapping(typeof(String)));
    }

    [TestMethod]
    public void Filtering_Configuration_Exists_When_Added()
    {
      ProjectionConfiguration.ConfigureFiltering(typeof(String), Filters);

      Assert.IsNotNull(ProjectionConfiguration.GetFilterMapping(typeof(String)));
    }
  }
}
