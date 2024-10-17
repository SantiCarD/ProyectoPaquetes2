import React, { useState } from 'react';
import guideService from '../Servicios/GuideService';
import { Guide } from '../modelo/Guide';
import '../App.css'; // Asegúrate de importar el CSS

const EliminarGuia = () => {
  const [id, setId] = useState('');
  const [guia, setGuia] = useState<Guide | null>(null);

  const buscarGuia = async () => {
    const guiaEncontrada = await guideService.buscarGuiaPorId(Number(id));
    if (guiaEncontrada) {
      setGuia(guiaEncontrada);
    } else {
      alert('Guía no encontrada');
      setGuia(null);
    }
  };

  const handleDelete = async () => {
    if (guia) {
      const success = await guideService.eliminarGuia(Number(id));
      if (success) {
        alert('Guía eliminada con éxito');
        setId('');
        setGuia(null);
      } else {
        alert('Error al eliminar la guía');
      }
    }
  };

  return (
    <div className="buscar-container"> {/* Clase para el contenedor principal */}
      <h2 className="buscar-title">Eliminar Guía</h2>
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

      {guia && (
        <div className="guia-card"> {/* Clase para el contenedor de la guía */}
          <h3 className="guia-title">Datos de la Guía</h3>
          <p className="guia-attribute">ID: {guia.getid}</p>
          <p className="guia-attribute">Nombre: {guia.getnombre}</p>
          <p className="guia-attribute">Calificación: {guia.getcalificacion}</p>
          <p className="guia-attribute">Edad: {guia.getedad}</p>
          <p className="guia-attribute">Fecha de Nacimiento: {guia.getfechaNacimiento}</p>
          <button onClick={handleDelete} className="buscar-button">Eliminar</button>
        </div>
      )}
    </div>
  );
};

export default EliminarGuia;
