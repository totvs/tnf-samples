using System;
using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Dto.Registration;
using Tnf.Dapper.Repositories;

namespace Tnf.Architecture.Dapper.Repositories
{
    public class ProfessionalDapperRepository : IProfessionalDapperRepository, DapperEfRepositoryBase<Professional>
    {
        public bool ExistsProfessional(ProfessionalKeysDto keys)
        {
            throw new NotImplementedException();
        }

        public ListDto<ProfessionalDto> GetAllProfessionals(GetAllProfessionalsDto request)
        {
            throw new NotImplementedException();
        }

        public ProfessionalDto GetProfessional(RequestDto<ProfessionalKeysDto> requestDto)
        {
            throw new NotImplementedException();
        }
    }
}
