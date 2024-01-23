using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Negocio
    {
        private CD_Negocio objCD_Negocio = new CD_Negocio();

        public Negocio Datos_Negocio()
        {
            return objCD_Negocio.Datos_Negocio(); // RETORNAR la lista que contiene los atributos de la clase Negocio de la CD.
        }

        // METODOS para las operaciones de REGISTRAR del menu de Negocios (frmNegocios)
        public bool Registrar_Negocio(Negocio objNeg, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (objNeg.Nombre == "")
            {
                Mensaje += "Se requiere tener un nombre para el Negocio";
            }

            if (objNeg.RUC == "")
            {
                Mensaje += "Se requiere que el Negocio disponga de un numero de RUC";
            }

            if (objNeg.Direccion == "")
            {
                Mensaje += "Se requiere que el Negocio disponga de una direccion ";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }

            else
            {
                return objCD_Negocio.Registrar_Negocio(objNeg, out Mensaje); // RETORNAR los resultados de la operacion de REGISTRAR Negocios en la CD.
            }
        }

        public byte[] Registrar_Logo(out bool registrado)
        {
            return objCD_Negocio.Registrar_Logo(out registrado);
        }

        public bool Actualizar_Logo(byte[] imagen, out string mensaje)
        {
            return objCD_Negocio.Actualizar_Logo(imagen, out mensaje);
        }
    }
}
