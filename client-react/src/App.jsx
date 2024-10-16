/* eslint-disable no-unused-vars */
/* eslint-disable react/prop-types */
import React, { useState } from 'react';
import TravellImg from './assets/images/TravellImg.png';
import santiagoImg from './assets/images/santiagoImg.png';
import camiloImg from './assets/images/camiloImg.png';
import whatsappIcon from './assets/images/whatsappIcon.png';

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

// Paquete Cultural submenus
const EliminarPaquete = () => (
  <div className="p-4">
    <h2 className="text-xl font-bold mb-4">Eliminar Paquetes</h2>
    <form className="space-y-4">
      <div>
        <label className="block">
          Parámetro:
          <select className="mt-1 block w-full">
            <option value="id">Id</option>
          </select>
        </label>
      </div>
      <div>
        <input type="text" className="mt-1 block w-full" />
        <button type="submit" className="mt-2 px-4 py-2 bg-blue-500 text-white rounded">Buscar</button>
      </div>
      <div className="space-y-2">
        <label className="block">Id: <input type="text" className="mt-1 block w-full" /></label>
        <label className="block">Nombre: <input type="text" className="mt-1 block w-full" /></label>
        <label className="block">Precio: <input type="text" className="mt-1 block w-full" /></label>
        <label className="block">Fecha de inicio: <input type="date" className="mt-1 block w-full" /></label>
        <label className="block">Fecha de finalización: <input type="date" className="mt-1 block w-full" /></label>
      </div>
      <button type="button" className="px-4 py-2 bg-red-500 text-white rounded">Eliminar</button>
    </form>
  </div>
);

const ListarPaquetes = () => (
  <div className="p-4">
    <h2 className="text-xl font-bold mb-4">Listar Paquetes</h2>
    <div className="mb-4">
      <input type="text" placeholder="Filtrar por nombre" className="mr-2 px-2 py-1 border rounded" />
      <button className="px-4 py-2 bg-blue-500 text-white rounded">Filtrar</button>
    </div>
    <div className="w-full h-64 bg-gray-200 mb-4">
      {/* Placeholder for list of packages */}
    </div>
    <button className="px-4 py-2 bg-green-500 text-white rounded">Refrescar</button>
  </div>
);

const BuscarPaquetes = () => (
  <div className="p-4">
    <h2 className="text-xl font-bold mb-4">Buscar Paquetes</h2>
    <form className="space-y-4">
      <div>
        <label className="block">
          Parámetro:
          <select className="mt-1 block w-full">
            <option value="id">Id</option>
          </select>
        </label>
      </div>
      <div>
        <input type="text" className="mt-1 block w-full" />
        <button type="submit" className="mt-2 px-4 py-2 bg-blue-500 text-white rounded">Buscar</button>
      </div>
      <div className="space-y-2">
        <label className="block">Id: <input type="text" className="mt-1 block w-full" /></label>
        <label className="block">Nombre: <input type="text" className="mt-1 block w-full" /></label>
        <label className="block">Precio: <input type="text" className="mt-1 block w-full" /></label>
        <label className="block">Fecha de inicio: <input type="text" className="mt-1 block w-full" /></label>
        <label className="block">Fecha Final: <input type="text" className="mt-1 block w-full" /></label>
        <label className="block">Guías: <input type="text" className="mt-1 block w-full" /></label>
      </div>
    </form>
  </div>
);

const ActualizarPaquetes = () => (
  <div className="p-4">
    <h2 className="text-xl font-bold mb-4">Actualizar Paquetes</h2>
    <form className="space-y-4">
      <div>
        <label className="block">
          Parámetro:
          <select className="mt-1 block w-full">
            <option value="id">Id</option>
          </select>
        </label>
      </div>
      <div>
        <input type="text" className="mt-1 block w-full" />
        <button type="submit" className="mt-2 px-4 py-2 bg-blue-500 text-white rounded">Buscar</button>
      </div>
      <div className="flex justify-between space-x-4">
        <div className="space-y-2 flex-1">
          <h3 className="font-bold">Datos Actuales</h3>
          <label className="block">Nombre del paquete: <input type="text" className="mt-1 block w-full" /></label>
          <label className="block">Precio del paquete: <input type="text" className="mt-1 block w-full" /></label>
          <label className="block">Fecha de inicio: <input type="text" className="mt-1 block w-full" /></label>
          <label className="block">Fecha de finalización: <input type="text" className="mt-1 block w-full" /></label>
        </div>
        <div className="space-y-2 flex-1">
          <h3 className="font-bold">Nuevos Datos</h3>
          <label className="block">Nombre del paquete: <input type="text" className="mt-1 block w-full" /></label>
          <label className="block">Precio del paquete: <input type="text" className="mt-1 block w-full" /></label>
          <label className="block">Nueva Fecha de inicio: <input type="date" className="mt-1 block w-full" /></label>
          <label className="block">Nueva Fecha de finalización: <input type="date" className="mt-1 block w-full" /></label>
        </div>
      </div>
      <button type="button" className="px-4 py-2 bg-green-500 text-white rounded">Actualizar</button>
    </form>
  </div>
);

