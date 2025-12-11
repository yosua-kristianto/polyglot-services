using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bogus.DataSets;
using SysConfig.Model.NMA.LOV;

namespace SysConfig.Model.NMA;

[Table("nma_tbl_personal_notifications")]
public class PersonalNotification
{
    [Key]
    [Column("notification_id")]
    public Guid NotificationId { get; set; }

    [Required]
    [Column("user_id")]
    public Guid UserId { get; set; }

    [Required]
    [Column("notification_platform_type")]
    [MaxLength(20)]
    public string NotificationPlatformType {get; set;}
    
    [Required]
    [Column("notification_title")]
    [MaxLength(60)]
    public string Title { get; set;}

    [Required]
    [Column("notification_body")]
    [DataType(DataType.Text)]
    public string Body {get;set;}
    
    [Required]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    public NotificationPlatformType PlatformType {get; set; }
}
