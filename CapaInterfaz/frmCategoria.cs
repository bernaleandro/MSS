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
    public partial class frmCategoria : Form
    {


        // FORMULARIO CATEGORIAS --------------------------------------------------------------------------------------------------------------------------------------------
        // Este formulario CATEGORIAS podra ser abierto por un USUARIO que tenga el ROL ADMINISTRADOR. Aqui mostraran las CATEGORIAS para los PRODUCTOS que se quieran vender
        // ademas de que tambien se podran agregar otras CATEGORIAS o editarlas o eliminarlas. Estas CATEGORIAS se mostraran en una ComboBox en el formulario de PRODUCTOS 
        // para asociar un PRODUCTO a una CATEGORIA.
        // ------------------------------------------------------------------------------------------------------------------------------------------------------------------


        private bool Editar_Clicked = false; // Este bool se encarga de VALIDAR cuando una CATEGORIA esta SELECCIONADA para editar.

        public frmCategoria()
        {
            InitializeComponent();
        }

        private void frmCategoria_Load(object sender, EventArgs e)
        {
            cmbEstado.Items.Add(new OptionsComboBox() { Value = 0, Text = "No Activo" });
            cmbEstado.Items.Add(new OptionsComboBox() { Value = 1, Text = "Activo" });
            cmbEstado.DisplayMember = "Text";
            cmbEstado.ValueMember = "Value";
            cmbEstado.SelectedIndex = 0;

            List<Categoria> ListCategoria = new CN_Categoria().ToList();

            foreach (Categoria item in ListCategoria) // Este foreach se encarga de MOSTRAR los CATEGORIAS GUARDADAS en el DataGridView.
            {
                dgvCategorias.Rows.Add(new object[] {
                    item.IdCategoria,                  
                    item.Descripcion,
                    item.Estado == true ? "Activo":"No Activo"
                });
            }
        }


        /* --------------------------------------------------------------------- APLICACION DE BUTTONS --------------------------------------------------------------------- */



        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string Mensaje = string.Empty;

            Categoria objcategoria = new Categoria()
            {
                IdCategoria = Convert.ToInt32(lblIdCategoria.Text),
                Descripcion = txbDescription.Text,              
                Estado = Convert.ToInt32(((OptionsComboBox)cmbEstado.SelectedItem).Value) == 1 ? true : false
            };

            if (Editar_Clicked == false) // Este if se encarga de VERIFICAR que NO se selecciono ninguna CATEGORIA de la lista.
            {
                int new_idCategoria = new CN_Categoria().Registrar_Categoria(objcategoria, out Mensaje);

                if (new_idCategoria != 0) // Este if se encarga de VERIFICAR que se genero una nueva CATEGORIA correctamente, si IdCategoria = 0 significa que no se registro un nuevo Categoria"
                {
                    dgvCategorias.Rows.Add(new object[] {
                    new_idCategoria,
                    txbDescription.Text,
                    ((OptionsComboBox)cmbEstado.SelectedItem).Text.ToString()});

                    CleanUp();
                }

                else
                {
                    MessageBox.Show(Mensaje);
                }
            }

            if (Editar_Clicked == true) // Este if se encarga de VERIFICAR que SI se selecciono ninguna CATEGORIA de la lista.
            {
                bool edit_Categoria = new CN_Categoria().Editar_Categoria(objcategoria, out Mensaje);

                if (edit_Categoria)
                {
                    DataGridViewRow row = dgvCategorias.CurrentRow;
                    row.Cells["IdCategoria"].Value = lblIdCategoria;
                    row.Cells["Description"].Value = txbDescription;
                    row.Cells["Estado"].Value = ((OptionsComboBox)cmbEstado.SelectedItem).Text.ToString();

                    CleanUp();
                    Editar_Clicked = false;
                    pnlCategoria.Visible = false;
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e) // Este Button se encarga de MOSTRAR los datos en la tabla de datos de la CATEGORIA para poder EDITARLOS.
        {
            Editar_Clicked = true;
            pnlCategoria.Visible = true;
            btnEliminar.Visible = true;
            btnCancelarEditar.Visible = true;

            lblIdCategoria.Text = dgvCategorias.CurrentRow.Cells["IdCategoria"].Value.ToString();
            txbDescription.Text = dgvCategorias.CurrentRow.Cells["Description"].Value.ToString();
            cmbEstado.Text = dgvCategorias.CurrentRow.Cells["Estado"].Value.ToString();
        }

        private void btnEliminar_Click(object sender, EventArgs e) // Este Button ABRE el pnlEliminar que cumple la funcion de MessageBox.
        {
            pnlEliminar.Visible = true;
            btnEliminar.Visible = false;
        }

        private void btnCancelarEditar_Click(object sender, EventArgs e) // Este Button DESELECCIONA la CATEGORIA anteriormente seleccionada.
        {
            CleanUp();
            Editar_Clicked = false;
            pnlCategoria.Visible = false;
            btnCancelarEditar.Visible = false;
            btnEliminar.Visible = false;
            pnlEliminar.Visible = false;
        }

        private void btnEliminarYES_Click(object sender, EventArgs e) // Este Button se encarga de ELIMINAR la CATEGORIA.
        {
            if (Convert.ToInt32(lblIdCategoria.Text) != 0)
            {
                string Mensaje = string.Empty;

                Categoria objcategoria = new Categoria()
                {
                    IdCategoria = Convert.ToInt32(lblIdCategoria.Text)
                };

                bool delete_Categoria = new CN_Categoria().Eliminar_Categoria(objcategoria, out Mensaje);

                if (delete_Categoria)
                {
                    dgvCategorias.Rows.Remove(dgvCategorias.CurrentRow);
                }
            }

            Editar_Clicked = false;
            pnlEliminar.Visible = false;
            btnEliminar.Visible = true;
            pnlCategoria.Visible = false;
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


        private void CleanUp() // Este void se encarga de LIMPIAR los valores de las TextBox.
        {
            txbDescription.Text = "";
            cmbEstado.SelectedIndex = 0;

            txbDescription.Select();
        }

    }
}
