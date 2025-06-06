﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Modelos
{
    internal class Usuarios
    {
        public int Id { get; set; }

        public string Identificacion { get; set; }

        public string NombreCompleto { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string NumeroCelular { get; set; }
        public string CorreoElectronico { get; set; }
        public DateTime FechaDeCreacion { get; set; }
        public  bool Estado { get; set; }

    }
}
