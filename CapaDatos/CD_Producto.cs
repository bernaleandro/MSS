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
    public class CD_Producto
    {
        public List<Producto> ToList() // Esta List retiene los atributos de la clase "Producto".
        {
            List<Producto> list = new List<Producto>();

            using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder(); // Este StringBuilder se encarga de LEER las lineas de codigo en el query del SQL que CONTIENE los datos de los Productos.
                    query.AppendLine("select IdProducto, Codigo, Nombre, pr.Descripcion, ca.IdCategoria,ca.Descripcion[DescripcionCategoria] , Stock, PrecioCompra, PrecioVenta, pr.Estado from PRODUCTO pr");
                    query.AppendLine("inner join CATEGORIA ca on ca.IdCategoria = pr.IdCategoria");

                    SqlCommand command = new SqlCommand(query.ToString(), connection);
                    command.CommandType = CommandType.Text;

                    connection.Open();

                    using (SqlDataReader d_reader = command.ExecuteReader())
                    {
                        while (d_reader.Read())
                        {
                            list.Add(new Producto()
                            {
                                IdProducto = Convert.ToInt32(d_reader["IdProducto"]),
                                Codigo = d_reader["Codigo"].ToString(),
                                Nombre = d_reader["Nombre"].ToString(),
                                Descripcion = d_reader["Descripcion"].ToString(),
                                objCategoria = new Categoria() { IdCategoria = Convert.ToInt32(d_reader["IdCategoria"]), Descripcion = d_reader["DescripcionCategoria"].ToString()},
                                Stock = Convert.ToInt32(d_reader["Stock"].ToString()),
                                PrecioCompra = Convert.ToDecimal(d_reader["PrecioCompra"].ToString()),
                                PrecioVenta = Convert.ToDecimal(d_reader["PrecioVenta"].ToString()),
                                Estado = Convert.ToBoolean(d_reader["Estado"]),                              
                            });
                        }
                    }
                }

                catch (Exception e)
                {
                    list = new List<Producto>();
                }
            }

            return list;
        }

        public int Registrar_Producto(Producto objPrd, out string Mensaje) // REGISTRAR los Productos con parametros de entrada "objPrd" y salida "Mensaje".
        {
            int new_idProducto = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
                {

                    SqlCommand command = new SqlCommand("SP_REGISTRARPRODUCTO".ToString(), connection);

                    // Declarando paramentros de entrada (input)
                    command.Parameters.AddWithValue("Codigo", objPrd.Codigo);
                    command.Parameters.AddWithValue("Nombre", objPrd.Nombre);
                    command.Parameters.AddWithValue("Descripcion", objPrd.Descripcion);
                    command.Parameters.AddWithValue("IdCategoria", objPrd.objCategoria.IdCategoria);
                    command.Parameters.AddWithValue("Estado", objPrd.Estado);

                    // Declarando paramentros de salida (output)
                    command.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    // Ejecutar el comando con los parametros de salida
                    command.ExecuteNonQuery();
                    new_idProducto = Convert.ToInt32(command.Parameters["Resultado"].Value);
                    Mensaje = command.Parameters["Mensaje"].Value.ToString();
                }
            }

            catch (Exception e)
            {
                new_idProducto = 0;
                Mensaje = e.Message;
            }

            return new_idProducto; // Este retorno puede ser igual a los siguientes usando "respuesta" como un valor booleano.
        }

        public bool Editar_Producto(Producto objPrd, out string Mensaje) // EDITAR los Productos con parametros de entrada "objPrd" y salida "Mensaje".
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
                {

                    SqlCommand command = new SqlCommand("SP_EDITARPRODUCTO".ToString(), connection);

                    // Declarando paramentros de entrada (input)
                    command.Parameters.AddWithValue("IdProducto", objPrd.IdProducto);
                    command.Parameters.AddWithValue("Codigo", objPrd.Codigo);
                    command.Parameters.AddWithValue("Nombre", objPrd.Nombre);
                    command.Parameters.AddWithValue("Descripcion", objPrd.Descripcion);
                    command.Parameters.AddWithValue("IdCategoria", objPrd.objCategoria.IdCategoria);
                    command.Parameters.AddWithValue("Estado", objPrd.Estado);

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

        public bool Eliminar_Producto(Producto objPrd, out string Mensaje) // ELIMINAR los Productos con parametros de entrada "objPrd" y salida "Mensaje".
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
                {

                    SqlCommand command = new SqlCommand("SP_ELIMINARPRODUCTO".ToString(), connection);

                    // Declarando paramentros de entrada (input)
                    command.Parameters.AddWithValue("IdProducto", objPrd.IdProducto); // Solo se requiere el IdProducto para identificar al Producto que se quiera ELIMINAR

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
