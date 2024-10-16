package com.example.Package.services;

import com.example.Package.models.CulturalPackage;
import com.example.Package.models.Guide;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import com.example.Package.exceptions.*;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

@Service
public class PackageService implements IPackageService {
    private static PackageService packageService;
    private List<CulturalPackage> PackagesList = new ArrayList<CulturalPackage>();
    @Autowired
    private PackageService() {

    }
    @Autowired
    public static PackageService getPackageService() {
        if (packageService == null) {
            packageService = new PackageService();
        }

        return packageService;
    }

    public boolean guideExist(List<Integer> guideIds) {
        for (CulturalPackage cu : PackagesList) {
            for (Guide g : cu.getGuias()) {
                if (guideIds.contains(g.getId())) {
                    return true; // Ya existe una guía en uso por otro paquete
                }
            }
        }
        return false; // Ninguna guía está en otro paquete
    }


    public List<CulturalPackage> getList() {
        return PackagesList;
    }



    ///CRUD//////CRUD//////CRUD//////CRUD//////CRUD//////CRUD//////CRUD//////CRUD//////CRUD//////CRUD//////CRUD//////CRUD///
    // Buscar paquete cultural por ID
    public CulturalPackage searchPackageById(int id) {
        for (CulturalPackage culturalPackage : getList()) {
            if (culturalPackage.getId() == id) {
                return culturalPackage; // Retorna el paquete si se encuentra
            }
        }
        return null; // Retorna null si no se encuentra
    }

    // Buscar paquete cultural por nombre
    public CulturalPackage searchPackageByName(String nombre) {
        for (CulturalPackage culturalPackage : getList()) {
            if (culturalPackage.getNombre().equalsIgnoreCase(nombre)) {
                return culturalPackage; // Retorna el paquete si se encuentra
            }
        }
        return null; // Retorna null si no se encuentra
    }



    public CulturalPackage createPackage(String nombre, int id, Double precio, LocalDateTime fechaInicio, LocalDateTime fechaFin, ArrayList<Guide> guias) throws DuplicatedIdException, DuplicatedNameException, InvalidDateRangeException {
        // Verificar si ya existe un paquete con el mismo ID
        if (searchPackageById(id) != null) {
            throw new DuplicatedIdException("Ya existe un paquete con este ID.");
        }

        // Verificar si ya existe un paquete con el mismo nombre
        if (searchPackageByName(nombre) != null) {
            throw new DuplicatedNameException("Ya existe un paquete con este nombre.");
        }

        // El resto de las validaciones se mantienen igual
        if (nombre == null || nombre.trim().isEmpty()) {
            throw new IllegalArgumentException("El nombre del paquete no puede estar vacío.");
        }
        if (id <= 0) {
            throw new IllegalArgumentException("El ID debe ser un número positivo.");
        }
        if (precio == null || precio <= 0) {
            throw new IllegalArgumentException("El precio debe ser mayor que 0.");
        }
        if (fechaInicio == null || fechaFin == null) {
            throw new IllegalArgumentException("Las fechas de inicio y fin no pueden ser nulas.");
        }
        if (fechaInicio.isAfter(fechaFin)) {
            throw new InvalidDateRangeException("La fecha de inicio debe ser anterior a la fecha de fin.");
        }

        CulturalPackage paquete = new CulturalPackage(nombre, id, precio, fechaInicio, fechaFin);
        paquete.setGuias(guias);
        PackagesList.add(paquete);
        return paquete;
    }



    public CulturalPackage updatePackage(int id, String nuevoNombre, Double nuevoPrecio, LocalDateTime nuevaFechaInicio, LocalDateTime nuevaFechaFin, ArrayList<Guide> guias) throws PackageNotFoundException, InvalidDateRangeException {
        // Buscar el paquete existente por ID
        CulturalPackage paqueteExistente = searchPackageById(id);

        // Verificar si el paquete existe
        if (paqueteExistente == null) {
            throw new PackageNotFoundException("No se encontró un paquete con el ID proporcionado.");
        }

        // Validar que el nuevo nombre no esté vacío
        if (nuevoNombre == null || nuevoNombre.trim().isEmpty()) {
            throw new IllegalArgumentException("El nuevo nombre no puede estar vacío.");
        }

        // Validar que el nuevo precio sea mayor que 0
        if (nuevoPrecio == null || nuevoPrecio <= 0) {
            throw new IllegalArgumentException("El nuevo precio debe ser mayor que 0.");
        }

        // Validar que las nuevas fechas no sean nulas
        if (nuevaFechaInicio == null || nuevaFechaFin == null) {
            throw new IllegalArgumentException("Las nuevas fechas de inicio y fin no pueden ser nulas.");
        }

        // Validar que la nueva fecha de inicio sea anterior a la fecha de fin
        if (nuevaFechaInicio.isAfter(nuevaFechaFin)) {
            throw new InvalidDateRangeException("La nueva fecha de inicio debe ser anterior a la fecha de fin.");
        }

        // Actualizar los atributos del paquete
        paqueteExistente.setNombre(nuevoNombre);
        paqueteExistente.setPrecio(nuevoPrecio);
        paqueteExistente.setFechaInicio(nuevaFechaInicio);
        paqueteExistente.setFechaFin(nuevaFechaFin);
        paqueteExistente.setGuias(guias);

        return paqueteExistente; // Retorna el paquete actualizado
    }



    public boolean deletePackage(int id) throws PackageNotFoundException {
        // Buscar el paquete existente por ID
        CulturalPackage paqueteExistente = searchPackageById(id);

        // Verificar si el paquete existe
        if (paqueteExistente == null) {
            throw new PackageNotFoundException("No se encontró un paquete con el ID proporcionado.");
        }

        // Eliminar el paquete de la lista
        boolean eliminado = PackagesList.remove(paqueteExistente); // Intenta eliminar el paquete

        // Retornar true si se eliminó con éxito, false de lo contrario
        return eliminado;
    }






    ///CRUD//////CRUD//////CRUD//////CRUD//////CRUD//////CRUD//////CRUD//////CRUD//////CRUD//////CRUD//////CRUD//////CRUD///

    public List<CulturalPackage> listPackages(String filter) throws PackageNotFoundException {
        List<CulturalPackage> filtrados = new ArrayList<>();
        if (PackagesList.isEmpty()) {
            throw new PackageNotFoundException("No hay paquetes disponibles.");
        }

        if(filter.isEmpty() || filter.isBlank())
        {
            filtrados = PackagesList;
        }

        else{
            for (CulturalPackage culturalPackage : PackagesList) {
                if(culturalPackage.getNombre().contains(filter))
                {
                    filtrados.add(culturalPackage);
                }
            }
        }
        return filtrados;
    }

}

