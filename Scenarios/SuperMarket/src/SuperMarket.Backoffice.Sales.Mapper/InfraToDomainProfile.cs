using AutoMapper;
using SuperMarket.Backoffice.Sales.Domain.Entities;
using SuperMarket.Backoffice.Sales.Infra.Pocos;

namespace SuperMarket.Backoffice.Sales.Mapper
{
    public class InfraToDomainProfile : Profile
    {
        public InfraToDomainProfile()
        {
            CreateMap<PurchaseOrderPoco, PurchaseOrder>()
                .ForMember(domain => domain.Lines, s => s.Ignore())
                .AfterMap((poco, domain) =>
                {
                    foreach (var purchaseOrderProduct in poco.PurchaseOrderProducts)
                    {
                        domain.Lines.Add(new PurchaseOrder.PurchaseOrderLine(purchaseOrderProduct.ProductId, purchaseOrderProduct.Quantity));
                    }
                });
        }
    }
}
