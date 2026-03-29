package com.devcraftlabs.gateway.api.kyc;

/**
 * IKYC Service is an interface that defines the contract for
 * KYC business logic. Mostly used to handling request from Orchestrator.
 */
public interface IKYCService {
    
    /**
     * 
     * Extract KTP information. Orchestrator flow will start by sending KTP image link through Kafka
     * 
     * API GW -> (
     *  1. Kafka Produce to ML Service (Utilizing its OCR) (topic: ml-service-ocr)
     *  2. ML Service send Extracted data to KYC Service through Kafka (topic: kyc-service-ktp-extraction) 
     *  3. KYC Service save the extracted data to Memcache.
     * ) 
     * 
     * @param fileName Minio link.
     * @return
     */
    public void extractKTP(String fileName);

    /**
     * Get extracted KTP information from KYC Service. 
     * Getting data from Memcache, which is sent by ML Service through Kafka. 
     * 
     * If this method returning null, then the KYC Service is still waiting for ML Service to send the extracted data through Kafka.
     * @param userId
     * @return
     */
    public Object getExtractedKTP(String userId);
    
}
