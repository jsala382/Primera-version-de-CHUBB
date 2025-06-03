using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE.Modelos;
using BL.Servicio;
using DAC.Repositorio;
using OfficeOpenXml;



namespace CHUBBWebForms
{
    public partial class _Default : System.Web.UI.Page
    {
        private readonly IUsuarioService _usuarioService;
        public _Default()
        {
            _usuarioService = new UsuarioService(new UsuarioRepository());
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getUserTable();
            }
        }

        protected void EditingUser(object sender, GridViewEditEventArgs e)
        {
            gvUsuarios.EditIndex = e.NewEditIndex;
            getUserTable();
        }

        protected void UpdatingUser(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvUsuarios.Rows[e.RowIndex];
            string identificacion = ((TextBox)row.FindControl("txtIdentificacionGrid")).Text;
            string nombreCompleto = ((TextBox)row.FindControl("txtNombreCompleto")).Text;
            string primerNombre = ((TextBox)row.FindControl("txtPrimerNombre")).Text;
            string segundoNombre = ((TextBox)row.FindControl("txtSegundoNombre")).Text;
            string primerApellido = ((TextBox)row.FindControl("txtPrimerApellido")).Text;
            string segundoApellido = ((TextBox)row.FindControl("txtSegundoApellido")).Text;
            DateTime fechaNacimientoPersed;
            string fechaNacimiento = ((TextBox)row.FindControl("txtFechaNacimiento")).Text;
            DateTime.TryParse(fechaNacimiento, out fechaNacimientoPersed);
            string direccion = ((TextBox)row.FindControl("txtDireccion")).Text;
            string numeroCelular = ((TextBox)row.FindControl("txtNumeroCelular")).Text;
            string correoElectronico = ((TextBox)row.FindControl("txtCorreoElectronico")).Text;
            DateTime fechaCreacionParsed;
            string fechaCreacion = ((TextBox)row.FindControl("txtFechaCreacion")).Text;
            DateTime.TryParse(fechaCreacion, out fechaCreacionParsed);
            bool estadoParsed;
            string estado = ((TextBox)row.FindControl("txtEstado")).Text;
            Boolean.TryParse(estado, out estadoParsed);
            UsuariosDto userDto = new UsuariosDto
            {
                Identificacion = identificacion,
                NombreCompleto = nombreCompleto,
                PrimerNombre = primerNombre,
                SegundoNombre = segundoNombre,
                PrimerApellido = primerApellido,
                SegundoApellido = segundoApellido,
                FechaNacimiento = fechaNacimientoPersed,
                Direccion = direccion,
                NumeroCelular = numeroCelular,
                CorreoElectronico = correoElectronico,
                FechaDeCreacion = fechaCreacionParsed,
                Estado = estadoParsed
            };
            _usuarioService.UpdaterUserDto(userDto);
            gvUsuarios.EditIndex = -1;
            getUserTable();
        }

        public void getUserTable()
        {
            List<UsuariosDto> Users = _usuarioService.GetUsuarios();
            gvUsuarios.DataSource = Users;
            gvUsuarios.DataBind();
        }

        protected void InsertUser(object sender, EventArgs e)
        {
            try
            {
                UsuariosDto userDto = new UsuariosDto
                {
                    Identificacion = txtIdentificacion.Text.Trim(),
                    NombreCompleto = txtNombreCompleto.Text.Trim(),
                    PrimerNombre = txtPrimerNombre.Text.Trim(),
                    SegundoNombre = txtSegundoNombre.Text.Trim(),
                    PrimerApellido = txtPrimerApellido.Text.Trim(),
                    SegundoApellido = txtSegundoApellido.Text.Trim(),
                    FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text.ToString()),
                    Direccion = txtDireccion.Text.Trim(),
                    NumeroCelular = txtNumeroCelular.Text.Trim(),
                    CorreoElectronico = txtCorreoElectronico.Text.Trim(),
                    FechaDeCreacion = DateTime.Parse(txtFechaDeCreacion.Text.ToString()),
                    Estado = ckEstado.Checked
                };
                UsuariosDto userDtoCreated = _usuarioService.CreateUserDto(userDto);
                getUserTable();

            }
            catch (Exception ex) {
                lblMensaje.Text = $"Error al crear el Usuario.m " + ex.Message;
            }
        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            List<UsuariosDto> userData = _usuarioService.GetUsuarios();
            StringBuilder contentArchive = new StringBuilder();
            foreach (UsuariosDto userDto in userData)
            {
                contentArchive.AppendLine(userDto.ToFixedWidthString());
            }
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.AddHeader("Content-Disposition", "atachment; filename=user.txt");
            Response.Write(contentArchive.ToString());
            Response.End();
        }

        protected void CancellingEditionUser(object sender, GridViewCancelEditEventArgs e)
        {
            gvUsuarios.EditIndex = -1;
            getUserTable();
        }

        protected void DownloadFileXls(object sender, EventArgs e)
        {
            List<UsuariosDto> userList = _usuarioService.GetUsuarios();
            using( var package = new OfficeOpenXml.ExcelPackage())
            {
                var workSheet = package.Workbook.Worksheets.Add("Usuarios");
                workSheet.Cells[1, 1].Value = "Identificacion";
                workSheet.Cells[1, 2].Value = "Nombre Completo";
                workSheet.Cells[1, 3].Value = "Primer Nombre";
                workSheet.Cells[1, 4].Value = "Segundo Nombre";
                workSheet.Cells[1, 5].Value = "Primer Apellido";
                workSheet.Cells[1, 6].Value = "Segundo Apellido";
                workSheet.Cells[1, 7].Value = "Fecha Nacimiento";
                workSheet.Cells[1, 8].Value = "Direccion";
                workSheet.Cells[1, 9].Value = "Numero Celular";
                workSheet.Cells[1, 10].Value = "Correo Electronico";
                workSheet.Cells[1, 11].Value = "Fecha Creacion";
                workSheet.Cells[1, 12].Value = "Estado";

                int userRow = 2;
                foreach (var users in userList)
                {
                    workSheet.Cells[userRow, 1].Value = users.Identificacion;
                    workSheet.Cells[userRow, 2].Value = users.NombreCompleto;
                    workSheet.Cells[userRow, 3].Value = users.PrimerNombre;
                    workSheet.Cells[userRow, 4].Value = users.SegundoNombre;
                    workSheet.Cells[userRow, 5].Value = users.PrimerApellido;
                    workSheet.Cells[userRow, 6].Value = users.SegundoApellido;

                    workSheet.Cells[userRow, 7].Value = users.FechaNacimiento;
                    workSheet.Cells[userRow, 7].Style.Numberformat.Format = "yyyy/dd/MM";

                    workSheet.Cells[userRow, 8].Value = users.Direccion;
                    workSheet.Cells[userRow, 9].Value = users.NumeroCelular;
                    workSheet.Cells[userRow, 10].Value = users.CorreoElectronico;

                    workSheet.Cells[userRow, 11].Value = users.FechaDeCreacion;
                    workSheet.Cells[userRow, 11].Style.Numberformat.Format = "yyyy/dd/MM";

                    if (users.Estado is bool estadoUser) {
                        workSheet.Cells[userRow, 12].Value = estadoUser  ? "Activo" : "Inactivo";
                    }
                   
                    userRow++;
                }
                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
                Response.Clear();
                Response.ContentType = "application/vmd.openxml-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachement; filename = Usuarios.xls");
                Response.BinaryWrite(package.GetAsByteArray());
                Response.End();
            }

        }



    }
}