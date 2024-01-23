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
   public class CD_Categoria
    {
        public List<Categoria> ToList() // Esta List retiene los atributos de la clase "CATEGORIA".
        {
            List<Categoria> list = new List<Categoria>();

            using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder(); // Este StringBuilder se encarga de LEER las lineas de codigo en el query del SQL que CONTIENE los datos de los CATEGORIAS.
                    query.AppendLine("select IdCategoria, Descripcion, Estado from CATEGORIA");

                    SqlCommand command = new SqlCommand(query.ToString(), connection);
                    command.CommandType = CommandType.Text;

                    connection.Open();

                    using (SqlDataReader d_reader = command.ExecuteReader())
                    {
                        while (d_reader.Read())
                        {
                            list.Add(new Categoria()
                            {
                                IdCategoria = Convert.ToInt32(d_reader["IdCategoria"]),
                                Descripcion = d_reader["Descripcion"].ToString(),
                                Estado = Convert.ToBoolean(d_reader["Estado"])
                            });
                        }
                    }
                }

                catch (Exception e)
                {
                    list = new List<Categoria>();
                }
            }

            return list;
        }

        public int Registrar_Categoria(Categoria objCat, out string Mensaje) // REGISTRAR los CATEGORIAS con parametros de entrada "objCat" y salida "Mensaje".
        {
            int new_idCategoria = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
                {

                    SqlCommand command = new SqlCommand("SP_REGISTRARCATEGORIA".ToString(), connection);

                    // Declarando paramentros de entrada (input)
                    command.Parameters.AddWithValue("Descripcion", objCat.Descripcion);
                    command.Parameters.AddWithValue("Estado", objCat.Estado);

                    // Declarando paramentros de salida (output)
                    command.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    // Ejecutar el comando con los parametros de salida
                    command.ExecuteNonQuery();
                    new_idCategoria = Convert.ToInt32(command.Parameters["Resultado"].Value);
                    Mensaje = command.Parameters["Mensaje"].Value.ToString();
                }
            }

            catch (Exception e)
            {
                new_idCategoria = 0;
                Mensaje = e.Message;
            }

            return new_idCategoria; // Este retorno puede ser igual a los siguientes usando "respuesta" como un valor booleano.
        }

        public bool Editar_Categoria(Categoria objCat, out string Mensaje) // EDITAR los CATEGORIAS con parametros de entrada "objCat" y salida "Mensaje".
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
                {

                    SqlCommand command = new SqlCommand("SP_EDITARCATEGORIA".ToString(), connection);

                    // Declarando paramentros de entrada (input)
                    command.Parameters.AddWithValue("IdCategoria", objCat.IdCategoria);
                    command.Parameters.AddWithValue("Descripcion", objCat.Descripcion);
                    command.Parameters.AddWithValue("Estado", objCat.Estado);

                    // Declarando paramentros de salida (output)
                    command.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    // Ejecutar el comando con los parametros de salida
                    command.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(command.Parameters["Resultado"].Value);
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

        public bool Eliminar_Categoria(Categoria objCat, out string Mensaje) // ELIMINAR las CATEGORIAS con parametros de entrada "objCat" y salida "Mensaje".
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
                {

                    SqlCommand command = new SqlCommand("SP_ELIMINARCATEGORIA".ToString(), connection);

                    // Declarando paramentros de entrada (input)
                    command.Parameters.AddWithValue("IdCategoria", objCat.IdCategoria); // Solo se requiere el IdCategoria para identificar a la CATEGORIA que se quiera ELIMINAR

                    // Declarando paramentros de salida (output)
                    command.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    // Ejecutar el comando con los parametros de salida
                    command.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(command.Parameters["Resultado"].Value);
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
