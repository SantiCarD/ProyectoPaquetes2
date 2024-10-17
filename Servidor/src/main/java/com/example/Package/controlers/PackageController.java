package com.example.Package.controlers;

import com.example.Package.exceptions.*;
import com.example.Package.models.CulturalPackage;
import com.example.Package.models.Guide;
import com.example.Package.services.GuideService;
import com.example.Package.services.IPackageService;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.databind.node.ObjectNode;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import com.fasterxml.jackson.datatype.jsr310.JavaTimeModule;
import java.io.DataInput;
import java.io.IOException;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.List;
import java.util.HashMap;
import java.util.Map;

@RestController
@RequestMapping("/api/packages")
public class PackageController {

    private final IPackageService packageService;
    @Autowired
    private GuideService servicio;
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
    @GetMapping("/get/{filter}")
    public ResponseEntity<List<CulturalPackage>> getAllPackages(@PathVariable String filter) {
        List<CulturalPackage> packages = packageService.listPackages(filter);
        return ResponseEntity.ok(packages); // 200 OK
    }

    @GetMapping("/get/")
    public ResponseEntity<List<CulturalPackage>> getAllPackages() {
        List<CulturalPackage> packages = packageService.getList();
        return ResponseEntity.ok(packages); // 200 OK
    }

    // Obtener paquete por ID
    @GetMapping("/getById/{id}")
    public ResponseEntity<CulturalPackage> getPackageById(@PathVariable int id) {
        CulturalPackage culturalPackage = packageService.searchPackageById(id);
        if (culturalPackage != null) {
            return ResponseEntity.ok(culturalPackage); // 200 OK
        } else {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body(null); // 404 Not Found
        }
    }

    @GetMapping("/getByName/{nombre}")
    public ResponseEntity<CulturalPackage> getPackageByName(@PathVariable String nombre) {
        CulturalPackage culturalPackage = packageService.searchPackageByName(nombre);
        if (culturalPackage != null) {
            return ResponseEntity.ok(culturalPackage); // 200 OK
        } else {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body(null); // 404 Not Found
        }
    }

