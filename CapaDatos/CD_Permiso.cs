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
    public class CD_Permiso
    {
        public List<Permiso> ToList(int IdUsuario)
        {
            List<Permiso> list = new List<Permiso>();

            using (SqlConnection connection = new SqlConnection(SQL_Connection.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder(); // Este StringBuilder se encarga de LEER las lineas de codigo en el query del SQL que CONTIENE los datos de los Permisos.
                    query.AppendLine("select pe.IdRol, pe.NombreMenu from PERMISO pe");
                    query.AppendLine("inner join ROL ro on ro.IdRol = pe.IdRol");
                    query.AppendLine("inner join USUARIO us on us.IdRol = ro.IdRol");
                    query.AppendLine("where us.IdUsuario = @IdUsuario");
                 
                    SqlCommand command = new SqlCommand(query.ToString(), connection);
                    command.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    command.CommandType = CommandType.Text;

                    connection.Open();

                    using (SqlDataReader d_reader = command.ExecuteReader())
                    {
                        while (d_reader.Read())
                        {
                            list.Add(new Permiso()
                            {
                                objRol = new Rol() { IdRol = Convert.ToInt32(d_reader["IdRol"]) },
                                NombreMenu = d_reader["NombreMenu"].ToString(),                              
                            });
                        }
                    }
                }

                catch (Exception excepcion)
                {
                    list = new List<Permiso>();
                }
            }

            return list;
        }
    }
}
