﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Permiso
    {
        private CD_Permiso objCD_Permiso = new CD_Permiso();

        public List<Permiso> ToList(int IdUsuario)
        {
            return objCD_Permiso.ToList(IdUsuario);
        }
    }
}
