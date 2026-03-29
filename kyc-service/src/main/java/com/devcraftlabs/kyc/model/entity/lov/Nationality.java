package com.devcraftlabs.kyc.model.entity.lov;

import java.time.Instant;
import java.util.UUID;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import lombok.Data;

@Entity
@Table(name = "cma_tbl_lov_nationality")
@Data
public class Nationality {

    @Id
    @Column(name = "nationality_id", nullable = false)
    private UUID nationalityId;

    @Column(name = "nationality_name", nullable = false, length = 80)
    private String nationalityName;

    @Column(name = "created_at", nullable = false)
    private Instant createdAt = Instant.now();

    @Column(name = "updated_at")
    private Instant updatedAt;

    @Column(name = "deleted_at")
    private Instant deletedAt;
}