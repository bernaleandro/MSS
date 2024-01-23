using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Producto
    {
        private CD_Producto objCD_Producto = new CD_Producto();

        public List<Producto> ToList()
        {
            return objCD_Producto.ToList(); // RETORNAR la lista que contiene los atributos de la clase Producto de la CD.
        }

        // METODOS para las operaciones de REGISTRAR, EDITAR y ELIMINAR del menu de Productos (frmProductos)
        public int Registrar_Producto(Producto objPrd, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (objPrd.Codigo == "")
            {
                Mensaje += "Se requiere tener un codigo para el Producto";
            }

            if (objPrd.Nombre == "")
            {
                Mensaje += "Se requiere que el Producto disponga de un nombre";
            }

            if (objPrd.Descripcion == "")
            {
                Mensaje += "Se requiere que el Producto disponga de una descripcion";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }

            else
            {
                return objCD_Producto.Registrar_Producto(objPrd, out Mensaje); // RETORNAR los resultados de la operacion de REGISTRAR Productos en la CD.
            }
        }

        public bool Editar_Producto(Producto objPrd, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (objPrd.Codigo == "")
            {
                Mensaje += "Se requiere tener un codigo para el Producto";
            }

            if (objPrd.Nombre == "")
            {
                Mensaje += "Se requiere que el Producto disponga de un nombre";
            }

            if (objPrd.Descripcion == "")
            {
                Mensaje += "Se requiere que el Producto disponga de una descripcion";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }

            else
            {
                return objCD_Producto.Editar_Producto(objPrd, out Mensaje); // RETORNAR los resultados de la operacion de EDITAR Productos en la CD..
            }

        }

        public bool Eliminar_Producto(Producto objPrd, out string Mensaje)
        {
            return objCD_Producto.Eliminar_Producto(objPrd, out Mensaje); // RETORNAR los resultados de la operacion de ELIMINAR Productos en la CD.
        }
    }
}
