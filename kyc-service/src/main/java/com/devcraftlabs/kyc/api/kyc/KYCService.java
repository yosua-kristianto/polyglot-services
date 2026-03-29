package com.devcraftlabs.kyc.api.kyc;

import java.util.UUID;

import org.springframework.stereotype.Service;

import com.devcraftlabs.kyc.model.entity.Customer;

@Service
public class KYCService implements IKYCService {

    @Override
    public void submitKYCApplicationOCR(String fileUri) {
        // TODO Auto-generated method stub
        throw new UnsupportedOperationException("Unimplemented method 'submitKYCApplicationOCR'");
    }

    @Override
    public void submitKYCApplication(Customer customer) {
        // TODO Auto-generated method stub
        throw new UnsupportedOperationException("Unimplemented method 'submitKYCApplication'");
    }

    @Override
    public void approveKYCApplication(UUID kycId) {
        // TODO Auto-generated method stub
        throw new UnsupportedOperationException("Unimplemented method 'approveKYCApplication'");
    }

    @Override
    public void rejectKYCApplication(UUID kycId) {
        // TODO Auto-generated method stub
        throw new UnsupportedOperationException("Unimplemented method 'rejectKYCApplication'");
    }
    
}
