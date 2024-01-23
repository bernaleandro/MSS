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
    public class CD_Cliente
    {
        public List<Cliente> ToList() // Esta List retiene los atributos de la clase "CLIENTE".
        {
            List<Cliente> list = new List<Cliente>();

            using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder(); // Este StringBuilder se encarga de LEER las lineas de codigo en el query del SQL que CONTIENE los datos de los CLIENTES.
                    query.AppendLine("select IdCliente, Documento, NombreCompleto, Correo, Telefono, Estado from CLIENTE");

                    SqlCommand command = new SqlCommand(query.ToString(), connection);
                    command.CommandType = CommandType.Text;

                    connection.Open();

                    using (SqlDataReader d_reader = command.ExecuteReader())
                    {
                        while (d_reader.Read())
                        {
                            list.Add(new Cliente()
                            {
                                IdCliente = Convert.ToInt32(d_reader["IdCliente"]),
                                Documento = d_reader["Documento"].ToString(),
                                NombreCompleto = d_reader["NombreCompleto"].ToString(),
                                Correo = d_reader["Correo"].ToString(),
                                Telefono = d_reader["Telefono"].ToString(),
                                Estado = Convert.ToBoolean(d_reader["Estado"]),                              
                            });
                        }
                    }
                }

                catch (Exception e)
                {
                    list = new List<Cliente>();
                }
            }

            return list;
        }

        public int Registrar_Cliente(Cliente objCli, out string Mensaje) // REGISTRAR los CLIENTES con parametros de entrada "objCli" y salida "Mensaje".
        {
            int new_idCliente = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
                {

                    SqlCommand command = new SqlCommand("SP_REGISTRARCLIENTE".ToString(), connection);

                    // Declarando paramentros de entrada (input)
                    command.Parameters.AddWithValue("Documento", objCli.Documento);
                    command.Parameters.AddWithValue("NombreCompleto", objCli.NombreCompleto);
                    command.Parameters.AddWithValue("Correo", objCli.Correo);
                    command.Parameters.AddWithValue("Telefono", objCli.Telefono);
                    command.Parameters.AddWithValue("Estado", objCli.Estado);

                    // Declarando paramentros de salida (output)
                    command.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    // Ejecutar el comando con los parametros de salida
                    command.ExecuteNonQuery();
                    new_idCliente = Convert.ToInt32(command.Parameters["Resultado"].Value);
                    Mensaje = command.Parameters["Mensaje"].Value.ToString();
                }
            }

            catch (Exception e)
            {
                new_idCliente = 0;
                Mensaje = e.Message;
            }

            return new_idCliente; // Este retorno puede ser igual a los siguientes usando "respuesta" como un valor booleano.
        }

        public bool Editar_Cliente(Cliente objCli, out string Mensaje) // EDITAR los CLIENTES con parametros de entrada "objCli" y salida "Mensaje".
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
                {

                    SqlCommand command = new SqlCommand("SP_EDITARCLIENTE".ToString(), connection);

                    // Declarando paramentros de entrada (input)
                    command.Parameters.AddWithValue("IdCliente", objCli.IdCliente);
                    command.Parameters.AddWithValue("Documento", objCli.Documento);
                    command.Parameters.AddWithValue("NombreCompleto", objCli.NombreCompleto);
                    command.Parameters.AddWithValue("Correo", objCli.Correo);
                    command.Parameters.AddWithValue("Telefono", objCli.Telefono);
                    command.Parameters.AddWithValue("Estado", objCli.Estado);

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

        public bool Eliminar_Cliente(Cliente objCli, out string Mensaje) // ELIMINAR los CLIENTES sin necesidad de parametros porque es una clase independiente, no es de mucha importancia eliminar al CLIENTE
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
                {

                    SqlCommand command = new SqlCommand("delete from CLIENTE where IdCliente = @Id", connection);

                    command.Parameters.AddWithValue("@Id", objCli.IdCliente); // Solo se requiere el IdCliente para identificar al CLIENTE que se quiera ELIMINAR
                    command.CommandType = CommandType.Text;

                    connection.Open();

                    respuesta = command.ExecuteNonQuery() > 0 ? true : false;
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
