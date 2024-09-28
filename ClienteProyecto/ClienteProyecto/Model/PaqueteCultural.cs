using System;

namespace ClienteApp.Model
{
    public class PaqueteCultural
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public PaqueteCultural() { }

        public PaqueteCultural(int id, string nombre, double precio, DateTime fechaInicio, DateTime fechaFin)
        {
            Id = id;
            Nombre = nombre;
            Precio = precio;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Nombre: {Nombre}, Precio: {Precio:C2}, Inicio: {FechaInicio:d}, Fin: {FechaFin:d}";
        }
    }
}