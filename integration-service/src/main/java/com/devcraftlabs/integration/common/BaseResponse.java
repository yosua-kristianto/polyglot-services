package com.devcraftlabs.integration.common;

import lombok.Data;

@Data
public class BaseResponse<T> {

    private boolean status;
    private String code;
    private String message;
    private T data;

    public static <T> BaseResponse<T> error(String code, String message) {
        BaseResponse<T> response = new BaseResponse<>();
        response.setStatus(false);
        response.setCode(code);
        response.setMessage(message);
        return response;
    }

    public static <T> BaseResponse<T> success(String message, T data) {
        BaseResponse<T> response = new BaseResponse<>();
        response.setStatus(true);
        response.setCode("200");
        response.setMessage(message);
        response.setData(data);
        return response;
    }
}
