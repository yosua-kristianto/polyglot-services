package com.devcraftlabs.kyc.model.entity;

import java.time.Instant;
import java.time.LocalDate;
import java.util.UUID;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import lombok.Data;

@Entity
@Table(name = "cma_tbl_customer")
@Data
public class Customer {

    @Id
    @Column(name = "customer_id", nullable = false)
    private UUID customerId;

    @Column(name = "account_no", nullable = false, length = 20)
    private String accountNo;

    @Column(name = "name", nullable = false, length = 200)
    private String name;

    @Column(name = "email", nullable = false, length = 100)
    private String email;

    @Column(name = "gender_id", nullable = false, length = 20)
    private String genderId;

    @Column(name = "title_id", nullable = false, length = 20)
    private String titleId;

    @Column(name = "marital_status_id", nullable = false, length = 20)
    private String maritalStatusId;

    @Column(name = "birth_date", nullable = false)
    private LocalDate birthDate;

    @Column(name = "birthplace_district_id", nullable = false)
    private UUID birthplaceDistrictId;

    @Column(name = "id_number", nullable = false, length = 40)
    private String idNumber;

    @Column(name = "identity_type_id", nullable = false, length = 20)
    private String identityTypeId;

    @Column(name = "nationality_id", nullable = false)
    private UUID nationalityId;

    @Column(name = "legal_address", nullable = false, length = 200)
    private String legalAddress;

    @Column(name = "legal_province_id", nullable = false)
    private Integer legalProvinceId;

    @Column(name = "legal_district_id", nullable = false)
    private Integer legalDistrictId;

    @Column(name = "legal_city_id", nullable = false)
    private Integer legalCityId;

    @Column(name = "legal_village_id", nullable = false)
    private Integer legalVillageId;

    @Column(name = "legal_zipcode_id", nullable = false, length = 15)
    private String legalZipcodeId;

    @Column(name = "domicile_address", length = 200)
    private String domicileAddress;

    @Column(name = "domicile_province_id")
    private Integer domicileProvinceId;

    @Column(name = "domicile_district_id")
    private Integer domicileDistrictId;

    @Column(name = "domicile_city_id")
    private Integer domicileCityId;

    @Column(name = "domicile_village_id")
    private Integer domicileVillageId;

    @Column(name = "domicile_zipcode_id", length = 15)
    private String domicileZipcodeId;

    @Column(name = "home_phone_no", length = 20)
    private String homePhoneNo;

    @Column(name = "mobile_phone_no", length = 20)
    private String mobilePhoneNo;

    @Column(name = "mobile_phone_no_2", length = 20)
    private String mobilePhoneNo2;

    @Column(name = "contact_phone_no", length = 20)
    private String contactPhoneNo;

    @Column(name = "photo", length = 400)
    private String photo;

    @Column(name = "id_card_photo", length = 400)
    private String idCardPhoto;

    @Column(name = "kyc_status", nullable = false)
    private Boolean kycStatus = false;

    @Column(name = "created_by")
    private UUID createdBy;

    @Column(name = "kyc_validated_at")
    private Instant kycValidatedAt;

    @Column(name = "kyc_validated_by")
    private Instant kycValidatedBy;

    @Column(name = "created_at", nullable = false)
    private Instant createdAt = Instant.now();

    @Column(name = "updated_at")
    private Instant updatedAt;

    @Column(name = "deleted_at")
    private Instant deletedAt;
}