using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartTech.Marketing.Domain.Entities;

/// <summary>
/// تواريخ السداد للعقد
/// </summary>
[Table("contract_due_dates")]
public partial class ContractDueDates
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("contract_id")]
    public int ContractId { get; set; }

    [Column("due_date")]
    public DateOnly DueDate { get; set; }

    [Column("notes")]
    public string? Notes { get; set; }

    [ForeignKey("ContractId")]
    [InverseProperty("ContractDueDates")]
    public virtual Contracts Contract { get; set; } = null!;
}
