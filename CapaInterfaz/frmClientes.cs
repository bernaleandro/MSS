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
    public partial class frmClientes : Form
    {


        // FORMULARIO CLIENTES ----------------------------------------------------------------------------------------------------------------------------------------------
        // Este formulario CLIENTES podra ser abierto por cualquier USUARIO sin importar su ROL. Aqui se podra ver la lista de CLIENTES registrados en la aplicacion y
        // tambien sus datos, se podra añadir nuevos CLIENTES o editarlos o eliminarlos.
        // ------------------------------------------------------------------------------------------------------------------------------------------------------------------


        private bool Editar_Clicked = false; // Este bool se encarga de VALIDAR cuando un CLIENTE esta SELECCIONADO para editar.

        public frmClientes()
        {
            InitializeComponent();
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            cmbEstado.Items.Add(new OptionsComboBox() { Value = 0, Text = "No Activo" });
            cmbEstado.Items.Add(new OptionsComboBox() { Value = 1, Text = "Activo" });
            cmbEstado.DisplayMember = "Text";
            cmbEstado.ValueMember = "Value";
            cmbEstado.SelectedIndex = 0;

            List<Cliente> ListCliente = new CN_Cliente().ToList();

            foreach (Cliente item in ListCliente) // Este foreach se encarga de MOSTRAR los CLIENTES GUARDADOS en el DataGridView.
            {
                dgvClientes.Rows.Add(new object[] {
                    item.IdCliente,
                    item.NombreCompleto,
                    item.Documento,
                    item.Correo,
                    item.Telefono,
                    item.Estado == true ? "Activo":"No Activo"
                });
            }
        }


        /* --------------------------------------------------------------------- APLICACION DE BUTTONS --------------------------------------------------------------------- */


        private void btnGuardar_Click(object sender, EventArgs e) // Este Button ESCRIBE los datos escritos en los Textbox en el DataGridView.
        {
            string Mensaje = string.Empty;

            Cliente objCliente = new Cliente()
            {
                IdCliente = Convert.ToInt32(lblIdCliente.Text),
                NombreCompleto = txbClientName.Text,
                Documento = txbDocument.Text,
                Correo = txbEmail.Text,
                Telefono = txbPhone.Text,

                Estado = Convert.ToInt32(((OptionsComboBox)cmbEstado.SelectedItem).Value) == 1 ? true : false
            };

            if (Editar_Clicked == false) // Este if se encarga de VERIFICAR que NO se selecciono ningun CLIENTE de la lista.
            {
                int new_idCliente = new CN_Cliente().Registrar_Cliente(objCliente, out Mensaje);

                if (new_idCliente != 0) // Este if se encarga de VERIFICAR que se genero un nuevo CLIENTE correctamente, si IdCliente = 0 significa que no se registro un nuevo CLIENTE
                {
                    dgvClientes.Rows.Add(new object[] {
                    new_idCliente,
                    txbClientName.Text,
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

            if (Editar_Clicked == true) // Este if se encarga de VERIFICAR que SI se selecciono ningun CLIENTE de la lista.
            {
                bool edit_Cliente = new CN_Cliente().Editar_Cliente(objCliente, out Mensaje);

                if (edit_Cliente)
                {
                    DataGridViewRow row = dgvClientes.CurrentRow;
                    row.Cells["IdCliente"].Value = lblIdCliente;
                    row.Cells["ClientName"].Value = txbClientName;
                    row.Cells["Document"].Value = txbDocument;
                    row.Cells["Email"].Value = txbEmail;
                    row.Cells["Phone"].Value = txbPhone;
                    row.Cells["Estado"].Value = ((OptionsComboBox)cmbEstado.SelectedItem).Text.ToString();

                    CleanUp();
                    Editar_Clicked = false;
                    pnlCliente.Visible = false;
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e) // Este Button se encarga de MOSTRAR los datos en la tabla de datos del CLIENTE para poder EDITARLOS.
        {
            Editar_Clicked = true;
            pnlCliente.Visible = true;
            btnEliminar.Visible = true;
            btnCancelarEditar.Visible = true;

            lblIdCliente.Text = dgvClientes.CurrentRow.Cells["IdCliente"].Value.ToString();
            txbClientName.Text = dgvClientes.CurrentRow.Cells["ClientName"].Value.ToString();
            txbDocument.Text = dgvClientes.CurrentRow.Cells["Document"].Value.ToString();
            txbEmail.Text = dgvClientes.CurrentRow.Cells["Email"].Value.ToString();
            txbPhone.Text = dgvClientes.CurrentRow.Cells["Phone"].Value.ToString();
            cmbEstado.Text = dgvClientes.CurrentRow.Cells["Estado"].Value.ToString();
        }

        private void btnEliminar_Click(object sender, EventArgs e) // Este Button se encarga de ABRIR el pnlEliminar para ELIMINAR un CLIENTE.
        {
            pnlEliminar.Visible = true;
            btnEliminar.Visible = false;
        }

        private void btnCancelarEditar_Click(object sender, EventArgs e) // Este Button DESELECCIONA el CLIENTE anteriormente seleccionada.
        {
            CleanUp();
            Editar_Clicked = false;
            pnlCliente.Visible = false;
            btnCancelarEditar.Visible = false;
            btnEliminar.Visible = false;
            pnlEliminar.Visible = false;
        }

        private void btnEliminarYES_Click(object sender, EventArgs e) // Este Button se encarga de ELIMINAR el CLIENTE.
        {
            if (Convert.ToInt32(lblIdCliente.Text) != 0)
            {
                string Mensaje = string.Empty;

                Cliente objCliente = new Cliente()
                {
                    IdCliente = Convert.ToInt32(lblIdCliente.Text)
                };

                bool delete_Cliente = new CN_Cliente().Eliminar_Cliente(objCliente, out Mensaje);

                if (delete_Cliente)
                {
                    dgvClientes.Rows.Remove(dgvClientes.CurrentRow);
                }
            }

            Editar_Clicked = false;
            pnlEliminar.Visible = false;
            btnEliminar.Visible = true;
            pnlCliente.Visible = false;
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


        private void CleanUp() // Este void se encarga de LIMPIAR los TextBox luego de presionar el Button de GUARDAR CLIENTE.
        {
            txbDocument.Text = "";
            txbClientName.Text = "";
            txbEmail.Text = "";
            txbPhone.Text = "";
            cmbEstado.SelectedIndex = 0;
        }     
    }
}
