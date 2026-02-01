package com.devcraftlabs.gateway.common;

import java.util.Map;

import org.springframework.core.ParameterizedTypeReference;
import org.springframework.http.HttpEntity;
import org.springframework.http.HttpHeaders;
import org.springframework.http.MediaType;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestTemplate;
import org.springframework.web.reactive.function.client.WebClientRequestException;

@Service
public class TemplateService<T, S> {
    protected final RestTemplate restTemplate;

    public TemplateService(RestTemplate restTemplate) {
        this.restTemplate = restTemplate;
    }

    /**
     * 1. Core request implementation using WebClient
     */
    private <S> S request(
            HttpMethodEnum method,
            String uri,
            T request,
            Object header,
            ParameterizedTypeReference<S> responseType
    ) {

        HttpHeaders httpHeaders = buildHeaders(header);

        try {

            HttpEntity<T> httpEntity = new HttpEntity<>(request, httpHeaders);

            return restTemplate.exchange(uri, resolveMethod(method), httpEntity, responseType).getBody();

        } catch (Exception ex) {
            System.out.println(ex);
            throw new RuntimeException(
                    String.format(
                            "Connection to %s is failed after attempt retries",
                            uri
                    ),
                    ex
            );
        }
    }

    /**
     * 2. Overloaded request with Access Token
     */
    public <S> S request(
            HttpMethodEnum method,
            String uri,
            T request,
            String accessToken,
            ParameterizedTypeReference<S> responseType
    ) {

        HttpHeaders headers = new HttpHeaders();
        headers.setContentType(MediaType.APPLICATION_JSON);
        headers.setAccept(MediaType.parseMediaTypes(MediaType.APPLICATION_JSON_VALUE));
        headers.setBearerAuth(accessToken);

        return this.request(method, uri, request, headers, responseType);
    }

    /**
     * 3. Overloaded request without custom header
     */
    public <S> S request(
            HttpMethodEnum method,
            String uri,
            T request,
            ParameterizedTypeReference<S> responseType
    ) {

        HttpHeaders headers = new HttpHeaders();
        headers.setContentType(MediaType.APPLICATION_JSON);
        headers.setAccept(MediaType.parseMediaTypes(MediaType.APPLICATION_JSON_VALUE));

        return this.request(method, uri, request, headers, responseType);
    }

    /* =========================
       Helper Methods
       ========================= */

    private org.springframework.http.HttpMethod resolveMethod(HttpMethodEnum method) {
        return switch (method) {
            case GET -> org.springframework.http.HttpMethod.GET;
            case POST -> org.springframework.http.HttpMethod.POST;
            case PUT -> org.springframework.http.HttpMethod.PUT;
            case PATCH -> org.springframework.http.HttpMethod.PATCH;
            case DELETE -> org.springframework.http.HttpMethod.DELETE;
        };
    }

    @SuppressWarnings("unchecked")
    private HttpHeaders buildHeaders(Object header) {
        HttpHeaders headers = new HttpHeaders();

        // Mandatory headers
        headers.setContentType(MediaType.APPLICATION_JSON);
        headers.setAccept(MediaType.parseMediaTypes(MediaType.APPLICATION_JSON_VALUE));

        if (header == null) {
            return headers;
        }

        if (header instanceof HttpHeaders httpHeaders) {
            headers.addAll(httpHeaders);
        } else if (header instanceof Map<?, ?> mapHeaders) {
            mapHeaders.forEach((key, value) ->
                    headers.add(String.valueOf(key), String.valueOf(value))
            );
        } else {
            throw new IllegalArgumentException(
                    "Unsupported header type: " + header.getClass().getName()
            );
        }

        return headers;
    }
}
