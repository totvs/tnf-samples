namespace Tnf.CarShop.Application.Messages;

public class ApplyFipeTable 
{
    public string FipeCode { get; set; }
    public string MonthYearReference { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal AveragePrice { get; set; }
}
