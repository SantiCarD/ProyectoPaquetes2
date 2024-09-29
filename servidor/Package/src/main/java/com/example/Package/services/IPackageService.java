package com.example.Package.services;

import com.example.Package.models.CulturalPackage;

import java.time.LocalDateTime;
import java.util.List;

public interface IPackageService {

    CulturalPackage searchPackageByName(String nombre);
    CulturalPackage searchPackageById(int id);
    CulturalPackage createPackage(String nombre, int id, Double precio, LocalDateTime fechaInicio, LocalDateTime fechaFin);
    CulturalPackage updatePackage(int id, String nuevoNombre, Double nuevoPrecio, LocalDateTime nuevaFechaInicio, LocalDateTime nuevaFechaFin);
    boolean deletePackage(int id);
    List<CulturalPackage> getList();

}
