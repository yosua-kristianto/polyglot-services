package com.devcraftlabs.notification.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import com.devcraftlabs.notification.model.entity.lov.PersonalNotificationType;

@Repository
public interface PersonalNotificationTypeRepository extends JpaRepository<PersonalNotificationType, String>{

}
