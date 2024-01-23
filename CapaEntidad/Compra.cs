﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Compra
    {
        public int IdCompra { get; set; }
        public Usuario objUsuario { get; set; }
        public Proveedor objProveerdor { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public decimal MontoTotal { get; set; }
        public string FechaRegistro { get; set; }
        public List<Detalle_Compra> objDetalleCompra { get; set; }

    }
}
