using System.Collections.Generic;
using System.Linq;
using Tnf.App.Dapper.Repositories;
using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Tnf.AutoMapper;
using Tnf.Data;

namespace Tnf.Architecture.EntityFrameworkCore.Repositories
{
    public class ProfessionalDapperRepository : AppDapperEfRepositoryBase<LegacyDbContext, ProfessionalPoco>, IProfessionalDapperRepository
    {
        public ProfessionalDapperRepository(IActiveTransactionProvider activeTransactionProvider)
            : base(activeTransactionProvider)
        {
        }

        public bool ExistsProfessional(ProfessionalKeysDto keys)
        {
            var professional = FirstOrDefault(w => w.ProfessionalId == keys.ProfessionalId && w.Code == keys.Code);
            return professional != null;
        }

        public ListDto<ProfessionalDto, ProfessionalKeysDto> GetAllProfessionals(GetAllProfessionalsDto request)
        {
            var professionalsPoco = GetAllPaged(w => request.Name == null || w.Name.Contains(request.Name) &&
                                                     request.ZipCode == null || w.ZipCode == request.ZipCode,
                request.Page,
                request.PageSize);

            var professionalPocos = professionalsPoco as ProfessionalPoco[] ?? professionalsPoco.ToArray();

            var response = new ListDto<ProfessionalDto, ProfessionalKeysDto>
            {
                Total = professionalPocos.Length,
                Items = professionalsPoco.MapTo<List<ProfessionalDto>>(),
                HasNext = professionalPocos.Length > (request.Page - 1) * request.PageSize +
                          professionalPocos.Length
            };


            return response;
        }

        public ProfessionalDto GetProfessional(RequestDto<ProfessionalKeysDto> requestDto)
        {
            var professional = FirstOrDefault(w => w.ProfessionalId == requestDto.GetId().ProfessionalId && w.Code == requestDto.GetId().Code);
            return professional.MapTo<ProfessionalDto>();
        }
    }
}
