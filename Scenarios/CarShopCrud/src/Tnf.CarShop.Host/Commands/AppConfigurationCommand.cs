using CarShop.Commands.Transactional;
using Tnf.CarShop.Host.Dtos;

namespace CarShop.Commands;

public class AppConfigurationCommand : ITransactionCommand
{
    public string AppCode { get; set; }
    public bool UseCarol { get; set; }
    public bool GenerateSecret { get; set; }
    public bool SendCredentialsEmail { get; set; }
    public CarolConfiguration CarolConfiguration { get; set; }
    public string CompletedCallBack { get; set; }
    public string StatusCallBack { get; set; }
    public bool IsEnabled { get; set; }
}
