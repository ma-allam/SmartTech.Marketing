using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartTech.Marketing.Domain.Entities;

[Table("sms_order_services")]
[Index("OrderId", Name = "IX_sms_order_services_order_id")]
[Index("ServiceId", Name = "IX_sms_order_services_service_id")]
public partial class SmsOrderServices
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("order_id")]
    public int OrderId { get; set; }

    [Column("service_id")]
    public int ServiceId { get; set; }

    [Column("notes")]
    public string? Notes { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("SmsOrderServices")]
    public virtual SmsOrder Order { get; set; } = null!;

    [ForeignKey("ServiceId")]
    [InverseProperty("SmsOrderServices")]
    public virtual ContractServices Service { get; set; } = null!;
}
