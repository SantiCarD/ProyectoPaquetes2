import React, { useState } from 'react';
import guideService from '../Servicios/GuideService';
import { Guide } from '../modelo/Guide';
import'../App.css';


const BuscarGuiaPorNombre = () => {
  const [nombre, setNombre] = useState('');
  const [guia, setGuia] = useState<Guide | null>(null);
const handleSearch = async () => {

    try {
      const result = await guideService.buscarGuiaPorNombre(nombre);
      setGuia(result);
      if (result) {
        alert(`Nombre de la guía: ${result.getnombre}`);
      } else {
        alert('No se encontró ninguna guía con ese ID');
      }
    } catch (error) {
      console.error('Error al buscar la guía:', error);
      alert('Ocurrió un error al buscar la guía');
    }
  };

  return (
    
    <div className="buscar-container">
      <h2 className="buscar-title">Buscar Guía por Nombre</h2>
      <div className="buscar-input-container">
        <input
          type="text"
          value={nombre}
          onChange={(e) => setNombre(e.target.value)}
          placeholder="Ingrese el nombre de la guía"
          className="buscar-input"
        />
        <button onClick={handleSearch} className="buscar-button">
          Buscar
        </button>
      </div>

      {guia && (
        <div className="guia-card">
          <h3 className="guia-title">Guía Encontrada:</h3>
          <p className="guia-attribute">ID: {guia.getid}</p>
          <p className="guia-attribute">Nombre: {guia.getnombre}</p>
          <p className="guia-attribute">Calificación: {guia.getcalificacion}</p>
          <p className="guia-attribute">Edad: {guia.getedad}</p>
          <p className="guia-attribute">
            Fecha de Nacimiento: {new Date(guia.getfechaNacimiento).toLocaleDateString()}
          </p>
        </div>
        
      )}
    
    </div>
  );
};

export default BuscarGuiaPorNombre;
