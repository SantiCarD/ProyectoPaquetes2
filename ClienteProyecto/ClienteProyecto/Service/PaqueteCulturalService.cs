using System;
using System.Collections.Generic;
using System.Linq;

public class PaqueteCulturalService
{
    private List<PaqueteCultural> paquetes;  // Lista para almacenar los paquetes culturales

    // Constructor que inicializa la lista de paquetes
    public PaqueteCulturalService()
    {
        paquetes = new List<PaqueteCultural>();
    }

    // Adicionar un nuevo Paquete
    public void AdicionarPaquete(PaqueteCultural paquete)
    {
        if (paquete != null)
        {
            paquetes.Add(paquete);
            Console.WriteLine("Paquete añadido exitosamente.");
        }
        else
        {
            Console.WriteLine("El paquete es nulo y no puede ser añadido.");
        }
    }

    // Eliminar Paquete por Id
    public bool EliminarPaquete(int id)
    {
        var paquete = BuscarPaquetePorId(id);
        if (paquete != null)
        {
            paquetes.Remove(paquete);
            Console.WriteLine($"Paquete con Id {id} eliminado exitosamente.");
            return true;
        }
        else
        {
            Console.WriteLine($"Paquete con Id {id} no encontrado.");
            return false;
        }
    }

    // Actualizar un Paquete existente
    public bool ActualizarPaquete(int id, PaqueteCultural paqueteActualizado)
    {
        var paqueteExistente = BuscarPaquetePorId(id);
        if (paqueteExistente != null)
        {
            paqueteExistente.Nombre = paqueteActualizado.Nombre;
            paqueteExistente.Precio = paqueteActualizado.Precio;
            paqueteExistente.FechaInicio = paqueteActualizado.FechaInicio;
            paqueteExistente.FechaFin = paqueteActualizado.FechaFin;
            Console.WriteLine($"Paquete con Id {id} actualizado exitosamente.");
            return true;
        }
        else
        {
            Console.WriteLine($"Paquete con Id {id} no encontrado.");
            return false;
        }
    }

    // Listar todos los Paquetes
    public List<PaqueteCultural> ListarPaquetes()
    {
        if (paquetes.Count == 0)
        {
            Console.WriteLine("No hay paquetes culturales disponibles.");
        }
        return paquetes;
    }

    // Buscar Paquete por Id
    public PaqueteCultural BuscarPaquetePorId(int id)
    {
        var paquete = paquetes.Find(p => p.Id == id);
        if (paquete != null)
        {
            return paquete;
        }
        else
        {
            Console.WriteLine($"Paquete con Id {id} no encontrado.");
            return null;
        }
    }

    // Buscar Paquetes por Nombre
    public List<PaqueteCultural> BuscarPaquetesPorNombre(string nombre)
    {
        // Convertimos ambos strings a minúsculas para hacer la comparación insensible a mayúsculas
        var resultado = paquetes.FindAll(p => p.Nombre.ToLower().Contains(nombre.ToLower()));
        if (resultado.Count == 0)
        {
            Console.WriteLine($"No se encontraron paquetes que coincidan con el nombre '{nombre}'.");
        }
        return resultado;
    }


    // Buscar Paquetes por Rango de Fechas
    public List<PaqueteCultural> BuscarPaquetesPorRangoFechas(DateTime fechaInicio, DateTime fechaFin)
    {
        var resultado = paquetes.FindAll(p => p.FechaInicio >= fechaInicio && p.FechaFin <= fechaFin);
        if (resultado.Count == 0)
        {
            Console.WriteLine($"No se encontraron paquetes en el rango de fechas: {fechaInicio.ToShortDateString()} - {fechaFin.ToShortDateString()}.");
        }
        return resultado;
    }
}
