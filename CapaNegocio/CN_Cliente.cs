using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Cliente
    {
        private CD_Cliente objCD_Cliente = new CD_Cliente();

        public List<Cliente> ToList()
        {
            return objCD_Cliente.ToList(); // RETORNAR la lista que contiene los atributos de la clase CLIENTE de la CD.
        }

        // METODOS para las operaciones de REGISTRAR, EDITAR y ELIMINAR del menu de CLIENTES (frmClientes)
        public int Registrar_Cliente(Cliente objUsu, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (objUsu.NombreCompleto == "")
            {
                Mensaje += "Se requiere tener un nombre para el Cliente";
            }

            if (objUsu.Documento == "")
            {
                Mensaje += "Se requiere que el Cliente disponga de un documento";
            }

            if (objUsu.Correo == "")
            {
                Mensaje += "Se requiere que el Cliente disponga de un correo";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }

            else
            {
                return objCD_Cliente.Registrar_Cliente(objUsu, out Mensaje); // RETORNAR los resultados de la operacion de REGISTRAR CLIENTES en la CD.
            }
        }

        public bool Editar_Cliente(Cliente objUsu, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (objUsu.NombreCompleto == "")
            {
                Mensaje += "Se requiere tener un nombre para el Cliente";
            }

            if (objUsu.Documento == "")
            {
                Mensaje += "Se requiere que el Cliente disponga de un documento";
            }

            if (objUsu.Correo == "")
            {
                Mensaje += "Se requiere que el Cliente disponga de un correo";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }

            else
            {
                return objCD_Cliente.Editar_Cliente(objUsu, out Mensaje); // RETORNAR los resultados de la operacion de EDITAR CLIENTES en la CD..
            }

        }

        public bool Eliminar_Cliente(Cliente objUsu, out string Mensaje)
        {
            return objCD_Cliente.Eliminar_Cliente(objUsu, out Mensaje); // RETORNAR los resultados de la operacion de ELIMINAR CLIENTES en la CD.
        }
    }
}
