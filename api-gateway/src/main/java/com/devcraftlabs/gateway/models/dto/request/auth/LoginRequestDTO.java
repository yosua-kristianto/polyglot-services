package com.devcraftlabs.gateway.models.dto.request.auth;

import com.fasterxml.jackson.annotation.JsonProperty;

import lombok.Data;

@Data
public class LoginRequestDTO {

    private String email;

    private String otp;
    
    @JsonProperty("device_id")
    private String deviceId;
}
