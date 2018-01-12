using System;
using Tnf.Dto;

namespace Case2.Infra.Dtos
{
    public class CustomerDto : DtoBase<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
