using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace SmartTech.Marketing.Domain.Entities;

[Table("sms_order")]
[Index("ClientId", Name = "IX_sms_order_client_id")]
[Index("ContractId", Name = "IX_sms_order_contract_id")]
[Index("OrderStatusId", Name = "IX_sms_order_order_status_id")]
public partial class SmsOrder
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("client_id")]
    public int ClientId { get; set; }

    [Column("contract_id")]
    public int ContractId { get; set; }

    [Column("order_date")]
    public DateOnly OrderDate { get; set; }

    [Column("contract_image_resolution_id")]
    public int ContractImageResolutionId { get; set; }

    [Column("contract_image_mode_id")]
    public int ContractImageModeId { get; set; }

    [Column("contract_order_pirority_id")]
    public int ContractOrderPirorityId { get; set; }

    [Column("shooting_angle")]
    public double ShootingAngle { get; set; }

    [Column("predicted_consumed_credit")]
    public double PredictedConsumedCredit { get; set; }

    [Column("actual_consumed_credit")]
    public double ActualConsumedCredit { get; set; }

    [Column("discount")]
    public double Discount { get; set; }

    [Column("notes")]
    public string? Notes { get; set; }

    [Column("compeleted_percentage")]
    public double CompeletedPercentage { get; set; }

    [Column("total_order_area_in_KM")]
    public double TotalOrderAreaInKm { get; set; }

    [Column("order_geometry")]
    public Geometry OrderGeometry { get; set; } = null!;

    [Column("due_date")]
    public DateOnly DueDate { get; set; }

    [Column("order_status_id")]
    public int OrderStatusId { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("SmsOrder")]
    public virtual Client Client { get; set; } = null!;

    [ForeignKey("ContractId")]
    [InverseProperty("SmsOrder")]
    public virtual Contracts Contract { get; set; } = null!;

    [ForeignKey("OrderStatusId")]
    [InverseProperty("SmsOrder")]
    public virtual SmsOrderStatus OrderStatus { get; set; } = null!;

    [InverseProperty("SmsOrder")]
    public virtual ICollection<SmsOrderImageService> SmsOrderImageService { get; set; } = new List<SmsOrderImageService>();

    [InverseProperty("Order")]
    public virtual ICollection<SmsOrderOpportunities> SmsOrderOpportunities { get; set; } = new List<SmsOrderOpportunities>();

    [InverseProperty("Order")]
    public virtual ICollection<SmsOrderRoutes> SmsOrderRoutes { get; set; } = new List<SmsOrderRoutes>();

    [InverseProperty("Order")]
    public virtual ICollection<SmsOrderServices> SmsOrderServices { get; set; } = new List<SmsOrderServices>();
}
