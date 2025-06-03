using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Modelos
{
    public class UsuariosDto
    {
    
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
        public bool Estado { get; set; }

      /* public string ToTabSeparatedString()
        {
            return $"{Identificacion}\t {NombreCompleto}\t" +
                   $"{PrimerNombre}\t {SegundoNombre}" +
                   $"{PrimerApellido}\t {SegundoApellido}" +
                   $"{FechaNacimiento: yyyy-MM-dd}\t {Direccion}\t" +
                   $"{NumeroCelular}\t {CorreoElectronico}" +
                   $"{FechaDeCreacion: yy-MM-dd}" +
                   $"{(Estado ? "Activo" : "Inactivo")}";
            
        }*/

        public string ToFixedWidthString()
        {
            const int IdentificacionAncho = 14;
            const int NombreCompletoAncho = 30;
            const int PrimerNombreAncho = 15;
            const int SegundoNombreAncho = 15;
            const int PrimerApellidoAncho = 15;
            const int SegundoApellidoAncho = 15;
            const int FechaNacimientoAncho = 10;
            const int DireccionAncho = 40;
            const int NumeroCelularAncho = 15;
            const int CorreoElectronicoAncho = 30;
            const int FechaCreacionAncho = 10;
            const int EstadoAncho = 8;
            string estadoTexto = Estado ? "Activo" : "Inactivo";
            return string.Format(
                "{0,-" + IdentificacionAncho + "}" +
                "{1,-" + NombreCompletoAncho + "}" +
                "{2,-" + PrimerNombreAncho + "}" +
                "{3,-" + SegundoNombreAncho + "}" +
                "{4,-" + PrimerApellidoAncho + "}" +
                "{5,-" + SegundoApellidoAncho + "}" +
                "{6,-" + FechaNacimientoAncho + "}" +
                "{7,-" + DireccionAncho + "}" +
                "{8,-" + NumeroCelularAncho + "}" +
                "{9,-" + CorreoElectronicoAncho + "}" +
                "{10,-" + FechaCreacionAncho + "}" +
                "{11,-" + EstadoAncho + "}",
                Identificacion,
                NombreCompleto,
                PrimerNombre,
                SegundoNombre,
                PrimerApellido,
                SegundoApellido,
                FechaNacimiento.ToString("yyyyMMdd"),
                Direccion,
                NumeroCelular,
                CorreoElectronico,
                FechaDeCreacion.ToString("yyyyMMdd"),
                estadoTexto
             );     
        }
    }
}
