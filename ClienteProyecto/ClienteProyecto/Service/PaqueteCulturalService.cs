using System;
using System.Collections.Generic;
using System.Linq;
using ClienteApp.Model;

namespace ClienteApp.Service
{
    public class PaqueteCulturalService
    {
        private static List<PaqueteCultural> paquetes = new List<PaqueteCultural>();

        public void AdicionarPaquete(PaqueteCultural paquete)
        {
            if (paquete == null)
                throw new ArgumentNullException(nameof(paquete), "El paquete no puede ser nulo.");

            if (paquetes.Any(p => p.Id == paquete.Id))
                throw new InvalidOperationException($"Ya existe un paquete con el ID {paquete.Id}");

            paquetes.Add(paquete);
        }

        public bool EliminarPaquete(int id)
        {
            var paquete = BuscarPaquetePorId(id);
            if (paquete != null)
            {
                return paquetes.Remove(paquete);
            }
            return false;
        }

        public bool ActualizarPaquete(int id, PaqueteCultural paqueteActualizado)
        {
            var paqueteExistente = BuscarPaquetePorId(id);
            if (paqueteExistente != null)
            {
                paqueteExistente.Nombre = paqueteActualizado.Nombre;
                paqueteExistente.Precio = paqueteActualizado.Precio;
                paqueteExistente.FechaInicio = paqueteActualizado.FechaInicio;
                paqueteExistente.FechaFin = paqueteActualizado.FechaFin;
                return true;
            }
            return false;
        }

        public List<PaqueteCultural> ListarPaquetes()
        {
            return new List<PaqueteCultural>(paquetes);
        }

        public PaqueteCultural BuscarPaquetePorId(int id)
        {
            return paquetes.FirstOrDefault(p => p.Id == id);
        }

        public List<PaqueteCultural> BuscarPaquetesPorNombre(string nombre)
        {
            return paquetes.Where(p => p.Nombre.ToLower().Contains(nombre.ToLower())).ToList();
        }
    }
}