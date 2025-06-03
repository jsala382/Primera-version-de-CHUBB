using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Modelos;
using BL.Validation;

namespace BL.Servicio
{
    public interface IUsuarioService
    {
        List<UsuariosDto> GetUsuarios();
        UsuariosDto CreateUserDto(UsuariosDto userDto);

        UsuariosDto UpdaterUserDto(UsuariosDto userDto);

       
    }
}
