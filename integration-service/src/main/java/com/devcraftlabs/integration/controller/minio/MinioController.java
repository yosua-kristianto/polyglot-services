package com.devcraftlabs.integration.controller.minio;

import java.io.InputStream;

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

import lombok.RequiredArgsConstructor;

@RestController
@RequestMapping("/api/v1/file")
@RequiredArgsConstructor
public class MinioController {
    
    private final MinioService minioService;

    @PostMapping("/upload")
    public BaseResponse upload(@RequestParam("file") MultipartFile file) throws Exception {
        minioService.uploadFile(file);
        return BaseResponse.success("File uploaded successfully", null);
    }

    @GetMapping("/{filename}/{bucket}")
    public ResponseEntity<InputStreamResource> download(@PathVariable String filename, @PathVariable String bucket) throws Exception {
        InputStream inputStream = this.minioService.getFile(filename, bucket);

        return ResponseEntity.ok()
                .contentType(MediaType.APPLICATION_OCTET_STREAM)
                .body(new InputStreamResource(inputStream));
    }

    @DeleteMapping("/{filename}/{bucket}")
    public BaseResponse delete(@PathVariable String filename, @PathVariable String bucket) throws Exception {
        minioService.deleteFile(filename, bucket);
        return BaseResponse.success("Deleted successfully", null);
    }
}
