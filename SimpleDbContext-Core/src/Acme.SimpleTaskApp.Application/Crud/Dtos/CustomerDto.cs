using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tnf.AutoMapper;

namespace Acme.SimpleTaskApp.Crud.Dtos
{
    [AutoMap(typeof(Customer))]
    public class CustomerDto : EntityDto
    {
        public string Nome { get; set; }

        public string NomeFantasia { get; set; }
    }
}
