package com.example.Package.services;

import com.example.Package.models.CulturalPackage;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

@Service
public class PackageService implements IPackageService {
    private static PackageService packageService;
    private ArrayList<CulturalPackage> PackagesList = new ArrayList<CulturalPackage>();

    private PackageService() {

    }

    public static PackageService getPackageService() {
        if (packageService == null) {
            packageService = new PackageService();
        }

        return packageService;
    }

    public ArrayList<CulturalPackage> getList() {
        return PackagesList;
    }


    ///CRUD//////CRUD//////CRUD//////CRUD//////CRUD//////CRUD//////CRUD//////CRUD//////CRUD//////CRUD//////CRUD//////CRUD///
    public CulturalPackage searchPackage(String param, String value) {
        CulturalPackage foundPackage = null;
        switch (param) {

            case "Id": {
                for (CulturalPackage culturalPackage : getList()) {
                    if (String.valueOf(culturalPackage.getId()).equals(value)) {
                        foundPackage = culturalPackage;
                        break;
                    }
                }
                break;
            }


            case "Nombre": {
                for (CulturalPackage culturalPackage : getList()) {
                    if (culturalPackage.getNombre().equalsIgnoreCase(value)) {
                        foundPackage = culturalPackage;
                        break; //
                    }
                }
                break;
            }


            default:
                System.out.println("Parámetro no reconocido.");
                break;
        }

        return foundPackage;
    }



    public CulturalPackage createPackage(String nombre, int id, Double precio, LocalDateTime fechaInicio, LocalDateTime fechaFin) throws IllegalArgumentException {
        // Verificar si ya existe un paquete con el mismo ID
        if (searchPackage("Id", String.valueOf(id)) != null) {
            throw new IllegalArgumentException("Ya existe un paquete con este ID.");
        }

        // Verificar si ya existe un paquete con el mismo nombre
        if (searchPackage("Nombre", nombre) != null) {
            throw new IllegalArgumentException("Ya existe un paquete con este nombre.");
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
            throw new IllegalArgumentException("La fecha de inicio debe ser anterior a la fecha de fin.");
        }

        CulturalPackage paquete = new CulturalPackage(nombre, id, precio, fechaInicio, fechaFin);
        PackagesList.add(paquete);
        return paquete;
    }


    public CulturalPackage updatePackage(int id, String nuevoNombre, Double nuevoPrecio, LocalDateTime nuevaFechaInicio, LocalDateTime nuevaFechaFin) {
        // Buscar el paquete existente por ID
        CulturalPackage paqueteExistente = searchPackage("Id", String.valueOf(id));

        // Verificar si el paquete existe
        if (paqueteExistente == null) {
            System.out.println("No se encontró un paquete con el ID proporcionado.");
            return null; // O lanzar una excepción
        }

        // Validar que el nuevo nombre no esté vacío
        if (nuevoNombre == null || nuevoNombre.trim().isEmpty()) {
            System.out.println("El nuevo nombre no puede estar vacío.");
            return null; // O lanzar una excepción
        }

        // Validar que el nuevo precio sea mayor que 0
        if (nuevoPrecio == null || nuevoPrecio <= 0) {
            System.out.println("El nuevo precio debe ser mayor que 0.");
            return null; // O lanzar una excepción
        }

        // Validar que las nuevas fechas no sean nulas
        if (nuevaFechaInicio == null || nuevaFechaFin == null) {
            System.out.println("Las nuevas fechas de inicio y fin no pueden ser nulas.");
            return null; // O lanzar una excepción
        }

        // Validar que la nueva fecha de inicio sea anterior a la fecha de fin
        if (nuevaFechaInicio.isAfter(nuevaFechaFin)) {
            System.out.println("La nueva fecha de inicio debe ser anterior a la fecha de fin.");
            return null; // O lanzar una excepción
        }

        // Si las validaciones son correctas, actualizar los atributos del paquete
        paqueteExistente.setNombre(nuevoNombre);
        paqueteExistente.setPrecio(nuevoPrecio);
        paqueteExistente.setFechaInicio(nuevaFechaInicio);
        paqueteExistente.setFechaFin(nuevaFechaFin);

        return paqueteExistente; // Retorna el paquete actualizado
    }


    public boolean deletePackage(int id) {
        // Buscar el paquete existente por ID
        CulturalPackage paqueteExistente = searchPackage("Id", String.valueOf(id));

        // Verificar si el paquete existe
        if (paqueteExistente == null) {
            System.out.println("No se encontró un paquete con el ID proporcionado.");
            return false; // Retorna false si no se encontró el paquete
        }

        // Eliminar el paquete de la lista

        boolean eliminado = PackagesList.remove(paqueteExistente); // Intenta eliminar el paquete
        // Retornar true si se eliminó con éxito, false de lo contrario
        return eliminado;
    }





    ///CRUD//////CRUD//////CRUD//////CRUD//////CRUD//////CRUD//////CRUD//////CRUD//////CRUD//////CRUD//////CRUD//////CRUD///

    public List<CulturalPackage> listPackages() {
        List<CulturalPackage> listaPaquetes = getList(); // Obtén la lista de paquetes
        return listaPaquetes; // Retorna la lista
    }


}

