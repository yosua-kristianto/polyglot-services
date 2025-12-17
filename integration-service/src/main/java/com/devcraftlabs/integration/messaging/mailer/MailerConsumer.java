package com.devcraftlabs.integration.messaging.mailer;

import org.springframework.kafka.annotation.KafkaListener;
import org.springframework.stereotype.Component;

import com.devcraftlabs.integration.model.object.SmtpMessageRequestDTO;

@Component
public class MailerConsumer {

    @KafkaListener(
        topics = "integration-service-01-smtp",
        groupId = "integration-service"
    )
    public void consume(SmtpMessageRequestDTO message){
        if (message == null) {
            return;
        }

        String recipient = message.getRecipient();
        String subject = message.getSubject();
        String htmlBody = message.getHtmlBody();

        // Here you can directly call SMTP service
        System.out.println("Received SMTP message:");
        System.out.println("Recipient: " + recipient);
        System.out.println("Subject: " + subject);
        System.out.println("HTML Body: " + htmlBody);


    }
}
