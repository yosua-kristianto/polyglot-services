package com.devcraftlabs.gateway.api.integration;

import org.springframework.web.multipart.MultipartFile;

public interface IIntegrationService {
    
    public void uploadFile(MultipartFile file, String fileName);
}
