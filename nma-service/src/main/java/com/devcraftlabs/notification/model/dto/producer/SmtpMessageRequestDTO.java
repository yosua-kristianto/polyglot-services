package com.devcraftlabs.notification.model.dto.producer;

import com.fasterxml.jackson.annotation.JsonProperty;

import lombok.Data;

@Data
public class SmtpMessageRequestDTO {
    private String recipient;
    private String subject;

    @JsonProperty("html_body")
    private String htmlBody;
}

