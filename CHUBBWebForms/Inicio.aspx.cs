using BL.Common;
using BL.ServicioImportacion;
using BL.Validation;
using DAC.Repositorio;
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
                    UsuarioImportServiceTxt usuarioImportService = new UsuarioImportServiceTxt();
                    Stream fileStream = tuArchivo.PostedFile.InputStream;
                    allRowErrors = usuarioImportService.ImportUsuarioDesdeTxt(fileStream);
                    if(allRowErrors.Count > 0)
                    {
                        lblResultado.Text = "Se encontraron los siguientes problemas"+ allRowErrors;
                    }
                    else
                    {
                        lblResultado.Text = "¡Importación de usuarios completada exitosamente!";
                    }

                }
                else if (tuArchivo.FileName.Contains(".xls"))
                {
                    UsuarioImportServiceXls usuarioImportServiceXls = new UsuarioImportServiceXls();
                    Stream fileStream = tuArchivo.PostedFile.InputStream;
                    allRowErrors = usuarioImportServiceXls.ImportUserDesdeXls(fileStream); 
                    if( allRowErrors.Count > 0)
                    {
                        lblResultado.Text = "Se encontraron los siguientes problemas" + allRowErrors;
                    }
                    else
                    {
                        lblResultado.Text = "¡Importación de usuarios completada exitosamente!";
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