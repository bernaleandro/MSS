using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CapaDatos
{
    public class SQL_Connection // Esta class se encarga de TRANSCRIBIR los datos de la coneccion de ConnectionStrings en la solucion de App.config que CONTIENE la base de datos de SQL.
    {
        public static string cadena = "Data Source=DESKTOP-VUKM8DF\\SQLEXPRESS;Initial Catalog=DBMSS;Integrated Security=True";  
    }
}
