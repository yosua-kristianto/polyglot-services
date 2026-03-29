using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemConfigurator.Model.CMA.LOV;

[Table("cma_tbl_lov_nationality")]
public class Nationality
{
    [Key]
    [Column("nationality_id")]
    public Guid NationalityId { get; set; } = Guid.NewGuid();

    [Required]
    [Column("nationality_name")]
    [StringLength(80)]
    public string NationalityName { get; set; } = "";

    [Required]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    // Navigation Property
    public ICollection<Customer> Customers { get; set; } = new List<Customer>();
}