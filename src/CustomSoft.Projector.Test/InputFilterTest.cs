using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CustomSoft.Projector.Test
{
  [TestClass]
  public class InputFilterTest
  {
    [TestMethod]
    public void Filters_Are_Detected_When_Provided()
    {
      var input = "[{\"type\":\"string\",\"value\":\"a00056\",\"field\":\"idh_num\"},{\"type\":\"numeric\",\"value\":\"10.89\",\"property\":\"price\"}]";
      var filters = InputFilter.FromInput(input);

      Assert.AreEqual(2, filters.Count());
    }

    [TestMethod]
    public void Filter_Is_Found()
    {
      var input = "[{\"type\":\"string\",\"value\":\"a00056\",\"property\":\"idh_num\"}]";
      var filters = InputFilter.FromInput(input);

      Assert.IsTrue(filters.Exists("idh_num"));
      Assert.AreEqual("a00056", filters.Get("idh_num").Value);
    }

    [TestMethod]
    public void Filter_With_Legacy_Field_Is_Evaluated_Correctly()
    {
      var input = "[{\"type\":\"string\",\"value\":\"a00056\",\"field\":\"idh_num\"}]";
      var filters = InputFilter.FromInput(input);

      Assert.IsTrue(filters.Exists("idh_num"));
      Assert.AreEqual("a00056", filters.Get("idh_num").Value);
    }
  }
}
