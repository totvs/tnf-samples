using SuperMarket.Backoffice.Sales.Domain.Entities;
using SuperMarket.Backoffice.Sales.Domain.Interfaces;
using SuperMarket.Backoffice.Sales.Infra.Contexts;
using SuperMarket.Backoffice.Sales.Infra.Pocos;
using System;
using System.Linq;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;

namespace SuperMarket.Backoffice.Sales.Infra.Repositories
{
    public class PriceTableRepository : EfCoreRepositoryBase<SalesContext, ProductPoco, Guid>, IPriceTableRepository
    {
        public PriceTableRepository(IDbContextProvider<SalesContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        public PriceTable GetPriceTable()
        {
            var values = GetAll().ToDictionary(k => k.Id, v => v.Value);

            return PriceTable.CreateTable(values);
        }
    }
}
