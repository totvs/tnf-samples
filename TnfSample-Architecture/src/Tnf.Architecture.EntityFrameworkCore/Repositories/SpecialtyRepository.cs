using System.Collections.Generic;
using System.Linq;
using Tnf.AutoMapper;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;
using Tnf.Domain.Repositories;
using Tnf.Dto.Response;
using Tnf.Dto.Request;

namespace Tnf.Architecture.EntityFrameworkCore.Repositories
{
    public class SpecialtyRepository : EfCoreRepositoryBase<LegacyDbContext, SpecialtyPoco>, ISpecialtyRepository
    {
        public SpecialtyRepository(IDbContextProvider<LegacyDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public SpecialtyDto CreateSpecialty(SpecialtyDto dto)
        {
            var dbEntity = dto.MapTo<SpecialtyPoco>();

            dto.Id = base.InsertAndGetId(dbEntity);

            return dto;
        }

        public void DeleteSpecialty(int id) => base.Delete(id);

        public bool ExistsSpecialty(int id) => base.Count(s => s.Id == id) > 0;

        public SuccessResponseListDto<SpecialtyDto> GetAllSpecialties(GetAllSpecialtiesDto request)
        {
            var response = new SuccessResponseListDto<SpecialtyDto>();

            var dbQuery = GetAll()
                .Where(w => request.Description == null || w.Description.Contains(request.Description))
                .SkipAndTakeByRequestDto(request)
                .OrderByRequestDto(request)
                .ToArray();

            response.Total = base.Count();
            response.Items = dbQuery.MapTo<List<SpecialtyDto>>();

            return response;
        }

        public SpecialtyDto GetSpecialty(RequestDto requestDto)
        {
            SpecialtyDto specialty = null;

            var dbEntity = base.Get(id);
            if (dbEntity != null)
                specialty = dbEntity.MapTo<SpecialtyDto>();

            return specialty;
        }

        public SpecialtyDto UpdateSpecialty(SpecialtyDto dto)
        {
            var dbEntity = dto.MapTo<SpecialtyPoco>();

            base.Update(dbEntity);

            return dto;
        }
    }
}
