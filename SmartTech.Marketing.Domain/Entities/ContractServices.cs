using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartTech.Marketing.Domain.Entities;

/// <summary>
/// الخدمات المتاحه خلال التعاقد
/// </summary>
[Table("contract_services")]
public partial class ContractServices
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("service_name")]
    public string ServiceName { get; set; } = null!;

    [Column("service_cost")]
    public double ServiceCost { get; set; }

    [Column("notes")]
    public string? Notes { get; set; }

    [Column("contract_id")]
    public int ContractId { get; set; }

    [ForeignKey("ContractId")]
    [InverseProperty("ContractServices")]
    public virtual Contracts Contract { get; set; } = null!;

    [InverseProperty("Service")]
    public virtual ICollection<SmsOrderServices> SmsOrderServices { get; set; } = new List<SmsOrderServices>();
}
