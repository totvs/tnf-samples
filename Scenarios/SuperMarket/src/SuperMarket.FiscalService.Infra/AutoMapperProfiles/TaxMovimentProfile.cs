using AutoMapper;
using SuperMarket.FiscalService.Domain.Entities;
using SuperMarket.FiscalService.Infra.Dtos;

namespace SuperMarket.FiscalService.Infra.AutoMapperProfiles
{
    public class TaxMovimentProfile : Profile
    {
        public TaxMovimentProfile()
        {
            CreateMap<TaxMoviment, TaxMovimentDto>();
        }
    }
}
