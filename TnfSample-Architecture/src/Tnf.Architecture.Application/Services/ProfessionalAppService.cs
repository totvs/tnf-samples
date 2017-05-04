using Tnf.Application.Services;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Dto;
using System.Linq;
using Tnf.Architecture.Application.Interfaces;

namespace Tnf.Architecture.Application.Services
{
    public class ProfessionalAppService : ApplicationService, IProfessionalAppService
    {
        private readonly IProfessionalRepository _repository;

        public ProfessionalAppService(IProfessionalRepository repository)
        {
            _repository = repository;
        }

        public PagingDtoResponse<ProfessionalDto> GetAllProfessionals()
        {
            var professionalsDto = _repository.GetAll().Select(s => new ProfessionalDto()
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();

            return new PagingDtoResponse<ProfessionalDto>(professionalsDto);
        }
    }
}
