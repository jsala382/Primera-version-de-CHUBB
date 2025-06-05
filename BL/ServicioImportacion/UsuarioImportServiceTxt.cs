using BE.Modelos;
using BL.Validation;
using DAC.Repositorio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ServicioImportacion
{
    public class UsuarioImportServiceTxt : IUsuarioImportServiceTxt
    {
        private readonly UsuarioRepository _usuarioRepository;
        private readonly ValidationBd _validationBd;

        public UsuarioImportServiceTxt()
        {
            _usuarioRepository = new UsuarioRepository();
            _validationBd = new ValidationBd();
        }
        public List<string> ImportUsuarioDesdeTxt(Stream fileStream)
        {
            List<string> errorList = new List<string>();
            try
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    int linerNumber = 0;
                    while (!reader.EndOfStream) {
                        linerNumber++;
                        string completeLine = reader.ReadLine();
                      
                        string identificacion = SafeSubstring(completeLine, 0, 10);
                        string nombreCompleto = SafeSubstring(completeLine, 14, 30);
                        string primerNombre = SafeSubstring(completeLine, 44, 15);
                        string segundoNombre = SafeSubstring(completeLine, 59, 15);
                        string primerApellido = SafeSubstring(completeLine, 74, 15);
                        string segundoApellido = SafeSubstring(completeLine, 89, 15);

                        string fechaNacimientoStr = SafeSubstring(completeLine, 104, 10);
                        DateTime fechaNacimParsed = DateTime.MinValue; 
                        bool fechaNacimientoValida = DateTime.TryParseExact(fechaNacimientoStr, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture,
                                                                          System.Globalization.DateTimeStyles.None, out fechaNacimParsed);

                        string direccion = SafeSubstring(completeLine, 114, 40);
                        string numeroCelular = SafeSubstring(completeLine, 154, 15);
                        string correoElectronico = SafeSubstring(completeLine, 169, 30);

                        string fechaDeCreacionStr = SafeSubstring(completeLine, 199, 10);
                        DateTime fechaCreacionParsed = DateTime.MinValue; 
                        bool fechaDeCreacionValida = DateTime.TryParseExact(fechaDeCreacionStr, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture,
                                                                           System.Globalization.DateTimeStyles.None, out fechaCreacionParsed);

                        string estadoStr = SafeSubstring(completeLine, 209, 8); 
                        bool estadoParsed = false;
                        bool estadoValido;
                        if (estadoStr.Equals("Activo", StringComparison.OrdinalIgnoreCase))
                        {
                            estadoParsed = true;
                            estadoValido = true;
                        }
                        else if (estadoStr.Equals("Inactivo", StringComparison.OrdinalIgnoreCase))
                        {
                            estadoParsed = false;
                            estadoValido = true;
                        }
                        else
                        {
                           estadoValido = bool.TryParse(estadoStr, out estadoParsed);
                        }
                        ValidationResultUser currentValidationResult = _validationBd.ValidationResult(identificacion,
                                                                                                        nombreCompleto,
                                                                                                        primerNombre,
                                                                                                        segundoNombre,
                                                                                                        primerApellido,
                                                                                                        segundoApellido,
                                                                                                        fechaNacimientoValida,
                                                                                                        direccion,
                                                                                                        numeroCelular,
                                                                                                        correoElectronico,
                                                                                                        fechaDeCreacionValida,
                                                                                                        estadoValido
                                                                                                       );
                        currentValidationResult.LineNumber = linerNumber;
                        if (currentValidationResult.IsValid)
                        {
                            UsuariosDto newUserDto = new UsuariosDto
                            {
                                Identificacion = identificacion,
                                NombreCompleto = nombreCompleto,
                                PrimerNombre = primerNombre,
                                SegundoNombre = segundoNombre,
                                PrimerApellido = primerApellido,
                                SegundoApellido = segundoApellido,
                                FechaNacimiento = fechaNacimParsed, 
                                Direccion = direccion,
                                NumeroCelular = numeroCelular,
                                CorreoElectronico = correoElectronico,
                                FechaDeCreacion = fechaCreacionParsed, 
                                Estado = estadoParsed 
                            };
                            _usuarioRepository.CreateUser(newUserDto);
                        }
                        else
                        {
                            foreach (string error in currentValidationResult.Errors)
                            {
                                errorList.Add($"Línea {currentValidationResult.LineNumber}: {error}");
                            }
                        }
                    }
                }
            } catch (Exception ex)
            {
                errorList.Add($"Error general al procesar el archivo TXT: {ex.Message}. Por favor, verifica el formato del archivo.");
            }
            return errorList; 
        }
        private string SafeSubstring(string s, int startIndex, int length)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            if (startIndex < 0) startIndex = 0;
            if (startIndex >= s.Length) return string.Empty;
            if (startIndex + length > s.Length) length = s.Length - startIndex;
            return s.Substring(startIndex, length).Trim(); 
        }
    }
}
    

