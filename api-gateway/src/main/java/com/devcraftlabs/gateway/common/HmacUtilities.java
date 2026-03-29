package com.devcraftlabs.gateway.common;

import javax.crypto.Mac;
import javax.crypto.spec.SecretKeySpec;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

import java.util.Base64;

@Service
public class HmacUtilities {
    private String secret;

    public HmacUtilities(@Value("${gateway.hmac.secret}") String secret) {
        this.secret = secret;
    }

    public String hmacSha512(String data) {
        try {
            Mac mac = Mac.getInstance("HmacSHA512");
            SecretKeySpec keySpec =
                    new SecretKeySpec(this.secret.getBytes(), "HmacSHA512");

            mac.init(keySpec);
            byte[] rawHmac = mac.doFinal(data.getBytes());

            return Base64.getEncoder().encodeToString(rawHmac);
        } catch (Exception e) {
            throw new RuntimeException("HMAC calculation failed", e);
        }
    }
}
