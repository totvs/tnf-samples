using Acme.SimpleTaskApp.Courses;
using Acme.SimpleTaskApp.People;
using Acme.SimpleTaskApp.Service.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Application.Services.Dto;
using Tnf.Domain.Repositories;
using Tnf.Runtime.Validation;

namespace Acme.SimpleTaskApp.Transaction
{
    public class ImplicitTransactionAppService : SimpleTaskAppAppServiceBase
    {
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<Person> _personRepository;
        public ImplicitTransactionAppService(IRepository<Course> courseRepository, IRepository<Person> personRepository)
        {
            _courseRepository = courseRepository;
            _personRepository = personRepository;
        }

        [DisableValidation]
        public void Create()
        {
            _personRepository.Insert(new Person("Person"));
            _courseRepository.Insert(new Course("Course") { Id = 1 });

        }

        public async Task<ListResultDto<PersonDto>> GetCourses()
        {

            var courses = await _courseRepository
                      .GetAll()
                      .ToListAsync();

            return new ListResultDto<PersonDto>(
                ObjectMapper.Map<List<PersonDto>>(courses)
            );
        }

        public async Task<ListResultDto<PersonDto>> GetPersons()
        {
            var persons = await _personRepository
                      .GetAll()
                      .ToListAsync();

            return new ListResultDto<PersonDto>(
                ObjectMapper.Map<List<PersonDto>>(persons)
            );
        }
    }
}
