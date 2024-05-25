using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartTech.Marketing.Domain.Entities;

/// <summary>
/// العقود لكل عميل
/// </summary>
[Table("contracts")]
public partial class Contracts
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("contract_number")]
    public string ContractNumber { get; set; } = null!;

    [Column("start_date")]
    public DateOnly StartDate { get; set; }

    [Column("end_date")]
    public DateOnly EndDate { get; set; }

    [Column("total_contract_cost")]
    public double TotalContractCost { get; set; }

    [Column("total_credit")]
    public int TotalCredit { get; set; }

    [Column("currency_id")]
    public int CurrencyId { get; set; }

    [Column("notes")]
    public string? Notes { get; set; }

    [Column("client_id")]
    public int ClientId { get; set; }

    [Column("contract_payment_type_id")]
    public int ContractPaymentTypeId { get; set; }

    [Column("enabled")]
    public bool Enabled { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("Contracts")]
    public virtual Client Client { get; set; } = null!;

    [InverseProperty("Contract")]
    public virtual ICollection<ContractAttachments> ContractAttachments { get; set; } = new List<ContractAttachments>();

    [InverseProperty("Contract")]
    public virtual ICollection<ContractDueDates> ContractDueDates { get; set; } = new List<ContractDueDates>();

    [InverseProperty("Contract")]
    public virtual ICollection<ContractImageModes> ContractImageModes { get; set; } = new List<ContractImageModes>();

    [InverseProperty("Contract")]
    public virtual ICollection<ContractImageResolution> ContractImageResolution { get; set; } = new List<ContractImageResolution>();

    [InverseProperty("Contract")]
    public virtual ICollection<ContractOrderPriority> ContractOrderPriority { get; set; } = new List<ContractOrderPriority>();

    [InverseProperty("Contract")]
    public virtual ICollection<ContractPaymentInformation> ContractPaymentInformation { get; set; } = new List<ContractPaymentInformation>();

    [ForeignKey("ContractPaymentTypeId")]
    [InverseProperty("Contracts")]
    public virtual ContractPaymentType ContractPaymentType { get; set; } = null!;

    [InverseProperty("Contract")]
    public virtual ICollection<ContractPeriods> ContractPeriods { get; set; } = new List<ContractPeriods>();

    [InverseProperty("Contract")]
    public virtual ICollection<ContractServices> ContractServices { get; set; } = new List<ContractServices>();

    [ForeignKey("CurrencyId")]
    [InverseProperty("Contracts")]
    public virtual Currency Currency { get; set; } = null!;

    [InverseProperty("Contract")]
    public virtual ICollection<SmsOrder> SmsOrder { get; set; } = new List<SmsOrder>();
}
