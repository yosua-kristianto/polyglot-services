using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemConfigurator.Model.UMA;

[Table("uma_tbl_mobile_users")]
public class User
{
    public const short STATUS_INACTIVE = 0;
    public const short STATUS_ACTIVE = 1;
    public const short STATUS_SUSPENDED = 2;

    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [Column("email")]
    [EmailAddress]
    public string Email { get; set; } = "";

    [Required]
    [Column("name")]
    public string Name { get; set; } = "";

    [Required]
    [Column("status")]
    public short Status {get;set;} = 0;

    [Required]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    public ICollection<Device> Devices { get; set; } = new List<Device>();
}