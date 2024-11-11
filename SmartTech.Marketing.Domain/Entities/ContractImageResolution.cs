using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartTech.Marketing.Domain.Entities;

[Table("contract_image_resolution")]
[Index("ContractId", Name = "IX_contract_image_resolution_contract_id")]
[Index("ContractImageTypeId", Name = "IX_contract_image_resolution_contract_image_type_id")]
public partial class ContractImageResolution
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("resolution_in_cm")]
    public int ResolutionInCm { get; set; }

    [Column("min_order_area_size")]
    public double MinOrderAreaSize { get; set; }

    [Column("credit_factor")]
    public double CreditFactor { get; set; }

    [Column("contract_id")]
    public int ContractId { get; set; }

    [Column("contract_image_type_id")]
    public int ContractImageTypeId { get; set; }

    [ForeignKey("ContractId")]
    [InverseProperty("ContractImageResolution")]
    public virtual Contracts Contract { get; set; } = null!;

    [ForeignKey("ContractImageTypeId")]
    [InverseProperty("ContractImageResolution")]
    public virtual ContractImageType ContractImageType { get; set; } = null!;
}
