package com.devcraftlabs.gateway.api.auth;

import com.devcraftlabs.gateway.models.dto.BaseResponseDTO;
import com.devcraftlabs.gateway.models.dto.request.auth.AuthorizeOTPRequestDTO;
import com.devcraftlabs.gateway.models.dto.request.auth.LoginRequestDTO;
import com.devcraftlabs.gateway.models.dto.request.auth.RegisterRequestDTO;
import com.devcraftlabs.gateway.models.dto.response.auth.LoginResponseDTO;

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
