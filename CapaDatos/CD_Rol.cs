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
    public class CD_Rol
    {
        public List<Rol> ToList()
        {
            List<Rol> list = new List<Rol>();

            using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select IdRol, Descripcion from ROL");

                    SqlCommand command = new SqlCommand(query.ToString(), connection);
                    command.CommandType = CommandType.Text;

                    connection.Open();

                    using (SqlDataReader d_reader = command.ExecuteReader())
                    {
                        while (d_reader.Read())
                        {
                            list.Add(new Rol()
                            {
                                IdRol = Convert.ToInt32(d_reader["IdRol"]),
                                Descripcion = d_reader["Descripcion"].ToString(),
                            });
                        }
                    }
                }

                catch (Exception excepcion)
                {
                    list = new List<Rol>();
                }
            }

            return list;
        }
    }
}
