using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartTech.Marketing.Domain.Entities;

[Table("contract_attachments")]
[Index("ContractId", Name = "IX_contract_attachments_contract_id")]
public partial class ContractAttachments
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string? Name { get; set; }

    public string? Tags { get; set; }

    [Column("file_extension")]
    public string? FileExtension { get; set; }

    [Column("file_url")]
    public string? FileUrl { get; set; }

    [Column("notes")]
    public string? Notes { get; set; }

    [Column("contract_id")]
    public int? ContractId { get; set; }

    public Guid AttachmentId { get; set; }

    public DateOnly UploadDate { get; set; }

    [ForeignKey("ContractId")]
    [InverseProperty("ContractAttachments")]
    public virtual Contracts? Contract { get; set; }
}
