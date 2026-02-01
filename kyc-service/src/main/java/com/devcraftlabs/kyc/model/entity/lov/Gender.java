package com.devcraftlabs.kyc.model.entity.lov;

import java.time.Instant;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import lombok.Data;

@Entity
@Table(name = "cma_tbl_lov_customer_gender")
@Data
public class Gender {

    @Id
    @Column(name = "gender_id", nullable = false, length = 20)
    private String genderId;

    @Column(name = "created_at", nullable = false)
    private Instant createdAt = Instant.now();

    @Column(name = "updated_at")
    private Instant updatedAt;

    @Column(name = "deleted_at")
    private Instant deletedAt;
}