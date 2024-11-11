using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartTech.Marketing.Domain.Entities;

[Table("sms_target_type_sub_category")]
[Index("SmsTargetTypeMainCategoryId", Name = "IX_sms_target_type_sub_category_sms_target_type_main_category_~")]
public partial class SmsTargetTypeSubCategory
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("sms_target_type_main_category_id")]
    public int SmsTargetTypeMainCategoryId { get; set; }

    [ForeignKey("SmsTargetTypeMainCategoryId")]
    [InverseProperty("SmsTargetTypeSubCategory")]
    public virtual SmsTargetTypeMainCategory SmsTargetTypeMainCategory { get; set; } = null!;

    [InverseProperty("SmsTargetTypeSubCategory")]
    public virtual ICollection<SmsTargets> SmsTargets { get; set; } = new List<SmsTargets>();
}