const AdicionarPaquete = () => (
  <div className="p-4">
    <h2 className="text-xl font-bold mb-4">Adicionar Paquetes</h2>
    <form className="space-y-4">
      <label className="block">
        Id: <input type="text" className="mt-1 block w-full" />
      </label>
      <label className="block">
        Nombre: <input type="text" className="mt-1 block w-full" />
      </label>
      <label className="block">
        Precio: <input type="text" className="mt-1 block w-full" />
      </label>
      <label className="block">
        Guías: 
        <select className="mt-1 block w-full">
          <option>Seleccionar guía</option>
        </select>
      </label>
      <button type="button" className="px-2 py-1 bg-blue-500 text-white rounded">Agregar</button>
      <label className="block">
        Fecha de inicio: <input type="date" className="mt-1 block w-full" />
      </label>
      <label className="block">
        Fecha de finalización: <input type="date" className="mt-1 block w-full" />
      </label>
      <button type="submit" className="px-4 py-2 bg-green-500 text-white rounded">Adicionar Paquete</button>
    </form>
  </div>
);

const AdicionarGuia = () => (
  <div className="p-4">
    <h2 className="text-xl font-bold mb-4">Adicionar Guía</h2>
    <form className="space-y-4">
      <label className="block">
        Id: <input type="text" className="mt-1 block w-full" />
      </label>
      <label className="block">
        Nombre: <input type="text" className="mt-1 block w-full" />
      </label>
      <label className="block">
        Calificación: <input type="number" min="0" max="5" step="0.1" className="mt-1 block w-full" />
      </label>
      <label className="block">
        Edad: <input type="number" min="18" className="mt-1 block w-full" />
      </label>
      <label className="block">
        Fecha de Nacimiento: <input type="date" className="mt-1 block w-full" />
      </label>
      <button type="submit" className="px-4 py-2 bg-green-500 text-white rounded">Adicionar Guía</button>
    </form>
  </div>
);

const BuscarGuia = () => (
  <div className="p-4">
    <h2 className="text-xl font-bold mb-4">Buscar Guías</h2>
    <form className="space-y-4">
      <div className="flex items-center space-x-2">
        <label>
          Parámetro:
          <select className="mt-1 block w-full">
            <option value="id">Id</option>
          </select>
        </label>
        <input type="text" className="mt-1 block w-full" />
        <button type="submit" className="px-4 py-2 bg-blue-500 text-white rounded">Buscar</button>
      </div>
      <div className="space-y-2">
        <label className="block">Id: <input type="text" className="mt-1 block w-full" /></label>
        <label className="block">Nombre: <input type="text" className="mt-1 block w-full" /></label>
        <label className="block">Calificación: <input type="text" className="mt-1 block w-full" /></label>
        <label className="block">Edad: <input type="text" className="mt-1 block w-full" /></label>
        <label className="block">Fecha Nacimiento: <input type="date" className="mt-1 block w-full" /></label>
      </div>
    </form>
  </div>
);

const Desarrolladores = () => (
  <div className="bg-white rounded-lg shadow-lg p-6 max-w-4xl mx-auto">
    <h2 className="text-2xl font-bold text-center mb-6 italic">Desarrolladores</h2>
    <div className="flex justify-center flex-wrap gap-8">
      <div className="flex flex-col items-center min-w-[150px]">
        <div className="w-48 h-48 overflow-hidden rounded-lg mb-2">
          <img 
            src={santiagoImg}
            alt="Santiago Cárdenas" 
            className="w-full h-full object-cover"
          />
        </div>
        <p className="text-lg font-semibold italic">Santiago Cárdenas</p>
      </div>
      <div className="flex flex-col items-center min-w-[150px]">
        <div className="w-48 h-48 overflow-hidden rounded-lg mb-2">
          <img 
            src={camiloImg}
            alt="Camilo Novoa" 
            className="w-full h-full object-cover"
          />
        </div>
        <p className="text-lg font-semibold italic">Camilo Novoa</p>
      </div>
    </div>
  </div>
);

