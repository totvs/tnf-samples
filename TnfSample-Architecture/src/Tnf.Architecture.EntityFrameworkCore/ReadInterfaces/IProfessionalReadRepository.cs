using System;
using Tnf.App.Dto.Response;
using Tnf.Architecture.Common.ValueObjects;
using Tnf.Architecture.Dto.Registration;
using Tnf.Domain.Repositories;

namespace Tnf.Architecture.EntityFrameworkCore.ReadInterfaces
{
    public interface IProfessionalReadRepository : IRepository
    {
        ListDto<ProfessionalDto, ComposeKey<Guid, decimal>> GetAllProfessionals(GetAllProfessionalsDto request);
    }
}
