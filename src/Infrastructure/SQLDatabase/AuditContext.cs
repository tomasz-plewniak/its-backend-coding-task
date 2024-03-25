﻿using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SQLDatabase
{
    public class AuditContext : DbContext
    {
        public AuditContext(DbContextOptions<AuditContext> options) : base(options)
        {
        }
        
        public DbSet<ClaimAudit> ClaimAudits { get; set; } = null!;

        public DbSet<CoverAudit> CoverAudits { get; set; } = null!;
    }
}
