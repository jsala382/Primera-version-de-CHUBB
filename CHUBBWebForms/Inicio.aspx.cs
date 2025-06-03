using BL.Common;
using BL.Validation;
using Microsoft.Ajax.Utilities;
using OfficeOpenXml.Drawing.Chart;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CHUBBWebForms
{
    public partial class Inicio : System.Web.UI.Page
    {
        
       
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviarClick(object sender, EventArgs e)
        {
            ValidationBd validationUser = new ValidationBd();
            List<string> allRowErrors = new List<string>();
            if (tuArchivo.HasFile)
            {
                if (tuArchivo.FileName.Contains(".txt"))
                {
                    try
                    {
                        using (StreamReader reader = new StreamReader(tuArchivo
                       .PostedFile
                       .InputStream))
                        {
                            string connBd = ConfigurationManager
                                            .ConnectionStrings["UserConnection"]
                                            .ConnectionString;
                            using (SqlConnection sqlConnection = new SqlConnection(connBd))
                            {
                                sqlConnection.Open();
                                int linea = 1;
                                while (!reader.EndOfStream)
                                {
                                    string linea1 = reader.ReadLine();
                                    string identificacion = linea1.Substring(0, 10);
                                    string nombreCompleto = linea1.Substring(14, 30);
                                    string primerNombre = linea1.Substring(44, 15);
                                    string segundoNombre = linea1.Substring(59, 15);
                                    string primerApellido = linea1.Substring(74, 15);
                                    string segundoApellido = linea1.Substring(89, 15);
                                    string fechaNacimiento = linea1.Substring(104, 10).Trim();
                                    DateTime fechaNacimParsed;
                                    bool fechaNacimientoValida = DateTime.TryParseExact(fechaNacimiento, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture,
                                                                                 System.Globalization.DateTimeStyles.None, out fechaNacimParsed);
                                    string direccion = linea1.Substring(114, 40);
                                    string numeroCelular = linea1.Substring(154, 15);
                                    string correoElectronico = linea1.Substring(169, 30);
                                    string fechaDeCreacion = linea1.Substring(199, 10).Trim();
                                    DateTime fechaCreacionParsed;
                                    bool fechaDeCreacionValida = DateTime.TryParseExact(fechaDeCreacion, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture,
                                                                                 System.Globalization.DateTimeStyles.None, out fechaCreacionParsed);
                                    string estado = linea1.Substring(209, 8).Trim();
                                    bool estadoParsed;
                                    bool estadoValido;
                                    if (estado.Equals("Activo", StringComparison.OrdinalIgnoreCase))
                                    {
                                        estadoParsed = true;
                                        estadoValido = true; ;
                                    }else if (estado.Equals("iinactivo", StringComparison.OrdinalIgnoreCase))
                                    {
                                        estadoParsed = false; ;
                                        estadoValido = true;
                                    }
                                    else
                                    {
                                        estadoValido = bool.TryParse(estado, out estadoParsed);
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
                                                using (SqlCommand cmd = new SqlCommand("InsertUsuario", sqlConnection))
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
                                                    cmd.Parameters.AddWithValue("@FechaDeCreacion", fechaDeCreacionValida);
                                                    cmd.Parameters.AddWithValue("@Estado", estadoValido);
                                                    cmd.ExecuteNonQuery();
                                                }
                                        }
  
                                 }
                                    linea++;
                                }
                            }

                    }
                    catch (Exception exc)
                    {
                        lblResultado.Text= exc.Message;
                        allRowErrors.Add($"Error general al procesar el archivo : {exc.Message}");
                    }


                }
                else if (tuArchivo.FileName.Contains(".xls"))
                {
                    try
                    {
                        using (var package = new OfficeOpenXml
                                                .ExcelPackage(tuArchivo
                                                .PostedFile.InputStream))
                        {
                            var worksheet = package.Workbook.Worksheets[0];
                            int rowCount = worksheet.Dimension.Rows;
                            string connBd = ConfigurationManager
                                           .ConnectionStrings["UserConnection"]
                                           .ConnectionString;
                            using (SqlConnection connUser = new SqlConnection(connBd))
                            {
                                connUser.Open();
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
                                        using (SqlCommand cmd = new SqlCommand("InsertUsuario", connUser))
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
                                        /*lblResultado.Text*/
                                        string mensajeErrorDetallado = string.Format(MessageResponse.messageErrorDetail, row);
                                        foreach (string error in currentValidationResult.Errors)
                                        {
                                            allRowErrors.Add(mensajeErrorDetallado + error);
                                        }
                                        if (allRowErrors.Any())
                                        {
                                            lblResultado.Text = "Se encontraron los siguientes errores:<br/>" + string.Join("<br/>", allRowErrors);
                                        }
                                    }
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        lblResultado.Text = "Error" + ex.Message;
                    }

                }

            }
            else
            {
                lblResultado.Text = "Por favor cargue el archivo para subir";
            }
        }
    }
}