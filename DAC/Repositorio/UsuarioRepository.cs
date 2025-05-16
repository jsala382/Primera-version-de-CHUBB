using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Modelos;
using System.Configuration;

namespace DAC.Repositorio
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _conexionUsuarioRepository;


        public UsuarioRepository()
        {
            _conexionUsuarioRepository = ConfigurationManager.
                                         ConnectionStrings["UserConnection"]
                                         .ConnectionString;

        }
        public List<UsuariosDto> GetAllUsers()
        {

            List<UsuariosDto> listUsertDto = new List<UsuariosDto>();
            using (SqlConnection connUser = new SqlConnection(_conexionUsuarioRepository))
            {
                connUser.Open();
                SqlCommand commandUserSql = new SqlCommand("SelectUsers", connUser);
                commandUserSql.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = commandUserSql.ExecuteReader();
                while (reader.Read())
                {
                    UsuariosDto usuarios = new UsuariosDto
                    {
                        Identificacion = reader["Identificacion"].ToString(),
                        NombreCompleto = reader["NombreCompleto"].ToString(),
                        PrimerNombre = reader["PrimerNombre"].ToString(),
                        SegundoNombre = reader["SegundoNombre"].ToString(),
                        PrimerApellido = reader["PrimerApellido"].ToString(),
                        SegundoApellido = reader["SegundoApellido"].ToString(),
                        FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"].ToString() ?? ""),
                        Direccion = reader["Direccion"].ToString(),
                        NumeroCelular = reader["NumeroCelular"].ToString(),
                        CorreoElectronico = reader["CorreoElectronico"].ToString(),
                        //FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"].ToString() ?? ""),
                        Estado = Convert.ToBoolean(reader["Estado"])
                    };
                    listUsertDto.Add(usuarios);
                }
            }
            return listUsertDto;
        }
    }
}
