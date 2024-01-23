using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;
using System.Data;

namespace CapaNegocio
{
    public class CN_Venta
    {
        private CD_Venta objCD_Venta = new CD_Venta();

        public int Obtener_Correlativo()
        {
            return objCD_Venta.Obtener_Correlativo(); // RETORNAR la lista que contiene los atributos de la clase Venta de la CD.
        }

        public bool Reducir_Stock(int idproducto, int cantidad)
        {
            return objCD_Venta.Reducir_Stock(idproducto, cantidad);
        }

        public bool Devolver_Stock(int idproducto, int cantidad)
        {
            return objCD_Venta.Devolver_Stock(idproducto, cantidad);
        }

        // METODOS para las operacion de REGISTRAR del menu de VENTAS (frmVentas)
        public bool Registrar_Venta(Venta objCom, DataTable DetalleVenta, out string Mensaje)
        {
            return objCD_Venta.Registrar_Venta(objCom, DetalleVenta, out Mensaje); // RETORNAR los resultados de la operacion de REGISTRAR Ventas en la CD.
        }

        public Venta Obtener_Venta(string numero)
        {
            Venta objVenta = objCD_Venta.Obtener_Venta(numero);

            if (objVenta.IdVenta != 0)
            {
                List<Detalle_Venta> objDetalle_Venta = objCD_Venta.Obtener_DetalleVenta(objVenta.IdVenta);

                objVenta.objDetalleVenta = objDetalle_Venta;
            }

            return objVenta;
        }
    }
}
