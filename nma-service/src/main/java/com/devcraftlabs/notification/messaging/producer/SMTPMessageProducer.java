package com.devcraftlabs.notification.messaging.producer;

import org.springframework.kafka.core.KafkaTemplate;
import org.springframework.stereotype.Service;

import com.devcraftlabs.notification.model.dto.producer.SmtpMessageRequestDTO;

@Service
public class SMTPMessageProducer {
    private static final String TOPIC = "integration-service-01-smtp";

    private final KafkaTemplate<String, SmtpMessageRequestDTO> kafkaTemplate;

    public SMTPMessageProducer(KafkaTemplate<String, SmtpMessageRequestDTO> kafkaTemplate){
        this.kafkaTemplate = kafkaTemplate;
    }

    public void produce(SmtpMessageRequestDTO message){
        this.kafkaTemplate.send(TOPIC, message);
    }
}
