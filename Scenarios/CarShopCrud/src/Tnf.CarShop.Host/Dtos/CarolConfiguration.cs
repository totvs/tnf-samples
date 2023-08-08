using Dap.Domain;
using Dap.Domain.Enums;

namespace Tnf.CarShop.Host.Dtos;

public class CarolConfiguration
{
    public List<string> Apps { get; set; } = new List<string>();
    public bool UseErpIntegration { get; set; }
    public bool UserDefinedErp { get; set; }
    public string ConnectorName { get; set; }
    public string ConnectorGroup { get; set; }

    public ConnectorSelectionMode GetConnectorSelectionMode() => UseErpIntegration ?
        ConnectorSelectionMode.ErpInferred :
        ConnectorSelectionMode.Custom;

    public bool HasApp(string appName)
    {
        return Apps.Any(app => app == appName);
    }
}
