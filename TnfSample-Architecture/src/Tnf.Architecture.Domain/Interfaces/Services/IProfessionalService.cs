using System;
using Tnf.App.Dto.Request;
using Tnf.Architecture.Common.ValueObjects;
using Tnf.Architecture.Domain.Registration;
using Tnf.Domain.Services;

namespace Tnf.Architecture.Domain.Interfaces.Services
{
    public interface IProfessionalService : IDomainService
    {
        Professional GetProfessional(IRequestDto<ComposeKey<Guid, decimal>> key);
        ComposeKey<Guid, decimal> CreateProfessional(ProfessionalBuilder builder);
        void UpdateProfessional(ProfessionalBuilder builder);
        void DeleteProfessional(ComposeKey<Guid, decimal> key);
    }
}
