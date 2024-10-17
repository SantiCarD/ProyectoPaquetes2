import React, { useEffect, useState } from 'react';
import guideService from '../Servicios/GuideService';
import { Guide } from '../modelo/Guide';
import '../App.css'; // Asegúrate de importar el CSS

const ListarGuias = () => {
  const [guias, setGuias] = useState<Array<Guide>>([]);

  useEffect(() => {
    const fetchGuias = async () => {
      const result = await guideService.listarGuias();
      setGuias(result);
    };
    fetchGuias();
  }, []);

  return (
    <div className="listar-container"> {/* Clase para el contenedor principal */}
      <h2 className="listar-title">Lista de Guías</h2>
      <table className="listar-table"> {/* Tabla para mostrar los datos */}
        <thead>
          <tr>
            <th>ID</th>
            <th>Nombre</th>
            <th>Calificación</th>
            <th>Edad</th>
            <th>Fecha de Nacimiento</th>
          </tr>
        </thead>
        <tbody>
          {guias.map((guia) => (
            <tr key={guia.getid}>
              <td>{guia.getid}</td>
              <td>{guia.getnombre}</td>
              <td>{guia.getcalificacion}</td>
              <td>{guia.getedad}</td>
              <td>{guia.getfechaNacimiento}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default ListarGuias;
