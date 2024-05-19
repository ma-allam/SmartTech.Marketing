using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace SmartTech.Marketing.Domain.Entities;

[Table("sms_route_scenes")]
public partial class SmsRouteScenes
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("route_id")]
    public int RouteId { get; set; }

    [Column("geometry")]
    public Geometry Geometry { get; set; } = null!;

    [Column("cloudness")]
    public double Cloudness { get; set; }

    [Column("shooting_date")]
    public DateOnly ShootingDate { get; set; }

    [Column("shooting_angle")]
    public double ShootingAngle { get; set; }

    [Column("ql_path")]
    public string QlPath { get; set; } = null!;

    [ForeignKey("RouteId")]
    [InverseProperty("SmsRouteScenes")]
    public virtual SmsOrderRoutes Route { get; set; } = null!;

    [InverseProperty("Scene")]
    public virtual ICollection<SmsSceneTargets> SmsSceneTargets { get; set; } = new List<SmsSceneTargets>();
}
