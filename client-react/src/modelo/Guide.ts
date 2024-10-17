export class Guide {
    private id: number;
    private nombre: string;
    private calificacion: number;
    private edad: number;
    private fechaNacimiento: string;
  
    // Constructor para inicializar las propiedades
    constructor(
      id: number,
      nombre: string,
      calificacion: number,
      edad: number,
      fechaNacimiento: string
    ) {
      this.id = id;
      this.nombre = nombre;
      this.calificacion = calificacion;
      this.edad = edad;
      this.fechaNacimiento = fechaNacimiento;
    }
  
    // Getters y Setters
    get getid(): number {
      return this.id;
    }
  
    set setid(value: number) {
      this.id = value;
    }
  
    get getnombre(): string {
      return this.nombre;
    }
  
    set setnombre(value: string) {
      this.nombre = value;
    }
  
    get getcalificacion(): number {
      return this.calificacion;
    }
  
    set setcalificacion(value: number) {
      this.calificacion = value;
    }
  
    get getedad(): number {
      return this.edad;
    }
  
    set setedad(value: number) {
      this.edad = value;
    }
  
    get getfechaNacimiento(): string {
      return this.fechaNacimiento;
    }
  
    set setfechaNacimiento(value: string) {
      this.fechaNacimiento = value;
    }
  
    // Método para imprimir la información del guía
    toStringg(): string {
      return `ID: ${this.id}, Nombre: ${this.nombre}, Calificación: ${this.calificacion}, Edad: ${this.edad}, Fecha de Nacimiento: ${this.fechaNacimiento}`;
    }
  }
  