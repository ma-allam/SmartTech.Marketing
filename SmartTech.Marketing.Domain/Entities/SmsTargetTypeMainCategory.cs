using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartTech.Marketing.Domain.Entities;

[Table("sms_target_type_main_category")]
public partial class SmsTargetTypeMainCategory
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [InverseProperty("SmsTargetTypeMainCategory")]
    public virtual ICollection<SmsTargetTypeSubCategory> SmsTargetTypeSubCategory { get; set; } = new List<SmsTargetTypeSubCategory>();
}
