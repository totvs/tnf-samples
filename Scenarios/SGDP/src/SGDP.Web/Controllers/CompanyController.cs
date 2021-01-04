using Microsoft.AspNetCore.Mvc;
using SGDP.Application.Services.Interfaces;
using SGDP.Dto;
using SGDP.Dto.Company;
using System;
using System.Threading.Tasks;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.Dto;

namespace Tnf.Sgdp.GuineaPig.Controllers
{
    /// <summary>
    /// Company API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : TnfController
    {
        private readonly ICompanyAppService _appService;
        private const string _name = "Company";

        public CompanyController(ICompanyAppService appService)
        {
            _appService = appService;
        }

        /// <summary>
        /// Get all companies
        /// </summary>
        /// <param name="requestDto">Request params</param>
        /// <returns>List of companies</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IListDto<CompanyDto>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> GetAll([FromQuery] CompanyRequestAllDto requestDto)
        {
            var response = await _appService.GetAllAsync(requestDto);

            return CreateResponseOnGetAll(response, _name);
        }

        /// <summary>
        /// Get Company
        /// </summary>
        /// <param name="id">Company id</param>
        /// <param name="requestDto">Request params</param>
        /// <returns>Company requested</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CompanyDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Get(Guid id, [FromQuery] RequestDto requestDto)
        {
            var request = new DefaultRequestDto(id, requestDto);

            var response = await _appService.GetAsync(request);

            return CreateResponseOnGet(response, _name);
        }

        /// <summary>
        /// Create a new Company
        /// </summary>
        /// <param name="CompanyDto">Company to create</param>
        /// <returns>Company created</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CompanyDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Post([FromBody] CompanyDto CompanyDto)
        {
            CompanyDto = await _appService.CreateAsync(CompanyDto);

            return CreateResponseOnPost(CompanyDto, _name);
        }

        /// <summary>
        /// Update a Company
        /// </summary>
        /// <param name="id">Company id</param>
        /// <param name="CompanyDto">Company content to update</param>
        /// <returns>Updated Company</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CompanyDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Put(Guid id, [FromBody] CompanyDto CompanyDto)
        {
            CompanyDto = await _appService.UpdateAsync(id, CompanyDto);

            return CreateResponseOnPut(CompanyDto, _name);
        }

        /// <summary>
        /// Delete a Company
        /// </summary>
        /// <param name="id">Company id</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _appService.DeleteAsync(id);

            return CreateResponseOnDelete(_name);
        }
    }
}
