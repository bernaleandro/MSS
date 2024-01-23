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
using CapaInterfaz.Modal_Forms;
using CapaEntidad;
using CapaNegocio;

namespace CapaInterfaz
{
    public partial class frmRegistrar_Venta : Form
    {

        private Usuario UsuarioLogeado;

        public frmRegistrar_Venta(Usuario objUsuario = null)
        {
            UsuarioLogeado = objUsuario;

            InitializeComponent();
        }

        private void frmRegistrar_Venta_Load(object sender, EventArgs e)
        {
            cmbDocument.Items.Add(new OptionsComboBox() { Value = 0, Text = "Boleta" });
            cmbDocument.Items.Add(new OptionsComboBox() { Value = 1, Text = "Factura" });
            cmbDocument.DisplayMember = "Text";
            cmbDocument.ValueMember = "Value";
            cmbDocument.SelectedIndex = 0;

            txbDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txbIdProducto.Text = "";
            txbIdCliente.Text = "";
        }


        /* --------------------------------------------------------------------- APLICACION DE BUTTONS --------------------------------------------------------------------- */


        private void btnBuscarCliente_Click(object sender, EventArgs e) // Este Button se encarga de MOSTRAR el MODAL FORM que contiene la tabla de CLIENTES.
        {
            using (var modal = new mdfrmClientes())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txbIdCliente.Text = modal.propCliente.IdCliente.ToString();
                    txbClientDocument.Text = modal.propCliente.Documento;
                    txbClientName.Text = modal.propCliente.NombreCompleto;
                }

