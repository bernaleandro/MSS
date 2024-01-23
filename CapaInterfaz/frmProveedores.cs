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
    public partial class frmProveedores : Form
    {


        // FORMULARIO PROVEEDORES -------------------------------------------------------------------------------------------------------------------------------------------
        // Este formulario PROVEEDORES podra ser abierto por cualquier USUARIO sin importar el ROL. Aqui se podra ver la lista de PROVEEDORES registrados en la aplicacion y
        // tambien sus datos, se podra añadir nuevos PROVEEDORES o editarlos o eliminarlos. 
        // ------------------------------------------------------------------------------------------------------------------------------------------------------------------


        private bool Editar_Clicked = false; // Este bool se encarga de VALIDAR cuando un PROVEEDOR esta SELECCIONADO para editar.

        public frmProveedores()
        {
            InitializeComponent();
        }

        private void frmProveedores_Load(object sender, EventArgs e)
        {
            cmbEstado.Items.Add(new OptionsComboBox() { Value = 0, Text = "No Activo" });
            cmbEstado.Items.Add(new OptionsComboBox() { Value = 1, Text = "Activo" });
            cmbEstado.DisplayMember = "Text";
            cmbEstado.ValueMember = "Value";
            cmbEstado.SelectedIndex = 0;

            List<Proveedor> ListProveedor = new CN_Proveedor().ToList();

            foreach (Proveedor item in ListProveedor) // Este foreach se encarga de MOSTRAR los PROVEEDORES GUARDADOS en el DataGridView.
            {
                dgvProveedores.Rows.Add(new object[] {
                    item.IdProveedor,
                    item.RazonSocial,
                    item.Documento,
                    item.Correo,
                    item.Telefono,
                    item.Estado == true ? "Activo":"No Activo"
                });
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e) // Este Button ESCRIBE los datos escritos en los Textbox en el DataGridView.
        {
            string Mensaje = string.Empty;

            Proveedor objProveedor = new Proveedor()
            {
                IdProveedor = Convert.ToInt32(lblIdProveedor.Text),
                RazonSocial = txbRazonSocial.Text,
                Documento = txbDocument.Text,
                Correo = txbEmail.Text,
                Telefono = txbPhone.Text,

                Estado = Convert.ToInt32(((OptionsComboBox)cmbEstado.SelectedItem).Value) == 1 ? true : false
            };

            if (Editar_Clicked == false) // Este if se encarga de VERIFICAR que NO se selecciono ningun PROVEEDOR de la lista.
            {
                int new_idProveedor = new CN_Proveedor().Registrar_Proveedor(objProveedor, out Mensaje);

                if (new_idProveedor != 0) // Este if se encarga de VERIFICAR que se genero un nuevo PROVEEDOR correctamente, si IdProveedor = 0 significa que no se registro un nuevo PROVEEDOR.
                {
                    dgvProveedores.Rows.Add(new object[] {
                    new_idProveedor,
                    txbRazonSocial.Text,
                    txbDocument.Text,
                    txbEmail.Text,
                    txbPhone.Text,
                    ((OptionsComboBox)cmbEstado.SelectedItem).Text.ToString()});

                    CleanUp();
                }

                else
                {
                    MessageBox.Show(Mensaje);
                }
            }

            if (Editar_Clicked == true) // Este if se encarga de VERIFICAR que SI se selecciono ningun PROVEEDOR de la lista.
            {
                bool edit_Proveedor = new CN_Proveedor().Editar_Proveedor(objProveedor, out Mensaje);

                if (edit_Proveedor)
                {
                    DataGridViewRow row = dgvProveedores.CurrentRow;
                    row.Cells["IdProveedor"].Value = lblIdProveedor;
                    row.Cells["RazonSocial"].Value = txbRazonSocial;
                    row.Cells["Document"].Value = txbDocument;
                    row.Cells["Email"].Value = txbEmail;
                    row.Cells["Phone"].Value = txbPhone;
                    row.Cells["Estado"].Value = ((OptionsComboBox)cmbEstado.SelectedItem).Text.ToString();

                    CleanUp();
                    Editar_Clicked = false;
                    pnlProveedor.Visible = false;
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e) // Este Button se encarga de MOSTRAR los datos en la tabla de datos del PROVEEDOR para poder EDITARLOS.
        {
            Editar_Clicked = true;
            pnlProveedor.Visible = true;
            btnEliminar.Visible = true;
            btnCancelarEditar.Visible = true;

            lblIdProveedor.Text = dgvProveedores.CurrentRow.Cells["IdProveedor"].Value.ToString();
            txbRazonSocial.Text = dgvProveedores.CurrentRow.Cells["RazonSocial"].Value.ToString();
            txbDocument.Text = dgvProveedores.CurrentRow.Cells["Document"].Value.ToString();
            txbEmail.Text = dgvProveedores.CurrentRow.Cells["Email"].Value.ToString();
            txbPhone.Text = dgvProveedores.CurrentRow.Cells["Phone"].Value.ToString();
            cmbEstado.Text = dgvProveedores.CurrentRow.Cells["Estado"].Value.ToString();
        }

        private void btnEliminar_Click(object sender, EventArgs e) // Este Button se encarga de ABRIR el pnlEliminar para ELIMINAR un PROVEEDOR.
        {
            pnlEliminar.Visible = true;
            btnEliminar.Visible = false;
        }

        private void btnCancelarEditar_Click(object sender, EventArgs e) // Este Button DESELECCIONA el PROVEEDOR anteriormente seleccionada.
        {
            CleanUp();
            Editar_Clicked = false;
            pnlProveedor.Visible = false;
            btnCancelarEditar.Visible = false;
            btnEliminar.Visible = false;
            pnlEliminar.Visible = false;
        }

        private void btnEliminarYES_Click(object sender, EventArgs e) // Este Button se encarga de ELIMINAR el PROVEEDOR.
        {
            if (Convert.ToInt32(lblIdProveedor.Text) != 0)
            {
                string Mensaje = string.Empty;

                Proveedor objProveedor = new Proveedor()
                {
                    IdProveedor = Convert.ToInt32(lblIdProveedor.Text)
                };

                bool delete_Proveedor = new CN_Proveedor().Eliminar_Proveedor(objProveedor, out Mensaje);

                if (delete_Proveedor)
                {
                    dgvProveedores.Rows.Remove(dgvProveedores.CurrentRow);
                }
            }

            Editar_Clicked = false;
            pnlEliminar.Visible = false;
            btnEliminar.Visible = true;
            pnlProveedor.Visible = false;
            btnEliminar.Visible = false;
            btnCancelarEditar.Visible = false;
            CleanUp();
        }

        private void btnEliminarNO_Click(object sender, EventArgs e) // Este Button CIERRA el pnlEliminar
        {
            pnlEliminar.Visible = false;
            btnEliminar.Visible = true;
        }

        private void icoClean_Click(object sender, EventArgs e) // Este Icon se encarga de LIMPIAR los valores de las TextBox haciendo uso del void que se encarga de esta funcion.
        {
            CleanUp();
        }


        /* --------------------------------------------------------------------- APLICACION DE EVENTO --------------------------------------------------------------------- */


        private void CleanUp() // Este void se encarga de LIMPIAR los TextBox luego de presionar el Button de GUARDAR PROVEEDOR.
        {
            txbDocument.Text = "";
            txbRazonSocial.Text = "";
            txbEmail.Text = "";
            txbPhone.Text = "";
            cmbEstado.SelectedIndex = 0;
        }
    }
}
