namespace Tnf.SmartX.Domain.CodeFirst.Entities;

public class ZipCodeEntity : SXObject<ZipCodeEntity>
{
    public string ZipCode { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
}
