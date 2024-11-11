using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartTech.Marketing.Domain.Entities;

[Table("contract_periods")]
[Index("ContractId", Name = "IX_contract_periods_contract_id")]
public partial class ContractPeriods
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("start_date")]
    public DateOnly StartDate { get; set; }

    [Column("end_date")]
    public DateOnly EndDate { get; set; }

    [Column("available _credit")]
    public double AvailableCredit { get; set; }

    [Column("remaining_credit")]
    public double RemainingCredit { get; set; }

    [Column("contract_id")]
    public int ContractId { get; set; }

    [ForeignKey("ContractId")]
    [InverseProperty("ContractPeriods")]
    public virtual Contracts Contract { get; set; } = null!;
}
