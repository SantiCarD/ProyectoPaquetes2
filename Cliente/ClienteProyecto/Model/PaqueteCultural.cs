using ClienteProyecto.Model;
using System;
using System.Collections.Generic;

namespace ClienteApp.Model
{
    public class PaqueteCultural
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public double precio { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public List<Guia> guias { get; set; } = new List<Guia>();

        public PaqueteCultural() { }

        public PaqueteCultural(int id, string nombre, double precio, DateTime fechaInicio, DateTime fechaFin)
        {
            this.id = id;
            this.nombre = nombre;
            this.precio = precio;
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
        }

        public override string ToString()
        {
            return $"ID: {id}, Nombre: {nombre}, Precio: {precio:C2}, Inicio: {fechaInicio:d}, Fin: {fechaFin:d}";
        }
    }
}