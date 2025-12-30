package com.devcraftlabs.gateway.common;

import java.time.Duration;
import java.util.Map;

import org.springframework.http.HttpHeaders;
import org.springframework.http.MediaType;
import org.springframework.web.reactive.function.client.WebClient;
import org.springframework.web.reactive.function.client.WebClientRequestException;

import reactor.util.retry.Retry;


public abstract class TemplateService<T, S> {
    protected final WebClient webClient;

    protected TemplateService(WebClient webClient) {
        this.webClient = webClient;
    }

    /**
     * 1. Core request implementation using WebClient
     */
    private S request(
            HttpMethodEnum method,
            String uri,
            T request,
            Object header
    ) {

        HttpHeaders httpHeaders = buildHeaders(header);

        try {
            return webClient
                    .method(resolveMethod(method))
                    .uri(uri)
                    .headers(h -> h.addAll(httpHeaders))
                    .bodyValue(request)
                    .retrieve()
                    .bodyToMono(getResponseClass())
                    .retryWhen(
                            Retry.fixedDelay(4, Duration.ofSeconds(5))
                                    .filter(this::isRetryableException)
                    )
                    .block();

        } catch (Exception ex) {
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
    protected S request(
            HttpMethodEnum method,
            String uri,
            T request,
            String accessToken
    ) {

        HttpHeaders headers = new HttpHeaders();
        headers.setContentType(MediaType.APPLICATION_JSON);
        headers.setAccept(MediaType.parseMediaTypes(MediaType.APPLICATION_JSON_VALUE));
        headers.setBearerAuth(accessToken);

        return request(method, uri, request, headers);
    }

    /**
     * 3. Overloaded request without custom header
     */
    protected S request(
            HttpMethodEnum method,
            String uri,
            T request
    ) {

        HttpHeaders headers = new HttpHeaders();
        headers.setContentType(MediaType.APPLICATION_JSON);
        headers.setAccept(MediaType.parseMediaTypes(MediaType.APPLICATION_JSON_VALUE));

        return request(method, uri, request, headers);
    }

    /* =========================
       Helper Methods
       ========================= */

    private org.springframework.http.HttpMethod resolveMethod(HttpMethodEnum method) {
        return switch (method) {
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

    private boolean isRetryableException(Throwable throwable) {
        return throwable instanceof WebClientRequestException;
    }

    /**
     * Each concrete Facade must define its response DTO class
     */
    protected abstract Class<S> getResponseClass();
}
