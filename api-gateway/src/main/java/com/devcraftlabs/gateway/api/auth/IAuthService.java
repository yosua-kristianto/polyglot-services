package com.devcraftlabs.gateway.api.auth;

import com.devcraftlabs.gateway.api.auth.dto.request.AuthorizeOTPRequestDTO;
import com.devcraftlabs.gateway.api.auth.dto.request.LoginRequestDTO;
import com.devcraftlabs.gateway.api.auth.dto.request.RegisterRequestDTO;
import com.devcraftlabs.gateway.api.auth.dto.response.LoginResponseDTO;
import com.devcraftlabs.gateway.common.BaseResponseDTO;

public interface IAuthService {
    /**
     * login
     * 
     * This function act as gateway to POST /api/v1/auth/login
     */
    public BaseResponseDTO<LoginResponseDTO> login(LoginRequestDTO request);

    /**
     * register
     * 
     * This function act as gateway to POST /api/v1/auth/register
     * 
     * @param request
     * @return
     */
    public BaseResponseDTO<Object> register(RegisterRequestDTO request);

    /**
     * verifyEmailRegistration
     * 
     * This function act as gateway to POST /api/v1/auth/verify-email
     * 
     * @param request
     * @return
     */
    public BaseResponseDTO<LoginResponseDTO> verifyEmailRegistration(LoginRequestDTO request);

    /**
     * invokeOTP
     * 
     * This function act as gateway to POST /api/v1/auth/invoke-otp
     * 
     * @param request
     * @return
     */
    public BaseResponseDTO<Object> invokeOTP(AuthorizeOTPRequestDTO request);

}
