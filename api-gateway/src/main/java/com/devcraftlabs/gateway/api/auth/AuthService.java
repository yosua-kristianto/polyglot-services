package com.devcraftlabs.gateway.api.auth;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.core.ParameterizedTypeReference;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestTemplate;

import com.devcraftlabs.gateway.common.HttpMethodEnum;
import com.devcraftlabs.gateway.common.TemplateService;
import com.devcraftlabs.gateway.models.dto.BaseResponseDTO;
import com.devcraftlabs.gateway.models.dto.request.auth.AuthorizeOTPRequestDTO;
import com.devcraftlabs.gateway.models.dto.request.auth.LoginRequestDTO;
import com.devcraftlabs.gateway.models.dto.request.auth.RegisterRequestDTO;
import com.devcraftlabs.gateway.models.dto.response.auth.LoginResponseDTO;

@Service
public class AuthService implements IAuthService {
    private String baseUri;

    private RestTemplate restTemplate;

    public AuthService(
        RestTemplate restTemplate,
        @Value("${gateway.uri.auth-service}") String authUri
    ) {
        this.restTemplate = restTemplate;
        baseUri = authUri + "/v1/auth/";
    }

    @Override
    public BaseResponseDTO<LoginResponseDTO> login(LoginRequestDTO request) {
        TemplateService<LoginRequestDTO, BaseResponseDTO<LoginResponseDTO>> restTemplate = new TemplateService<LoginRequestDTO, BaseResponseDTO<LoginResponseDTO>>(this.restTemplate);
        BaseResponseDTO<LoginResponseDTO> response = restTemplate.request(
            HttpMethodEnum.POST, 
            this.baseUri + "login", 
            request,
            new ParameterizedTypeReference<BaseResponseDTO<LoginResponseDTO>>() {}
        );

        return response;
    }

    @Override
    public BaseResponseDTO<Object> register(RegisterRequestDTO request) {
        TemplateService<RegisterRequestDTO, BaseResponseDTO<Object>> restTemplate = new TemplateService<RegisterRequestDTO, BaseResponseDTO<Object>>(this.restTemplate);

        return restTemplate.request(
            HttpMethodEnum.POST, 
            this.baseUri + "register", 
            request, 
            new ParameterizedTypeReference<BaseResponseDTO<Object>>() {}
        );
    }

    @Override
    public BaseResponseDTO<LoginResponseDTO> verifyEmailRegistration(LoginRequestDTO request) {
        TemplateService<LoginRequestDTO, BaseResponseDTO<LoginResponseDTO>> restTemplate = new TemplateService<LoginRequestDTO, BaseResponseDTO<LoginResponseDTO>>(this.restTemplate);

        return restTemplate.request(
            HttpMethodEnum.POST, 
            this.baseUri + "verify-email", 
            request,
            new ParameterizedTypeReference<BaseResponseDTO<LoginResponseDTO>>() {}
        );
    }

    @Override
    public BaseResponseDTO<Object> invokeOTP(AuthorizeOTPRequestDTO request) {
        TemplateService<AuthorizeOTPRequestDTO, BaseResponseDTO<Object>> restTemplate = new TemplateService<AuthorizeOTPRequestDTO, BaseResponseDTO<Object>>(this.restTemplate);
        return restTemplate.request(
            HttpMethodEnum.POST, 
            this.baseUri + "invoke-otp", 
            request,
            new ParameterizedTypeReference<BaseResponseDTO<Object>>() {}
        );
    }

}
