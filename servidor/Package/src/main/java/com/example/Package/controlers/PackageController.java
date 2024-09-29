package com.example.Package.controlers;

import com.example.Package.exceptions.*;
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

    // Health check
    @GetMapping("/health")
    public ResponseEntity<Map<String, String>> healthCheck() {
        Map<String, String> status = new HashMap<>();
        status.put("status", "UP");
        status.put("message", "El servidor de paquetes culturales está funcionando correctamente");
        return ResponseEntity.ok(status); // 200 OK
    }

    // Obtener todos los paquetes
    @GetMapping
    public ResponseEntity<List<CulturalPackage>> getAllPackages() {
        List<CulturalPackage> packages = packageService.getList();
        return ResponseEntity.ok(packages); // 200 OK
    }

    // Obtener paquete por ID
    @GetMapping("/I/{id}")
    public ResponseEntity<CulturalPackage> getPackageById(@PathVariable int id) {
        CulturalPackage culturalPackage = packageService.searchPackageById(id);
        if (culturalPackage != null) {
            return ResponseEntity.ok(culturalPackage); // 200 OK
        } else {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body(null); // 404 Not Found
        }
    }

    @GetMapping("/N/{nombre}")
    public ResponseEntity<CulturalPackage> getPackageByName(@PathVariable String nombre) {
        CulturalPackage culturalPackage = packageService.searchPackageByName(nombre);
        if (culturalPackage != null) {
            return ResponseEntity.ok(culturalPackage); // 200 OK
        } else {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body(null); // 404 Not Found
        }
    }

    // Crear paquete cultural
    @PostMapping("")
    public ResponseEntity<?> createPackage(@RequestBody CulturalPackage packageDto) {
        try {
            CulturalPackage createdPackage = packageService.createPackage(
                    packageDto.getNombre(),
                    packageDto.getId(),
                    packageDto.getPrecio(),
                    packageDto.getFechaInicio(),
                    packageDto.getFechaFin()
            );
            return ResponseEntity.status(HttpStatus.CREATED).body(createdPackage); // 201 Created

        } catch (DuplicatedIdException e) {
            return ResponseEntity.status(HttpStatus.CONFLICT).body(e.getMessage()); // 409 Conflict

        } catch (DuplicatedNameException e) {
            return ResponseEntity.status(HttpStatus.CONFLICT).body(e.getMessage()); // 409 Conflict

        } catch (InvalidDateRangeException e) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(e.getMessage()); // 400 Bad Request

        } catch (IllegalArgumentException e) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(e.getMessage()); // 400 Bad Request
        }
    }

    // Actualizar paquete cultural
    @PutMapping("/P/{id}")
    public ResponseEntity<?> updatePackage(
            @PathVariable int id,
            @RequestBody CulturalPackage packageDto) {
        try {
            CulturalPackage updatedPackage = packageService.updatePackage(
                    id,
                    packageDto.getNombre(),
                    packageDto.getPrecio(),
                    packageDto.getFechaInicio(),
                    packageDto.getFechaFin()
            );
            if (updatedPackage != null) {
                return ResponseEntity.ok(updatedPackage); // 200 OK
            } else {
                return ResponseEntity.status(HttpStatus.NOT_FOUND).body("Paquete no encontrado"); // 404 Not Found
            }

        } catch (DuplicatedNameException e) {
            return ResponseEntity.status(HttpStatus.CONFLICT).body(e.getMessage()); // 409 Conflict

        } catch (InvalidDateRangeException e) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(e.getMessage()); // 400 Bad Request

        } catch (IllegalArgumentException e) {
            return ResponseEntity.badRequest().body(e.getMessage()); // 400 Bad Request
        }
    }

    // Eliminar paquete cultural
    @DeleteMapping("/{id}")
    public ResponseEntity<?> deletePackage(@PathVariable int id) {
        boolean deleted = packageService.deletePackage(id);
        if (deleted) {
            return ResponseEntity.noContent().build(); // 204 No Content
        } else {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body("Paquete no encontrado"); // 404 Not Found
        }
    }
}
