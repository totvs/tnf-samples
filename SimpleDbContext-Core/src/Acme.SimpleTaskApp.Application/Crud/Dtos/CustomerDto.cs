using Tnf.Application.Services.Dto;
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
