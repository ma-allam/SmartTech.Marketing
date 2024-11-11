using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace SmartTech.Marketing.Domain.Entities;

[Table("sms_order_routes")]
[Index("OrderId", Name = "IX_sms_order_routes_order_id")]
[Index("SatId", Name = "IX_sms_order_routes_sat_id")]
public partial class SmsOrderRoutes
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("sat_id")]
    public int SatId { get; set; }

    [Column("order_id")]
    public int OrderId { get; set; }

    [Column("external_system_identifier")]
    public string ExternalSystemIdentifier { get; set; } = null!;

    [Column("geometry")]
    public Geometry Geometry { get; set; } = null!;

    [ForeignKey("OrderId")]
    [InverseProperty("SmsOrderRoutes")]
    public virtual SmsOrder Order { get; set; } = null!;

    [ForeignKey("SatId")]
    [InverseProperty("SmsOrderRoutes")]
    public virtual Satellite Sat { get; set; } = null!;

    [InverseProperty("Route")]
    public virtual ICollection<SmsRouteScenes> SmsRouteScenes { get; set; } = new List<SmsRouteScenes>();
}
