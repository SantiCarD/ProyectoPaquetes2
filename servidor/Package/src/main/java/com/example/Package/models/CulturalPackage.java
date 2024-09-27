package com.example.Package.models;

import java.time.LocalDateTime;

public class CulturalPackage {

    private String Nombre;
    private int id;
    private Double precio;
    private LocalDateTime fechaInicio;
    private LocalDateTime fechaFin;

    public CulturalPackage(String nombre, int id, Double precio, LocalDateTime fechaInicio, LocalDateTime fechaFin) {
        Nombre = nombre;
        this.id = id;
        this.precio = precio;
        this.fechaInicio = fechaInicio;
        this.fechaFin = fechaFin;
    }

    public String getNombre() {
        return Nombre;
    }

    public void setNombre(String nombre) {
        Nombre = nombre;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public Double getPrecio() {
        return precio;
    }

    public void setPrecio(Double precio) {
        this.precio = precio;
    }

    public LocalDateTime getFechaInicio() {
        return fechaInicio;
    }

    public void setFechaInicio(LocalDateTime fechaInicio) {
        this.fechaInicio = fechaInicio;
    }

    public LocalDateTime getFechaFin() {
        return fechaFin;
    }

    public void setFechaFin(LocalDateTime fechaFin) {
        this.fechaFin = fechaFin;
    }
}
