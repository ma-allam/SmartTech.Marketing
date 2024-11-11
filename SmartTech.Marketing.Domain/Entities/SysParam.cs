using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartTech.Marketing.Domain.Entities;

public partial class SysParam
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    public string ParamName { get; set; } = null!;

    public bool ParamValue { get; set; }
}
