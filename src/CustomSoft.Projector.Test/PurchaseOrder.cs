using System.Collections.Generic;

namespace CustomSoft.Projector.Test
{
  internal class PurchaseOrder
  {
    public int Id { get; set; }

    public string Status { get; set; }

    public string IdhNum { get; set; }

    public decimal Price { get; set; }

    public static IEnumerable<PurchaseOrder> Data
    {
      get
      {
        return new List<PurchaseOrder>()
        {
          new PurchaseOrder()
          {
            Id = 1,
            Status = "Test",
            IdhNum = "00006778",
            Price = 100.78M
          },
          new PurchaseOrder()
          {
            Id = 2,
            Status = "Offline",
            IdhNum = "00006779",
            Price = 100.78M
          },
          new PurchaseOrder()
          {
            Id = 3,
            Status = "AAA",
            IdhNum = "00006745",
            Price = 9.78M
          },
          new PurchaseOrder()
          {
            Id = 4,
            Status = "Offline",
            IdhNum = "1234567890",
            Price = 48.34M
          },
          new PurchaseOrder()
          {
            Id = 5,
            Status = "AAA",
            IdhNum = "GAGS3473856",
            Price = 66.50M
          }
        };
      }
    }
  }
}
