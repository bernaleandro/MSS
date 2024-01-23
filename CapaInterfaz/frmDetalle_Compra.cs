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
    public partial class frmDetalle_Compra : Form
    {
        public frmDetalle_Compra()
        {
            InitializeComponent();
        }


        /* --------------------------------------------------------------------- APLICACION DE BUTTONS --------------------------------------------------------------------- */


        private void btnBuscar_Click(object sender, EventArgs e) // Este Button se encarga de MOSTRAR los datos de la COMPRA que se desea buscar.
        {
            Compra objCompra = new CN_Compra().Obtener_Compra(txbBuscador.Text);

            if (objCompra.IdCompra != 0)
            {
                txbDate.Text = objCompra.FechaRegistro;
                txbTipoDocumento.Text = objCompra.TipoDocumento;
                txbUser.Text = objCompra.objUsuario.NombreCompleto;
                txbDocument.Text = objCompra.NumeroDocumento;
                txbRazonSocial.Text = objCompra.objProveerdor.RazonSocial;

                dgvCompras.Rows.Clear();

                foreach (Detalle_Compra dc in objCompra.objDetalleCompra)
                {
                    dgvCompras.Rows.Add(new object[] { dc.objProducto.Nombre, dc.PrecioCompra, dc.Cantidad, dc.MontoTotal });
                }

                txbTotal.Text = objCompra.MontoTotal.ToString("0.00");
            }
        }

        private void icoClean_Click(object sender, EventArgs e) // Este Icon se encarga de LIMPIAR los campos.
        {
            txbBuscador.Text = "";
            txbDate.Text = "";
            txbTipoDocumento.Text = "";
            txbUser.Text = "";
            txbDocument.Text = "";
            txbRazonSocial.Text = "";
            txbTotal.Text = "0.00";

            dgvCompras.Rows.Clear();
        }
    }
}