    // Crear paquete cultural
    @PostMapping("/create")
    public ResponseEntity<?> createPackage(@RequestBody String packageDto) {
        try {
            ObjectMapper mapper = new ObjectMapper();
            JsonNode jsonNode = mapper.readTree(packageDto);
            JsonNode guiasNode = jsonNode.get("guias");
            ArrayList<Guide> guides = new ArrayList<>();
            List<Integer> ints = new ArrayList<>();

            for (int i = 0; i < guiasNode.size(); i++) {
                JsonNode idNode = guiasNode.get(i);

                if (idNode != null && !idNode.isNull()) {
                    int guideId = idNode.asInt();
                    ints.add(guideId);
                    Guide guide = servicio.searchGuideById(guideId);

                    if (guide != null) {
                        guides.add(guide);
                    }

                    else {
                        System.out.println("Guía no encontrada para ID: " + guideId);
                    }
                } else {
                    System.out.println("ID no encontrado o nulo en el nodo guía en la posición: " + i);
                }
            }

            String fechaString = jsonNode.get("fechaInicio").asText();
            DateTimeFormatter formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm:ss");
            LocalDateTime fechaInicio = LocalDateTime.parse(fechaString, formatter);
            String fechaString1 = jsonNode.get("fechaFin").asText();
            DateTimeFormatter formatter2 = DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm:ss");
            LocalDateTime fechaFin = LocalDateTime.parse(fechaString1, formatter2);

            if(packageService.guideExist(ints))
            {
                return ResponseEntity.status(HttpStatus.CONFLICT).body("Ya existe un paquete con un guia de los adicionados"); // 409 Conflict
            }
            else
            {
                CulturalPackage createdPackage = packageService.createPackage(
                        jsonNode.get("nombre").asText(),
                        jsonNode.get("id").asInt(),
                        jsonNode.get("precio").asDouble(),
                        fechaInicio,
                        fechaFin,
                        guides
                );
                return ResponseEntity.status(HttpStatus.CREATED).body(createdPackage); // 201 Created
            }
        } catch (DuplicatedIdException e) {
            return ResponseEntity.status(HttpStatus.CONFLICT).body(e.getMessage()); // 409 Conflict

        } catch (DuplicatedNameException e) {
            return ResponseEntity.status(HttpStatus.CONFLICT).body(e.getMessage()); // 409 Conflict

        } catch (InvalidDateRangeException e) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(e.getMessage()); // 400 Bad Request

        } catch (IllegalArgumentException e) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(e.getMessage()); // 400 Bad Request
        } catch (JsonProcessingException e) {
            throw new RuntimeException(e);
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }

    // Actualizar paquete cultural
    @PutMapping("/put/{id}")
    public ResponseEntity<?> updatePackage(
            @PathVariable int id,
            @RequestBody String packageDto) {
        try {
            ObjectMapper mapper = new ObjectMapper();
            JsonNode jsonNode = mapper.readTree(packageDto);
            JsonNode guiasNode = jsonNode.get("guias");

            List<Integer> newGuideIds = new ArrayList<>();
            ArrayList<Guide> updatedGuides = new ArrayList<>();

            // Recorrer los IDs de los guías proporcionados en el JSON
            for (int i = 0; i < guiasNode.size(); i++) {
                int guideId = guiasNode.get(i).asInt();
                newGuideIds.add(guideId); // Añadir a la lista de IDs

                Guide guide = servicio.searchGuideById(guideId);
                if (guide != null) {
                    updatedGuides.add(guide); // Añadir guía al listado actualizado
                } else {
                    return ResponseEntity.status(HttpStatus.NOT_FOUND)
                            .body("Guía con ID " + guideId + " no encontrada.");
                }
            }

            // Verificar si existe el paquete que queremos actualizar
            CulturalPackage existingPackage = packageService.searchPackageById(id);
            if (existingPackage == null) {
                return ResponseEntity.status(HttpStatus.NOT_FOUND)
                        .body("Paquete no encontrado.");
            }

            // Identificar conflictos con los nuevos guías
            List<Integer> existingGuideIds = existingPackage.getGuias().stream()
                    .map(Guide::getId)
                    .toList();

            // Eliminar de la nueva lista los IDs que ya están en el paquete actual (no deben considerarse conflicto)
            List<Integer> conflictingGuides = newGuideIds.stream()
                    .filter(idGuide -> !existingGuideIds.contains(idGuide)) // Excluir guías ya presentes
                    .toList();

            // Verificar si alguno de los guías restantes está asociado a otro paquete
            if (packageService.guideExist(conflictingGuides)) {
                return ResponseEntity.status(HttpStatus.CONFLICT)
                        .body("Conflicto: Algunos de los guías ya están asignados a otro paquete.");
            }

            // Actualizar las fechas del paquete
            DateTimeFormatter formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm:ss");
            LocalDateTime fechaInicio = LocalDateTime.parse(jsonNode.get("fechaInicio").asText(), formatter);
            LocalDateTime fechaFin = LocalDateTime.parse(jsonNode.get("fechaFin").asText(), formatter);

            // Actualizar el paquete con los nuevos datos
            CulturalPackage updatedPackage = packageService.updatePackage(
                    id,
                    jsonNode.get("nombre").asText(),
                    jsonNode.get("precio").asDouble(),
                    fechaInicio,
                    fechaFin,
                    updatedGuides
            );

            return ResponseEntity.status(HttpStatus.OK).body(updatedPackage); // 200 OK

        }  catch (DuplicatedNameException e) {
            return ResponseEntity.status(HttpStatus.CONFLICT).body(e.getMessage()); // 409 Conflict

        } catch (InvalidDateRangeException e) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(e.getMessage()); // 400 Bad Request

        } catch (IllegalArgumentException e) {
            return ResponseEntity.badRequest().body(e.getMessage()); // 400 Bad Request
        } catch (JsonMappingException e) {
            throw new RuntimeException(e);
        } catch (JsonProcessingException e) {
            throw new RuntimeException(e);
        }
    }

    // Eliminar paquete cultural
    @DeleteMapping("/delete/{id}")
    public ResponseEntity<?> deletePackage(@PathVariable int id) {
        boolean deleted = packageService.deletePackage(id);
        if (deleted) {
            return ResponseEntity.noContent().build(); // 204 No Content
        } else {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body("Paquete no encontrado"); // 404 Not Found
        }
    }
}
