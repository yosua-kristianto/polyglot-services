package com.devcraftlabs.notification.repository;

import java.util.UUID;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import com.devcraftlabs.notification.model.entity.PersonalNotification;

@Repository
public interface PersonalNotificationRepository extends JpaRepository<PersonalNotification, UUID> {

}
