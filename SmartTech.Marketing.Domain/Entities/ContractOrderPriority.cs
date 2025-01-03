﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartTech.Marketing.Domain.Entities;

[Table("contract_order_priority")]
[Index("ContractId", Name = "IX_contract_order_priority_contract_id")]
public partial class ContractOrderPriority
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("contract_id")]
    public int ContractId { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("max_allowed_days")]
    public int MaxAllowedDays { get; set; }

    [Column("credit_factor")]
    public double CreditFactor { get; set; }

    [ForeignKey("ContractId")]
    [InverseProperty("ContractOrderPriority")]
    public virtual Contracts Contract { get; set; } = null!;
}
