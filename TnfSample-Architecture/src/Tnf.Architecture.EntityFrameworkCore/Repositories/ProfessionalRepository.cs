using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.EntityFrameworkCore.Repositories;
using Tnf.EntityFrameworkCore;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using System.Collections.Generic;
using System.Linq;
using Tnf.AutoMapper;
using Tnf.Architecture.Dto.Registration;
using Microsoft.EntityFrameworkCore;
using Tnf.Domain.Repositories;
using Tnf.Dto.Request;
using Tnf.Dto.Response;
using Tnf.Architecture.Domain.Registration;

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
                .SelectFieldsByRequestDto(requestDto)
                .SingleOrDefault(w => w.ProfessionalId == requestDto.Key.ProfessionalId && w.Code == requestDto.Key.Code);

            return dbEntity;
        }

        public ProfessionalDto GetProfessional(RequestDto<ProfessionalKeysDto> requestDto)
        {
            var dbEntity = GetProfessionalPoco(requestDto);

            return dbEntity != null ? dbEntity.MapTo<ProfessionalDto>() : null;
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

        public SuccessResponseListDto<ProfessionalDto> GetAllProfessionals(GetAllProfessionalsDto request)
        {
            var response = new SuccessResponseListDto<ProfessionalDto>();

            var dbBaseQuery = Context.Professionals
                .Include(i => i.ProfessionalSpecialties)
                    .ThenInclude(i => i.Specialty)
                .Where(w => request.Name == null || w.Name.Contains(request.Name));

            var dbQuery = dbBaseQuery
                .SkipAndTakeByRequestDto(request)
                .OrderByRequestDto(request)
                .ToArray();

            response.Total = base.Count();
            response.Items = dbQuery.MapTo<List<ProfessionalDto>>();

            return response;
        }

        public void AddOrRemoveSpecialties(ProfessionalKeysDto keys, List<SpecialtyDto> dto)
        {
            var dbProfessional = GetProfessionalPoco(new RequestDto<ProfessionalKeysDto>(keys));

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
