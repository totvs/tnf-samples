using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Tnf.App.Dto.Response;
using Tnf.App.EntityFrameworkCore.Repositories;
using Tnf.Architecture.Common.ValueObjects;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.EntityFrameworkCore.Contexts;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Tnf.Architecture.EntityFrameworkCore.ReadInterfaces;
using Tnf.Domain.Repositories;
using Tnf.EntityFrameworkCore;

namespace Tnf.Architecture.EntityFrameworkCore.ReadRepositories
{
    public class ProfessionalReadRepository : AppEfCoreRepositoryBase<LegacyDbContext, ProfessionalPoco>, IProfessionalReadRepository
    {
        public ProfessionalReadRepository(IDbContextProvider<LegacyDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
        
        public IListDto<ProfessionalDto, ComposeKey<Guid, decimal>> GetAllProfessionals(GetAllProfessionalsDto request)
        {
            var dbBaseQuery = Context.Professionals
                .Include(i => i.ProfessionalSpecialties)
                .ThenInclude(i => i.Specialty)
                .Where(w => request.Name == null || w.Name.Contains(request.Name));

            return dbBaseQuery
                .SkipAndTakeByRequestDto(request)
                .OrderByRequestDto(request)
                .ToListDto<ProfessionalPoco, ProfessionalDto, ComposeKey<Guid, decimal>>(request, dbBaseQuery.Count());
        }
    }
}
