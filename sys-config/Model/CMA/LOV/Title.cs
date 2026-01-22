using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemConfigurator.Model.CMA.LOV;

[Table("cma_tbl_lov_customer_title")]
public class Title
{
    [Key]
    [Column("title_id")]
    [StringLength(20)]
    public string TitleId { get; set; } = "";

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