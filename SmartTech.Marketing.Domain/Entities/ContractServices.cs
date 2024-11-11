using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartTech.Marketing.Domain.Entities;

[Table("contract_services")]
[Index("ContractId", Name = "IX_contract_services_contract_id")]
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

    [InverseProperty("ContractImageService")]
    public virtual ICollection<SmsOrderImageService> SmsOrderImageService { get; set; } = new List<SmsOrderImageService>();

    [InverseProperty("Service")]
    public virtual ICollection<SmsOrderServices> SmsOrderServices { get; set; } = new List<SmsOrderServices>();
}
