using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Usuario
    {
        public List <Usuario> ToList() // Esta List retiene los atributos de la clase "Usuario".
        {
            List<Usuario> list = new List<Usuario>();

            using(SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder(); // Este StringBuilder se encarga de LEER las lineas de codigo en el query del SQL que CONTIENE los datos de los Usuarios.
                    query.AppendLine("select us.IdUsuario, us.Documento, us.NombreCompleto, us.Correo, us.Clave, us.Estado, ro.IdRol, ro.Descripcion from USUARIO us");
                    query.AppendLine("inner join ROL ro on ro.IdRol = us.IdRol");

                    SqlCommand command = new SqlCommand(query.ToString(), connection);
                    command.CommandType = CommandType.Text;

                    connection.Open();

                    using(SqlDataReader d_reader = command.ExecuteReader())
                    {
                        while (d_reader.Read())
                        {
                            list.Add(new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(d_reader["IdUsuario"]),
                                Documento = d_reader["Documento"].ToString(),
                                NombreCompleto = d_reader["NombreCompleto"].ToString(),
                                Correo = d_reader["Correo"].ToString(),
                                Clave = d_reader["Clave"].ToString(),
                                Estado = Convert.ToBoolean(d_reader["Estado"]),
                                objRol = new Rol() { IdRol = Convert.ToInt32(d_reader["IdRol"]), Descripcion = d_reader["Descripcion"].ToString() }
                            });                
                        }
                    }
                }

                catch(Exception e)
                {
                    list = new List<Usuario>();
                }
            }

            return list;
        }

        public int Registrar_Usuario(Usuario objUsu, out string Mensaje) // REGISTRAR los usuarios con parametros de entrada "objUsu" y salida "Mensaje".
        {
            int new_idusuario = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
                {

                    SqlCommand command = new SqlCommand("SP_REGISTRARUSUARIO".ToString(), connection);

                    // Declarando paramentros de entrada (input)
                    command.Parameters.AddWithValue("Documento", objUsu.Documento);
                    command.Parameters.AddWithValue("NombreCompleto", objUsu.NombreCompleto);
                    command.Parameters.AddWithValue("Correo", objUsu.Correo);
                    command.Parameters.AddWithValue("Clave", objUsu.Clave);
                    command.Parameters.AddWithValue("IdRol", objUsu.objRol.IdRol);
                    command.Parameters.AddWithValue("Estado", objUsu.Estado);

                    // Declarando paramentros de salida (output)
                    command.Parameters.Add("IdUsuarioResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    // Ejecutar el comando con los parametros de salida
                    command.ExecuteNonQuery();
                    new_idusuario = Convert.ToInt32(command.Parameters["IdUsuarioResultado"].Value);
                    Mensaje = command.Parameters["Mensaje"].Value.ToString();
                }
            }

            catch(Exception e)
            {
                new_idusuario = 0;
                Mensaje = e.Message;
            }

            return new_idusuario; // Este retorno puede ser igual a los siguientes usando "respuesta" como un valor booleano.
        }

        public bool Editar_Usuario(Usuario objUsu, out string Mensaje) // EDITAR los usuarios con parametros de entrada "objUsu" y salida "Mensaje".
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
                {

                    SqlCommand command = new SqlCommand("SP_EDITARUSUARIO".ToString(), connection);

                    // Declarando paramentros de entrada (input)
                    command.Parameters.AddWithValue("IdUsuario", objUsu.IdUsuario);
                    command.Parameters.AddWithValue("Documento", objUsu.Documento);
                    command.Parameters.AddWithValue("NombreCompleto", objUsu.NombreCompleto);
                    command.Parameters.AddWithValue("Correo", objUsu.Correo);
                    command.Parameters.AddWithValue("Clave", objUsu.Clave);
                    command.Parameters.AddWithValue("IdRol", objUsu.objRol.IdRol);
                    command.Parameters.AddWithValue("Estado", objUsu.Estado);

                    // Declarando paramentros de salida (output)
                    command.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    // Ejecutar el comando con los parametros de salida
                    command.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(command.Parameters["Respuesta"].Value);
                    Mensaje = command.Parameters["Mensaje"].Value.ToString();
                }
            }

            catch (Exception e)
            {
                respuesta = false;
                Mensaje = e.Message;
            }

            return respuesta;
        }

        public bool Eliminar_Usuario(Usuario objUsu, out string Mensaje) // ELIMINAR los usuarios con parametros de entrada "objUsu" y salida "Mensaje".
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
                {

                    SqlCommand command = new SqlCommand("SP_ELIMINARUSUARIO".ToString(), connection);

                    // Declarando paramentros de entrada (input)
                    command.Parameters.AddWithValue("IdUsuario", objUsu.IdUsuario); // Solo se requiere el IdUsuario para identificar al usuario que se quiera ELIMINAR

                    // Declarando paramentros de salida (output)
                    command.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    // Ejecutar el comando con los parametros de salida
                    command.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(command.Parameters["Respuesta"].Value);
                    Mensaje = command.Parameters["Mensaje"].Value.ToString();
                }
            }

            catch (Exception e)
            {
                respuesta = false;
                Mensaje = e.Message;
            }

            return respuesta;
        }
    }
}
