using System;
using System.Collections.Generic;
using Tnf.App.Dto.Request;
using Tnf.Architecture.Common.ValueObjects;
using Tnf.Architecture.Domain.Registration;
using Tnf.Domain.Repositories;

namespace Tnf.Architecture.Domain.Interfaces.Repositories
{
    public interface IProfessionalRepository : IRepository
    {
        Professional GetProfessional(RequestDto<ComposeKey<Guid, decimal>> requestDto);
        ComposeKey<Guid, decimal> CreateProfessional(Professional entity);
        void UpdateProfessional(Professional entity);
        bool DeleteProfessional(ComposeKey<Guid, decimal> keys);
        void AddOrRemoveSpecialties(ComposeKey<Guid, decimal> keys, IList<Specialty> specialties);
        bool ExistsProfessional(ComposeKey<Guid, decimal> keys);
    }
}
