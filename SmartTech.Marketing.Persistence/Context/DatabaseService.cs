using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using SmartTech.Marketing.Application.Contract;
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
    public virtual DbSet<ClientType> ClientType { get; set; }

    public virtual DbSet<Client> Client { get; set; }

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

    public virtual DbSet<SmsOrderOpportunities> SmsOrderOpportunities { get; set; }

    public virtual DbSet<SmsOrderRoutes> SmsOrderRoutes { get; set; }

    public virtual DbSet<SmsOrderServices> SmsOrderServices { get; set; }

    public virtual DbSet<SmsOrderStatus> SmsOrderStatus { get; set; }

    public virtual DbSet<SmsRouteScenes> SmsRouteScenes { get; set; }

    public virtual DbSet<SmsSceneTargets> SmsSceneTargets { get; set; }

    public virtual DbSet<SmsTargetTypeMainCategory> SmsTargetTypeMainCategory { get; set; }

    public virtual DbSet<SmsTargetTypeSubCategory> SmsTargetTypeSubCategory { get; set; }

    public virtual DbSet<SmsTargets> SmsTargets { get; set; }
    public DbSet<SysParam> SysParam { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=172.16.30.50:5432;Database=SMS;Username=postgres;Password=postgres;", x => x.UseNetTopologySuite());

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.HasPostgresExtension("postgis");

    //    modelBuilder.Entity<ClientType>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("client_type_pkey");

    //        entity.ToTable("client_type", tb => tb.HasComment("نوع العميل \nجهه حكومية - عسكرية - مدنيه او اخري"));

    //        entity.Property(e => e.Id).UseIdentityAlwaysColumn();
    //    });

    //    modelBuilder.Entity<Clients>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("clients_pkey");

    //        entity.ToTable("clients", tb => tb.HasComment("بيانات العميل \nاسم - نوع - تليفون - ايميل - دولة"));

    //        entity.Property(e => e.Id).UseIdentityAlwaysColumn();

    //        entity.HasOne(d => d.ClientTypeNavigation).WithMany(p => p.Clients)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("client_type_fk");

    //        entity.HasOne(d => d.Country).WithMany(p => p.Clients)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("country_fk");
    //    });

    //    modelBuilder.Entity<ContractAttachments>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("contract_attachments_pkey");

    //        entity.ToTable("contract_attachments", tb => tb.HasComment("معلومات عن مرفقات العقد"));

    //        entity.Property(e => e.Id).UseIdentityAlwaysColumn();

    //        entity.HasOne(d => d.Contract).WithMany(p => p.ContractAttachments).HasConstraintName("contract_fk");
    //    });

    //    modelBuilder.Entity<ContractDueDates>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("contract_payment_schedule_pkey");

    //        entity.ToTable("contract_due_dates", tb => tb.HasComment("تواريخ السداد للعقد"));

    //        entity.Property(e => e.Id).UseIdentityAlwaysColumn();

    //        entity.HasOne(d => d.Contract).WithMany(p => p.ContractDueDates)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("contract_due_date_fk");
    //    });

    //    modelBuilder.Entity<ContractImageModes>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("image_modes_pkey");

    //        entity.ToTable("contract_image_modes", tb => tb.HasComment("mono\nstereo\ntri Stereo"));

    //        entity.Property(e => e.Id).UseIdentityAlwaysColumn();

    //        entity.HasOne(d => d.Contract).WithMany(p => p.ContractImageModes)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("contract_fk");
    //    });

    //    modelBuilder.Entity<ContractImageResolution>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("contract_image_resolution_pkey");

    //        entity.ToTable("contract_image_resolution", tb => tb.HasComment("دقه التصوير سواء للتصوير الحديث او الصور الارشيفيه"));

    //        entity.Property(e => e.Id).UseIdentityAlwaysColumn();

    //        entity.HasOne(d => d.Contract).WithMany(p => p.ContractImageResolution)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("contract_fk");

    //        entity.HasOne(d => d.ContractImageType).WithMany(p => p.ContractImageResolution)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("contract_image_type_fk");
    //    });

    //    modelBuilder.Entity<ContractImageType>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("contract_image_type_pkey");

    //        entity.ToTable("contract_image_type", tb => tb.HasComment("نوع التصوير\nحديث او من الارشيف"));

    //        entity.Property(e => e.Id).UseIdentityAlwaysColumn();
    //    });

    //    modelBuilder.Entity<ContractOrderPriority>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("order_priority_pkey");

    //        entity.ToTable("contract_order_priority", tb => tb.HasComment("اولويه الطلب عاديه عاجله طارئه"));

    //        entity.Property(e => e.Id).UseIdentityAlwaysColumn();

    //        entity.HasOne(d => d.Contract).WithMany(p => p.ContractOrderPriority)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("contract_fk");
    //    });

    //    modelBuilder.Entity<ContractPaymentInformation>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("contract_payment_information_pkey");

    //        entity.ToTable("contract_payment_information", tb => tb.HasComment("بيانات الدفع للعقد"));

    //        entity.Property(e => e.Id).UseIdentityAlwaysColumn();

    //        entity.HasOne(d => d.Contract).WithMany(p => p.ContractPaymentInformation)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("contract_fk");
    //    });

    //    modelBuilder.Entity<ContractPaymentType>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("contract_type_pkey");

    //        entity.ToTable("contract_payment_type", tb => tb.HasComment("نوع الدفع كاش او كريدت"));

    //        entity.Property(e => e.Id).UseIdentityAlwaysColumn();
    //    });

    //    modelBuilder.Entity<ContractPeriods>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("contract_periods_pkey");

    //        entity.ToTable("contract_periods", tb => tb.HasComment("فترات العقد والكريديت المتاح في كل فتره"));

    //        entity.Property(e => e.Id).UseIdentityAlwaysColumn();

    //        entity.HasOne(d => d.Contract).WithMany(p => p.ContractPeriods)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("contract_periods_fk");
    //    });

    //    modelBuilder.Entity<ContractServices>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("contract_services_pkey");

    //        entity.ToTable("contract_services", tb => tb.HasComment("الخدمات المتاحه خلال التعاقد"));

    //        entity.Property(e => e.Id).UseIdentityAlwaysColumn();

    //        entity.HasOne(d => d.Contract).WithMany(p => p.ContractServices)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("contract_fk");
    //    });

    //    modelBuilder.Entity<Contracts>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("contracts_pkey");

    //        entity.ToTable("contracts", tb => tb.HasComment("العقود لكل عميل"));

    //        entity.Property(e => e.Id).UseIdentityAlwaysColumn();

    //        entity.HasOne(d => d.Client).WithMany(p => p.Contracts)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("client_fk");

    //        entity.HasOne(d => d.ContractPaymentType).WithMany(p => p.Contracts)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("contract_type_fk");

    //        entity.HasOne(d => d.Currency).WithMany(p => p.Contracts)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("currency_fk");
    //    });

    //    modelBuilder.Entity<Country>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("country_pkey");

    //        entity.ToTable("country", tb => tb.HasComment("البلاد "));

    //        entity.Property(e => e.Id).UseIdentityAlwaysColumn();
    //    });

    //    modelBuilder.Entity<Currency>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("currency_pkey");

    //        entity.ToTable("currency", tb => tb.HasComment("العملات"));

    //        entity.Property(e => e.Id).UseIdentityAlwaysColumn();
    //    });

    //    modelBuilder.Entity<Satellite>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("satellite_pkey");

    //        entity.Property(e => e.Id).UseIdentityAlwaysColumn();
    //    });

    //    modelBuilder.Entity<SmsOrder>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("sms_order_pkey");

    //        entity.ToTable("sms_order", tb => tb.HasComment("الطلبات المسجله عن طريق محطه تخطيط المهام"));

    //        entity.Property(e => e.Id).UseIdentityAlwaysColumn();

    //        entity.HasOne(d => d.Client).WithMany(p => p.SmsOrder)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("client_fk");

    //        entity.HasOne(d => d.Contract).WithMany(p => p.SmsOrder)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("contract_fk");

    //        entity.HasOne(d => d.OrderStatus).WithMany(p => p.SmsOrder)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("status_fk");
    //    });

    //    modelBuilder.Entity<SmsOrderOpportunities>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("sms_order_opportunities_pkey");

    //        entity.Property(e => e.Id).UseIdentityAlwaysColumn();

    //        entity.HasOne(d => d.Order).WithMany(p => p.SmsOrderOpportunities)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("order_fk");

    //        entity.HasOne(d => d.Sat).WithMany(p => p.SmsOrderOpportunities)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("satellite_fk");
    //    });

    //    modelBuilder.Entity<SmsOrderRoutes>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("sms_order_routes_pkey");

    //        entity.Property(e => e.Id).UseIdentityAlwaysColumn();

    //        entity.HasOne(d => d.Order).WithMany(p => p.SmsOrderRoutes)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("order_fk");

    //        entity.HasOne(d => d.Sat).WithMany(p => p.SmsOrderRoutes)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("sat_fk");
    //    });

    //    modelBuilder.Entity<SmsOrderServices>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("sms_order_services_pkey");

    //        entity.ToTable("sms_order_services", tb => tb.HasComment("الخدمات المتعلقه بكل طلب"));

    //        entity.Property(e => e.Id).UseIdentityAlwaysColumn();

    //        entity.HasOne(d => d.Order).WithMany(p => p.SmsOrderServices)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("order_fk");

    //        entity.HasOne(d => d.Service).WithMany(p => p.SmsOrderServices)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("service_fk");
    //    });

    //    modelBuilder.Entity<SmsOrderStatus>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("sms_order_status_pkey");

    //        entity.ToTable("sms_order_status", tb => tb.HasComment("حاله الطلب \nتم التسليم للعميل\nالطلب جاهز للتسليم\nمنتظر الخطه\nجاري التصوير\nفشل"));

    //        entity.Property(e => e.Id).UseIdentityAlwaysColumn();
    //    });

    //    modelBuilder.Entity<SmsRouteScenes>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("sms_route_scenes_pkey");

    //        entity.Property(e => e.Id).UseIdentityAlwaysColumn();

    //        entity.HasOne(d => d.Route).WithMany(p => p.SmsRouteScenes)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("route_fk");
    //    });

    //    modelBuilder.Entity<SmsSceneTargets>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("sms_scene_targets_pkey");

    //        entity.Property(e => e.Id).UseIdentityAlwaysColumn();

    //        entity.HasOne(d => d.Scene).WithMany(p => p.SmsSceneTargets)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("scene_fk");

    //        entity.HasOne(d => d.Target).WithMany(p => p.SmsSceneTargets)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("target_fk");
    //    });

    //    modelBuilder.Entity<SmsTargetTypeMainCategory>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("sms_target_type_main_category_pkey");

    //        entity.Property(e => e.Id).UseIdentityAlwaysColumn();
    //    });

    //    modelBuilder.Entity<SmsTargetTypeSubCategory>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("sms_target_type_sub_category_pkey");

    //        entity.Property(e => e.Id).UseIdentityAlwaysColumn();

    //        entity.HasOne(d => d.SmsTargetTypeMainCategory).WithMany(p => p.SmsTargetTypeSubCategory)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("sms_target_type_main_category_fk");
    //    });

    //    modelBuilder.Entity<SmsTargets>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("sms_targets_pkey");

    //        entity.Property(e => e.Id).UseIdentityAlwaysColumn();

    //        entity.HasOne(d => d.Country).WithMany(p => p.SmsTargets)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("country_fk");

    //        entity.HasOne(d => d.SmsTargetTypeSubCategory).WithMany(p => p.SmsTargets)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("sms_target_type_sub_category_fk");
    //    });

    //    OnModelCreatingPartial(modelBuilder);
    //}

    //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
