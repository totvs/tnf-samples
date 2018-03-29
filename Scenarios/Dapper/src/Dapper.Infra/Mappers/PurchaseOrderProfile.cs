using AutoMapper;
using Dapper.Infra.Dto;
using Dapper.Infra.Entities;

namespace Dapper.Infra.Mappers
{
    public class PurchaseOrderProfile : Profile
    {
        public PurchaseOrderProfile()
        {
            CreateMap<PurchaseOrder, PurchaseOrderDto>();
        }
    }
}
