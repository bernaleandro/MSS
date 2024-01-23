using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidad;
using CapaNegocio;

namespace CapaInterfaz
{
    public partial class frmDetalle_Venta : Form
    {
        public frmDetalle_Venta()
        {
            InitializeComponent();
        }


        /* --------------------------------------------------------------------- APLICACION DE BUTTONS --------------------------------------------------------------------- */


        private void btnBuscar_Click(object sender, EventArgs e) // Este Button se encarga de MOSTRAR los datos de la COMPRA que se desea buscar.
        {
            Venta objVenta = new CN_Venta().Obtener_Venta(txbBuscador.Text);

            if (objVenta.IdVenta != 0)
            {
                txbDate.Text = objVenta.FechaRegistro;
                txbTipoDocumento.Text = objVenta.TipoDocumento;
                txbUser.Text = objVenta.objUsuario.NombreCompleto;
                txbDocument.Text = objVenta.NumeroDocumento;
                txbClientName.Text = objVenta.NombreCliente;

                dgvVentas.Rows.Clear();

                foreach (Detalle_Venta dc in objVenta.objDetalleVenta)
                {
                    dgvVentas.Rows.Add(new object[] { dc.objProducto.Nombre, dc.PrecioVenta, dc.Cantidad, dc.SubTotal });
                }

                txbTotal.Text = objVenta.MontoTotal.ToString("0.00");
                txbClientPay.Text = objVenta.MontoPago.ToString("0.00");
                txbChange.Text = objVenta.MontoCambio.ToString("0.00");
            }
        }

        private void icoClean_Click(object sender, EventArgs e) // Este Icon se encarga de LIMPIAR los campos.
        {
            txbBuscador.Text = "";
            txbDate.Text = "";
            txbTipoDocumento.Text = "";
            txbUser.Text = "";
            txbDocument.Text = "";
            txbClientName.Text = "";
            txbTotal.Text = "0.00";
            txbClientPay.Text = "0.00";
            txbChange.Text = "0.00";

            dgvVentas.Rows.Clear();
        }
    }
}
