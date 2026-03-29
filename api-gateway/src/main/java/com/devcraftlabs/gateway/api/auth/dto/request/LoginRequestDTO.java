package com.devcraftlabs.gateway.api.auth.dto.request;

import com.fasterxml.jackson.annotation.JsonProperty;

public record LoginRequestDTO (
    String email,
    String otp,

    @JsonProperty("device_id")
    String deviceId
) {}


