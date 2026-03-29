package com.devcraftlabs.integration.config;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import io.minio.MinioClient;

@Configuration
public class MinioConfig {
    private final String minioClientUri;
    private final String minioAccessKey;
    private final String minioSecretKey;

    public MinioConfig(@Value("${minio.url}") String minioClientUri,
                       @Value("${minio.access-key}") String minioAccessKey,
                       @Value("${minio.secret-key}") String minioSecretKey) {
        this.minioClientUri = minioClientUri;
        this.minioAccessKey = minioAccessKey;
        this.minioSecretKey = minioSecretKey;
    }

    @Bean
    public MinioClient minioClient() {
        return MinioClient.builder()
                .endpoint(this.minioClientUri)
                .credentials(this.minioAccessKey, this.minioSecretKey)
                .build();
    }
}