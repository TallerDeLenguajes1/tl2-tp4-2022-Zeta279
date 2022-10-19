﻿namespace TP4.Models
{
    public class PersonaModel
    {
        public int id { get; }
        public string nombre { get; }
        public string direccion { get; }
        public int telefono { get; }

        public PersonaModel(int id, string nombre, string direccion, int telefono)
        {
            this.id = id;
            this.nombre = nombre;
            this.direccion = direccion;
            this.telefono = telefono;
        }

        public override string ToString()
        {
            return $"ID: {id}\nNombre: {nombre}\nDireccion: {direccion}\nTelefono: {telefono}";
        }
    }
}