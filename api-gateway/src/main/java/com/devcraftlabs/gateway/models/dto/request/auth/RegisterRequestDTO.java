package com.devcraftlabs.gateway.models.dto.request.auth;

import lombok.Data;

@Data
public class RegisterRequestDTO {

    private String email;
    private String name;
}
