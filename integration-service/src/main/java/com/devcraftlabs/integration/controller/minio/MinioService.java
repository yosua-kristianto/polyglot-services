package com.devcraftlabs.integration.controller.minio;

import java.io.InputStream;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;
import org.springframework.web.multipart.MultipartFile;

import io.minio.BucketExistsArgs;
import io.minio.GetObjectArgs;
import io.minio.MakeBucketArgs;
import io.minio.MinioClient;
import io.minio.PutObjectArgs;
import io.minio.RemoveObjectArgs;
import lombok.RequiredArgsConstructor;

@Service
@RequiredArgsConstructor
public class MinioService {

    private final MinioClient client;

    @Value("${minio.default-bucket}")
    private final String defaultBucket;

    /**
     * 
     * @param file
     * @param bucket
     * @throws Exception
     */
    public void uploadFile(MultipartFile file, String bucket) throws Exception {

        // Ensure bucket exists
        boolean found = this.client.bucketExists(
                BucketExistsArgs.builder().bucket(bucket).build());

        if (!found) {
            this.client.makeBucket(
                    MakeBucketArgs.builder().bucket(bucket).build());
        }

        this.client.putObject(
                PutObjectArgs.builder()
                        .bucket(bucket)
                        .object(file.getOriginalFilename())
                        .stream(file.getInputStream(), file.getSize(), -1)
                        .contentType(file.getContentType())
                        .build()
        );

    }

    /**
     * Overloading to upload to the default bucket.
     * @param file
     * @throws Exception
     */
    public void uploadFile(MultipartFile file) throws Exception {
        this.uploadFile(file, this.defaultBucket);
    }



    public InputStream getFile(String filename, String bucket) throws Exception {
        return this.client.getObject(
                GetObjectArgs.builder()
                        .bucket(bucket)
                        .object(filename)
                        .build()
        );
    }

    public void deleteFile(String filename, String bucket) throws Exception {
        this.client.removeObject(
                RemoveObjectArgs.builder()
                        .bucket(bucket)
                        .object(filename)
                        .build()
        );
    }
}
