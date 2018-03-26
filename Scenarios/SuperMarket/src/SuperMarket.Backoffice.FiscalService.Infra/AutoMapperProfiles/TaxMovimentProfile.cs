using AutoMapper;
using SuperMarket.Backoffice.FiscalService.Domain.Entities;
using SuperMarket.Backoffice.FiscalService.Infra.Dtos;

namespace SuperMarket.Backoffice.FiscalService.Infra.AutoMapperProfiles
{
    public class TaxMovimentProfile : Profile
    {
        public TaxMovimentProfile()
        {
            CreateMap<TaxMoviment, TaxMovimentDto>();
        }
    }
}
