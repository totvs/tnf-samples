﻿using System.Collections.Generic;
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
using Tnf.Architecture.Domain.Registration;

namespace Tnf.Architecture.EntityFrameworkCore.Repositories
{
    public class SpecialtyRepository : EfCoreRepositoryBase<LegacyDbContext, SpecialtyPoco>, ISpecialtyRepository
    {
        public SpecialtyRepository(IDbContextProvider<LegacyDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public int CreateSpecialty(Specialty dto)
        {
            var dbEntity = dto.MapTo<SpecialtyPoco>();

            return base.InsertAndGetId(dbEntity);
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
            response.HasNext = response.Total > response.Items.Count();

            return response;
        }

        public SpecialtyDto GetSpecialty(RequestDto<int> requestDto)
        {
            SpecialtyDto specialty = null;
            
            var dbEntity = base.Get(requestDto.Key);
            if (dbEntity != null)
                specialty = dbEntity.MapTo<SpecialtyDto>();

            return specialty;
        }

        public Specialty UpdateSpecialty(Specialty dto)
        {
            var dbEntity = dto.MapTo<SpecialtyPoco>();

            base.Update(dbEntity);

            return dto;
        }
    }
}
