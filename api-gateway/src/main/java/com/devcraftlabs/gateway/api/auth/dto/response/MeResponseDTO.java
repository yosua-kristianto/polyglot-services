package com.devcraftlabs.gateway.api.auth.dto.response;

import com.fasterxml.jackson.annotation.JsonProperty;
import java.time.OffsetDateTime;
import java.util.List;
import java.util.UUID;

public record MeResponseDTO (
    UUID id,
    String email,
    String name,
    int status,

    @JsonProperty("created_at")
    OffsetDateTime createdAt,

    @JsonProperty("updated_at")
    OffsetDateTime updatedAt,

    @JsonProperty("deleted_at")
    OffsetDateTime deletedAt,

    List<Object> devices
) {}