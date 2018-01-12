using Case2.Infra.Dtos;
using Case2.Infra.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tnf.Dapper.Repositories;
using Tnf.Dto;

namespace Case2.Web.Controllers
{
    [Route("api/employee")]
    public class EmployeeController : TnfController
    {
        private readonly IDapperRepository<Employee, Guid> _employeeRepository;

        public EmployeeController(IDapperRepository<Employee, Guid> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]RequestAllDto request)
        {
            var response = await _employeeRepository.GetAllAsync<EmployeeDto>(request);

            return CreateResponseOnGetAll(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, [FromQuery]RequestDto<Guid> requestDto)
        {
            requestDto.WithId(id);

            var response = await _employeeRepository.GetAsync(requestDto);

            return CreateResponseOnGet<EmployeeDto, Guid>(response.MapTo<EmployeeDto>());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]EmployeeDto employee)
        {
            var entity = employee.MapTo<Employee>();

            entity.Id = await _employeeRepository.InsertAndGetIdAsync(entity);

            return CreateResponseOnPost<EmployeeDto, Guid>(entity.MapTo<EmployeeDto>());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]EmployeeDto employee)
        {
            var entity = employee.MapTo<Employee>();
            entity.Id = id;

            await _employeeRepository.UpdateAsync(entity);

            return CreateResponseOnPut<EmployeeDto, Guid>(entity.MapTo<EmployeeDto>());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _employeeRepository.DeleteAsync(w => w.Id == id);

            return CreateResponseOnDelete<EmployeeDto, Guid>();
        }
    }
}
