using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Tnf.AutoMapper;
using Tnf.Domain.Repositories;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;

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

        private ProfessionalPoco GetProfessionalPoco(RequestDto<ProfessionalKeysDto> requestDto)
        {
            var dbEntity = Context.Professionals
                .IncludeByRequestDto(requestDto)
                .Where(w => w.ProfessionalId == requestDto.GetId().ProfessionalId && w.Code == requestDto.GetId().Code)
                .SelectFieldsByRequestDto(requestDto)
                .SingleOrDefault();

            return dbEntity;
        }

        public ProfessionalDto GetProfessional(RequestDto<ProfessionalKeysDto> requestDto)
        {
            var dbEntity = GetProfessionalPoco(requestDto);
            var dto = dbEntity != null ? dbEntity.MapTo<ProfessionalDto>() : null;

            dto.RemoveExpandable(requestDto);

            return dto;
        }

        public ProfessionalKeysDto CreateProfessional(Professional entity)
        {
            var dbEntity = entity.MapTo<ProfessionalPoco>();

            Context.Professionals.Add(dbEntity);

            Context.SaveChanges();

            return new ProfessionalKeysDto(dbEntity.ProfessionalId, dbEntity.Code);
        }

        public Professional UpdateProfessional(Professional entity)
        {
            var mappedEntity = GetProfessionalPoco(new RequestDto<ProfessionalKeysDto>(new ProfessionalKeysDto(entity.ProfessionalId, entity.Code)));

            entity.MapTo(mappedEntity);

            Context.Professionals.Update(mappedEntity);

            Context.SaveChanges();

            return entity;
        }

        public ListDto<ProfessionalDto, ProfessionalKeysDto> GetAllProfessionals(GetAllProfessionalsDto request)
        {
            var dbBaseQuery = Context.Professionals
                .Include(i => i.ProfessionalSpecialties)
                .ThenInclude(i => i.Specialty)
                .Where(w => request.Name == null || w.Name.Contains(request.Name));

            return dbBaseQuery
                .SkipAndTakeByRequestDto(request)
                .OrderByRequestDto(request)
                .ToListDto<ProfessionalPoco, ProfessionalDto, ProfessionalKeysDto>(request, dbBaseQuery.Count());
        }

        public void AddOrRemoveSpecialties(ProfessionalKeysDto keys, List<SpecialtyDto> dto)
        {
            var request = new RequestDto<ProfessionalKeysDto>(keys);
            request.Expand = new ProfessionalDto()._expandables[0];

            var dbProfessional = GetProfessionalPoco(request);

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

        public bool ExistsProfessional(ProfessionalKeysDto keys)
        {
            var dbEntity = Context.Professionals
                .SingleOrDefault(s => s.ProfessionalId == keys.ProfessionalId && s.Code == keys.Code);

            return dbEntity != null;
        }
    }
}