                else
                {
                    txbClientDocument.Select();
                }
            }
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e) // Este Button se encarga de MOSTRAR el MODAL FORM que contiene la tabla de PRODUCTOS.
        {
            using (var modal = new mdfrmProductos())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txbIdProducto.Text = modal.propProducto.IdProducto.ToString();
                    txbProductCode.Text = modal.propProducto.Codigo;
                    txbProduct.Text = modal.propProducto.Nombre;
                    txbPrice.Text = modal.propProducto.PrecioVenta.ToString("0.00");
                    txbStock.Text = modal.propProducto.Stock.ToString();

                    nudCantidad.Select();
                }

                else
                {
                    txbProductCode.Select();
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e) // Este Button ESCRIBE los datos escritos en los Textbox en el DataGridView.
        {
            decimal price = 0;

            bool existProduct = false;

            if (string.Format(txbIdProducto.Text) == "")
            {
                pnlError.Visible = true;
                lblError.Text = "Seleccione un producto";

                return;
            }

            /*if (!decimal.TryParse(txbPrice.Text, out price))
            {
                pnlError.Visible = true;
                lblError.Text = "El formato del precio \nde Venta es incorrecto";

                return;
            }*/

            if (Convert.ToInt32(txbStock.Text) < Convert.ToInt32(nudCantidad.Value.ToString()))
            {
                pnlError.Visible = true;
                lblError.Text = "No hay el suficiente\nstock disponible";

                return;
            }
            

            foreach (DataGridViewRow row in dgvProductos.Rows)
            {
                if (row.Cells["IdProducto"].Value.ToString() == txbIdProducto.Text)
                {
                    existProduct = true;
                    break;
                }
            }

            if (existProduct == false)
            {
                bool respuesta = new CN_Venta().Reducir_Stock(
                  Convert.ToInt32(txbIdProducto.Text),
                  Convert.ToInt32(nudCantidad.Value.ToString())
                );

                if (respuesta)
                {
                    dgvProductos.Rows.Add(new object[]
                    {
                        txbIdProducto.Text,
                        txbProduct.Text,
                        price.ToString("0.00"),
                        nudCantidad.Value.ToString(),
                        (nudCantidad.Value*price).ToString("0.00")
                    });

                    CalculateTotal();
                    CleanUpProduct();

                    pnlError.Visible = false;
                    txbProductCode.Select();
                }                
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e) // Este Button se encarga de GUARDAR los datos en la base de datos y DEVOLVER un NUMERO DE DOCUMENTO.
        {
            if (Convert.ToString(txbIdCliente.Text) == "")
            {
                pnlError.Visible = true;
                lblError.Text = "Debe seleccionar un\ncliente";
                return;
            }

            if (dgvProductos.Rows.Count < 1)
            {
                pnlError.Visible = true;
                lblError.Text = "Debe ingresar un\nproducto";
                return;
            }

            DataTable Detalle_Venta = new DataTable();

            Detalle_Venta.Columns.Add("IdProducto", typeof(int));
            Detalle_Venta.Columns.Add("PrecioVenta", typeof(decimal));
            Detalle_Venta.Columns.Add("Cantidad", typeof(int));
            Detalle_Venta.Columns.Add("SubTotal", typeof(decimal));

            foreach (DataGridViewRow row in dgvProductos.Rows)
            {
                Detalle_Venta.Rows.Add(

                    new object[]
                    {
                        Convert.ToInt32(row.Cells["IdProducto"].Value.ToString()),
                        row.Cells["PrecioVenta"].Value.ToString(),
                        row.Cells["Cantidad"].Value.ToString(),
                        row.Cells["SubTotal"].Value.ToString()
                    }
                );
            }

            // Para obtener el correlativo
            int idcorrelativo = new CN_Venta().Obtener_Correlativo();
            string nrodocumento = string.Format("{0:00000}", idcorrelativo);

            Venta objVenta = new Venta()
            {
                objUsuario = new Usuario() { IdUsuario = UsuarioLogeado.IdUsuario },
                TipoDocumento = ((OptionsComboBox)cmbDocument.SelectedItem).Text,
                NumeroDocumento = nrodocumento,
                DocumentoCliente = txbClientDocument.Text,
                NombreCliente = txbClientName.Text,
                MontoTotal = Convert.ToDecimal(txbTotal.Text),
                MontoCambio = Convert.ToDecimal(txbChange.Text),
                MontoPago = Convert.ToDecimal(txbClientPay.Text)
            };

            string mensaje = string.Empty;
            bool respuesta = new CN_Venta().Registrar_Venta(objVenta, Detalle_Venta, out mensaje);

            if (respuesta)
            {
                pnlRegistered.Visible = true;
                lblNumeroDocumento.Text = nrodocumento;

                txbIdCliente.Text = "";
                txbClientDocument.Text = "";
                txbClientName.Text = "";
                txbTotal.Text = "";
                txbChange.Text = "";
                txbClientPay.Text = "";
                dgvProductos.Rows.Clear();

                CalculateTotal();
            }

            else
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK);
            }

            pnlError.Visible = false;
        }

        private void icoOK_Click(object sender, EventArgs e) // Este Icon se encarga de CERRAR el panel de mensaje.
        {
            pnlRegistered.Visible = false;
        }


        /* ----------------------------------------------------------- APLICACION DE EVENTOS DE LAS HERRAMIENTAS ----------------------------------------------------------- */


        private void txbPrice_KeyPress(object sender, KeyPressEventArgs e) // Este evento se encarga de PERMITIR unicamente valores numericos.
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }

            else
            {
                if (txbPrice.Text.Trim().Length == 0 && e.KeyChar.ToString() == ",")
                {
                    e.Handled = true;
                }

                else
                {
                    if (char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ",")
                    {
                        e.Handled = false;
                    }

                    else
                    {
                        e.Handled = true;
                    }
                }
            }
        }
      
        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e) // Este evento se encarga de ELIMINAR la VENTA.
        {
            if (dgvProductos.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    bool respuesta = new CN_Venta().Devolver_Stock(
                        Convert.ToInt32(dgvProductos.Rows[indice].Cells["IdProducto"].Value.ToString()),
                        Convert.ToInt32(dgvProductos.Rows[indice].Cells["Cantidad"].Value.ToString())
                    );

                    if (respuesta)
                    {
                        dgvProductos.Rows.RemoveAt(indice);

                        CalculateTotal();
                    }                 
                }
            }
        }

        private void txbClientPay_KeyDown(object sender, KeyEventArgs e) // Este evento se encarga de CALCULAR el CAMBIO cuando se presione ENTER.
        {
            if (e.KeyData == Keys.Enter)
            {
                if(txbTotal.Text.Trim() == "")
                {
                    pnlErrorChange.Visible = true;
                    lblErrorChange.Text = "No se agrego ningun\nproducto a la venta";                  

                    return;
                }

                decimal clientpay;
                decimal total = Convert.ToDecimal(txbTotal.Text);

                if(decimal.TryParse(txbClientPay.Text.Trim(), out clientpay))
                {
                    if (clientpay < total)
                    {
                        pnlErrorChange.Visible = true;
                        lblErrorChange.Text = "El pago del cliente es\nmenor al total";
                    }

                    else
                    {
                        decimal change = clientpay - total;

                        txbChange.Text = change.ToString("0.00");

                        pnlErrorChange.Visible = false;
                    }
                }
            }
        }


        /* --------------------------------------------------------------------- APLICACION DE EVENTOS --------------------------------------------------------------------- */


        private void CalculateTotal() // Este evento se encarga de SUMAR el valor de los SUBTOTALES de la Venta.
        {
            decimal total = 0;

            if (dgvProductos.Rows.Count > 0) // Este if valida que el DataGridView tenga REGISTROS.
            {
                foreach (DataGridViewRow row in dgvProductos.Rows)
                {
                    total += Convert.ToDecimal(row.Cells["SubTotal"].Value.ToString());
                }

                txbTotal.Text = total.ToString("0.00");
            }
        }

        private void CleanUpProduct() // Este evento se encarga de LIMPIAR los TextBox referentes a PRODUCTOS luego de presionar el Button de AGREGAR Venta.
        {
            txbIdProducto.Text = "";
            txbProductCode.Text = "";
            txbProduct.Text = "";
            txbPrice.Text = "";
            txbStock.Text = "";
            nudCantidad.Value = 1;
        }
    }
}
