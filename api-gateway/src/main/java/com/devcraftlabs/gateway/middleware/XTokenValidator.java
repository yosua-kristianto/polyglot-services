package com.devcraftlabs.gateway.middleware;

import java.io.IOException;
import java.nio.charset.StandardCharsets;

import org.springframework.core.Ordered;
import org.springframework.core.annotation.Order;
import org.springframework.stereotype.Component;

import com.devcraftlabs.gateway.common.CachedBodyHttpServletRequest;
import com.devcraftlabs.gateway.common.HmacUtilities;

import jakarta.servlet.Filter;
import jakarta.servlet.FilterChain;
import jakarta.servlet.ServletException;
import jakarta.servlet.ServletRequest;
import jakarta.servlet.ServletResponse;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;

@Component
@Order(Ordered.HIGHEST_PRECEDENCE)
public class XTokenValidator implements Filter {
    private HmacUtilities hmacUtilities;

    public XTokenValidator(HmacUtilities hmacUtilities) {
        this.hmacUtilities = hmacUtilities;
    }

    @Override
    public void doFilter(ServletRequest request, ServletResponse response, FilterChain chain)
            throws IOException, ServletException {
        HttpServletRequest httpRequest = (HttpServletRequest) request;
        HttpServletResponse httpResponse = (HttpServletResponse) response;

        String xToken = httpRequest.getHeader("X-Token");
        if (xToken == null || xToken.isBlank()) {
            httpResponse.sendError(HttpServletResponse.SC_UNAUTHORIZED, "Invalid or missing X-Token");
            return;
        }

        CachedBodyHttpServletRequest cachedRequest = new CachedBodyHttpServletRequest(httpRequest);

        String body = new String(cachedRequest.getCachedBody(), StandardCharsets.UTF_8);

        try {
            String hash = this.hmacUtilities.hmacSha512(body);

            if (!hash.equals(xToken)) {
                httpResponse.sendError(
                        HttpServletResponse.SC_UNAUTHORIZED,
                        "Invalid session"
                );
                return;
            }

            chain.doFilter(cachedRequest, response);
        } catch(Exception ex) {
            httpResponse.sendError(
                    HttpServletResponse.SC_INTERNAL_SERVER_ERROR,
                    "Internal server error"
            );
        }
    }
    
}
