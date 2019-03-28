using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ProdutoXyz.Infra.Context;
using Tnf.Runtime.Session;

namespace ProdutoXyz.Infra.Postgres.Context
{
    public class PostgresCrudDbContext : CrudDbContext
    {
        public PostgresCrudDbContext(DbContextOptions<CrudDbContext> options, ITnfSession session) : base(options, session)
        {
        }
    }
}
