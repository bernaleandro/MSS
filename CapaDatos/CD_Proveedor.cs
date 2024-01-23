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
    public class CD_Proveedor
    {
        public List<Proveedor> ToList() // Esta List retiene los atributos de la clase "Proveedor".
        {
            List<Proveedor> list = new List<Proveedor>();

            using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder(); // Este StringBuilder se encarga de LEER las lineas de codigo en el query del SQL que CONTIENE los datos de los Provedores.
                    query.AppendLine("select IdProveedor, Documento, RazonSocial, Correo, Telefono, Estado from PROVEEDOR");

                    SqlCommand command = new SqlCommand(query.ToString(), connection);
                    command.CommandType = CommandType.Text;

                    connection.Open();

                    using (SqlDataReader d_reader = command.ExecuteReader())
                    {
                        while (d_reader.Read())
                        {
                            list.Add(new Proveedor()
                            {
                                IdProveedor = Convert.ToInt32(d_reader["IdProveedor"]),
                                Documento = d_reader["Documento"].ToString(),
                                RazonSocial = d_reader["RazonSocial"].ToString(),
                                Correo = d_reader["Correo"].ToString(),
                                Telefono = d_reader["Telefono"].ToString(),
                                Estado = Convert.ToBoolean(d_reader["Estado"]),
                            });
                        }
                    }
                }

                catch (Exception e)
                {
                    list = new List<Proveedor>();
                }
            }

            return list;
        }

        public int Registrar_Proveedor(Proveedor objPvd, out string Mensaje) // REGISTRAR los Provedores con parametros de entrada "objPvd" y salida "Mensaje".
        {
            int new_idProveedor = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
                {

                    SqlCommand command = new SqlCommand("SP_REGISTRARPROVEEDOR".ToString(), connection);

                    // Declarando paramentros de entrada (input)
                    command.Parameters.AddWithValue("Documento", objPvd.Documento);
                    command.Parameters.AddWithValue("RazonSocial", objPvd.RazonSocial);
                    command.Parameters.AddWithValue("Correo", objPvd.Correo);
                    command.Parameters.AddWithValue("Telefono", objPvd.Correo);
                    command.Parameters.AddWithValue("Estado", objPvd.Estado);

                    // Declarando paramentros de salida (output)
                    command.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    // Ejecutar el comando con los parametros de salida
                    command.ExecuteNonQuery();
                    new_idProveedor = Convert.ToInt32(command.Parameters["Resultado"].Value);
                    Mensaje = command.Parameters["Mensaje"].Value.ToString();
                }
            }

            catch (Exception e)
            {
                new_idProveedor = 0;
                Mensaje = e.Message;
            }

            return new_idProveedor; // Este retorno puede ser igual a los siguientes usando "respuesta" como un valor booleano.
        }

        public bool Editar_Proveedor(Proveedor objPvd, out string Mensaje) // EDITAR los Provedores con parametros de entrada "objPvd" y salida "Mensaje".
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
                {

                    SqlCommand command = new SqlCommand("SP_EDITARPROVEEDOR".ToString(), connection);

                    // Declarando paramentros de entrada (input)
                    command.Parameters.AddWithValue("IdProveedor", objPvd.IdProveedor);
                    command.Parameters.AddWithValue("Documento", objPvd.Documento);
                    command.Parameters.AddWithValue("RazonSocial", objPvd.RazonSocial);
                    command.Parameters.AddWithValue("Correo", objPvd.Correo);
                    command.Parameters.AddWithValue("Telefono", objPvd.Telefono);
                    command.Parameters.AddWithValue("Estado", objPvd.Estado);

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

        public bool Eliminar_Proveedor(Proveedor objPvd, out string Mensaje) // ELIMINAR los Provedores con parametros de entrada "objPvd" y salida "Mensaje".
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
                {

                    SqlCommand command = new SqlCommand("SP_ELIMINARPROVEEDOR".ToString(), connection);

                    // Declarando paramentros de entrada (input)
                    command.Parameters.AddWithValue("IdProveedor", objPvd.IdProveedor); // Solo se requiere el IdProveedor para identificar al Proveedor que se quiera ELIMINAR

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
