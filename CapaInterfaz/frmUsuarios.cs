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
using System.Text.RegularExpressions;
using System.Xml;

namespace CapaInterfaz
{
    public partial class frmUsuarios : Form
    {

        // FORMULARIO USUARIOS ----------------------------------------------------------------------------------------------------------------------------------------------
        // Este formulario USUARIOS podra ser abierto por un USUARIO que tenga el ROL ADMINISTRADOR. Aqui se podra ver la lista de USUARIOS registrados en la aplicacion y
        // tambien sus datos, se podra añadir nuevos USUARIOS o editarlos o eliminarlos. 
        // ------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private bool Editar_Clicked = false; // Este bool se encarga de VALIDAR cuando un USUARIO esta SELECCIONADO para editar.
        public frmUsuarios()
        {
            InitializeComponent();
        }
      
        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            cmbEstado.Items.Add(new OptionsComboBox() { Value = 0, Text = "No Activo" });
            cmbEstado.Items.Add(new OptionsComboBox() { Value = 1, Text = "Activo" });
            cmbEstado.DisplayMember = "Text";
            cmbEstado.ValueMember = "Value";
            cmbEstado.SelectedIndex = 0;

            List<Rol> ListRol = new CN_Rol().ToList();

            foreach(Rol item in ListRol) // Este foreach se encarga de GUARDAR los roles y el Id de los roles en las ComboBox de Rol.
            {
                cmbRol.Items.Add(new OptionsComboBox() { Value = item.IdRol, Text = item.Descripcion });
            }

            cmbRol.DisplayMember = "Text";
            cmbRol.ValueMember = "Value";
            cmbRol.SelectedIndex = 0;

            List<Usuario> ListUsuario = new CN_Usuario().ToList();

            foreach(Usuario item in ListUsuario) // Este foreach se encarga de MOSTRAR los USUARIOS GUARDADOS en el DataGridView.
            {
                dgvUsuarios.Rows.Add(new object[] {
                    item.IdUsuario,
                    item.NombreCompleto,
                    item.Documento,                
                    item.Correo,
                    item.Clave,
                    item.objRol.IdRol,
                    item.objRol.Descripcion,
                    item.Estado == true ? "Activo":"No Activo" 
                });
            }
        }

        

        /* --------------------------------------------------------------------- APLICACION DE BUTTONS --------------------------------------------------------------------- */


        private void btnGuardar_Click(object sender, EventArgs e) // Este Button ESCRIBE los datos escritos en los Textbox en el DataGridView.
        {
            //Ejemplo de "Expresiones Regulares".
            Regex rx = new Regex(@"^[0-9]{1,9}(\.[0-9]{0,2})?$");

            if (rx.IsMatch(txbDocument.Text))
            {
                if (txbPassword.Text != txbConfirmPassword.Text)
                {
                    lblPasswordError.Visible = true;
                }

                else
                {
                    string Mensaje = string.Empty;

                    Usuario objusuario = new Usuario()
                    {
                        IdUsuario = Convert.ToInt32(lblIdUsuario.Text),
                        NombreCompleto = txbUserName.Text,
                        Documento = txbDocument.Text,
                        Correo = txbEmail.Text,
                        Clave = txbPassword.Text,
                        objRol = new Rol() { IdRol = Convert.ToInt32(((OptionsComboBox)cmbRol.SelectedItem).Value) },
                        Estado = Convert.ToInt32(((OptionsComboBox)cmbEstado.SelectedItem).Value) == 1 ? true : false
                    };

                    if (Editar_Clicked == false) // Este if se encarga de VERIFICAR que NO se selecciono ningun USUARIO de la lista.
                    {
                        int new_idusuario = new CN_Usuario().Registrar_Usuario(objusuario, out Mensaje);

                        if (new_idusuario != 0) // Este if se encarga de VERIFICAR que se genero un nuevo USUARIO correctamente, si IdUsuario = 0 significa que no se registro un nuevo USUARIO.
                        {
                            dgvUsuarios.Rows.Add(new object[] {
                        new_idusuario,
                        txbUserName.Text,
                        txbDocument.Text,
                        txbEmail.Text,
                        txbPassword.Text,
                        ((OptionsComboBox)cmbRol.SelectedItem).Value.ToString(),
                        ((OptionsComboBox)cmbRol.SelectedItem).Text.ToString(),
                        ((OptionsComboBox)cmbEstado.SelectedItem).Text.ToString()});

                            CleanUp();
                        }

                        else
                        {
                            MessageBox.Show(Mensaje);
                        }
                    }

                    if (Editar_Clicked == true) // Este if se encarga de VERIFICAR que SI se selecciono ningun USUARIO de la lista.
                    {
                        bool edit_usuario = new CN_Usuario().Editar_Usuario(objusuario, out Mensaje);

                        if (edit_usuario)
                        {
                            DataGridViewRow row = dgvUsuarios.CurrentRow;
                            row.Cells["IdUsuario"].Value = lblIdUsuario;
                            row.Cells["UserName"].Value = txbUserName;
                            row.Cells["Document"].Value = txbDocument;
                            row.Cells["Email"].Value = txbEmail;
                            row.Cells["Password"].Value = txbPassword;
                            row.Cells["Password"].Value = txbConfirmPassword;
                            row.Cells["Rol"].Value = ((OptionsComboBox)cmbRol.SelectedItem).Value.ToString();
                            row.Cells["Estado"].Value = ((OptionsComboBox)cmbEstado.SelectedItem).Text.ToString();

                            CleanUp();
                            Editar_Clicked = false;
                            pnlUsuario.Visible = false;
                        }
                    }

                    lblPasswordError.Visible = false;
                    lblDocumentError.Visible = false;
                }
            }

            else
            {
                lblDocumentError.Visible = true;
                txbDocument.Text = "";

                txbDocument.Focus();
            }
        }

