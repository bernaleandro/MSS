using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;


namespace CapaNegocio
{
    public class CN_Reporte
    {
        private CD_Reporte objCD_Reporte = new CD_Reporte();

        public List<Reporte_Compra> Reportar_Compra(string fechainicio, string fechafin, int idproveedor)
        {
            return objCD_Reporte.Reportar_Compra(fechainicio, fechafin, idproveedor);
        }

        public List<Reporte_Venta> Reportar_Venta(string fechainicio, string fechafin)
        {
            return objCD_Reporte.Reportar_Venta(fechainicio, fechafin);
        }
    }
}
