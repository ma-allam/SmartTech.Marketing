using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace SmartTech.Marketing.Domain.Entities;

[Table("sms_targets")]
[Index("CountryId", Name = "IX_sms_targets_country_id")]
[Index("SmsTargetTypeSubCategoryId", Name = "IX_sms_targets_sms_target_type_sub_category_id")]
public partial class SmsTargets
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [Column("geometry")]
    public Geometry Geometry { get; set; } = null!;

    [Column("country_id")]
    public int CountryId { get; set; }

    [Column("code")]
    public string Code { get; set; } = null!;

    [Column("sms_target_type_sub_category_id")]
    public int SmsTargetTypeSubCategoryId { get; set; }

    [ForeignKey("CountryId")]
    [InverseProperty("SmsTargets")]
    public virtual Country Country { get; set; } = null!;

    [InverseProperty("Target")]
    public virtual ICollection<SmsSceneTargets> SmsSceneTargets { get; set; } = new List<SmsSceneTargets>();

    [ForeignKey("SmsTargetTypeSubCategoryId")]
    [InverseProperty("SmsTargets")]
    public virtual SmsTargetTypeSubCategory SmsTargetTypeSubCategory { get; set; } = null!;
}
