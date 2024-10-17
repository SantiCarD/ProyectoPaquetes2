package com.example.Package.controlers;

import com.example.Package.exceptions.DuplicatedIdException;
import com.example.Package.exceptions.DuplicatedNameException;
import com.example.Package.exceptions.InvalidDateRangeException;
import com.example.Package.models.CulturalPackage;
import com.example.Package.models.Guide;
import com.example.Package.services.GuideService;
import com.example.Package.services.IGuideService;
import com.example.Package.services.PackageService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

@RestController
@RequestMapping("/api/guides")
public class GuideController {
    private final IGuideService guideService;
    @Autowired
    private PackageService servicio;
    @Autowired
    public GuideController(IGuideService guideService) {
        this.guideService = guideService;
    }

    @GetMapping("/health")
    public ResponseEntity<Map<String, String>> healthCheck() {
        Map<String, String> status = new HashMap<>();
        status.put("status", "UP");
        status.put("message", "El servidor de guias está funcionando correctamente");
        return ResponseEntity.ok(status); // 200 OK
    }

    // Obtener todos los paquetes
    @GetMapping("/get/{filter}")
    public ResponseEntity<List<Guide>> getAllPackages(@PathVariable String filter) {
        List<Guide> packages = guideService.listGuides(filter);
        return ResponseEntity.ok(packages); // 200 OK
    }

    @GetMapping("/get/")
    public ResponseEntity<List<Guide>> getAllPackages() {
        List<Guide> packages = guideService.getList();
        return ResponseEntity.ok(packages); // 200 OK
    }
    // Obtener paquete por ID
    @GetMapping("/getById/{id}")
    public ResponseEntity<Guide> getPackageById(@PathVariable int id) {
        Guide guide = guideService.searchGuideById(id);
        if (guide != null) {
            return ResponseEntity.ok(guide); // 200 OK
        } else {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body(null); // 404 Not Found
        }
    }

    @GetMapping("/getByName/{nombre}")
    public ResponseEntity<Guide> getPackageByName(@PathVariable String nombre) {
        Guide guide = guideService.searchGuideByName(nombre);
        if (guide != null) {
            return ResponseEntity.ok(guide); // 200 OK
        } else {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body(null); // 404 Not Found
        }
    }

    // Crear paquete cultural
    @PostMapping("/create")
    public ResponseEntity<?> createPackage(@RequestBody Guide guidee) {
        try {
            Guide guide = guideService.createGuide(
                    guidee.getId(),
                    guidee.getNombre(),
                    guidee.getCalificacion(),
                    guidee.getEdad(),
                    guidee.getFechaNacimiento()
            );
            return ResponseEntity.status(HttpStatus.CREATED).body(guide); // 201 Created

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
    @PutMapping("/put/{id}")
    public ResponseEntity<?> updatePackage(
            @PathVariable int id,
            @RequestBody Guide guide) {
        try {
            Guide guide1 = guideService.updateGuide(
                    id,
                    guide.getNombre(),
                    guide.getCalificacion(),
                    guide.getEdad(),
                    guide.getFechaNacimiento()
            );
            if (guide1 != null) {
                return ResponseEntity.ok(guide1); // 200 OK
            } else {
                return ResponseEntity.status(HttpStatus.NOT_FOUND).body("Guia no encontrado"); // 404 Not Found
            }

        } catch (DuplicatedNameException e) {
            return ResponseEntity.status(HttpStatus.CONFLICT).body(e.getMessage()); // 409 Conflict

        } catch (InvalidDateRangeException e) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(e.getMessage()); // 400 Bad Request

        } catch (IllegalArgumentException e) {
            return ResponseEntity.badRequest().body(e.getMessage()); // 400 Bad Request
        }
    }

    @DeleteMapping("/delete/{id}")
    public ResponseEntity<?> deletePackage(@PathVariable int id) {
        Guide guia = guideService.searchGuideById(id);
        boolean deleted = guideService.deleteGuide(id);
        if (deleted) {
            for(CulturalPackage cp : servicio.getList())
                {
                    if(cp.guideExist(id))
                    {
                        ArrayList<Guide> guiaspc = cp.getGuias();
                        guiaspc.remove(guia);
                        System.out.println(guiaspc.toString());
                        cp.setGuias(guiaspc);
                    }
                }
            return ResponseEntity.noContent().build(); // 204 No Content
        } else {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body("Guia no encontrado"); // 404 Not Found
        }
    }

}
