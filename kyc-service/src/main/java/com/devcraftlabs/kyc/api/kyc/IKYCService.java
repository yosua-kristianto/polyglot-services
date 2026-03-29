package com.devcraftlabs.kyc.api.kyc;

import java.util.UUID;

import com.devcraftlabs.kyc.model.entity.Customer;

public interface IKYCService {

    /**
     * This function expect request multipart file of image. 
     * 
     * @param fileUri -> THe file pre-uploaded to S3 through Integration Service, provide the S3 file URI here.
     * 
     * Flow:
     * 
     * 1. Hit ML Service to identify whether the document is valid KTP file or not (Synchronous)
     * 2. If valid, upload the file to S3 bucket and get the file URL
     * 3. Hit OCR Service through Kafka to extract the data from KTP (Asynchronous)
     * 4. Save an empty KYC application with type KTP and status PENDING_OCR 
     */
    public void submitKYCApplicationOCR(String fileUri);

    /**
     * This function expect request manual data for KYC application. 
     * 
     * @param customer -> From direct request body DTO, convert it to Customer  
     *
     * Flow:
     * 
     * 1. Save KYC application with status PENDING_REVIEW
     */
    public void submitKYCApplication(Customer customer);

    /**
     * Approve KYC application by providing KYC ID. 
     * @param kycId
     */
    public void approveKYCApplication(UUID kycId);

    /**
     * Reject KYC application by providing KYC ID. 
     * @param kycId
     */
    public void rejectKYCApplication(UUID kycId);

    
}
