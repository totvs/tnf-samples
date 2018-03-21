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
                .ForMember(d => d.Lines, s => s.Ignore())
                .AfterMap((s, d) =>
                {
                    foreach (var purchaseOrderProduct in s.PurchaseOrderProducts)
                    {
                        d.Lines.Add(new PurchaseOrder.PurchaseOrderLine(purchaseOrderProduct.ProductId, purchaseOrderProduct.Quantity));
                    }
                });
        }
    }
}
