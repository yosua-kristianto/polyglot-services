package com.devcraftlabs.integration.messaging.mailer;

import org.springframework.kafka.annotation.KafkaListener;
import org.springframework.stereotype.Component;

import com.devcraftlabs.integration.model.object.SmtpMessageRequestDTO;
import com.devcraftlabs.integration.services.smtp.SMTPService;
import com.devcraftlabs.integration.services.telegram.TelegramService;

import jakarta.mail.MessagingException;

@Component
public class MailerConsumer {

    private SMTPService _smtpService;

    public MailerConsumer(SMTPService smtpService){
        this._smtpService = smtpService;
    }

    @KafkaListener(
        topics = "integration-service-01-smtp",
        groupId = "integration-service"
    )
    public void consume(SmtpMessageRequestDTO message) throws MessagingException{
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

        try{
            this._smtpService.sendHtmlEmail(recipient, subject, htmlBody, null);
        }catch(MessagingException e){
            System.out.println(e.getMessage());
            e.printStackTrace();
        }
        
    }
}
