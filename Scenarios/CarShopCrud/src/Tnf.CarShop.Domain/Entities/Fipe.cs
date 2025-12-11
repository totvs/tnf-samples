using Tnf.Repositories.Entities.Auditing;

namespace Tnf.CarShop.Domain.Entities;

public class Fipe : IHasCreationTime, IHasModificationTime
{
    public Guid Id { get; }
    public string FipeCode { get; private set; }
    public string MonthYearReference { get; private set; }
    public string Brand { get; private set; }
    public string Model { get; private set; }
    public int Year { get; private set; }
    public decimal AveragePrice { get; private set; }

    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }

    public Fipe(string fipeCode, string monthYearReference, string brand, string model, int year, decimal averagePrice)
    {
        FipeCode = fipeCode;
        MonthYearReference = monthYearReference;
        Brand = brand;
        Model = model;
        Year = year;
        AveragePrice = averagePrice;
    }

    public void UpdateTable(string monthYearReference, string brand, string model, int year, decimal averagePrice)
    {
        MonthYearReference = monthYearReference;
        Brand = brand;
        Model = model;
        Year = year;
        AveragePrice = averagePrice;
    }
}
