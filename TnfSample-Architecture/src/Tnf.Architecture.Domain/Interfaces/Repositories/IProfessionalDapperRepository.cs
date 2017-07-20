using System;
using Tnf.App.Dto.Request;
using Tnf.Architecture.Common.ValueObjects;
using Tnf.Architecture.Domain.Registration;
using Tnf.Domain.Repositories;

namespace Tnf.Architecture.Domain.Interfaces.Repositories
{
    public interface IProfessionalDapperRepository : IRepository
    {
        Professional GetProfessional(RequestDto<ComposeKey<Guid, decimal>> requestDto);
        bool ExistsProfessional(ComposeKey<Guid, decimal> keys);
    }
}
