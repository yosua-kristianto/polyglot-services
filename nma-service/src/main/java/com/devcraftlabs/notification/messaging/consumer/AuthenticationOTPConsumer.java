package com.devcraftlabs.notification.messaging.consumer;

import java.util.function.Consumer;

import org.apache.kafka.clients.consumer.ConsumerRecord;
import org.springframework.kafka.annotation.KafkaListener;
import org.springframework.stereotype.Component;
import org.springframework.stereotype.Service;

import com.devcraftlabs.notification.handler.PersonalNotificationHandler;
import com.devcraftlabs.notification.model.dto.consumer.OTPMessageRequestDTO;

@Component
public class AuthenticationOTPConsumer {
    private PersonalNotificationHandler _handler;

    public AuthenticationOTPConsumer(PersonalNotificationHandler handler) {
        this._handler = handler;
    }

    @KafkaListener(
        topics = "nma-service-01-authotp",
        groupId = "nma-service"
    )
    public void consume(OTPMessageRequestDTO message){
        if (message == null) {
            return;
        }

        // Here you can directly call SMTP service
        System.out.printf("Received Auth OTP message: %s - %s - %s - %s", message.getUserId(), message.getRecipientName(), message.getOtp(), message.getEmail());

        this._handler.handleAuthenticationOTP(
            message.getUserId(), 
            message.getRecipientName(), 
            message.getOtp(),
            message.getEmail()
        );
    }
}
