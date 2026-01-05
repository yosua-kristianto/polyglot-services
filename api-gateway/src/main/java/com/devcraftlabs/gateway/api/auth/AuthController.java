package com.devcraftlabs.gateway.api.auth;

import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import com.devcraftlabs.gateway.models.dto.BaseResponseDTO;
import com.devcraftlabs.gateway.models.dto.request.auth.LoginRequestDTO;
import com.devcraftlabs.gateway.models.dto.request.auth.RegisterRequestDTO;
import com.devcraftlabs.gateway.models.dto.response.auth.LoginResponseDTO;

@RestController
@RequestMapping(value = "/api/v1/auth")
public class AuthController {
    
    private final IAuthService authService;

    public AuthController(IAuthService authService) {
        this.authService = authService;
    }

    @RequestMapping(method = RequestMethod.POST, value = "login")
    public BaseResponseDTO<LoginResponseDTO> login(@RequestBody LoginRequestDTO request) {
        return authService.login(request);
    }

    @RequestMapping(method = RequestMethod.POST, value = "register")
    public BaseResponseDTO<Object> register(@RequestBody RegisterRequestDTO request){
        return authService.register(request);
    }

}
