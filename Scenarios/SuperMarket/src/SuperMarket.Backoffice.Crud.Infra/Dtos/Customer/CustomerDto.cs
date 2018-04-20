using System;
using Tnf.Dto;

namespace SuperMarket.Backoffice.Crud.Infra.Dtos
{
    public class CustomerDto : BaseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
