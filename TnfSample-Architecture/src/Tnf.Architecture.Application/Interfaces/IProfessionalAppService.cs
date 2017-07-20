using System;
using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;
using Tnf.Application.Services;
using Tnf.Architecture.Common.ValueObjects;
using Tnf.Architecture.Dto.Registration;

namespace Tnf.Architecture.Application.Interfaces
{
    public interface IProfessionalAppService : IApplicationService
    {
        ListDto<ProfessionalDto, ComposeKey<Guid, decimal>> GetAllProfessionals(GetAllProfessionalsDto request);
        ProfessionalDto GetProfessional(RequestDto<ComposeKey<Guid, decimal>> keys);
        ProfessionalDto CreateProfessional(ProfessionalDto professional);
        ProfessionalDto UpdateProfessional(ComposeKey<Guid, decimal> keys, ProfessionalDto professional);
        void DeleteProfessional(ComposeKey<Guid, decimal> keys);
    }
}
