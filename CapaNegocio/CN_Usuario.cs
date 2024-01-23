using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;


namespace CapaNegocio
{
    public class CN_Usuario
    {
        private CD_Usuario objCD_Usuario = new CD_Usuario();

        public List<Usuario> ToList()
        {
            return objCD_Usuario.ToList(); // RETORNAR la lista que contiene los atributos de la clase Usuario de la CD.
        }

        // METODOS para las operaciones de REGISTRAR, EDITAR y ELIMINAR del menu de Usuarios (frmUsuarios)
        public int Registrar_Usuario(Usuario objUsu, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (objUsu.NombreCompleto == "")
            {
                Mensaje += "Se requiere tener un nombre para el usuario";
            }

            if (objUsu.Documento == "")
            {
                Mensaje += "Se requiere que el usuario disponga de un documento";
            }

            if (objUsu.Correo== "")
            {
                Mensaje += "Se requiere que el usuario disponga de un correo";
            }

            if (objUsu.Clave == "")
            {
                Mensaje += "Se requiere que el usuario disponga de una contraseña";
            }

            if(Mensaje != string.Empty)
            {
                return 0;
            }

            else
            {
                return objCD_Usuario.Registrar_Usuario(objUsu, out Mensaje); // RETORNAR los resultados de la operacion de REGISTRAR usuarios en la CD.
            }        
        }

        public bool Editar_Usuario(Usuario objUsu, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (objUsu.NombreCompleto == "")
            {
                Mensaje += "Se requiere tener un nombre para el usuario";
            }

            if (objUsu.Documento == "")
            {
                Mensaje += "Se requiere que el usuario disponga de un documento";
            }

            if (objUsu.Correo == "")
            {
                Mensaje += "Se requiere que el usuario disponga de un correo";
            }

            if (objUsu.Clave == "")
            {
                Mensaje += "Se requiere que el usuario disponga de una contraseña";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }

            else
            {
                return objCD_Usuario.Editar_Usuario(objUsu, out Mensaje); // RETORNAR los resultados de la operacion de EDITAR usuarios en la CD..
            }
            
        }

        public bool Eliminar_Usuario(Usuario objUsu, out string Mensaje)
        {
            return objCD_Usuario.Eliminar_Usuario(objUsu, out Mensaje); // RETORNAR los resultados de la operacion de ELIMINAR usuarios en la CD.
        }
    }    
}
