package com.devcraftlabs.notification.model.entity.lov;

import java.time.Instant;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import lombok.Data;

@Entity
@Table(name = "personal_notification_types")
@Data
public class PersonalNotificationType {
    
    @Id
    @Column(name = "personal_notification_type_id", nullable = false)
    private String id;

    @Column(name = "created_at", nullable = false)
    private Instant createdAt = Instant.now();

    @Column(name = "updated_at")
    private Instant updatedAt;

    @Column(name = "deleted_at")
    private Instant deletedAt;
}
