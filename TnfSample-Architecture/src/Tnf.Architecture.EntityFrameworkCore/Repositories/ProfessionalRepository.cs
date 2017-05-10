using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.EntityFrameworkCore.Repositories;
using Tnf.EntityFrameworkCore;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Tnf.Architecture.Dto;
using System.Collections.Generic;
using System.Linq;
using Tnf.AutoMapper;
using Tnf.Architecture.Dto.Registration;
using Microsoft.EntityFrameworkCore;

namespace Tnf.Architecture.EntityFrameworkCore.Repositories
{
    public class ProfessionalRepository : EfCoreRepositoryBase<LegacyDbContext, ProfessionalPoco>, IProfessionalRepository
    {
        public ProfessionalRepository(IDbContextProvider<LegacyDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public bool DeleteProfessional(ProfessionalKeysDto keys)
        {
            var dbEntity = Context.Professionals
                .Include(i => i.ProfessionalSpecialties)
                .SingleOrDefault(s => s.ProfessionalId == keys.ProfessionalId && s.Code == keys.Code);
            
            if (dbEntity != null)
            {
                dbEntity.ProfessionalSpecialties.ForEach(w => Context.ProfessionalSpecialties.Remove(w));

                Context.Professionals.Remove(dbEntity);
            }

            return dbEntity != null;
        }

        public ProfessionalDto GetProfessional(ProfessionalKeysDto keys)
        {
            var dbEntity = Context.Professionals
                .Include(i => i.ProfessionalSpecialties)
                .Include("ProfessionalSpecialties.Specialty")
                .SingleOrDefault(w => w.ProfessionalId == keys.ProfessionalId && w.Code == keys.Code);

            return dbEntity != null ? dbEntity.MapTo<ProfessionalDto>() : null;
        }

        public ProfessionalDto CreateProfessional(ProfessionalCreateDto entity)
        {
            var dbEntity = entity.MapTo<ProfessionalPoco>();

            Context.Professionals.Add(dbEntity);

            Context.SaveChanges();

            return dbEntity.MapTo<ProfessionalDto>();
        }

        public ProfessionalDto UpdateProfessional(ProfessionalDto entity)
        {
            var mappedEntity = entity.MapTo<ProfessionalPoco>();

            Context.Professionals.Update(mappedEntity);

            Context.SaveChanges();

            return mappedEntity.MapTo<ProfessionalDto>();
        }

        public PagingResponseDto<ProfessionalDto> GetAllProfessionals(GetAllProfessionalsDto request)
        {
            var response = new PagingResponseDto<ProfessionalDto>();

            var dbBaseQuery = Context.Professionals
                .Include(i => i.ProfessionalSpecialties)
                .Include("ProfessionalSpecialties.Specialty")
                .Where(w => request.Name == null || w.Name.Contains(request.Name));

            var dbQuery = dbBaseQuery
                .Skip(request.Offset)
                .Take(request.PageSize)
                .ToArray();

            response.Total = base.Count();
            response.Data = dbQuery.MapTo<List<ProfessionalDto>>();

            return response;
        }

        public void AddOrRemoveSpecialties(ProfessionalKeysDto keys, List<SpecialtyDto> dto)
        {
            var dbProfessional = Context.Professionals
                .Include(i => i.ProfessionalSpecialties)
                .Include("ProfessionalSpecialties.Specialty")
                .SingleOrDefault(w => w.ProfessionalId == keys.ProfessionalId && w.Code == keys.Code);

            if (dbProfessional != null)
            {
                var idsToAdd = dto.Select(s => s.Id).ToArray();

                if (dbProfessional.ProfessionalSpecialties == null)
                    dbProfessional.ProfessionalSpecialties = new List<ProfessionalSpecialtiesPoco>();

                dbProfessional.ProfessionalSpecialties.RemoveAll(w => !idsToAdd.Contains(w.SpecialtyId));

                dto.ForEach(w =>
                {
                    var dbProfessionalSpecialties = dbProfessional.ProfessionalSpecialties
                        .FirstOrDefault(s => s.SpecialtyId == w.Id);

                    if (dbProfessionalSpecialties == null)
                    {
                        dbProfessional.ProfessionalSpecialties.Add(new ProfessionalSpecialtiesPoco()
                        {
                            ProfessionalId = dbProfessional.ProfessionalId,
                            Code = dbProfessional.Code,
                            SpecialtyId = w.Id
                        });
                    }
                });
            }
        }
    }
}
