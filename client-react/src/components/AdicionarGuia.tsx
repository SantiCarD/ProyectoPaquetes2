import React, { useState } from 'react';
import guideService from '../Servicios/GuideService';
import { Guide } from '../modelo/Guide';
import VolverMenu from './VolverMenu'; 
import '../App.css'; 

const AdicionarGuia = () => {
  const [id, setId] = useState<number | ''>('');
  const [nombre, setNombre] = useState('');
  const [calificacion, setCalificacion] = useState<number | ''>('');
  const [edad, setEdad] = useState<number | ''>('');
  const [fechaNacimiento, setFechaNacimiento] = useState('');

  const handleAdd = async () => {
    const guia = new Guide(
      Number(id),
      nombre,
      Number(calificacion),
      Number(edad),
      fechaNacimiento
    );
    const success = await guideService.adicionarGuia(guia);
    if (success) {
      alert('Guía añadida con éxito');
      setId('');
      setNombre('');
      setCalificacion('');
      setEdad('');
      setFechaNacimiento('');
    } else {
      alert(guia)
      alert('Error al añadir la guía');
     
    }
  };

  return (
     

    <div className="buscar-container">
      <h2 className="buscar-title">Añadir Guía</h2>
      <div className="buscar-input-container" style={{ flexDirection: 'column' }}>
      <input
          type="number"
          className="buscar-input"
          placeholder="Id"
          value={id}
          onChange={(e) => setId(Number(e.target.value))}
        />
        <input
          type="text"
          className="buscar-input"
          placeholder="Nombre"
          value={nombre}
          onChange={(e) => setNombre(e.target.value)}
        />
        <input
          type="number"
          className="buscar-input"
          placeholder="Calificación"
          value={calificacion}
          onChange={(e) => setCalificacion(Number(e.target.value))}
        />
        <input
          type="number"
          className="buscar-input"
          placeholder="Edad"
          value={edad}
          onChange={(e) => setEdad(Number(e.target.value))}
        />
        <input
          type="date"
          className="buscar-input"
          value={fechaNacimiento}
          onChange={(e) => setFechaNacimiento(e.target.value)}
        />
        <button className="buscar-button" onClick={handleAdd}>Añadir</button>
      </div>
    </div>
  );
};

export default AdicionarGuia;
