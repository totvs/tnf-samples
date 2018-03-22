using AutoMapper;
using SuperMarket.Backoffice.Sales.Dto;
using SuperMarket.Backoffice.Sales.Infra.Pocos;

namespace SuperMarket.Backoffice.Sales.Mapper
{
    public class InfraToDtoProfile : Profile
    {
        public InfraToDtoProfile()
        {
            CreateMap<PurchaseOrderPoco, PurchaseOrderDto>()
                .ForMember(dto => dto.Products, s => s.Ignore())
                .AfterMap((poco, dto) =>
                {
                    foreach (var purchaseOrderProduct in poco.PurchaseOrderProducts)
                    {
                        dto.Products.Add(new PurchaseOrderDto.ProductDto(purchaseOrderProduct.ProductId, purchaseOrderProduct.Quantity));
                    }
                });
        }
    }
}
