﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartTech.Marketing.Domain.Entities;

/// <summary>
/// حاله الطلب 
/// تم التسليم للعميل
/// الطلب جاهز للتسليم
/// منتظر الخطه
/// جاري التصوير
/// فشل
/// </summary>
[Table("sms_order_status")]
public partial class SmsOrderStatus
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [InverseProperty("OrderStatus")]
    public virtual ICollection<SmsOrder> SmsOrder { get; set; } = new List<SmsOrder>();
}
