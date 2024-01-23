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
    public class CD_Compra
    {
        public int Obtener_Correlativo() // Se encarga de GENERAR un valor entero cada vez que se REGISTRA una nueva COMPRA para identificarlo. En la clase se conoce como "NumeroDocumento".
        {
            int idcorrelativo = 0;

            using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder(); // Este StringBuilder se encarga de LEER las lineas de codigo en el query del SQL que CONTIENE los datos de los Usuarios.
                    query.AppendLine("select count(*) + 1 from COMPRA");

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

        public bool Registrar_Compra(Compra objCom, DataTable DetalleCompra, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
            {
                try
                {
                    SqlCommand command = new SqlCommand("SP_REGISTRARCOMPRA".ToString(), connection);

                    // Declarando paramentros de entrada (input)
                    command.Parameters.AddWithValue("IdUsuario", objCom.objUsuario.IdUsuario);
                    command.Parameters.AddWithValue("IdProveedor", objCom.objProveerdor.IdProveedor);
                    command.Parameters.AddWithValue("TipoDocumento", objCom.TipoDocumento);
                    command.Parameters.AddWithValue("NumeroDocumento", objCom.NumeroDocumento);
                    command.Parameters.AddWithValue("MontoTotal", objCom.MontoTotal);
                    command.Parameters.AddWithValue("DetalleCompra", DetalleCompra);

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

        public Compra Obtener_Compra(string numero)
        {
            Compra objCompra = new Compra();

            using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder(); // Este StringBuilder se encarga de LEER las lineas de codigo en el query del SQL que CONTIENE los datos de los Usuarios.
                    query.AppendLine("select co.IdCompra, us.NombreCompleto, pr.Documento, pr.RazonSocial, co.TipoDocumento, co.NumeroDocumento, co.MontoTotal, convert(char(10),co.FechaRegistro,103)[FechaRegistro] from COMPRA co");
                    query.AppendLine("inner join USUARIO us on us.IdUsuario = co.IdUsuario");
                    query.AppendLine("inner join PROVEEDOR pr on pr.IdProveedor = co.IdProveedor");
                    query.AppendLine("where co.NumeroDocumento = @numero");

                    SqlCommand command = new SqlCommand(query.ToString(), connection);
                    command.Parameters.AddWithValue("@numero", numero);
                    command.CommandType = CommandType.Text;

                    connection.Open();

                    using (SqlDataReader d_reader = command.ExecuteReader())
                    {
                        while (d_reader.Read())
                        {
                            objCompra = new Compra()
                            {
                                IdCompra = Convert.ToInt32(d_reader["IdCompra"]),
                                objUsuario = new Usuario()
                                {
                                    NombreCompleto = d_reader["NombreCompleto"].ToString()
                                },
                                objProveerdor = new Proveedor()
                                {
                                    Documento = d_reader["Documento"].ToString(),
                                    RazonSocial = d_reader["RazonSocial"].ToString(),
                                },
                                TipoDocumento = d_reader["TipoDocumento"].ToString(),
                                NumeroDocumento = d_reader["NumeroDocumento"].ToString(),
                                MontoTotal = Convert.ToDecimal(d_reader["MontoTotal"].ToString()),
                                FechaRegistro = d_reader["FechaRegistro"].ToString(),
                            };
                        }
                    }
                }

                catch (Exception e)
                {
                    objCompra = new Compra();
                }
            }

            return objCompra;
        }

        public List<Detalle_Compra> Obtener_DetalleCompra(int idcompra)
        {
            List<Detalle_Compra> list = new List<Detalle_Compra>();

            using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
            {
                try
                {
                    connection.Open();

                    StringBuilder query = new StringBuilder(); // Este StringBuilder se encarga de LEER las lineas de codigo en el query del SQL que CONTIENE los datos de los Usuarios.
                    query.AppendLine("select pr.Nombre, dc.PrecioCompra, dc.Cantidad, dc.MontoTotal from DETALLE_COMPRA dc");
                    query.AppendLine("inner join PRODUCTO pr on pr.IdProducto = dc.IdProducto");
                    query.AppendLine("where dc.IdCompra = @idcompra");

                    SqlCommand command = new SqlCommand(query.ToString(), connection);
                    command.Parameters.AddWithValue("@idcompra", idcompra);
                    command.CommandType = CommandType.Text;

                    using (SqlDataReader d_reader = command.ExecuteReader())
                    {
                        while (d_reader.Read())
                        {
                            list.Add(new Detalle_Compra()
                            {
                                objProducto = new Producto() { Nombre = d_reader["Nombre"].ToString() },
                                PrecioCompra = Convert.ToDecimal(d_reader["PrecioCompra"].ToString()),
                                Cantidad = Convert.ToInt32(d_reader["Cantidad"].ToString()),
                                MontoTotal = Convert.ToDecimal(d_reader["MontoTotal"].ToString()),
                            });
                        }
                    }
                }

                catch (Exception e)
                {
                    list = new List<Detalle_Compra>();
                }
            }
          
            return list;
        }
    }
}
