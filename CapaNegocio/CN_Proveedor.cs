using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Proveedor
    {
        private CD_Proveedor objCD_Proveedor = new CD_Proveedor();

        public List<Proveedor> ToList()
        {
            return objCD_Proveedor.ToList(); // RETORNAR la lista que contiene los atributos de la clase Proveedor de la CD.
        }

        // METODOS para las operaciones de REGISTRAR, EDITAR y ELIMINAR del menu de PROVEEDORES (frmPROVEEDORES)
        public int Registrar_Proveedor(Proveedor objPvd, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (objPvd.RazonSocial == "")
            {
                Mensaje += "Se requiere tener una razon social para el Proveedor";
            }

            if (objPvd.Documento == "")
            {
                Mensaje += "Se requiere que el Proveedor disponga de un documento";
            }

            if (objPvd.Correo == "")
            {
                Mensaje += "Se requiere que el Proveedor disponga de un correo";
            }

            if (objPvd.Telefono == "")
            {
                Mensaje += "Se requiere que el Proveedor disponga de un telefono";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }

            else
            {
                return objCD_Proveedor.Registrar_Proveedor(objPvd, out Mensaje); // RETORNAR los resultados de la operacion de REGISTRAR PROVEEDORES en la CD.
            }
        }

        public bool Editar_Proveedor(Proveedor objPvd, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (objPvd.RazonSocial == "")
            {
                Mensaje += "Se requiere tener una razon social para el Proveedor";
            }

            if (objPvd.Documento == "")
            {
                Mensaje += "Se requiere que el Proveedor disponga de un documento";
            }

            if (objPvd.Correo == "")
            {
                Mensaje += "Se requiere que el Proveedor disponga de un correo";
            }

            if (objPvd.Telefono == "")
            {
                Mensaje += "Se requiere que el Proveedor disponga de un telefono";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }

            else
            {
                return objCD_Proveedor.Editar_Proveedor(objPvd, out Mensaje); // RETORNAR los resultados de la operacion de EDITAR PROVEEDORES en la CD..
            }

        }

        public bool Eliminar_Proveedor(Proveedor objPvd, out string Mensaje)
        {
            return objCD_Proveedor.Eliminar_Proveedor(objPvd, out Mensaje); // RETORNAR los resultados de la operacion de ELIMINAR PROVEEDORES en la CD.
        }
    }
}
