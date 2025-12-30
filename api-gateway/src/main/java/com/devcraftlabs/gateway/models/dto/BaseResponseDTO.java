package com.devcraftlabs.gateway.models.dto;

import lombok.Data;

@Data
public class BaseResponseDTO<T> {

    private boolean status;
    private String code;
    private String message;
    private T data;
}
