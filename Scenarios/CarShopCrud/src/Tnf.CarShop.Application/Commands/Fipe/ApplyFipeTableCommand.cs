using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Fipe;

//Exemplo de um comando sem retorno e que implementa um ICommand
public class ApplyFipeTableCommand : ICommand
{
    public string FipeCode { get; set; }
    public string MonthYearReference { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal AveragePrice { get; set; }
}
