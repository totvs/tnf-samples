using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.EntityFrameworkCore.Repositories;
using Tnf.EntityFrameworkCore;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Tnf.Architecture.Dto;
using System.Collections.Generic;
using System.Linq;
using Tnf.AutoMapper;
using Tnf.Architecture.Dto.Paging;
using Tnf.Architecture.Dto.Registration;

namespace Tnf.Architecture.EntityFrameworkCore.Repositories
{
    public class ProfessionalRepository : EfCoreRepositoryBase<LegacyDbContext, ProfessionalPoco>, IProfessionalRepository
    {
        public ProfessionalRepository(IDbContextProvider<LegacyDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public void Delete(ProfessionalKeysDto keys)
        {
            var dbEntity = Context.Professionals.Single(s => s.ProfessionalId == keys.ProfessionalId && s.Code == keys.Code);
            Context.Professionals.Remove(dbEntity);
        }

        public ProfessionalDto Get(ProfessionalKeysDto keys)
        {
            var dbEntity = Context.Professionals.Single(w => w.ProfessionalId == keys.ProfessionalId && w.Code == keys.Code);
            return dbEntity.MapTo<ProfessionalDto>();
        }

        public ProfessionalDto Insert(ProfessionalCreateDto entity)
        {
            var dbEntity = entity.MapTo<ProfessionalPoco>();

            Context.Professionals.Add(dbEntity);

            Context.SaveChanges();

            return dbEntity.MapTo<ProfessionalDto>();
        }

        public ProfessionalDto Update(ProfessionalDto entity)
        {
            var mappedEntity = entity.MapTo<ProfessionalPoco>();

            Context.Professionals.Update(mappedEntity);

            Context.SaveChanges();

            return mappedEntity.MapTo<ProfessionalDto>();
        }

        public PagingResponseDto<ProfessionalDto> All(GetAllProfessionalsDto request)
        {
            var response = new PagingResponseDto<ProfessionalDto>();

            var dbQuery = base.GetAll()
                .Where(w => request.Name == null || w.Name.Contains(request.Name))
                .Skip(request.Offset)
                .Take(request.PageSize)
                .ToArray();

            response.Total = base.Count();
            response.Data = dbQuery.MapTo<List<ProfessionalDto>>();

            return response;
        }
    }
}
