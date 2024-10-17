/* eslint-disable no-unused-vars */
/* eslint-disable react/prop-types */
import React, { useState } from 'react';
import TravellImg from './assets/images/TravellImg.png';
import santiagoImg from './assets/images/santiagoImg.png';
import camiloImg from './assets/images/camiloImg.png';
import whatsappIcon from './assets/images/whatsappIcon.png';
import Ayuda from './components/Ayuda';
import AdicionarGuia from './components/AdicionarGuia';
import BuscarGuiaPorId from './components/BuscarGuiaPorId';
import BuscarGuiaPorNombre from './components/BuscarGuiaPorNombre';
import ActualizarGuia from './components/ActualizarGuia';
import EliminarGuia from './components/EliminarGuia';
import ListarGuias from './components/ListarGuias';
import VolverMenu from './components/VolverMenu';
import BuscarPaquetePorId from './components/BuscarPaquetePorId';
import BuscarPaquetePorNombre from './components/BuscarPaquetePorNombre';
import AdicionarPaquete from './components/AdicionarPaquete';
import ActualizarPaquete from './components/ActualizarPaquete';
import EliminarPaquete from './components/EliminarPaquete';
import ListarPaquetesCulturales from './components/ListarPaquetesCulturales';
// Main components
const Form1 = ({ setCurrentView }) => (
  <div className="p-4">
    <h1 className="text-2xl font-bold mb-4">Proyecto Turismo</h1>
    <nav>
      <ul className="flex space-x-4">
        <li><button className="text-blue-500 hover:underline" onClick={() => setCurrentView('paqueteCultural')}>Paquete Cultural</button></li>
        <li><button className="text-blue-500 hover:underline" onClick={() => setCurrentView('guia')}>Guía</button></li>
        <li><button className="text-blue-500 hover:underline" onClick={() => setCurrentView('ayuda')}>Ayuda</button></li>
      </ul>
    </nav>
    <img 
      src={TravellImg}
      alt="Travell" 
      className="w-full h-full object-cover"
    />
  </div>
);



// Main App component
const App = () => {
  const [currentView, setCurrentView] = useState('main');
  const [currentSubView, setCurrentSubView] = useState(null);

  const renderView = () => {
    switch (currentView) {
      case 'paqueteCultural':
        return (
          <div className="flex flex-col mb-4"> {/* Cambiamos a flex-col para mantener los elementos en columna */}
  <div className="flex justify-between items-center mb-4">
    <VolverMenu /> {/* Este botón estará a la izquierda */}
    <h2 className="text-xl font-bold">Paquete Cultural</h2> {/* El título estará al centro */}
  </div>

  <nav>
    <ul className="flex space-x-4">
      <li>
        <button className="text-blue-500 hover:underline" onClick={() => setCurrentSubView('adicionar')}>
          Adicionar
        </button>
      </li>
      <li>
        <button className="text-blue-500 hover:underline" onClick={() => setCurrentSubView('buscarporid')}>
          Buscar por ID
        </button>
      </li>
      <li>
        <button className="text-blue-500 hover:underline" onClick={() => setCurrentSubView('buscarpornombre')}>
          Buscar por Nombre
        </button>
      </li>
      <li>
        <button className="text-blue-500 hover:underline" onClick={() => setCurrentSubView('actualizar')}>
          Actualizar
        </button>
      </li>
      <li>
        <button className="text-blue-500 hover:underline" onClick={() => setCurrentSubView('eliminar')}>
          Eliminar
        </button>
      </li>
      <li>
        <button className="text-blue-500 hover:underline" onClick={() => setCurrentSubView('listar')}>
          Listar
        </button>
      </li>
    </ul>
  </nav>

  {renderSubView()}
</div>
        );
      case 'guia':
        return (
          <div className="flex flex-col mb-4"> {/* Cambiamos a flex-col para mantener los elementos en columna */}
  <div className="flex justify-between items-center mb-4">
    <VolverMenu /> {/* Este botón estará a la izquierda */}
    <h2 className="text-xl font-bold">Guía</h2> {/* El título estará al centro */}
  </div>

  <nav>
    <ul className="flex space-x-4">
      <li>
        <button className="text-blue-500 hover:underline" onClick={() => setCurrentSubView('adicionar')}>
          Adicionar
        </button>
      </li>
      <li>
        <button className="text-blue-500 hover:underline" onClick={() => setCurrentSubView('buscarporid')}>
          Buscar por ID
        </button>
      </li>
      <li>
        <button className="text-blue-500 hover:underline" onClick={() => setCurrentSubView('buscarpornombre')}>
          Buscar por Nombre
        </button>
      </li>
      <li>
        <button className="text-blue-500 hover:underline" onClick={() => setCurrentSubView('actualizar')}>
          Actualizar
        </button>
      </li>
      <li>
        <button className="text-blue-500 hover:underline" onClick={() => setCurrentSubView('eliminar')}>
          Eliminar
        </button>
      </li>
      <li>
        <button className="text-blue-500 hover:underline" onClick={() => setCurrentSubView('listar')}>
          Listar
        </button>
      </li>
    </ul>
  </nav>

  {renderSubView2()}
</div>
        );
      case 'ayuda':
        
        return <div>
          <VolverMenu /> {/* Este botón estará a la izquierda */}
          <Ayuda />
          </div>
        ;
      default:
        return <Form1 setCurrentView={setCurrentView} />;
    }
  };

  const renderSubView = () => {
    switch (currentSubView) {
      case 'adicionar':
        return <AdicionarPaquete />;
      case 'buscarporid':
        return <BuscarPaquetePorId />;
      case 'buscarpornombre':
        return <BuscarPaquetePorNombre />;
      case 'actualizar':
        return <ActualizarPaquete />;
      case 'eliminar':
        return <EliminarPaquete />;
      case 'listar':
        return <ListarPaquetesCulturales />;
      default:
        return <ListarPaquetesCulturales />;
    }
  };
  const renderSubView2 = () => {
    switch (currentSubView) {
      case 'adicionar':
        return <AdicionarGuia />;
      case 'buscarporid':
        return <BuscarGuiaPorId />;
        case 'buscarpornombre':
        return <BuscarGuiaPorNombre />;
      case 'actualizar':
        return <ActualizarGuia />;
      case 'eliminar':
        return <EliminarGuia />;
      case 'listar':
        return <ListarGuias />;
      default:
        return <ListarGuias />;
    }
  };

  return (
    <div className="container mx-auto">
      {renderView()}
    </div>
  );
};

export default App;
