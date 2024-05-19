using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace SmartTech.Marketing.Domain.Entities;

/// <summary>
/// البلاد 
/// </summary>
[Table("country")]
public partial class Country
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("country_name")]
    public string CountryName { get; set; } = null!;

    [Column("country_geometry")]
    public Geometry? CountryGeometry { get; set; }

    [Column("geom", TypeName = "geometry(Point,3857)")]
    public Point? Geom { get; set; }

    [Column("county_geometry", TypeName = "geometry(Point,3857)")]
    public Point? CountyGeometry { get; set; }

    [Column("country_prefix")]
    public string CountryPrefix { get; set; } = null!;

    [InverseProperty("Country")]
    public virtual ICollection<Clients> Clients { get; set; } = new List<Clients>();

    [InverseProperty("Country")]
    public virtual ICollection<SmsTargets> SmsTargets { get; set; } = new List<SmsTargets>();
}