        private void icoShowPassword_Click(object sender, EventArgs e) // Este Button se encarga de quitar el PasswordChar de la TextBox que tiene la contraseña.
        {
            if (txbPassword.PasswordChar == '*')
            {
                txbPassword.PasswordChar = '\0';
                icoShowPassword.IconFont = FontAwesome.Sharp.IconFont.Solid;
            }
            else
            {
                txbPassword.PasswordChar = '*';
                icoShowPassword.IconFont = FontAwesome.Sharp.IconFont.Regular;
            }

            txbPassword.Focus();
        }

        private void icoShowConfirmPassword_Click(object sender, EventArgs e) // Este Button se encarga de quitar el PasswordChar de la TextBox que confirma la contraseña.
        {
            if (txbConfirmPassword.PasswordChar == '*')
            {
                txbConfirmPassword.PasswordChar = '\0';
                icoShowConfirmPassword.IconFont = FontAwesome.Sharp.IconFont.Solid;
            }
            else
            {
                txbConfirmPassword.PasswordChar = '*';
                icoShowConfirmPassword.IconFont = FontAwesome.Sharp.IconFont.Regular;
            }

            txbConfirmPassword.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e) // Este Button se encarga de MOSTRAR los datos en la tabla de datos del USUARIO para poder EDITARLOS.
        {
            Editar_Clicked = true;
            pnlUsuario.Visible = true;
            btnEliminar.Visible = true;
            btnCancelarEditar.Visible = true;

            lblIdUsuario.Text = dgvUsuarios.CurrentRow.Cells["IdUsuario"].Value.ToString();
            txbUserName.Text = dgvUsuarios.CurrentRow.Cells["UserName"].Value.ToString();
            txbDocument.Text = dgvUsuarios.CurrentRow.Cells["Document"].Value.ToString();
            txbEmail.Text = dgvUsuarios.CurrentRow.Cells["Email"].Value.ToString();
            txbPassword.Text = dgvUsuarios.CurrentRow.Cells["Password"].Value.ToString();
            txbConfirmPassword.Text = dgvUsuarios.CurrentRow.Cells["Password"].Value.ToString();
            cmbRol.Text = dgvUsuarios.CurrentRow.Cells["Rol"].Value.ToString();
            cmbEstado.Text = dgvUsuarios.CurrentRow.Cells["Estado"].Value.ToString();
        }

        private void btnEliminar_Click(object sender, EventArgs e) // Este Button se encarga de ABRIR el pnlEliminar para ELIMINAR un USUARIO.
        {
            pnlEliminar.Visible = true;           
            btnEliminar.Visible = false;
        }

        private void btnCancelarEditar_Click(object sender, EventArgs e) // Este Button DESELECCIONA el USUARIO anteriormente seleccionada.
        {
            CleanUp();
            Editar_Clicked = false;
            pnlUsuario.Visible = false;
            btnCancelarEditar.Visible = false;
            btnEliminar.Visible = false;
            pnlEliminar.Visible = false;
        }

        private void btnEliminarYES_Click(object sender, EventArgs e) // Este Button se encarga de ELIMINAR el USUARIO.
        {
            if (Convert.ToInt32(lblIdUsuario.Text) != 0)
            {
                string Mensaje = string.Empty;

                Usuario objusuario = new Usuario()
                {
                    IdUsuario = Convert.ToInt32(lblIdUsuario.Text)
                };

                bool delete_usuario = new CN_Usuario().Eliminar_Usuario(objusuario, out Mensaje);

                if (delete_usuario)
                {
                    dgvUsuarios.Rows.Remove(dgvUsuarios.CurrentRow);
                }
            }

            Editar_Clicked = false;
            pnlEliminar.Visible = false;
            btnEliminar.Visible = true;
            pnlUsuario.Visible = false;
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
            txbDocument.Text = "";
            txbUserName.Text = "";
            txbEmail.Text = "";
            txbPassword.Text = "";
            txbConfirmPassword.Text = "";
            cmbRol.SelectedIndex = 0;
            cmbEstado.SelectedIndex = 0;
        }
    }
}
