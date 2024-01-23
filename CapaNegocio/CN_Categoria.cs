using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Categoria
    {
        private CD_Categoria objCD_Categoria = new CD_Categoria();

        public List<Categoria> ToList()
        {
            return objCD_Categoria.ToList(); // RETORNAR la lista que contiene los atributos de la clase Categoria de la CD.
        }

        // METODOS para las operaciones de REGISTRAR, EDITAR y ELIMINAR del menu de Categorias (frmCategorias)
        public int Registrar_Categoria(Categoria objCat, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (objCat.Descripcion == "")
            {
                Mensaje += "La categoria debe tener una descripcion";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }

            else
            {
                return objCD_Categoria.Registrar_Categoria(objCat, out Mensaje); // RETORNAR los resultados de la operacion de REGISTRAR Categorias en la CD.
            }
        }

        public bool Editar_Categoria(Categoria objCat, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (objCat.Descripcion == "")
            {
                Mensaje += "La categoria debe tener una descripcion";
            }           

            if (Mensaje != string.Empty)
            {
                return false;
            }

            else
            {
                return objCD_Categoria.Editar_Categoria(objCat, out Mensaje); // RETORNAR los resultados de la operacion de EDITAR Categorias en la CD..
            }

        }

        public bool Eliminar_Categoria(Categoria objCat, out string Mensaje)
        {
            return objCD_Categoria.Eliminar_Categoria(objCat, out Mensaje); // RETORNAR los resultados de la operacion de ELIMINAR Categorias en la CD.
        }
    }
}
