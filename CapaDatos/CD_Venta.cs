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
    public class CD_Venta
    {
        public int Obtener_Correlativo() // Se encarga de GENERAR un valor entero cada vez que se REGISTRA una nueva VENTA para identificarlo. En la clase se conoce como "NumeroDocumento".
        {
            int idcorrelativo = 0;

            using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder(); // Este StringBuilder se encarga de LEER las lineas de codigo en el query del SQL que CONTIENE los datos de los Usuarios.
                    query.AppendLine("select count(*) + 1 from VENTA");

                    SqlCommand command = new SqlCommand(query.ToString(), connection);
                    command.CommandType = CommandType.Text;

                    connection.Open();

                    idcorrelativo = Convert.ToInt32(command.ExecuteScalar());
                }

                catch (Exception e)
                {
                    idcorrelativo = 0;
                }
            }

            return idcorrelativo;
        }

        public bool Reducir_Stock(int idproducto, int cantidad) // Se encarga de REDUCIR la cantidad del STOCK disponible cada vez que se AGREGA una nueva VENTA.
        {
            bool respuesta = true;

            using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder(); // Este StringBuilder se encarga de LEER las lineas de codigo en el query del SQL que CONTIENE los datos de los Usuarios.
                    query.AppendLine("update PRODUCTO set Stock = Stock - @cantidad WHERE IdProducto = @IdProducto");

                    SqlCommand command = new SqlCommand(query.ToString(), connection);

                    command.Parameters.AddWithValue("@cantidad", cantidad);
                    command.Parameters.AddWithValue("@idproducto", idproducto);

                    command.CommandType = CommandType.Text;

                    connection.Open();

                    respuesta = command.ExecuteNonQuery() > 0 ? true : false; // Realiza un CONTEO de filas AFECTADAS. 
                }

                catch (Exception e)
                {
                    respuesta = false;
                }
            }

            return respuesta;
        }

        public bool Devolver_Stock(int idproducto, int cantidad) // Se encarga de DEVOLVER la cantidad del STOCK disponible cada vez que se CANCELA una nueva VENTA.
        {
            bool respuesta = true;

            using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder(); // Este StringBuilder se encarga de LEER las lineas de codigo en el query del SQL que CONTIENE los datos de los Usuarios.
                    query.AppendLine("update PRODUCTO set Stock = Stock + @cantidad WHERE IdProducto = @IdProducto");

                    SqlCommand command = new SqlCommand(query.ToString(), connection);

                    command.Parameters.AddWithValue("@cantidad", cantidad);
                    command.Parameters.AddWithValue("@idproducto", idproducto);

                    command.CommandType = CommandType.Text;

                    connection.Open();

                    respuesta = command.ExecuteNonQuery() > 0 ? true : false; // Realiza un CONTEO de filas AFECTADAS. 
                }

                catch (Exception e)
                {
                    respuesta = false;
                }
            }

            return respuesta;
        }

        public bool Registrar_Venta(Venta objCom, DataTable DetalleVenta, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
            {
                try
                {
                    SqlCommand command = new SqlCommand("SP_REGISTRARVENTA".ToString(), connection);

                    // Declarando paramentros de entrada (input)
                    command.Parameters.AddWithValue("IdUsuario", objCom.objUsuario.IdUsuario);
                    command.Parameters.AddWithValue("TipoDocumento", objCom.TipoDocumento);
                    command.Parameters.AddWithValue("NumeroDocumento", objCom.NumeroDocumento);
                    command.Parameters.AddWithValue("DocumentoCliente", objCom.DocumentoCliente);
                    command.Parameters.AddWithValue("NombreCliente", objCom.NombreCliente);
                    command.Parameters.AddWithValue("MontoPago", objCom.MontoPago);
                    command.Parameters.AddWithValue("MontoCambio", objCom.MontoCambio);
                    command.Parameters.AddWithValue("MontoTotal", objCom.MontoTotal);
                    command.Parameters.AddWithValue("DetalleVenta", DetalleVenta);

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

                catch (Exception e)
                {
                    respuesta = false;
                    Mensaje = e.Message;
                }
            }

            return respuesta;
        }

        public Venta Obtener_Venta(string numero)
        {
            Venta objVenta = new Venta();

            using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder(); // Este StringBuilder se encarga de LEER las lineas de codigo en el query del SQL que CONTIENE los datos de los Usuarios.
                    query.AppendLine("select ve.IdVenta, us.NombreCompleto, ve.DocumentoCliente, ve.NombreCliente, ve.TipoDocumento, ve.NumeroDocumento, ve.MontoPago, ve.MontoCambio, ve.MontoTotal, convert(char(10),ve.FechaRegistro,103)[FechaRegistro] from VENTA ve");
                    query.AppendLine("inner join USUARIO us on us.IdUsuario = ve.IdUsuario");
                    query.AppendLine("where ve.NumeroDocumento = @numero");

                    SqlCommand command = new SqlCommand(query.ToString(), connection);
                    command.Parameters.AddWithValue("@numero", numero);
                    command.CommandType = CommandType.Text;

                    connection.Open();

                    using (SqlDataReader d_reader = command.ExecuteReader())
                    {
                        while (d_reader.Read())
                        {
                            objVenta = new Venta()
                            {
                                IdVenta = Convert.ToInt32(d_reader["IdVenta"]),
                                objUsuario = new Usuario()
                                {
                                    NombreCompleto = d_reader["NombreCompleto"].ToString()
                                },
                                DocumentoCliente = d_reader["DocumentoCliente"].ToString(),
                                NombreCliente = d_reader["NombreCliente"].ToString(),
                                TipoDocumento = d_reader["TipoDocumento"].ToString(),
                                NumeroDocumento = d_reader["NumeroDocumento"].ToString(),
                                MontoPago = Convert.ToDecimal(d_reader["MontoPago"].ToString()),
                                MontoCambio = Convert.ToDecimal(d_reader["MontoCambio"].ToString()),
                                MontoTotal = Convert.ToDecimal(d_reader["MontoTotal"].ToString()),
                                FechaRegistro = d_reader["FechaRegistro"].ToString(),
                            };
                        }
                    }
                }

                catch (Exception e)
                {
                    objVenta = new Venta();
                }
            }

            return objVenta;
        }

        public List<Detalle_Venta> Obtener_DetalleVenta(int idventa)
        {
            List<Detalle_Venta> list = new List<Detalle_Venta>();

            using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
            {
                try
                {
                    connection.Open();

                    StringBuilder query = new StringBuilder(); // Este StringBuilder se encarga de LEER las lineas de codigo en el query del SQL que CONTIENE los datos de los Usuarios.
                    query.AppendLine("select pr.Nombre, dv.PrecioVenta, dv.Cantidad, dv.SubTotal from DETALLE_VENTA dv");
                    query.AppendLine("inner join PRODUCTO pr on pr.IdProducto = dv.IdProducto");
                    query.AppendLine("where dv.IdVenta = @idventa");

                    SqlCommand command = new SqlCommand(query.ToString(), connection);
                    command.Parameters.AddWithValue("@idventa", idventa);
                    command.CommandType = CommandType.Text;

                    using (SqlDataReader d_reader = command.ExecuteReader())
                    {
                        while (d_reader.Read())
                        {
                            list.Add(new Detalle_Venta()
                            {
                                objProducto = new Producto() { Nombre = d_reader["Nombre"].ToString() },
                                PrecioVenta = Convert.ToDecimal(d_reader["PrecioVenta"].ToString()),
                                Cantidad = Convert.ToInt32(d_reader["Cantidad"].ToString()),
                                SubTotal = Convert.ToDecimal(d_reader["SubTotal"].ToString()),
                            });
                        }
                    }
                }

                catch (Exception e)
                {
                    list = new List<Detalle_Venta>();
                }
            }

            return list;
        }
    }
}
