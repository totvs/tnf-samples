using System;
using System.Linq;
using Tnf.App.Application.Services;
using Tnf.App.Domain.Services;
using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Dto.Registration;
using Tnf.AutoMapper;

namespace Tnf.Architecture.Application.Services
{
    public class PersonAppService : AppApplicationService, IPersonAppService
    {
        private readonly IAppDomainService<Person> _service;

        public PersonAppService(IAppDomainService<Person> service)
        {
            _service = service;
        }

        public ListDto<PersonDto, int> GetAll(GetAllPeopleDto request)
            => _service.GetAll<PersonDto>(request, p => request.Name.IsNullOrEmpty() || p.Name.Contains(request.Name));

        public PersonDto Get(RequestDto<int> id)
        {
            ValidateRequestDto(id, nameof(id));

            if (Notification.HasNotification())
                return PersonDto.NullInstance;

            var entity = _service.Get(id);

            return entity.MapTo<PersonDto>();
        }

        public PersonDto Create(PersonDto person)
        {
            ValidateDto(person, nameof(person));

            if (Notification.HasNotification())
                return PersonDto.NullInstance;

            var personBuilder = new PersonBuilder(Notification)
                    .WithId(person.Id)
                    .WithName(person.Name)
                    .WithChildren(person.Children.Select(p => new PersonBuilder(Notification).WithId(p.Id).WithName(p.Name)).ToList());

            person.Id = _service.InsertAndGetId(personBuilder);

            return person;
        }

        public PersonDto Update(int id, PersonDto person)
        {
            ValidateDtoAndId(person, id, nameof(person), nameof(id));

            if (Notification.HasNotification())
                return PersonDto.NullInstance;

            var personBuilder = new PersonBuilder(Notification)
                    .WithId(id)
                    .WithName(person.Name)
                    .WithChildren(person.Children.Select(p => new PersonBuilder(Notification).WithId(p.Id).WithName(p.Name)).ToList());

            _service.Update(personBuilder);

            person.Id = id;
            return person;
        }

        public void Delete(int id)
        {
            ValidateId(id, nameof(id));

            if (Notification.HasNotification())
                return;

            _service.Delete(id);
        }
    }
}
