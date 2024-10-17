import React, { useState } from 'react';
import guideService from '../Servicios/GuideService';
import { Guide } from '../modelo/Guide';

const ActualizarGuia = () => {
  const [id, setId] = useState('');
  const [nombre, setNombre] = useState('');
  const [calificacion, setCalificacion] = useState<number | ''>('');
  const [edad, setEdad] = useState<number | ''>('');
  const [fechaNacimiento, setFechaNacimiento] = useState('');
  const [guiaEncontrada, setGuiaEncontrada] = useState<boolean>(false); // Nuevo estado para verificar si se encontró la guía

  const buscarGuia = async () => {
    const guia = await guideService.buscarGuiaPorId(Number(id)); // Asegúrate de tener este método en tu servicio
    if (guia) {
      setNombre(guia.getnombre);
      setCalificacion(guia.getcalificacion);
      setEdad(guia.getedad);
      setFechaNacimiento(guia.getfechaNacimiento);
      setGuiaEncontrada(true); // Marca que la guía fue encontrada
    } else {
      alert('Guía no encontrada');
      setGuiaEncontrada(false); // Resetea el estado si no se encuentra
    }
  };

  const handleUpdate = async () => {
    const guia = new Guide(
      Number(id),
      nombre,
      Number(calificacion),
      Number(edad),
      fechaNacimiento
    );
    const success = await guideService.actualizarGuia(Number(id), guia);
    if (success) {
      alert('Guía actualizada con éxito');
      setId('');
      setNombre('');
      setCalificacion('');
      setEdad('');
      setFechaNacimiento('');
      setGuiaEncontrada(false); // Resetea el estado después de la actualización
    } else {
      alert('Error al actualizar la guía');
    }
  };

  return (
    <div className="buscar-container"> {/* Clase para el contenedor principal */}
      <h2 className="buscar-title">Actualizar Guía</h2>
      <div className="buscar-input-container">
        <input
          type="number"
          value={id}
          onChange={(e) => setId(e.target.value)}
          placeholder="ID de la guía"
          className="buscar-input"
        />
        <button onClick={buscarGuia} className="buscar-button">Buscar Guía</button>
      </div>

      {guiaEncontrada && (
        <div className="guia-card"> {/* Clase para el contenedor de la guía */}
          <h3 className="guia-title">Datos de la Guía</h3>
          <input
            type="text"
            value={nombre}
            onChange={(e) => setNombre(e.target.value)}
            placeholder="Nuevo nombre"
            className="buscar-input"
          />
          <input
            type="number"
            value={calificacion}
            onChange={(e) => setCalificacion(e.target.valueAsNumber)}
            placeholder="Nueva calificación"
            className="buscar-input"
          />
          <input
            type="number"
            value={edad}
            onChange={(e) => setEdad(e.target.valueAsNumber)}
            placeholder="Nueva edad"
            className="buscar-input"
          />
          <input
            type="date"
            value={fechaNacimiento}
            onChange={(e) => setFechaNacimiento(e.target.value)}
            className="buscar-input"
          />
          <button onClick={handleUpdate} className="buscar-button">Actualizar</button>
        </div>
      )}
    </div>
  );
};

export default ActualizarGuia;
