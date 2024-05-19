using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartTech.Marketing.Domain.Entities;

/// <summary>
/// معلومات عن مرفقات العقد
/// </summary>
[Table("contract_attachments")]
public partial class ContractAttachments
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string? Name { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("file_extension")]
    public string? FileExtension { get; set; }

    [Column("file_url")]
    public string? FileUrl { get; set; }

    [Column("notes")]
    public string? Notes { get; set; }

    [Column("contract_id")]
    public int? ContractId { get; set; }

    [ForeignKey("ContractId")]
    [InverseProperty("ContractAttachments")]
    public virtual Contracts? Contract { get; set; }
}
