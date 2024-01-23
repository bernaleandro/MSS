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
    public partial class frmProducto : Form
    {


        // FORMULARIO PRODUCTOS ---------------------------------------------------------------------------------------------------------------------------------------------
        // Este formulario PRODUCTOS podra ser abierto por un USUARIO que tenga el ROL ADMINISTRADOR. Aqui mostraranlos PRODUCTOS que se quieran vender ademas que tambien se
        // podran agregar nuevos PRODUCTOS o editarlos o eliminarlos.
        // ------------------------------------------------------------------------------------------------------------------------------------------------------------------


        private bool Editar_Clicked = false; // Este bool se encarga de VALIDAR cuando un PRODUCTO esta SELECCIONADO para editar.
        public frmProducto()
        {
            InitializeComponent();
        }

        private void frmProducto_Load(object sender, EventArgs e)
        {
            cmbEstado.Items.Add(new OptionsComboBox() { Value = 0, Text = "No Activo" });
            cmbEstado.Items.Add(new OptionsComboBox() { Value = 1, Text = "Activo" });
            cmbEstado.DisplayMember = "Text";
            cmbEstado.ValueMember = "Value";
            cmbEstado.SelectedIndex = 0;

            List<Categoria> ListCategoria = new CN_Categoria().ToList();

            foreach (Categoria item in ListCategoria) // Este foreach se encarga de GUARDAR los PRODUCTOS y el Id de los PRODUCTOS en las ComboBox de Categoria.
            {
                cmbCategoria.Items.Add(new OptionsComboBox() { Value = item.IdCategoria, Text = item.Descripcion });
            }

            cmbCategoria.DisplayMember = "Text";
            cmbCategoria.ValueMember = "Value";
            cmbCategoria.SelectedIndex = 0;

            List<Producto> ListProducto = new CN_Producto().ToList();

            foreach (Producto item in ListProducto) // Este foreach se encarga de MOSTRAR los PRODUCTOS GUARDADOS en el DataGridView.
            {
                dgvProductos.Rows.Add(new object[] {
                    item.IdProducto,
                    item.Codigo,
                    item.Nombre,
                    item.Descripcion,
                    item.objCategoria.IdCategoria,
                    item.objCategoria.Descripcion,
                    item.Stock,
                    item.PrecioCompra,
                    item.PrecioVenta,
                    item.Estado == true ? "Activo":"No Activo"
                });
            }
        }


        /* --------------------------------------------------------------------- APLICACION DE BUTTONS --------------------------------------------------------------------- */


        private void btnGuardar_Click(object sender, EventArgs e) // Este Button ESCRIBE los datos escritos en los Textbox en el DataGridView.
        {
            string Mensaje = string.Empty;

            Producto objProducto = new Producto()
            {
                IdProducto = Convert.ToInt32(lblIdProducto.Text),
                Codigo = txbCode.Text,
                Nombre = txbProductName.Text,
                Descripcion = txbDescription.Text,
                objCategoria = new Categoria() { IdCategoria = Convert.ToInt32(((OptionsComboBox)cmbCategoria.SelectedItem).Value) },
                Estado = Convert.ToInt32(((OptionsComboBox)cmbEstado.SelectedItem).Value) == 1 ? true : false
            };

            if (Editar_Clicked == false) // Este if se encarga de VERIFICAR que NO se selecciono ningun PRODUCTO de la lista.
            {
                int new_idProducto = new CN_Producto().Registrar_Producto(objProducto, out Mensaje);

                if (new_idProducto != 0) // Este if se encarga de VERIFICAR que se genero un nuevo PRODUCTO correctamente, si IdProducto = 0 significa que no se registro un nuevo PRODUCTO.
                {
                    dgvProductos.Rows.Add(new object[] {
                    new_idProducto,
                    txbCode.Text,
                    txbProductName.Text,
                    txbDescription.Text,
                    ((OptionsComboBox)cmbCategoria.SelectedItem).Value.ToString(),
                    ((OptionsComboBox)cmbCategoria.SelectedItem).Text.ToString(),
                    "0",
                    "0.00",
                    "0.00",
                    ((OptionsComboBox)cmbEstado.SelectedItem).Text.ToString()
                    });

                    CleanUp();
                }

                else
                {
                    MessageBox.Show(Mensaje);
                }
            }

            if (Editar_Clicked == true) // Este if se encarga de VERIFICAR que SI se selecciono ningun PRODUCTO de la lista.
            {
                bool edit_Producto = new CN_Producto().Editar_Producto(objProducto, out Mensaje);

                if (edit_Producto)
                {
                    DataGridViewRow row = dgvProductos.CurrentRow;
                    row.Cells["IdProducto"].Value = lblIdProducto;
                    row.Cells["Code"].Value = txbCode;
                    row.Cells["ProductName"].Value = txbProductName;
                    row.Cells["Description"].Value = txbDescription;
                    row.Cells["IdCategoria"].Value = ((OptionsComboBox)cmbCategoria.SelectedItem).Value.ToString();
                    row.Cells["Categoria"].Value = ((OptionsComboBox)cmbCategoria.SelectedItem).Text.ToString(); 
                    row.Cells["Estado"].Value = ((OptionsComboBox)cmbEstado.SelectedItem).Text.ToString();

                    CleanUp();
                    Editar_Clicked = false;
                    pnlProducto.Visible = false;
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e) // Este Button se encarga de MOSTRAR los datos en la tabla de datos del PRODUCTO para poder EDITARLOS.
        {
            Editar_Clicked = true;
            pnlProducto.Visible = true;
            btnEliminar.Visible = true;
            btnCancelarEditar.Visible = true;

            lblIdProducto.Text = dgvProductos.CurrentRow.Cells["IdProducto"].Value.ToString();
            txbCode.Text = dgvProductos.CurrentRow.Cells["Code"].Value.ToString();
            txbProductName.Text = dgvProductos.CurrentRow.Cells["ProductName"].Value.ToString();
            txbDescription.Text = dgvProductos.CurrentRow.Cells["Description"].Value.ToString();
            cmbCategoria.Text = dgvProductos.CurrentRow.Cells["Categoria"].Value.ToString();
            cmbEstado.Text = dgvProductos.CurrentRow.Cells["Estado"].Value.ToString();
        }
        
        private void btnEliminar_Click(object sender, EventArgs e) // Este Button se encarga de ABRIR el pnlEliminar para ELIMINAR un PRODUCTO.
        {
            pnlEliminar.Visible = true;
            btnEliminar.Visible = false;
        }

        private void btnCancelarEditar_Click(object sender, EventArgs e) // Este Button DESELECCIONA el PRODUCTO anteriormente seleccionada.
        {
            CleanUp();
            Editar_Clicked = false;
            pnlProducto.Visible = false;
            btnCancelarEditar.Visible = false;
            btnEliminar.Visible = false;
            pnlEliminar.Visible = false;
        }

        private void btnEliminarYES_Click(object sender, EventArgs e) // Este Button se encarga de ELIMINAR el PRODUCTO.
        {
            if (Convert.ToInt32(lblIdProducto.Text) != 0)
            {
                string Mensaje = string.Empty;

                Producto objProducto = new Producto()
                {
                    IdProducto = Convert.ToInt32(lblIdProducto.Text)
                };

                bool delete_Producto = new CN_Producto().Eliminar_Producto(objProducto, out Mensaje);

                if (delete_Producto)
                {
                    dgvProductos.Rows.Remove(dgvProductos.CurrentRow);
                }
            }

            Editar_Clicked = false;
            pnlEliminar.Visible = false;
            btnEliminar.Visible = true;
            pnlProducto.Visible = false;
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


        private void CleanUp() // Este evento se encarga de LIMPIAR los TextBox luego de presionar el Button de GUARDAR PRODUCTO.
        {
            txbCode.Text = "";
            txbProductName.Text = "";
            txbDescription.Text = "";
            cmbCategoria.SelectedIndex = 0;
            cmbEstado.SelectedIndex = 0;
        }     
    }
}
