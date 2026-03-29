package com.devcraftlabs.integration.controller.minio;

import java.io.InputStream;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.core.io.InputStreamResource;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.multipart.MultipartFile;

import com.devcraftlabs.integration.common.BaseResponse;
import com.devcraftlabs.integration.controller.minio.dto.UploadFileResponseDTO;

import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;

@RestController
@RequestMapping("/api/v1/file")
public class MinioController {
    
    private final MinioService _minioService;
    private final String _minioClientUri;
    private final String _minioDefaultBucket;

    public MinioController(
        MinioService minioService,
        @Value("${minio.url}") String minioClientUri,
        @Value("${minio.bucket}") String minioDefaultBucket
    ){
        this._minioService = minioService;
        this._minioClientUri = minioClientUri;
        this._minioDefaultBucket = minioDefaultBucket;
    }    

    @PostMapping("/upload")
    public BaseResponse<UploadFileResponseDTO> upload(
        @RequestParam("file") MultipartFile file,
        @RequestParam("file_name") String fileName
    ) throws Exception {
        _minioService.uploadFile(file, fileName);
        
        final UploadFileResponseDTO response = new UploadFileResponseDTO(
            String.format("%s/%s/%s", this._minioClientUri, this._minioDefaultBucket, fileName)
        );

        return BaseResponse.success("File uploaded successfully", response);
    }

    @GetMapping("/{bucket}/{filename}")
    public ResponseEntity<InputStreamResource> download(@PathVariable String filename, @PathVariable String bucket) throws Exception {
        InputStream inputStream = this._minioService.getFile(filename, bucket);

        return ResponseEntity.ok()
                .contentType(MediaType.APPLICATION_OCTET_STREAM)
                .body(new InputStreamResource(inputStream));
    }

    @DeleteMapping("/{bucket}/{filename}")
    public BaseResponse delete(@PathVariable String filename, @PathVariable String bucket) throws Exception {
        _minioService.deleteFile(filename, bucket);
        return BaseResponse.success("Deleted successfully", null);
    }
}
