using BE.Modelos;
using OfficeOpenXml.Drawing.Chart;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public UsuariosDto CreateUser(UsuariosDto userDto)
        {
            using (SqlConnection connectioUserCreate = new SqlConnection(_conexionUsuarioRepository))
            {
                connectioUserCreate.Open();
                SqlCommand commandUserCreate = new SqlCommand("InsertUsuario", connectioUserCreate);
                commandUserCreate.CommandType = CommandType.StoredProcedure;
                commandUserCreate.Parameters.AddWithValue("@Identificacion", userDto.Identificacion);
                commandUserCreate.Parameters.AddWithValue("@NombreCompleto", userDto.NombreCompleto);
                commandUserCreate.Parameters.AddWithValue("@PrimerNombre", userDto.PrimerNombre);
                commandUserCreate.Parameters.AddWithValue("@SegundoNombre", userDto.SegundoNombre);
                commandUserCreate.Parameters.AddWithValue("@PrimerApellido", userDto.PrimerApellido);
                commandUserCreate.Parameters.AddWithValue("@SegundoApellido", userDto.SegundoApellido);
                commandUserCreate.Parameters.AddWithValue("@FechaNacimiento", userDto.FechaNacimiento);
                commandUserCreate.Parameters.AddWithValue("@Direccion", userDto.Direccion);
                commandUserCreate.Parameters.AddWithValue("@NumeroCelular", userDto.NumeroCelular);
                commandUserCreate.Parameters.AddWithValue("@CorreoElectronico", userDto.CorreoElectronico);
                commandUserCreate.Parameters.AddWithValue("@FechaDeCreacion", userDto.FechaDeCreacion);
                commandUserCreate.Parameters.AddWithValue("@Estado", userDto.Estado);
                commandUserCreate.ExecuteNonQuery();
            }
            return userDto;
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
                        FechaDeCreacion = Convert.ToDateTime(reader["FechaDeCreacion"].ToString() ?? ""),
                        Estado = Convert.ToBoolean(reader["Estado"])
                    };
                    listUsertDto.Add(usuarios);
                }
            }
            return listUsertDto;
        }

       

        public UsuariosDto UpdateUser(UsuariosDto userDto)
        {
            using (SqlConnection connUserUpdate = new SqlConnection(_conexionUsuarioRepository))
            {
                SqlCommand commandUserUpdate = new SqlCommand("UpdateUsuario", connUserUpdate);
                commandUserUpdate.CommandType = CommandType.StoredProcedure;
                commandUserUpdate.Parameters.AddWithValue("@identificacion", userDto.Identificacion);
                commandUserUpdate.Parameters.AddWithValue("@nombreCompleto", userDto.NombreCompleto);
                commandUserUpdate.Parameters.AddWithValue("@primerNombre", userDto.PrimerNombre);
                commandUserUpdate.Parameters.AddWithValue("@segundoNombre", userDto.SegundoNombre);
                commandUserUpdate.Parameters.AddWithValue("@primerApellido", userDto.PrimerApellido);
                commandUserUpdate.Parameters.AddWithValue("@segundoApellido", userDto.SegundoApellido);
                commandUserUpdate.Parameters.AddWithValue("@fechaNacimiento", userDto.FechaNacimiento);
                commandUserUpdate.Parameters.AddWithValue("@direccion", userDto.Direccion);
                commandUserUpdate.Parameters.AddWithValue("@numeroCelular", userDto.NumeroCelular);
                commandUserUpdate.Parameters.AddWithValue("@correoElectronico", userDto.CorreoElectronico ?? string.Empty);
                commandUserUpdate.Parameters.AddWithValue("@fechaDeCreacion", userDto.FechaDeCreacion);
                commandUserUpdate.Parameters.AddWithValue("@estado", userDto.Estado);
                connUserUpdate.Open();
                commandUserUpdate.ExecuteNonQuery();
                return userDto;
            }
        }
    }
}
