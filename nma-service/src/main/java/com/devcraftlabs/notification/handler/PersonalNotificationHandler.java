package com.devcraftlabs.notification.handler;

import java.time.Instant;
import java.util.UUID;

import org.springframework.stereotype.Service;
import org.thymeleaf.TemplateEngine;
import org.thymeleaf.context.Context;

import com.devcraftlabs.notification.messaging.producer.SMTPMessageProducer;
import com.devcraftlabs.notification.model.dto.producer.SmtpMessageRequestDTO;
import com.devcraftlabs.notification.model.entity.PersonalNotification;
import com.devcraftlabs.notification.repository.PersonalNotificationRepository;

@Service
public class PersonalNotificationHandler implements IPersonalNotificationHandler {
    private PersonalNotificationRepository _repository;
    private TemplateEngine templateEngine;
    private SMTPMessageProducer _smtpMessageProducer;

    public PersonalNotificationHandler(
        TemplateEngine engine, 
        PersonalNotificationRepository repository,
        SMTPMessageProducer smtpMessageProducer
    ){
        this.templateEngine = engine;
        this._repository = repository;
        this._smtpMessageProducer = smtpMessageProducer;
    }

    @Override
    public void handleAuthenticationOTP(UUID userId, String recipient, String otp, String email) {
        PersonalNotification notification = new PersonalNotification();
        notification.setId(UUID.randomUUID());
        notification.setTitle("Authentication OTP - " + recipient);
        notification.setNotificationPlatformType(PersonalNotification.PERSONAL_NOTIFICATION_TYPE_MAIL);

        Context context = new Context();
        context.setVariable("recipient", recipient);
        context.setVariable("fullTimestamp", Instant.now().toString());
        context.setVariable("otp", otp);

        String html = templateEngine.process("otp-mail", context);
        
        notification.setUserId(userId);
        notification.setBody(html);

        this._repository.save(notification);

        SmtpMessageRequestDTO request = new SmtpMessageRequestDTO();
        request.setHtmlBody(html);
        request.setSubject(notification.getTitle());
        request.setRecipient(email);

        this._smtpMessageProducer.produce(request);
    }

}
