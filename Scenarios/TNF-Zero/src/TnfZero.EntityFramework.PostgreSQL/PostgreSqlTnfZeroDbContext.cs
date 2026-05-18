using Microsoft.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace TnfZero.EntityFramework.PostgreSQL;

/// <summary>
///     PostgreSQL-specific concrete DbContext.
///     Registered in DI paired with the abstract <see cref="TnfZeroDbContext" />.
/// </summary>
public class PostgreSqlTnfZeroDbContext(
    DbContextOptions<TnfZeroDbContext> options,
    ITnfSession session)
    : TnfZeroDbContext(options, session);