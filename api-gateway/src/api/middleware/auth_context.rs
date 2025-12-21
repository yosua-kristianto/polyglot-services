use axum::{Router, middleware};

let app = Router::new()
    .nest("/auth", auth_routes())
    .layer(middleware::from_fn(signature_middleware));
