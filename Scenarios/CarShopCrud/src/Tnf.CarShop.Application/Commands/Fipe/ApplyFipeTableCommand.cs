namespace Tnf.CarShop.Application.Commands.Fipe;

public class ApplyFipeTableCommand
{
    public string FipeCode { get; set; }
    public string MonthYearReference { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal AveragePrice { get; set; }
}
