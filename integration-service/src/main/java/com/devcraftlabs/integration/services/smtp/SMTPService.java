package com.devcraftlabs.integration.services.smtp;

import jakarta.mail.MessagingException;
import jakarta.mail.internet.MimeMessage;
import org.springframework.core.io.Resource;
import org.springframework.mail.javamail.JavaMailSender;
import org.springframework.mail.javamail.MimeMessageHelper;
import org.springframework.stereotype.Service;

@Service
public class SMTPService {

    private final JavaMailSender mailSender;

    public SMTPService(JavaMailSender mailSender) {
        this.mailSender = mailSender;
    }

    public void sendHtmlEmail(
            String to,
            String subject,
            String htmlBody,
            Resource attachment
    ) throws MessagingException {

        MimeMessage message = mailSender.createMimeMessage();
        MimeMessageHelper helper =
                new MimeMessageHelper(message, attachment != null, "UTF-8");

        helper.setTo(to);
        helper.setSubject(subject);
        helper.setText(htmlBody, true);

        if (attachment != null) {
            helper.addAttachment(attachment.getFilename(), attachment);
        }

        mailSender.send(message);
    }
}