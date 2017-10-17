using System.Linq;
using System.Threading.Tasks;
using Tnf.App.AutoMapper;
using Tnf.App.Dto.Request;
using Tnf.App.EntityFrameworkCore.Repositories;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.EntityFrameworkCore.Contexts;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Tnf.EntityFrameworkCore;

namespace Tnf.Architecture.EntityFrameworkCore.Repositories
{
    public class SpecialtyRepository : AppEfCoreRepositoryBase<LegacyDbContext, SpecialtyPoco>, ISpecialtyRepository
    {
        public SpecialtyRepository(IDbContextProvider<LegacyDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<int> CreateSpecialty(Specialty dto)
        {
            var dbEntity = dto.MapTo<SpecialtyPoco>();

            return await InsertAndGetIdAsync(dbEntity).ForAwait();
        }

        public async Task DeleteSpecialty(int id)
        {
            var dbEntity = GetAllIncluding(s => s.ProfessionalSpecialties)
                               .SingleOrDefault(s => s.Id == id);

            dbEntity.ProfessionalSpecialties.ForEach(w => Context.ProfessionalSpecialties.Remove(w));

            await DeleteAsync(dbEntity).ForAwait();
        }

        public async Task<bool> ExistsSpecialty(int id) 
            => await CountAsync(s => s.Id == id).ForAwait() > 0;

        public async Task<Specialty> GetSpecialty(IRequestDto requestDto)
        {
            Specialty specialty = null;

            var dbEntity = await GetAsync(requestDto).ForAwait();

            if (dbEntity != null)
                specialty = dbEntity.MapTo<Specialty>();

            return specialty;
        }
        
        public async Task UpdateSpecialty(Specialty dto)
        {
            var dbEntity = dto.MapTo<SpecialtyPoco>();

            await UpdateAsync(dbEntity).ForAwait();
        }
    }
}
