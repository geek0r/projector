using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

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
