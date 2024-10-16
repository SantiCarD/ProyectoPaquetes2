package com.example.Package.services;

import com.example.Package.models.CulturalPackage;
import com.example.Package.models.Guide;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

public interface IPackageService {

    CulturalPackage searchPackageByName(String nombre);
    CulturalPackage searchPackageById(int id);
    CulturalPackage createPackage(String nombre, int id, Double precio, LocalDateTime fechaInicio, LocalDateTime fechaFin, ArrayList<Guide> guias);
    CulturalPackage updatePackage(int id, String nuevoNombre, Double nuevoPrecio, LocalDateTime nuevaFechaInicio, LocalDateTime nuevaFechaFin, ArrayList<Guide> guias);
    boolean deletePackage(int id);
    List<CulturalPackage> listPackages(String filter);
    List<CulturalPackage> getList();
    boolean guideExist(List<Integer> ints);
}
