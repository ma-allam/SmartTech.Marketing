using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartTech.Marketing.Domain.Entities;

/// <summary>
/// نوع الدفع كاش او كريدت
/// </summary>
[Table("contract_payment_type")]
public partial class ContractPaymentType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("type")]
    public string Type { get; set; } = null!;

    [InverseProperty("ContractPaymentType")]
    public virtual ICollection<Contracts> Contracts { get; set; } = new List<Contracts>();
}
