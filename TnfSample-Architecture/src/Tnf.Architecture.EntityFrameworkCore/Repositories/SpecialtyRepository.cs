using System.Linq;
using Tnf.App.Dto.Request;
using Tnf.App.EntityFrameworkCore.Repositories;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.EntityFrameworkCore.Contexts;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Tnf.AutoMapper;
using Tnf.EntityFrameworkCore;

namespace Tnf.Architecture.EntityFrameworkCore.Repositories
{
    public class SpecialtyRepository : AppEfCoreRepositoryBase<LegacyDbContext, SpecialtyPoco>, ISpecialtyRepository
    {
        public SpecialtyRepository(IDbContextProvider<LegacyDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public int CreateSpecialty(Specialty dto)
        {
            var dbEntity = dto.MapTo<SpecialtyPoco>();

            return InsertAndGetId(dbEntity);
        }

        public void DeleteSpecialty(int id)
        {
            var dbEntity = GetAllIncluding(s => s.ProfessionalSpecialties)
                               .SingleOrDefault(s => s.Id == id);

            dbEntity.ProfessionalSpecialties.ForEach(w => Context.ProfessionalSpecialties.Remove(w));

            Delete(dbEntity);
        }

        public bool ExistsSpecialty(int id) 
            => Count(s => s.Id == id) > 0;

        public Specialty GetSpecialty(RequestDto requestDto)
        {
            Specialty specialty = null;

            var dbEntity = Get(requestDto);

            if (dbEntity != null)
                specialty = dbEntity.MapTo<Specialty>();

            return specialty;
        }
        
        public void UpdateSpecialty(Specialty dto)
        {
            var dbEntity = dto.MapTo<SpecialtyPoco>();

            Update(dbEntity);
        }
    }
}
