package com.devcraftlabs.gateway.api.auth.dto.request;

import lombok.Data;

@Data
public class RegisterRequestDTO {

    private String email;
    private String name;
}
