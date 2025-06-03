using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Modelos;

namespace DAC.Repositorio
{
    public interface IUsuarioRepository
    {
        List<UsuariosDto> GetAllUsers();
        UsuariosDto CreateUser(UsuariosDto userDto);

        UsuariosDto UpdateUser(UsuariosDto userDto);
        

    }
}
