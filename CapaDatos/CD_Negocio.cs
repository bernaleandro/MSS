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
    public class CD_Negocio
    {
        public Negocio Datos_Negocio()
        {
            Negocio objNeg = new Negocio();

            try
            {
                using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
                {
                    connection.Open();

                    string query = "select IdNegocio, Nombre, RUC, Direccion from NEGOCIO where IdNegocio = 1";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandType = CommandType.Text;

                    using(SqlDataReader d_reader = command.ExecuteReader())
                    {
                        while (d_reader.Read())
                        {
                            objNeg = new Negocio()
                            {
                                IdNegocio = int.Parse(d_reader["IdNegocio"].ToString()),
                                Nombre = d_reader["Nombre"].ToString(),
                                RUC = d_reader["RUC"].ToString(),
                                Direccion = d_reader["Direccion"].ToString(),
                            };
                        }
                    }
                }
            }

            catch
            {
                objNeg = new Negocio();
            }

            return objNeg;
        }

        public bool Registrar_Negocio(Negocio objNeg, out string mensaje)
        {
            mensaje = string.Empty;
            bool respuesta = true;

            try
            {
                using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
                {
                    connection.Open();

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update NEGOCIO set Nombre = @Nombre,");
                    query.AppendLine("RUC = @RUC,");
                    query.AppendLine("Direccion = @Direccion");
                    query.AppendLine("where IdNegocio = 1;");

                    SqlCommand command = new SqlCommand(query.ToString(), connection);
                    command.Parameters.AddWithValue("@Nombre", objNeg.Nombre);
                    command.Parameters.AddWithValue("@RUC", objNeg.RUC);
                    command.Parameters.AddWithValue("@Direccion", objNeg.Direccion);

                    command.CommandType = CommandType.Text;

                    if (command.ExecuteNonQuery() < 1)
                    {
                        mensaje = "No se pudo guardar los datos";
                        respuesta = false;
                    }
                }
            }

            catch(Exception e)
            {
                mensaje = e.Message;
                respuesta = false;
            }

            return respuesta;
        }

        public byte[] Registrar_Logo(out bool registrado)
        {
            registrado = true;
            byte[] logoBytes = new byte[0];

            try
            {
                using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
                {
                    connection.Open();

                    string query = "select Logo from NEGOCIO where IdNegocio = 1";
                    SqlCommand command = new SqlCommand(query, connection);
                    
                    command.CommandType = CommandType.Text;

                    using (SqlDataReader d_reader = command.ExecuteReader())
                    {
                        while (d_reader.Read())
                        {
                            logoBytes = (byte[])d_reader["Logo"]; 
                        }
                    }
                }
            }

            catch (Exception e)
            {
                registrado = false;
                logoBytes = new byte[0];
            }

            return logoBytes;
        }

        public bool Actualizar_Logo(byte[] imagen, out string mensaje)
        {
            mensaje = string.Empty;
            bool respuesta = true;

            try
            {
                using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
                {
                    connection.Open();

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update NEGOCIO set Logo = @Imagen");
                    query.AppendLine("where IdNegocio = 1;");

                    SqlCommand command = new SqlCommand(query.ToString(), connection);
                    command.Parameters.AddWithValue("@Imagen", imagen);

                    command.CommandType = CommandType.Text;

                    if (command.ExecuteNonQuery() < 1)
                    {
                        mensaje = "No se pudo actualizar el logo";
                        respuesta = false;
                    }
                }
            }

            catch (Exception e)
            {
                mensaje = e.Message;
                respuesta = false;
            }

            return respuesta;
        }
    }
}
