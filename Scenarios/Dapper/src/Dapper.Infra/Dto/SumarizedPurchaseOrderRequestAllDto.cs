using System;
using Tnf.Dto;

namespace Dapper.Infra.Dto
{
    public class SumarizedPurchaseOrderRequestAllDto : RequestAllDto
    {
        public DateTime? Date { get; set; }
    }
}
