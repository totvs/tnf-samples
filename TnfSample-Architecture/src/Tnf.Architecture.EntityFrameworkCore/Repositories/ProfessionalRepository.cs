using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Tnf.App.AutoMapper;
using Tnf.App.Dto.Request;
using Tnf.App.EntityFrameworkCore;
using Tnf.App.EntityFrameworkCore.Repositories;
using Tnf.Architecture.Common.ValueObjects;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.EntityFrameworkCore.Contexts;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Tnf.Domain.Uow;
using Tnf.EntityFrameworkCore;

namespace Tnf.Architecture.EntityFrameworkCore.Repositories
{
    public class ProfessionalRepository : AppEfCoreRepositoryBase<LegacyDbContext, ProfessionalPoco>, IProfessionalRepository
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public ProfessionalRepository(IDbContextProvider<LegacyDbContext> dbContextProvider, IUnitOfWorkManager unitOfWorkManager)
            : base(dbContextProvider)
        {
            _unitOfWorkManager = unitOfWorkManager;
        }

        public bool DeleteProfessional(ComposeKey<Guid, decimal> keys)
        {
            var dbEntity = Context.Professionals
                .Include(i => i.ProfessionalSpecialties)
                .SingleOrDefault(s => s.ProfessionalId == keys.SecundaryKey && s.Code == keys.PrimaryKey);

            if (dbEntity == null)
                return false;

            dbEntity.ProfessionalSpecialties.ForEach(w => Context.ProfessionalSpecialties.Remove(w));

            Context.Professionals.Remove(dbEntity);

            return true;
        }

        private ProfessionalPoco GetProfessionalPoco(IRequestDto<ComposeKey<Guid, decimal>> requestDto)
        {
            var dbEntity = Context.Professionals
                .IncludeByRequestDto(requestDto)
                .Where(w => w.ProfessionalId == requestDto.GetId().SecundaryKey && w.Code == requestDto.GetId().PrimaryKey)
                .SelectFieldsByRequestDto(requestDto)
                .SingleOrDefault();

            return dbEntity;
        }

        public Professional GetProfessional(IRequestDto<ComposeKey<Guid, decimal>> requestDto)
        {
            var dbEntity = GetProfessionalPoco(requestDto);

            if (dbEntity == null)
                return null;

            var dto = dbEntity.MapTo<Professional>();

            return dto;
        }

        public ComposeKey<Guid, decimal> CreateProfessional(Professional entity)
        {
            var dbEntity = entity.MapTo<ProfessionalPoco>();

            dbEntity.ProfessionalId = GetNextKeyProfessional();

            Context.Professionals.Add(dbEntity);

            Context.SaveChanges();

            return new ComposeKey<Guid, decimal>(dbEntity.Code, dbEntity.ProfessionalId);
        }


        private decimal GetNextKeyProfessional()
        {
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                var lastKey = Context.Professionals.Select(s => s.ProfessionalId).DefaultIfEmpty(0).Max();
                return (lastKey + 1);
            }
        }

        public void UpdateProfessional(Professional entity)
        {
            var mappedEntity = GetProfessionalPoco(new RequestDto<ComposeKey<Guid, decimal>>(new ComposeKey<Guid, decimal>(entity.Code, entity.ProfessionalId)));

            entity.MapTo(mappedEntity);

            Context.Professionals.Update(mappedEntity);

            Context.SaveChanges();
        }

        public void AddOrRemoveSpecialties(ComposeKey<Guid, decimal> keys, IList<Specialty> specialties)
        {
            var request = new RequestDto<ComposeKey<Guid, decimal>>(keys) { Expand = new ProfessionalDto().Expandables[0] };

            var dbProfessional = GetProfessionalPoco(request);

            if (dbProfessional == null)
                return;

            var idsToAdd = specialties.Select(s => s.Id).ToArray();

            if (dbProfessional.ProfessionalSpecialties == null)
                dbProfessional.ProfessionalSpecialties = new List<ProfessionalSpecialtiesPoco>();

            dbProfessional.ProfessionalSpecialties.RemoveAll(w => !idsToAdd.Contains(w.SpecialtyId));

            foreach (var specialty in specialties)
            {
                var dbProfessionalSpecialties = dbProfessional.ProfessionalSpecialties
                    .FirstOrDefault(s => s.SpecialtyId == specialty.Id);

                if (dbProfessionalSpecialties == null)
                {
                    dbProfessional.ProfessionalSpecialties.Add(new ProfessionalSpecialtiesPoco
                    {
                        ProfessionalId = dbProfessional.ProfessionalId,
                        Code = dbProfessional.Code,
                        SpecialtyId = specialty.Id
                    });
                }
            }
        }

        public bool ExistsProfessional(ComposeKey<Guid, decimal> keys)
        {
            var dbEntity = Context.Professionals
                .SingleOrDefault(s => s.ProfessionalId == keys.SecundaryKey && s.Code == keys.PrimaryKey);

            return dbEntity != null;
        }
    }
}
