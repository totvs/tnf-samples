using System;
using Tnf.Architecture.Domain.Registration;
using Tnf.Domain.Repositories;

namespace Tnf.Architecture.Domain.Interfaces.Repositories
{
    public interface IProfessionalRepository : IRepository<Professional>
    {
        Professional GetProfessional(decimal professionalId, Guid code);
    }
}
