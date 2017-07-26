using Tnf.App.Dto.Response;
using Tnf.App.EntityFrameworkCore.Repositories;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.EntityFrameworkCore.Contexts;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Tnf.Architecture.EntityFrameworkCore.ReadInterfaces;
using Tnf.EntityFrameworkCore;

namespace Tnf.Architecture.EntityFrameworkCore.ReadRepositories
{
    public class SpecialtyReadRepository : AppEfCoreRepositoryBase<LegacyDbContext, SpecialtyPoco>, ISpecialtyReadRepository
    {
        public SpecialtyReadRepository(IDbContextProvider<LegacyDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public ListDto<SpecialtyDto, int> GetAllSpecialties(GetAllSpecialtiesDto request)
            => GetAll<SpecialtyDto>(w => request.Description == null || w.Description.Contains(request.Description), request);
    }
}
