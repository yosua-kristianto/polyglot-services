package com.devcraftlabs.notification.model.entity;

import java.time.Instant;
import java.util.UUID;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import jakarta.persistence.Lob;
import jakarta.persistence.Table;
import lombok.Data;

@Entity
@Table(name = "nma_tbl_personal_notifications")
@Data
public class PersonalNotification {
    public static String PERSONAL_NOTIFICATION_TYPE_DEBUG = "DEBUG";
    public static String PERSONAL_NOTIFICATION_TYPE_MAIL = "MAIL";
    public static String PERSONAL_NOTIFICATION_TYPE_WA = "WHATSAPP";
    public static String PERSONAL_NOTIFICATION_TYPE_SMS = "SMS";

    @Id
    @Column(name = "notification_id", nullable = false)
    private UUID id;

    @Column(name = "user_id", nullable = false)
    private UUID userId;

    @Column(name = "notification_platform_type", nullable = false, length = 20)
    private String notificationPlatformType;

    @Column(name = "notification_title", nullable = false, length = 60)
    private String title;

    @Column(name = "notification_body", nullable = false, columnDefinition = "TEXT")
    private String body;

    @Column(name = "created_at", nullable = false)
    private Instant createdAt = Instant.now();

    @Column(name = "updated_at")
    private Instant updatedAt;

    @Column(name = "deleted_at")
    private Instant deletedAt;
}
