use axum::{
    body::Bytes,
    http::{Request, StatusCode},
    middleware::Next,
    response::Response,
};
use sha2::{Sha512, Digest};
use hex::encode;
use chrono::{Utc, Duration};

pub async fn signature_middleware<B>(
    mut req: Request<B>,
    next: Next<B>,
) -> Result<Response, StatusCode>
where
    B: Send,
{
    let signature = req.headers()
        .get("X-SIGNATURE")
        .and_then(|v| v.to_str().ok())
        .ok_or(StatusCode::UNAUTHORIZED)?;

    let timestamp = req.headers()
        .get("X-TIMESTAMP")
        .and_then(|v| v.to_str().ok())
        .ok_or(StatusCode::UNAUTHORIZED)?;

    let body_bytes = axum::body::to_bytes(req.body_mut(), usize::MAX)
        .await
        .map_err(|_| StatusCode::BAD_REQUEST)?;

    let parts: Vec<&str> = signature.split(':').collect();
    if parts.len() != 2 {
        return Err(StatusCode::UNAUTHORIZED);
    }

    let payload_hash = sha512_hex(&body_bytes);
    let timestamp_hash = sha512_hex(timestamp.as_bytes());

    if parts[0] != payload_hash || parts[1] != timestamp_hash {
        return Err(StatusCode::UNAUTHORIZED);
    }

    // Replay protection (±30s)
    let ts: i64 = timestamp.parse().map_err(|_| StatusCode::UNAUTHORIZED)?;
    let now = Utc::now().timestamp();

    if (now - ts).abs() > 30 {
        return Err(StatusCode::UNAUTHORIZED);
    }

    // Restore body
    req.extensions_mut().insert(body_bytes.clone());
    let req = req.map(|_| body_bytes);

    Ok(next.run(req).await)
}

fn sha512_hex(data: &[u8]) -> String {
    let mut hasher = Sha512::new();
    hasher.update(data);
    encode(hasher.finalize())
}
