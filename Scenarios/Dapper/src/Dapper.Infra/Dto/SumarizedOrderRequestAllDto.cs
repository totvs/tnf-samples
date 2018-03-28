using System;
using Tnf.Dto;

namespace Dapper.Infra.Dto
{
    public class SumarizedOrderRequestAllDto : RequestAllDto
    {
        public DateTime Date { get; set; }
    }
}
