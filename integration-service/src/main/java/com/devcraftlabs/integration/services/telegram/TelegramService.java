package com.devcraftlabs.integration.services.telegram;


public class TelegramService implements ITelegramService {

    private static final int MAX_MESSAGE_LENGTH = 2000;

    private final WebClient webClient;
    private final String telegramUri;
    private final String chatId;

    public TelegramService(
        @Value("${integration.telegram.uri}") String telegramUri,
        WebClient.Builder webClientBuilder, 
        @Value("${integration.telegram.chatid}") String chatId
    ) {
        this.webClient = webClientBuilder.baseUrl(telegramUri).build();
        this.telegramUri = telegramUri;
        this.chatId = chatId;
    }

    @Override
    public void sendMessage(String message) {
        if (message == null || message.isBlank()) {
            return; 
        }

        // 1️⃣ Trim message to max 2000 characters
        String trimmedMessage = message.length() > MAX_MESSAGE_LENGTH
                ? message.substring(0, MAX_MESSAGE_LENGTH)
                : message;

        // 2️⃣ Prepare request payload
        Map<String, Object> payload = new HashMap<>();
        payload.put("chat_id", this.chatId);
        payload.put("text", this.trimmedMessage);

        // 3️⃣ Send message to Telegram API
        webClient.post()
                .uri(telegramUri)
                .bodyValue(payload)
                .retrieve()
                .bodyToMono(String.class)
                .doOnError(error ->
                    System.err.println("Failed to send Telegram message: " + error.getMessage())
                )
                .subscribe(); 
    }

}
