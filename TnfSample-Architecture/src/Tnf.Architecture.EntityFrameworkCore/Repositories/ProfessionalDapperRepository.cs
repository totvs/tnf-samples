using System;
using System.Collections.Generic;
using System.Linq;
using Tnf.App.AutoMapper;
using Tnf.App.Dapper.Repositories;
using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;
using Tnf.Architecture.Common.ValueObjects;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.EntityFrameworkCore.Contexts;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Tnf.Data;

namespace Tnf.Architecture.EntityFrameworkCore.Repositories
{
    public class ProfessionalDapperRepository : AppDapperEfRepositoryBase<LegacyDbContext, ProfessionalPoco>, IProfessionalDapperRepository
    {
        public ProfessionalDapperRepository(IActiveTransactionProvider activeTransactionProvider)
            : base(activeTransactionProvider)
        {
        }

        public bool ExistsProfessional(ComposeKey<Guid, decimal> keys)
        {
            var professional = FirstOrDefault(w => w.ProfessionalId == keys.SecundaryKey && w.Code == keys.PrimaryKey);
            return professional != null;
        }

        public ListDto<ProfessionalDto, ComposeKey<Guid, decimal>> GetAllProfessionals(GetAllProfessionalsDto request)
        {
            var professionalsPoco = GetAllPaged(w => request.Name == null || w.Name.Contains(request.Name) &&
                                                     request.ZipCode == null || w.ZipCode == request.ZipCode,
                request.Page,
                request.PageSize);

            var professionalPocos = professionalsPoco as ProfessionalPoco[] ?? professionalsPoco.ToArray();

            var response = new ListDto<ProfessionalDto, ComposeKey<Guid, decimal>>
            {
                Total = professionalPocos.Length,
                Items = professionalsPoco.MapTo<List<ProfessionalDto>>(),
                HasNext = professionalPocos.Length > (request.Page - 1) * request.PageSize +
                          professionalPocos.Length
            };


            return response;
        }

        public Professional GetProfessional(RequestDto<ComposeKey<Guid, decimal>> requestDto)
        {
            var professional = FirstOrDefault(w => w.ProfessionalId == requestDto.GetId().SecundaryKey && w.Code == requestDto.GetId().PrimaryKey);
            return professional.MapTo<Professional>();
        }
    }
}
