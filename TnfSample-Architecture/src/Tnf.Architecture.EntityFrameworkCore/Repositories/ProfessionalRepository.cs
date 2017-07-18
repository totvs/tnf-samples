using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
    public class ProfessionalRepository : AppEfCoreRepositoryBase<LegacyDbContext, ProfessionalPoco>, IProfessionalRepository
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

            if (dbEntity == null)
                return false;

            dbEntity.ProfessionalSpecialties.ForEach(w => Context.ProfessionalSpecialties.Remove(w));

            Context.Professionals.Remove(dbEntity);

            return true;
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

            if (dbEntity == null)
                return null;

            var dto = dbEntity.MapTo<ProfessionalDto>();

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
            var request = new RequestDto<ProfessionalKeysDto>(keys) { Expand = new ProfessionalDto().Expandables[0] };

            var dbProfessional = GetProfessionalPoco(request);

            if (dbProfessional == null)
                return;

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

        public bool ExistsProfessional(ProfessionalKeysDto keys)
        {
            var dbEntity = Context.Professionals
                .SingleOrDefault(s => s.ProfessionalId == keys.ProfessionalId && s.Code == keys.Code);

            return dbEntity != null;
        }
    }
}
