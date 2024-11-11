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

        modelBuilder.Entity<ApplicationUser>()
            .HasOne(a => a.Client)
            .WithOne(c => c.User)
            .HasForeignKey<Client>(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade); // Or another delete behavior as appropriate

    }


    public virtual DbSet<Client> Client { get; set; }

    public virtual DbSet<ClientType> ClientType { get; set; }

    public virtual DbSet<ContractAttachments> ContractAttachments { get; set; }

    public virtual DbSet<ContractDueDates> ContractDueDates { get; set; }

    public virtual DbSet<ContractImageModes> ContractImageModes { get; set; }

    public virtual DbSet<ContractImageResolution> ContractImageResolution { get; set; }

    public virtual DbSet<ContractImageType> ContractImageType { get; set; }

    public virtual DbSet<ContractOrderPriority> ContractOrderPriority { get; set; }

    public virtual DbSet<ContractPaymentInformation> ContractPaymentInformation { get; set; }

    public virtual DbSet<ContractPaymentType> ContractPaymentType { get; set; }

    public virtual DbSet<ContractPeriods> ContractPeriods { get; set; }

    public virtual DbSet<ContractServices> ContractServices { get; set; }

    public virtual DbSet<Contracts> Contracts { get; set; }

    public virtual DbSet<Country> Country { get; set; }

    public virtual DbSet<Currency> Currency { get; set; }

    public virtual DbSet<Satellite> Satellite { get; set; }

    public virtual DbSet<SmsOrder> SmsOrder { get; set; }

    public virtual DbSet<SmsOrderImageService> SmsOrderImageService { get; set; }

    public virtual DbSet<SmsOrderOpportunities> SmsOrderOpportunities { get; set; }

    public virtual DbSet<SmsOrderRoutes> SmsOrderRoutes { get; set; }

    public virtual DbSet<SmsOrderServices> SmsOrderServices { get; set; }

    public virtual DbSet<SmsOrderStatus> SmsOrderStatus { get; set; }

    public virtual DbSet<SmsRouteScenes> SmsRouteScenes { get; set; }

    public virtual DbSet<SmsSceneTargets> SmsSceneTargets { get; set; }

    public virtual DbSet<SmsTargetTypeMainCategory> SmsTargetTypeMainCategory { get; set; }

    public virtual DbSet<SmsTargetTypeSubCategory> SmsTargetTypeSubCategory { get; set; }

    public virtual DbSet<SmsTargets> SmsTargets { get; set; }

    public virtual DbSet<SysParam> SysParam { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql(SettingsDependancyInjection.PosSettings.ConnectionString!, x => x.UseNetTopologySuite());

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.HasPostgresExtension("postgis");

    //    modelBuilder.Entity<AspNetUsers>(entity =>
    //    {
    //        entity.HasMany(d => d.Role).WithMany(p => p.User)
    //            .UsingEntity<Dictionary<string, object>>(
    //                "AspNetUserRoles",
    //                r => r.HasOne<AspNetRoles>().WithMany().HasForeignKey("RoleId"),
    //                l => l.HasOne<AspNetUsers>().WithMany().HasForeignKey("UserId"),
    //                j =>
    //                {
    //                    j.HasKey("UserId", "RoleId");
    //                    j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
    //                });
    //    });

    //    modelBuilder.Entity<Client>(entity =>
    //    {
    //        entity.Property(e => e.UserId).HasDefaultValueSql("''::text");
    //    });

    //    modelBuilder.Entity<ContractAttachments>(entity =>
    //    {
    //        entity.Property(e => e.UploadDate).HasDefaultValueSql("'-infinity'::date");
    //    });

    //    modelBuilder.Entity<ContractImageResolution>(entity =>
    //    {
    //        entity.HasOne(d => d.ContractImageType).WithMany(p => p.ContractImageResolution).HasConstraintName("FK_contract_image_resolution_contract_image_type_contract_imag~");
    //    });

    //    modelBuilder.Entity<SmsOrderImageService>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("sms_order_image_service_pkey");

    //        entity.Property(e => e.Id).ValueGeneratedNever();

    //        entity.HasOne(d => d.ContractImageService).WithMany(p => p.SmsOrderImageService)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("contract_image_service_id_FK");

    //        entity.HasOne(d => d.SmsOrder).WithMany(p => p.SmsOrderImageService)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("sms_order_FK");
    //    });

    //    modelBuilder.Entity<SmsTargetTypeSubCategory>(entity =>
    //    {
    //        entity.HasOne(d => d.SmsTargetTypeMainCategory).WithMany(p => p.SmsTargetTypeSubCategory).HasConstraintName("FK_sms_target_type_sub_category_sms_target_type_main_category_~");
    //    });

    //    modelBuilder.Entity<SmsTargets>(entity =>
    //    {
    //        entity.HasOne(d => d.SmsTargetTypeSubCategory).WithMany(p => p.SmsTargets).HasConstraintName("FK_sms_targets_sms_target_type_sub_category_sms_target_type_su~");
    //    });

    //    OnModelCreatingPartial(modelBuilder);
    //}

    //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
