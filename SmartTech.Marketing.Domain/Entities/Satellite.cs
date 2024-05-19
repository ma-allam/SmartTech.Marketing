using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartTech.Marketing.Domain.Entities;

[Table("satellite")]
public partial class Satellite
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("is_active")]
    public bool IsActive { get; set; }

    [InverseProperty("Sat")]
    public virtual ICollection<SmsOrderOpportunities> SmsOrderOpportunities { get; set; } = new List<SmsOrderOpportunities>();

    [InverseProperty("Sat")]
    public virtual ICollection<SmsOrderRoutes> SmsOrderRoutes { get; set; } = new List<SmsOrderRoutes>();
}
