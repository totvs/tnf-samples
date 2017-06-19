using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Tnf.AutoMapper;
using Tnf.Dapper.Repositories;
using Tnf.Data;

namespace Tnf.Architecture.EntityFrameworkCore.Repositories
{
    public class ProfessionalDapperRepository : DapperEfRepositoryBase<LegacyDbContext, ProfessionalPoco>, IProfessionalDapperRepository
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

        public ListDto<ProfessionalDto> GetAllProfessionals(GetAllProfessionalsDto request)
        {
            var professionalsPoco = GetAllPaged(w => request.Name == null || w.Name.Contains(request.Name) &&
                                                     request.ZipCode == null || w.ZipCode == request.ZipCode,
                request.Page,
                request.PageSize);

            return professionalsPoco.MapTo<ListDto<ProfessionalDto>>();
        }

        public ProfessionalDto GetProfessional(RequestDto<ProfessionalKeysDto> requestDto)
        {
            var professional = FirstOrDefault(w => w.ProfessionalId == requestDto.GetId().ProfessionalId && w.Code == requestDto.GetId().Code);
            return professional.MapTo<ProfessionalDto>();
        }
    }
}
