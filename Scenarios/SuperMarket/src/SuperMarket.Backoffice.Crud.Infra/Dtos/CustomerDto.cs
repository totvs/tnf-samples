using System;
using Tnf.Dto;

namespace SuperMarket.Backoffice.Crud.Infra.Dtos
{
    public class CustomerDto : DtoBase<Guid>
    {
        public string Name { get; set; }
    }
}
