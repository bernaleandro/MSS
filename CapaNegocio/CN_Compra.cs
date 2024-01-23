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
    public class CN_Compra
    {
        private CD_Compra objCD_Compra = new CD_Compra();

        public int Obtener_Correlativo()
        {
            return objCD_Compra.Obtener_Correlativo(); // RETORNAR la lista que contiene los atributos de la clase Compra de la CD.
        }

        // METODOS para las operaciones de REGISTRAR, EDITAR y ELIMINAR del menu de Compras (frmCompras)
        public bool Registrar_Compra(Compra objCom, DataTable DetalleCompra,out string Mensaje)
        {
            return objCD_Compra.Registrar_Compra(objCom, DetalleCompra, out Mensaje); // RETORNAR los resultados de la operacion de REGISTRAR Compras en la CD.
        }

        public Compra Obtener_Compra(string numero)
        {
            Compra objCompra = objCD_Compra.Obtener_Compra(numero);

            if(objCompra.IdCompra != 0)
            {
                List<Detalle_Compra> objDetalle_Compra = objCD_Compra.Obtener_DetalleCompra(objCompra.IdCompra);

                objCompra.objDetalleCompra = objDetalle_Compra;
            }

            return objCompra;
        }
    }
}
