using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace SmartTech.Marketing.Domain.Entities;

[Table("sms_order_opportunities")]
public partial class SmsOrderOpportunities
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("geometry")]
    public Geometry Geometry { get; set; } = null!;

    [Column("date")]
    public DateOnly Date { get; set; }

    [Column("sat_id")]
    public int SatId { get; set; }

    [Column("order_id")]
    public int OrderId { get; set; }

    [Column("chosen")]
    public bool Chosen { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("SmsOrderOpportunities")]
    public virtual SmsOrder Order { get; set; } = null!;

    [ForeignKey("SatId")]
    [InverseProperty("SmsOrderOpportunities")]
    public virtual Satellite Sat { get; set; } = null!;
}
