using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemConfigurator.Model.UMA.LOV;

[Table("uma_tbl_lov_device_type")]
public class DeviceType
{
    [Key]
    [Column("device_type_id")]
    public string DeviceTypeId { get; set; } = null!;

    [Required]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    public ICollection<Device> Devices { get; set; } = new List<Device>();
}