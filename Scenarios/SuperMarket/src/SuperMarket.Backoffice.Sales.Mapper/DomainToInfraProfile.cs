using AutoMapper;
using SuperMarket.Backoffice.Sales.Domain.Entities;
using SuperMarket.Backoffice.Sales.Infra.Pocos;
using System.Linq;

namespace SuperMarket.Backoffice.Sales.Mapper
{
    public class DomainToInfraProfile : Profile
    {
        public DomainToInfraProfile()
        {
            CreateMap<PurchaseOrder, PurchaseOrderPoco>()
                .AfterMap((domain, poco) =>
                {
                    // Add or Update when item exist
                    foreach (var line in domain.Lines)
                    {
                        var purchaseOrderProduct = poco.PurchaseOrderProducts
                            .FirstOrDefault(w => w.ProductId == line.ProductId);

                        if (purchaseOrderProduct == null)
                        {
                            purchaseOrderProduct = new PurchaseOrderProductPoco();
                            poco.PurchaseOrderProducts.Add(purchaseOrderProduct);
                        }

                        purchaseOrderProduct.Quantity = line.Quantity;
                        purchaseOrderProduct.UnitValue = domain.GetProductPrice(line.ProductId);
                    }
                });
        }
    }
}
