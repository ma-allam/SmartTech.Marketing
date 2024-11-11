using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartTech.Marketing.Domain.Entities;

[Table("sms_order_image_service")]
public partial class SmsOrderImageService
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("sms_order_id")]
    public int SmsOrderId { get; set; }

    [Column("contract_image_service_id")]
    public int ContractImageServiceId { get; set; }

    [ForeignKey("ContractImageServiceId")]
    [InverseProperty("SmsOrderImageService")]
    public virtual ContractServices ContractImageService { get; set; } = null!;

    [ForeignKey("SmsOrderId")]
    [InverseProperty("SmsOrderImageService")]
    public virtual SmsOrder SmsOrder { get; set; } = null!;
}
