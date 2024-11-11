using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartTech.Marketing.Domain.Entities;

[Table("contract_payment_information")]
[Index("ContractId", Name = "IX_contract_payment_information_contract_id")]
public partial class ContractPaymentInformation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("contract_id")]
    public int ContractId { get; set; }

    [Column("bank_name")]
    public string? BankName { get; set; }

    [Column("bank_branch")]
    public string? BankBranch { get; set; }

    [Column("bank_address")]
    public string? BankAddress { get; set; }

    [Column("IBAN")]
    public string? Iban { get; set; }

    [Column("client_name_in_bank")]
    public string? ClientNameInBank { get; set; }

    [Column("notes")]
    public string? Notes { get; set; }

    [ForeignKey("ContractId")]
    [InverseProperty("ContractPaymentInformation")]
    public virtual Contracts Contract { get; set; } = null!;
}
