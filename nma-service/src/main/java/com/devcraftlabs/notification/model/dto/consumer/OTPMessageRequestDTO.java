package com.devcraftlabs.notification.model.dto.consumer;


import java.util.UUID;

import com.fasterxml.jackson.annotation.JsonProperty;

import lombok.Data;

@Data
public class OTPMessageRequestDTO {
    @JsonProperty("user_id")
    UUID userId;

    @JsonProperty("recipient_name")
    String recipientName;
    String otp;
    String email;
}
