using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Modelos;
using BL.Validation;
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

        public UsuariosDto CreateUserDto(UsuariosDto userDto)
        {
            return _usuRepos.CreateUser(userDto);
        }

        public List<UsuariosDto> GetUsuarios()
        {
            return _usuRepos.GetAllUsers();
        }

        public UsuariosDto UpdaterUserDto(UsuariosDto userDto)
        {
            return _usuRepos.UpdateUser(userDto);
        }
    }
}
