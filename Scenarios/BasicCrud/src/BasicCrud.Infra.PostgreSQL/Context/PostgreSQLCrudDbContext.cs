using System;
using System.Collections.Generic;
using System.Text;
using BasicCrud.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace BasicCrud.Infra.PostgreSQL.Context
{
    public class PostgreSQLCrudDbContext : CrudDbContext
    {
        public PostgreSQLCrudDbContext(DbContextOptions<CrudDbContext> options, ITnfSession session) 
            : base(options, session)
        {
        }
    }
}
