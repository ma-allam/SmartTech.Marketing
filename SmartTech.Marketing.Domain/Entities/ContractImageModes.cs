using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartTech.Marketing.Domain.Entities;

/// <summary>
/// mono
/// stereo
/// tri Stereo
/// </summary>
[Table("contract_image_modes")]
public partial class ContractImageModes
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("credit_factor")]
    public double CreditFactor { get; set; }

    [Column("contract_id")]
    public int ContractId { get; set; }

    [ForeignKey("ContractId")]
    [InverseProperty("ContractImageModes")]
    public virtual Contracts Contract { get; set; } = null!;
}
