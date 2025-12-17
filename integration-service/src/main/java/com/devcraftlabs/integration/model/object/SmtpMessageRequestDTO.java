package com.devcraftlabs.integration.model.object;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

import lombok.Data;

@Data
@JsonIgnoreProperties(ignoreUnknown = true)
public class SmtpMessageRequestDTO {
    private String recipient;
    private String subject;
    private String htmlBody;
}
