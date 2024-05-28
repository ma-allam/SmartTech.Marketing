using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartTech.Marketing.Domain.Entities;

/// <summary>
/// نوع العميل 
/// جهه حكومية - عسكرية - مدنيه او اخري
/// </summary>
[Table("client_type")]
public partial class ClientType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("type")]
    public string Type { get; set; } = null!;

    [InverseProperty("ClientTypeNavigation")]
    public virtual ICollection<Client> Client { get; set; } = new List<Client>();
}
