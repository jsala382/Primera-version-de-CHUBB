using BL.Common;
using BL.Validation;
using DAC.Repositorio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BL.ServicioImportacion
{
    public class UsuarioImportServiceXls : IUsuarioImportServiceXls
    {
        private readonly UsuarioRepository _usuarioRepository;
        private readonly ValidationBd _validationBd;
        public UsuarioImportServiceXls()
        {
            _usuarioRepository = new UsuarioRepository();
            _validationBd = new ValidationBd();
        }
        public List<string> ImportUserDesdeXls(Stream fileStream)
        {
           ValidationBd validationUser = new ValidationBd();
           List<string> errorList = new List<string>();
           List<string> allRowErrors = new List<string>();
            try
           {
                using (var package = new OfficeOpenXml.ExcelPackage(fileStream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;
                    string connBd = ConfigurationManager
                                   .ConnectionStrings["UserConnection"]
                                   .ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connBd))
                    {
                        conn.Open();
                        for (int row = 2; row <= rowCount - 1; row++)
                        {
                            string identificacion = worksheet.Cells[row, 1].Text;
                            string nombreCompleto = worksheet.Cells[row, 2].Text;
                            string primerNombre = worksheet.Cells[row, 3].Text;
                            string segundoNombre = worksheet.Cells[row, 4].Text;
                            string primerApellido = worksheet.Cells[row, 5].Text;
                            string segundoApellido = worksheet.Cells[row, 6].Text;
                            string fechaNacimiento = worksheet.Cells[row, 7].Text;
                            DateTime fechaNacimParsed;
                            bool fechaNacimientoValida = DateTime.TryParseExact(fechaNacimiento, "yyyy/dd/MM", System.Globalization.CultureInfo.InvariantCulture,
                                                                         System.Globalization.DateTimeStyles.None, out fechaNacimParsed);
                            string direccion = worksheet.Cells[row, 8].Text;
                            string numeroCelular = worksheet.Cells[row, 9].Text;
                            string correoElectronico = worksheet.Cells[row, 10].Text;
                            string fechaDeCreacion = worksheet.Cells[row, 11].Text;
                            DateTime fechaCreacParsed;
                            bool fechaDeCreacionValida = DateTime.TryParseExact(fechaDeCreacion, "yyyy/dd/MM", System.Globalization.CultureInfo.InvariantCulture,
                                                                         System.Globalization.DateTimeStyles.None, out fechaCreacParsed);
                            string estado = worksheet.Cells[row, 12].Text;
                            bool estadoParsed;
                            bool estadoValido;
                            if (estado.Equals("Activo", StringComparison.OrdinalIgnoreCase))
                            {
                                estadoParsed = true;
                                estadoValido = true;
                            }
                            else if (estado.Equals("Inactivo", StringComparison.OrdinalIgnoreCase))
                            {
                                estadoParsed = false;
                                estadoValido = true;
                            }
                            else
                            {
                                estadoValido = Boolean.TryParse(estado, out estadoParsed);
                            }
                            ValidationResultUser currentValidationResult = validationUser.ValidationResult(identificacion,
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
                            if (currentValidationResult.IsValid)
                            {
                                using (SqlCommand cmd = new SqlCommand("InsertUsuario", conn))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@Identificacion", identificacion);
                                    cmd.Parameters.AddWithValue("@NombreCompleto", nombreCompleto);
                                    cmd.Parameters.AddWithValue("@PrimerNombre", primerNombre);
                                    cmd.Parameters.AddWithValue("@SegundoNombre", segundoNombre);
                                    cmd.Parameters.AddWithValue("@PrimerApellido", primerApellido);
                                    cmd.Parameters.AddWithValue("@SegundoApellido", segundoApellido);
                                    cmd.Parameters.AddWithValue("@FechaNacimiento", fechaNacimParsed);
                                    cmd.Parameters.AddWithValue("@Direccion", direccion);
                                    cmd.Parameters.AddWithValue("@NumeroCelular", numeroCelular);
                                    cmd.Parameters.AddWithValue("@CorreoElectronico", correoElectronico);
                                    cmd.Parameters.AddWithValue("@FechaDeCreacion", fechaCreacParsed);
                                    cmd.Parameters.AddWithValue("@Estado", estadoParsed);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                foreach (string error in currentValidationResult.Errors)
                                {
                                    errorList.Add($"Linea {currentValidationResult.LineNumber}: {error}");
                                }
                            }
                        }
                    }
                }
           }catch(Exception ex)
           {
                errorList.Add($"Error general al procesar el archivo TXT: {ex.Message}. Por favor, verifica el formato del archivo.");
           }
            return errorList; 
        }
    }
}
