package com.devcraftlabs.gateway.api.auth.dto.request;

import com.fasterxml.jackson.annotation.JsonProperty;

import lombok.Data;

@Data
public class LoginRequestDTO {

    private String email;

    private String otp;
    
    @JsonProperty("device_id")
    private String deviceId;
}
