using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemConfigurator.Model.UMA.LOV;

namespace SystemConfigurator.Model.UMA;

[Table("uma_tbl_user_devices")]
public class Device
{
    [Key]
    [Column("device_id")]
    public Guid DeviceId { get; set; } = Guid.NewGuid();

    [ForeignKey("User")]
    [Column("user_id")]
    public Guid UserId { get; set; }

    [Required]
    [Column("last_login")]
    public DateTime LastLogin { get; set; } = DateTime.UtcNow;

    [ForeignKey("DeviceType")]
    [Column("device_type_id")]
    public string DeviceTypeId {get; set; } = null!;

    [Required]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    public User User {get; set; }
    public DeviceType DeviceType {get; set; }

}