const Contacto = () => (
  <div className="bg-white rounded-lg shadow-lg p-6 max-w-4xl mx-auto">
    <h2 className="text-2xl font-bold text-center mb-6 italic">Contacto</h2>
    <div className="flex justify-center flex-wrap gap-16">
      <div className="flex items-center min-w-[200px]">
        <div className="w-16 h-16 mr-4">
          <img 
            src={whatsappIcon}
            alt="WhatsApp Icon" 
            className="w-full h-full"
          />
        </div>
        <p className="text-lg font-medium">3006634197</p>
      </div>
      <div className="flex items-center min-w-[200px]">
        <div className="w-16 h-16 mr-4">
          <img 
            src={whatsappIcon}
            alt="WhatsApp Icon" 
            className="w-full h-full"
          />
        </div>
        <p className="text-lg font-medium">3116349561</p>
      </div>
    </div>
  </div>
);

const Ayuda = () => {
  const [currentSection, setCurrentSection] = useState('desarrolladores');

  return (
    <div className="p-4">
      <h2 className="text-xl font-bold mb-4">Ayuda</h2>
      <nav className="mb-6">
        <ul className="flex space-x-4">
          <li>
            <button 
              className={`text-blue-500 hover:underline ${currentSection === 'desarrolladores' ? 'font-bold' : ''}`}
              onClick={() => setCurrentSection('desarrolladores')}
            >
              Desarrolladores
            </button>
          </li>
          <li>
            <button 
              className={`text-blue-500 hover:underline ${currentSection === 'contacto' ? 'font-bold' : ''}`}
              onClick={() => setCurrentSection('contacto')}
            >
              Contacto
            </button>
          </li>
        </ul>
      </nav>
      
      {currentSection === 'desarrolladores' ? <Desarrolladores /> : <Contacto />}
    </div>
  );
};

const App = () => {
  const [currentView, setCurrentView] = useState('main');
  const [currentSubView, setCurrentSubView] = useState(null);

  const renderView = () => {
    switch (currentView) {
      case 'main':
        return <Form1 setCurrentView={setCurrentView} />;
      case 'paqueteCultural':
        return (
          <div className="p-4">
            <h2 className="text-xl font-bold mb-4">Paquete Cultural</h2>
            <nav>
              <ul className="flex space-x-4 mb-4">
                <li><button className="text-blue-500 hover:underline" onClick={() => setCurrentSubView('eliminar')}>Eliminar Paquete</button></li>
                <li><button className="text-blue-500 hover:underline" onClick={() => setCurrentSubView('listar')}>Listar Paquetes</button></li>
                <li><button className="text-blue-500 hover:underline" onClick={() => setCurrentSubView('buscar')}>Buscar Paquetes</button></li>
                <li><button className="text-blue-500 hover:underline" onClick={() => setCurrentSubView('actualizar')}>Actualizar Paquetes</button></li>
                <li><button className="text-blue-500 hover:underline" onClick={() => setCurrentSubView('adicionar')}>Adicionar Paquete</button></li>
              </ul>
            </nav>
            {renderSubView()}
          </div>
        );
        case 'guia':
          return (
            <div className="p-4">
              <h2 className="text-xl font-bold mb-4">Guía</h2>
              <nav>
                <ul className="flex space-x-4 mb-4">
                  <li><button className="text-blue-500 hover:underline" onClick={() => setCurrentSubView('buscarGuia')}>Buscar Guía</button></li>
                  <li><button className="text-blue-500 hover:underline" onClick={() => setCurrentSubView('adicionarGuia')}>Adicionar Guía</button></li>
                </ul>
              </nav>
              {renderSubView()}
            </div>
          );
      case 'ayuda':
        return <Ayuda />;
      default:
        return <div>View not found</div>;
    }
  };

  const renderSubView = () => {
    switch (currentSubView) {
      case 'eliminar':
        return <EliminarPaquete />;
      case 'listar':
        return <ListarPaquetes />;
      case 'buscar':
        return <BuscarPaquetes />;
      case 'actualizar':
        return <ActualizarPaquetes />;
      case 'adicionar':
        return <AdicionarPaquete />;
        case 'buscarGuia':
          return <BuscarGuia />;
        case 'adicionarGuia':
          return <AdicionarGuia />;
        default:
          return null;
      }
    };

  return (
    <div className="container mx-auto">
      {renderView()}
      {currentView !== 'main' && (
        <button className="mt-4 px-4 py-2 bg-gray-500 text-white rounded" onClick={() => {setCurrentView('main'); setCurrentSubView(null);}}>
          Volver a la página principal
        </button>
      )}
    </div>
  );
};

export default App;