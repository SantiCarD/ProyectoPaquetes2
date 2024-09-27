using System;

public class PaqueteCultural
{
    public int Id { get; set; }  // Identificador único del paquete
    public string Nombre { get; set; }  // Nombre del paquete cultural
    public double Precio { get; set; }  // Precio del paquete cultural
    public DateTime FechaInicio { get; set; }  // Fecha de inicio del paquete
    public DateTime FechaFin { get; set; }  // Fecha de finalización del paquete

    // Constructor vacío por defecto
    public PaqueteCultural() { }

    // Constructor para inicializar el paquete con datos
    public PaqueteCultural(int id, string nombre, double precio, DateTime fechaInicio, DateTime fechaFin)
    {
        Id = id;
        Nombre = nombre;
        Precio = precio;
        FechaInicio = fechaInicio;
        FechaFin = fechaFin;
    }

    // Método ToString() para mostrar la información del paquete en formato de texto
    public override string ToString()
    {
        return $"ID: {Id}, Nombre: {Nombre}, Precio: {Precio}, Inicio: {FechaInicio.ToShortDateString()}, Fin: {FechaFin.ToShortDateString()}";
    }
}
