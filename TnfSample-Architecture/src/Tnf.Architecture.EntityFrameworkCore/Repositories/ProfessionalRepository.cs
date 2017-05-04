using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.Registration;
using Tnf.EntityFrameworkCore.Repositories;
using Tnf.EntityFrameworkCore;
using System;
using System.Linq;

namespace Tnf.Architecture.EntityFrameworkCore.Repositories
{
    public class ProfessionalRepository : EfCoreRepositoryBase<LegacyDbContext, Professional>, IProfessionalRepository
    {
        public ProfessionalRepository(IDbContextProvider<LegacyDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        public Professional GetProfessional(decimal professionalId, Guid code)
        {
            return Context.Professionals.FirstOrDefault(w => w.ProfessionalId == professionalId && w.Code == code);
        }
    }
}
