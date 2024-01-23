using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaInterfaz.Utilities;
using CapaEntidad;
using CapaNegocio;

namespace CapaInterfaz
{
    public partial class frmReportes_Ventas : Form
    {
        public frmReportes_Ventas()
        {
            InitializeComponent();
        }


        /* ---------------------------------------------------------------------- APLICACION DE BUTTON ---------------------------------------------------------------------- */


        private void btnBuscar_Click(object sender, EventArgs e)  // Este Button se encarga de MOSTRAR los datos de la VENTA que se desea buscar
        {
            List<Reporte_Venta> list = new List<Reporte_Venta>();

            list = new CN_Reporte().Reportar_Venta(dtpFechaInicio.Value.ToString(), dtpFechaFin.Value.ToString());

            dgvVentas.Rows.Clear();

            foreach (Reporte_Venta rv in list)
            {
                dgvVentas.Rows.Add(new object[]
                {
                    rv.FechaRegistro,
                    rv.TipoDocumento,
                    rv.NumeroDocumento,
                    rv.MontoTotal,
                    rv.UsuarioRegistro,
                    rv.DocumentoCliente,
                    rv.NombreCliente,
                    rv.CodigoProducto,
                    rv.NombreProducto,
                    rv.Categoria,
                    rv.PrecioVenta,
                    rv.Cantidad,
                    rv.SubTotal
                });
            }
        }
    }
}
