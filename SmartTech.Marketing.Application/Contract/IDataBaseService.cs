using Microsoft.EntityFrameworkCore;
using SmartTech.Marketing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SmartTech.Marketing.Application.Contract
{
    public interface IDataBaseService
    {
        public DbSet<ClientType> ClientType { get; set; }

        public DbSet<Client> Client { get; set; }

        public DbSet<ContractAttachments> ContractAttachments { get; set; }

        public DbSet<ContractDueDates> ContractDueDates { get; set; }

        public DbSet<ContractImageModes> ContractImageModes { get; set; }

        public DbSet<ContractImageResolution> ContractImageResolution { get; set; }

        public DbSet<ContractImageType> ContractImageType { get; set; }

        public DbSet<ContractOrderPriority> ContractOrderPriority { get; set; }

        public DbSet<ContractPaymentInformation> ContractPaymentInformation { get; set; }

        public DbSet<ContractPaymentType> ContractPaymentType { get; set; }

        public DbSet<ContractPeriods> ContractPeriods { get; set; }

        public DbSet<ContractServices> ContractServices { get; set; }

        public DbSet<Contracts> Contracts { get; set; }

        public DbSet<Country> Country { get; set; }

        public DbSet<Currency> Currency { get; set; }

        public DbSet<Satellite> Satellite { get; set; }

        public DbSet<SmsOrder> SmsOrder { get; set; }

        public DbSet<SmsOrderOpportunities> SmsOrderOpportunities { get; set; }

        public DbSet<SmsOrderRoutes> SmsOrderRoutes { get; set; }

        public DbSet<SmsOrderServices> SmsOrderServices { get; set; }

        public DbSet<SmsOrderStatus> SmsOrderStatus { get; set; }

        public DbSet<SmsRouteScenes> SmsRouteScenes { get; set; }

        public DbSet<SmsSceneTargets> SmsSceneTargets { get; set; }

        public DbSet<SmsTargetTypeMainCategory> SmsTargetTypeMainCategory { get; set; }

        public DbSet<SmsTargetTypeSubCategory> SmsTargetTypeSubCategory { get; set; }

        public DbSet<SmsTargets> SmsTargets { get; set; }
        int DBSaveChanges();
        Task<int> DBSaveChangesAsync(CancellationToken cancellationToken = default);

    }

}
