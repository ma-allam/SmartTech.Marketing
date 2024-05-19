using Microsoft.EntityFrameworkCore;
using SmartTech.Marketing.Application.Contract;
using SmartTech.Marketing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace SmartTech.Marketing.Persistence.Context
{
    public partial class DatabaseServiceold : DbContext, IDataBaseService
    {


        public DatabaseServiceold()
        {
        }

        public DatabaseServiceold(DbContextOptions<DatabaseServiceold> options)
            : base(options)
        {
            Database.EnsureCreated();

        }

        public DbSet<ClientType> ClientType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<Clients> Clients { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<ContractAttachments> ContractAttachments { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<ContractDueDates> ContractDueDates { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<ContractImageModes> ContractImageModes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<ContractImageResolution> ContractImageResolution { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<ContractImageType> ContractImageType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<ContractOrderPriority> ContractOrderPriority { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<ContractPaymentInformation> ContractPaymentInformation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<ContractPaymentType> ContractPaymentType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<ContractPeriods> ContractPeriods { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<ContractServices> ContractServices { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<Contracts> Contracts { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<Country> Country { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<Currency> Currency { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<Satellite> Satellite { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<SmsOrder> SmsOrder { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<SmsOrderOpportunities> SmsOrderOpportunities { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<SmsOrderRoutes> SmsOrderRoutes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<SmsOrderServices> SmsOrderServices { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<SmsOrderStatus> SmsOrderStatus { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<SmsRouteScenes> SmsRouteScenes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<SmsSceneTargets> SmsSceneTargets { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<SmsTargetTypeMainCategory> SmsTargetTypeMainCategory { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<SmsTargetTypeSubCategory> SmsTargetTypeSubCategory { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<SmsTargets> SmsTargets { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("Server=.;Initial Catalog=SQlDBPhoneBook;Integrated Security=true;TrustServerCertificate=True;");
        }
    }
}
