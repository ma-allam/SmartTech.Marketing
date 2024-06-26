﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTech.Marketing.Domain.Entities
{
    /// <summary>
    /// بيانات العميل 
    /// اسم - نوع - تليفون - ايميل - دولة
    /// </summary>
    [Table("client")]
    public partial class Client
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = null!;

        [Column("phone_number")]
        public string? PhoneNumber { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("notes")]
        public string? Notes { get; set; }

        [Column("client_type")]
        public int ClientType { get; set; }

        [Column("country_id")]
        public int CountryId { get; set; }

        [Column("user_id")]
        public string UserId { get; set; } = null!;

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; } = null!;

        [ForeignKey("ClientType")]
        [InverseProperty("Client")]
        public virtual ClientType ClientTypeNavigation { get; set; } = null!;

        [ForeignKey("CountryId")]
        [InverseProperty("Client")]
        public virtual Country Country { get; set; } = null!;

        [InverseProperty("Client")]
        public virtual ICollection<Contracts> Contracts { get; set; } = new List<Contracts>();

        [InverseProperty("Client")]
        public virtual ICollection<SmsOrder> SmsOrder { get; set; } = new List<SmsOrder>();
    }
}
