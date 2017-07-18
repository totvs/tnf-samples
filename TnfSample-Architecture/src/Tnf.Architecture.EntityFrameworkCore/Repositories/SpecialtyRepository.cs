using System.Linq;
using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;
using Tnf.App.EntityFrameworkCore.Repositories;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Tnf.AutoMapper;
using Tnf.Domain.Repositories;
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

        public ListDto<SpecialtyDto, int> GetAllSpecialties(GetAllSpecialtiesDto request)
        {
            return GetAll()
                .Where(w => request.Description == null || w.Description.Contains(request.Description))
                .SkipAndTakeByRequestDto(request)
                .OrderByRequestDto(request)
                .ToListDto<SpecialtyPoco, SpecialtyDto>(request, Count());
        }

        public SpecialtyDto GetSpecialty(RequestDto requestDto)
        {
            SpecialtyDto specialty = null;

            var dbEntity = Get(requestDto);

            if (dbEntity != null)
                specialty = dbEntity.MapTo<SpecialtyDto>();

            return specialty;
        }
        
        public Specialty UpdateSpecialty(Specialty dto)
        {
            var dbEntity = dto.MapTo<SpecialtyPoco>();

            Update(dbEntity);

            return dto;
        }
    }
}
