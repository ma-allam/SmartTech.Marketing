using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartTech.Marketing.Application.Contract;
using SmartTech.Marketing.Core.AppSetting;
using SmartTech.Marketing.Core.Cache;
using SmartTech.Marketing.Domain.Entities;

namespace SmartTech.Marketing.Persistence.Context;

public partial class DatabaseService : IdentityDbContext<ApplicationUser>, IDataBaseService
{
    private readonly ChangeTrackerInterceptor _changeTrackerInterceptor;

    public DatabaseService()
    {
    }

    public DatabaseService(DbContextOptions<DatabaseService> options, ChangeTrackerInterceptor changeTrackerInterceptor)
         : base(options)
    {
        Database.EnsureCreated();
        _changeTrackerInterceptor = changeTrackerInterceptor;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

      

    }


    
    public virtual DbSet<SysParam> SysParam { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql(SettingsDependancyInjection.PosSettings.ConnectionString!, x => x.UseNetTopologySuite());

}
