using Acme.SimpleTaskApp.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tnf.Application.Services.Dto;
using Tnf.AutoMapper;

namespace Acme.SimpleTaskApp.Service.Dtos
{

    [AutoMap(typeof(Person))]
    public class PersonDto : EntityDto
    {
        public string PersonName { get; set; }
    }
}
