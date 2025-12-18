package com.devcraftlabs.notification.handler;

import java.util.UUID;

public interface IPersonalNotificationHandler {
    /**
     * handleAuthenticationOTP
     * 
     * This function handle the process of registering authentication OTP to sending the OTP through integration service.
     * 
     * Algo:
     * 1. Save personal notification data to database
     * 2. Generate Thymyleaf template through resources/templates/otp-mail.html
     * 3. Produce message to Integration Service.
     * 
     * @param userId
     * @param recipient
     * @param otp
     * @param email
     */
    public void handleAuthenticationOTP(UUID userId, String recipient, String otp, String email);
}
