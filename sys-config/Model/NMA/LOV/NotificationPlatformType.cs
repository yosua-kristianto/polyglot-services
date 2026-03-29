using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SysConfig.Model.NMA;

namespace SysConfig.Model.NMA.LOV;

[Table("nma_lov_notification_platform_type")]
public class NotificationPlatformType
{
    [Key]
    [Column("notification_platform_type_id")]
    [MaxLength(20)]
    public string Id { get; set; } = null!;

    [Required]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    public ICollection<PersonalNotification> PersonalNotifications { get; set; } = new List<PersonalNotification>();
}
