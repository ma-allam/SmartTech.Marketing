using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartTech.Marketing.Domain.Entities;

[Table("contract_image_modes")]
[Index("ContractId", Name = "IX_contract_image_modes_contract_id")]
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
