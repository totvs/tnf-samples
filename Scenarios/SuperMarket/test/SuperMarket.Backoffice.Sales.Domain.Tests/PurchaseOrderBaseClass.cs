using Microsoft.Extensions.DependencyInjection;
using SuperMarket.Backoffice.Sales.Domain.Entities;
using System;
using System.Collections.Generic;
using Tnf.TestBase;

namespace SuperMarket.Backoffice.Sales.Domain.Tests
{
    public abstract class PurchaseOrderBaseClass : TnfIntegratedTestBase
    {
        protected Guid InvalidProduct = Guid.NewGuid();
        protected Guid Product1 = Guid.NewGuid();
        protected Guid Product2 = Guid.NewGuid();
        protected Guid Product3 = Guid.NewGuid();
        protected Guid Product4 = Guid.NewGuid();

        protected PriceTable PriceTable;

        public PurchaseOrderBaseClass()
        {
            PriceTable = PriceTable.CreateTable(new Dictionary<Guid, decimal>()
            {
                { Product1, 10 },
                { Product2, 20 },
                { Product3, 30 },
                { Product4, 40 },
            });
        }

        protected override void PreInitialize(IServiceCollection services)
        {
            base.PreInitialize(services);

            services.AddSalesDomainDependency();
        }

        protected override void PostInitialize(IServiceProvider provider)
        {
            base.PostInitialize(provider);

            provider.ConfigureTnf().ConfigureSalesDomain();
        }
    }
}
