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
    public class CD_Reporte
    {
        public List<Reporte_Compra> Reportar_Compra(string fechainicio, string fechafin, int idproveedor)
        {
            List<Reporte_Compra> list = new List<Reporte_Compra>();

            using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
            {
                try
                {
                    connection.Open();

                    StringBuilder query = new StringBuilder(); // Este StringBuilder se encarga de LEER las lineas de codigo en el query del SQL que CONTIENE los datos de los Usuarios.

                    SqlCommand command = new SqlCommand("SP_REPORTECOMPRAS", connection);
                    command.Parameters.AddWithValue("FechaInicio", fechainicio);
                    command.Parameters.AddWithValue("FechaFin", fechafin);
                    command.Parameters.AddWithValue("IdProveedor", idproveedor);
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader d_reader = command.ExecuteReader())
                    {
                        while (d_reader.Read())
                        {
                            list.Add(new Reporte_Compra()
                            {
                                FechaRegistro = d_reader["FechaRegistro"].ToString(),
                                TipoDocumento = d_reader["TipoDocumento"].ToString(),
                                NumeroDocumento = d_reader["NumeroDocumento"].ToString(),
                                MontoTotal = d_reader["MontoTotal"].ToString(),
                                UsuarioRegistro = d_reader["UsuarioRegistro"].ToString(),
                                DocumentoProveedor = d_reader["DocumentoProveedor"].ToString(),
                                RazonSocial = d_reader["RazonSocial"].ToString(),
                                CodigoProducto = d_reader["CodigoProducto"].ToString(),
                                NombreProducto = d_reader["NombreProducto"].ToString(),
                                Categoria = d_reader["Categoria"].ToString(),
                                PrecioCompra = d_reader["PrecioCompra"].ToString(),
                                PrecioVenta = d_reader["PrecioVenta"].ToString(),
                                Cantidad = d_reader["Cantidad"].ToString(),
                                SubTotal = d_reader["SubTotal"].ToString(),
                            });
                        }
                    }
                }

                catch (Exception e)
                {
                    list = new List<Reporte_Compra>();
                }
            }

            return list;
        }

        public List<Reporte_Venta> Reportar_Venta(string fechainicio, string fechafin)
        {
            List<Reporte_Venta> list = new List<Reporte_Venta>();

            using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
            {
                try
                {
                    connection.Open();

                    StringBuilder query = new StringBuilder(); // Este StringBuilder se encarga de LEER las lineas de codigo en el query del SQL que CONTIENE los datos de los Usuarios.

                    SqlCommand command = new SqlCommand("SP_REPORTEVENTAS", connection);
                    command.Parameters.AddWithValue("FechaInicio", fechainicio);
                    command.Parameters.AddWithValue("FechaFin", fechafin);
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader d_reader = command.ExecuteReader())
                    {
                        while (d_reader.Read())
                        {
                            list.Add(new Reporte_Venta()
                            {
                                FechaRegistro = d_reader["FechaRegistro"].ToString(),
                                TipoDocumento = d_reader["TipoDocumento"].ToString(),
                                NumeroDocumento = d_reader["NumeroDocumento"].ToString(),
                                MontoTotal = d_reader["MontoTotal"].ToString(),
                                UsuarioRegistro = d_reader["UsuarioRegistro"].ToString(),
                                DocumentoCliente = d_reader["DocumentoCliente"].ToString(),
                                NombreCliente = d_reader["NombreCliente"].ToString(),
                                CodigoProducto = d_reader["CodigoProducto"].ToString(),
                                NombreProducto = d_reader["NombreProducto"].ToString(),
                                Categoria = d_reader["Categoria"].ToString(),
                                PrecioVenta = d_reader["PrecioVenta"].ToString(),
                                Cantidad = d_reader["Cantidad"].ToString(),
                                SubTotal = d_reader["SubTotal"].ToString(),
                            });
                        }
                    }
                }

                catch (Exception e)
                {
                    list = new List<Reporte_Venta>();
                }
            }

            return list;
        }
    }
}

