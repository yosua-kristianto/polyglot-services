using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemConfigurator.Model.CMA.LOV;

namespace SystemConfigurator.Model.CMA;

[Table("cma_tbl_customer")]
public class Customer
{
    [Key]
    [Column("customer_id")]
    public Guid CustomerId { get; set; } = Guid.NewGuid();

    [Required]
    [Column("account_no")]
    [StringLength(20)]
    public string AccountNo { get; set; } = "";

    [Required]
    [Column("name")]
    [StringLength(200)]
    public string Name { get; set; } = "";

    [Required]
    [Column("email")]
    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; } = "";

    [Required]
    [Column("gender_id")]
    [StringLength(20)]
    public string GenderId { get; set; } = "";

    [Required]
    [Column("title_id")]
    [StringLength(20)]
    public string TitleId { get; set; } = "";

    [Required]
    [Column("marital_status_id")]
    [StringLength(20)]
    public string MaritalStatusId { get; set; } = "";

    [Required]
    [Column("birth_date")]
    public DateOnly BirthDate { get; set; }

    [Required]
    [Column("birthplace_district_id")]
    public Guid BirthplaceDistrictId { get; set; }

    [Required]
    [Column("id_number")]
    [StringLength(40)]
    public string IdNumber { get; set; } = "";

    [Required]
    [Column("identity_type_id")]
    [StringLength(20)]
    public string IdentityTypeId { get; set; } = "";

    [Required]
    [Column("nationality_id")]
    public Guid NationalityId { get; set; }

    [Required]
    [Column("legal_address")]
    [StringLength(200)]
    public string LegalAddress { get; set; } = "";

    [Required]
    [Column("legal_province_id")]
    public int LegalProvinceId { get; set; }

    [Required]
    [Column("legal_district_id")]
    public int LegalDistrictId { get; set; }

    [Required]
    [Column("legal_city_id")]
    public int LegalCityId { get; set; }

    [Required]
    [Column("legal_village_id")]
    public int LegalVillageId { get; set; }

    [Required]
    [Column("legal_zipcode_id")]
    [StringLength(15)]
    public string LegalZipcodeId { get; set; } = "";

    [Column("domicile_address")]
    [StringLength(200)]
    public string? DomicileAddress { get; set; }

    [Column("domicile_province_id")]
    public int? DomicileProvinceId { get; set; }

    [Column("domicile_district_id")]
    public int? DomicileDistrictId { get; set; }

    [Column("domicile_city_id")]
    public int? DomicileCityId { get; set; }

    [Column("domicile_village_id")]
    public int? DomicileVillageId { get; set; }

    [Column("domicile_zipcode_id")]
    [StringLength(15)]
    public string? DomicileZipcodeId { get; set; }

    [Column("home_phone_no")]
    [StringLength(20)]
    public string? HomePhoneNo { get; set; }

    [Column("mobile_phone_no")]
    [StringLength(20)]
    public string? MobilePhoneNo { get; set; }

    [Column("mobile_phone_no_2")]
    [StringLength(20)]
    public string? MobilePhoneNo2 { get; set; }

    [Column("contact_phone_no")]
    [StringLength(20)]
    public string? ContactPhoneNo { get; set; }

    [Column("photo")]
    [StringLength(400)]
    public string? Photo { get; set; }

    [Column("id_card_photo")]
    [StringLength(400)]
    public string? IdCardPhoto { get; set; }

    [Required]
    [Column("kyc_status")]
    public bool KycStatus { get; set; } = false;

    [Column("created_by")]
    public Guid? CreatedBy { get; set; }

    [Column("kyc_validated_at")]
    public DateTime? KycValidatedAt { get; set; }

    [Column("kyc_validated_by")]
    public DateTime? KycValidatedBy { get; set; }

    [Required]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    // Foreign Key Navigation Properties
    [ForeignKey("GenderId")]
    public Gender? Gender { get; set; }

    [ForeignKey("TitleId")]
    public Title? Title { get; set; }

    [ForeignKey("MaritalStatusId")]
    public MaritalStatus? MaritalStatus { get; set; }

    [ForeignKey("IdentityTypeId")]
    public IdentityType? IdentityType { get; set; }

    [ForeignKey("NationalityId")]
    public Nationality? Nationality { get; set; }
}