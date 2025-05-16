using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE.Modelos;
using BL.Servicio;
using DAC.Repositorio;



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
            if(!IsPostBack)
            {
                getUserTable();
            }
        }

        public void getUserTable()
        {
            List<UsuariosDto> Users = _usuarioService.GetUsuarios();
            gvUsuarios.DataSource = Users;
            gvUsuarios.DataBind();
        }

    }
}