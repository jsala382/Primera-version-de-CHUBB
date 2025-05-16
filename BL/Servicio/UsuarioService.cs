using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Modelos;
using DAC.Repositorio;

namespace BL.Servicio
{
    public class UsuarioService : IUsuarioService
    {
        public readonly IUsuarioRepository _usuRepos;
        public UsuarioService(IUsuarioRepository usuRepos)
        {
            _usuRepos = usuRepos;
        }
        public List<UsuariosDto> GetUsuarios()
        {
            return _usuRepos.GetAllUsers();
        }
    }
}
