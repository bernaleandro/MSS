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
    public partial class frmRegistrar_Compra : Form
    {


        // FORMULARIO REGISTRAR COMPRA --------------------------------------------------------------------------------------------------------------------------------------
        // Este formulario REGISTRAR COMPRA podra ser abierto por cualquier USUARIO sin importar su ROL. Aqui se podra ver la lista de COMPRAS registrados en la aplicacion y
        // tambien sus datos, se podra añadir nuevas COMPRAS o eliminarlas.
        // ------------------------------------------------------------------------------------------------------------------------------------------------------------------


        private Usuario UsuarioLogeado;

        public frmRegistrar_Compra(Usuario objUsuario = null)
        {
            UsuarioLogeado = objUsuario;

            InitializeComponent();
        }

        private void frmRegistrar_Compra_Load(object sender, EventArgs e)
        {
            cmbDocument.Items.Add(new OptionsComboBox() { Value = 0, Text = "Boleta" });
            cmbDocument.Items.Add(new OptionsComboBox() { Value = 1, Text = "Factura" });
            cmbDocument.DisplayMember = "Text";
            cmbDocument.ValueMember = "Value";
            cmbDocument.SelectedIndex = 0;

            txbDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txbIdProducto.Text = "";
            txbIdProveedor.Text = "";
        }


        /* --------------------------------------------------------------------- APLICACION DE BUTTONS --------------------------------------------------------------------- */


        private void btnBuscarProveedor_Click(object sender, EventArgs e) // Este Button se encarga de MOSTRAR el MODAL FORM que contiene la tabla de PROVEEDORES.
        {
            using (var modal = new mdfrmProveedores())
            {
                var result = modal.ShowDialog();

                if(result == DialogResult.OK)
                {
                    txbIdProveedor.Text = modal.propProveedor.IdProveedor.ToString();
                    txbDocumentProveedor.Text = modal.propProveedor.Documento;
                    txbRazonSocial.Text = modal.propProveedor.RazonSocial;
                }

                else
                {
                    txbDocumentProveedor.Select();
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

                    txbPrecioCompra.Select();
                }

                else
                {
                    txbProductCode.Select();
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e) // Este Button ESCRIBE los datos escritos en los Textbox en el DataGridView.
        {
            decimal precio_compra = 0;
            decimal precio_venta = 0;

            bool existProduct = false;

            if (string.Format(txbIdProducto.Text) == "")
            {
                pnlError.Visible = true;
                lblError.Text = "Seleccione un producto";

                txbPrecioCompra.Select();
                return;
            }

            if (!decimal.TryParse(txbPrecioCompra.Text, out precio_compra))
            {
                pnlError.Visible = true;
                lblError.Text = "El formato del precio \nde compra es incorrecto";

                txbPrecioCompra.Select();
                return;
            }

            if (!decimal.TryParse(txbPrecioVenta.Text, out precio_venta))
            {
                pnlError.Visible = true;
                lblError.Text = "El formato del precio \nde venta es incorrecto";

                txbPrecioCompra.Select();
                return;
            }

            foreach(DataGridViewRow row in dgvProductos.Rows)
            {
                if(row.Cells["IdProducto"].Value.ToString() == txbIdProducto.Text)
                {
                    existProduct = true;
                    break;
                }
            }

            if(existProduct == false)
            {
                dgvProductos.Rows.Add(new object[]
                {
                    txbIdProducto.Text,
                    txbProduct.Text,
                    precio_compra.ToString("0.00"),
                    precio_venta.ToString("0.00"),
                    nudCantidad.Value.ToString(),
                    (nudCantidad.Value*precio_compra).ToString("0.00")
                });

                CalculateTotal();
                CleanUpProduct();

                pnlError.Visible = false;
                txbProductCode.Select();
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e) // Este Button se encarga de GUARDAR los datos en la base de datos y DEVOLVER un NUMERO DE DOCUMENTO.
        {
            if (Convert.ToString(txbIdProveedor.Text) == "")
            {
                pnlError.Visible = true;
                lblError.Text = "Debe seleccionar un\nproveedor";
                return;
            }

            if (dgvProductos.Rows.Count < 1)
            {
                pnlError.Visible = true;
                lblError.Text = "Debe ingresar un\nproducto";
                return;
            }

            DataTable Detalle_Compra = new DataTable();

            Detalle_Compra.Columns.Add("IdProducto", typeof(int));
            Detalle_Compra.Columns.Add("PrecioCompra", typeof(decimal));
            Detalle_Compra.Columns.Add("PrecioVenta", typeof(decimal));
            Detalle_Compra.Columns.Add("Cantidad", typeof(int));
            Detalle_Compra.Columns.Add("SubTotal", typeof(decimal));

            foreach(DataGridViewRow row in dgvProductos.Rows)
            {
                Detalle_Compra.Rows.Add(

                    new object[]
                    {
                        Convert.ToInt32(row.Cells["IdProducto"].Value.ToString()),
                        row.Cells["PrecioCompra"].Value.ToString(),
                        row.Cells["PrecioVenta"].Value.ToString(),
                        row.Cells["Cantidad"].Value.ToString(),
                        row.Cells["SubTotal"].Value.ToString(),
                    }
                );                   
            }

            // Para obtener el correlativo
            int idcorrelativo = new CN_Compra().Obtener_Correlativo();
            string nrodocumento = string.Format("{0:00000}", idcorrelativo);

            Compra objCompra = new Compra()
            {
                objUsuario = new Usuario() { IdUsuario = UsuarioLogeado.IdUsuario },
                objProveerdor = new Proveedor() { IdProveedor = Convert.ToInt32(txbIdProveedor.Text) },
                TipoDocumento = ((OptionsComboBox)cmbDocument.SelectedItem).Text,
                NumeroDocumento = nrodocumento,
                MontoTotal = Convert.ToDecimal(txbTotal.Text)
            };

            string mensaje = string.Empty;
            bool respuesta = new CN_Compra().Registrar_Compra(objCompra, Detalle_Compra, out mensaje);

            if (respuesta)
            {
                pnlRegistered.Visible = true;
                lblNumeroDocumento.Text = nrodocumento;

                txbIdProveedor.Text = "";
                txbDocumentProveedor.Text = "";
                txbRazonSocial.Text = "";
                txbTotal.Text = "";
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


        private void txbPrecioCompra_KeyPress(object sender, KeyPressEventArgs e) // Este evento se encarga de PERMITIR unicamente valores numericos.
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }

            else
            {
                if(txbPrecioCompra.Text.Trim().Length==0 && e.KeyChar.ToString() == ",")
                {
                    e.Handled = true;
                }

                else
                {
                    if(char.IsControl(e.KeyChar)|| e.KeyChar.ToString() == ",")
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

        private void txbPrecioVenta_KeyPress(object sender, KeyPressEventArgs e) // Este evento se encarga de PERMITIR unicamente valores numericos.
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }

            else
            {
                if (txbPrecioVenta.Text.Trim().Length == 0 && e.KeyChar.ToString() == ",")
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

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e) // Este evento se encarga de ELIMINAR la COMPRA.
        {
            if(dgvProductos.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                int indice = e.RowIndex;

                if(indice >= 0)
                {
                    dgvProductos.Rows.RemoveAt(indice);

                    CalculateTotal();
                }
            }
        }


        /* --------------------------------------------------------------------- APLICACION DE EVENTOS --------------------------------------------------------------------- */


        private void CalculateTotal() // Este evento se encarga de SUMAR el valor de los SUBTOTALES de la COMPRA.
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

        private void CleanUpProduct() // Este evento se encarga de LIMPIAR los TextBox referentes a PRODUCTOS luego de presionar el Button de AGREGAR COMPRA.
        {
            txbIdProducto.Text = "";
            txbProductCode.Text = "";
            txbProduct.Text = "";
            txbPrecioCompra.Text = "";
            txbPrecioVenta.Text = "";
            nudCantidad.Value = 1;
        }
    }  
}
