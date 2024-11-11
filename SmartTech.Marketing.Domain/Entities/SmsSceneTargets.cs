using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartTech.Marketing.Domain.Entities;

[Table("sms_scene_targets")]
[Index("SceneId", Name = "IX_sms_scene_targets_scene_id")]
[Index("TargetId", Name = "IX_sms_scene_targets_target_id")]
public partial class SmsSceneTargets
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("scene_id")]
    public int SceneId { get; set; }

    [Column("target_id")]
    public int TargetId { get; set; }

    [ForeignKey("SceneId")]
    [InverseProperty("SmsSceneTargets")]
    public virtual SmsRouteScenes Scene { get; set; } = null!;

    [ForeignKey("TargetId")]
    [InverseProperty("SmsSceneTargets")]
    public virtual SmsTargets Target { get; set; } = null!;
}
