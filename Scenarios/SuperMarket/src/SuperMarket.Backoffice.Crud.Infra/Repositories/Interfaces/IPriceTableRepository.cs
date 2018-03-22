using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Backoffice.Crud.Infra.Repositories.Interfaces
{
    public interface IPriceTableRepository
    {
        Task<Dictionary<Guid, decimal>> GetPriceTable();
    }
}
