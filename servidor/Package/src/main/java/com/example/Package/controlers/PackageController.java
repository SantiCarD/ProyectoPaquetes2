package com.example.Package.controllers;

import com.example.Package.models.CulturalPackage;
import com.example.Package.services.IPackageService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import java.util.List;
import java.util.HashMap;
import java.util.Map;


@RestController
@RequestMapping("/api/packages")
public class PackageController {

    private final IPackageService packageService;

    @Autowired
    public PackageController(IPackageService packageService) {
        this.packageService = packageService;
    }

    @GetMapping("/health")
    public ResponseEntity<Map<String, String>> healthCheck() {
        Map<String, String> status = new HashMap<>();
        status.put("status", "UP");
        status.put("message", "El servidor de paquetes culturales está funcionando correctamente");
        return ResponseEntity.ok(status);
    }

    @GetMapping
    public ResponseEntity<List<CulturalPackage>> getAllPackages() {
        List<CulturalPackage> packages = packageService.listPackages();
        return ResponseEntity.ok(packages);
    }

    @GetMapping("/{id}")
    public ResponseEntity<CulturalPackage> getPackageById(@PathVariable int id) {
        CulturalPackage culturalPackage = packageService.searchPackage("Id", String.valueOf(id));
        if (culturalPackage != null) {
            return ResponseEntity.ok(culturalPackage);
        } else {
            return ResponseEntity.notFound().build();
        }
    }

    @PostMapping
    public ResponseEntity<?> createPackage(@RequestBody CulturalPackage packageDto) {
        try {
            CulturalPackage createdPackage = packageService.createPackage(
                    packageDto.getNombre(),
                    packageDto.getId(),
                    packageDto.getPrecio(),
                    packageDto.getFechaInicio(),
                    packageDto.getFechaFin()
            );
            return ResponseEntity.status(HttpStatus.CREATED).body(createdPackage);
        } catch (IllegalArgumentException e) {
            return ResponseEntity.badRequest().body(e.getMessage());
        }
    }

    @PutMapping("/{id}")
    public ResponseEntity<CulturalPackage> updatePackage(
            @PathVariable int id,
            @RequestBody CulturalPackage packageDto) {
        CulturalPackage updatedPackage = packageService.updatePackage(
                id,
                packageDto.getNombre(),
                packageDto.getPrecio(),
                packageDto.getFechaInicio(),
                packageDto.getFechaFin()
        );
        if (updatedPackage != null) {
            return ResponseEntity.ok(updatedPackage);
        } else {
            return ResponseEntity.notFound().build();
        }
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deletePackage(@PathVariable int id) {
        boolean deleted = packageService.deletePackage(id);
        if (deleted) {
            return ResponseEntity.noContent().build();
        } else {
            return ResponseEntity.notFound().build();
        }
    }
}