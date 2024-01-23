using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;
using CapaEntidad;
using FontAwesome.Sharp;
using System.Text.RegularExpressions;

namespace CapaInterfaz
{
    public partial class frmLogin : Form
    {


        // FORMULARIO LOGIN -------------------------------------------------------------------------------------------------------------------------------------------------
        // Este formulario LOGIN sera el primero en mostrar el programa. Su funcion es que cualquier que ingrese a la aplicacion deba registrar su cuenta de USUARIO con su
        // respectivo Documento y Contraseña, todo esto haciendo uso de la clase USUARIO de la CAPA ENTIDAD que guarda sus datos en una base de datos SQL.
        // ------------------------------------------------------------------------------------------------------------------------------------------------------------------


        private bool MaximizeClicked = false;

        public frmLogin()
        {
            InitializeComponent();

            pnlError.Visible = false;
        }


        /* --------------------------------------------------------------------- APLICACION DE BUTTONS --------------------------------------------------------------------- */


        private void btnIngresar_Click(object sender, EventArgs e) // Este Button se encarga de INGRESAR al MENU con el USUARIO registrado.
        {           
            Usuario objUsuario = new CN_Usuario().ToList().Where(usu => usu.Documento == txbDocument.Text && usu.Clave == txbPassword.Text).FirstOrDefault();
            Negocio objNegocio = new CN_Negocio().Datos_Negocio();

            if (objUsuario != null)
            {
                frmMenu form = new frmMenu(objUsuario, objNegocio);

                form.Show();
                this.Hide();

                form.FormClosing += frm_Closing;
            }

            else
            {
                pnlError.Visible = true;
                txbDocument.Text = "";
                txbPassword.Text = "";
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

        private void icoExit_Click(object sender, EventArgs e) // Este Button se encarga de MOSTRAR el pnlMensaje que muestre las opciones para SALIR del MENU.
        {
            if (pnlMensaje.Visible == false)
            {
                pnlMensaje.Visible = true;

                icoExit.IconChar = IconChar.CaretUp;
                icoExit.BackColor = ColorTranslator.FromHtml("#D08F60");
                icoExit.IconColor = ColorTranslator.FromHtml("#404040");

                icoMaximize.BackColor = ColorTranslator.FromHtml("#D08F60");
                icoMaximize.IconColor = ColorTranslator.FromHtml("#404040");

                icoMinimize.BackColor = ColorTranslator.FromHtml("#D08F60");
                icoMinimize.IconColor = ColorTranslator.FromHtml("#404040");
            }

            else
            {
                pnlMensaje.Visible = false;

                icoExit.IconChar = IconChar.XmarkCircle;
                icoExit.BackColor = ColorTranslator.FromHtml("#353535");
                icoExit.IconColor = ColorTranslator.FromHtml("#D08F60");

                icoMaximize.BackColor = ColorTranslator.FromHtml("#353535");
                icoMaximize.IconColor = ColorTranslator.FromHtml("#646464");

                icoMinimize.BackColor = ColorTranslator.FromHtml("#353535");
                icoMinimize.IconColor = ColorTranslator.FromHtml("#646464");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e) // Este Button se encarga de CERRAR la APLICACION.
        {
            Application.Exit();
        }

        private void icoMaximize_Click(object sender, EventArgs e) // Este Button se encarga de MAXIMIZAR el FORMULARIO.
        {
            if (MaximizeClicked == false)
            {
                icoMaximize.IconChar = IconChar.CircleChevronDown;
                MaximizeClicked = true;
                this.WindowState = FormWindowState.Maximized;
            }

            else
            {
                icoMaximize.IconChar = IconChar.CircleChevronUp;
                MaximizeClicked = false;
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void icoMinimize_Click(object sender, EventArgs e) // Este Button se encarga de MINIMIZAR el FORMULARIO.
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /* --------------------------------------------------------------------- APLICACION DE EVENTO --------------------------------------------------------------------- */


        private void frm_Closing(object sender, FormClosingEventArgs e) // Este evento se encarga de LIMPIAR todas las TextBox cuando se vuelva a este formulario.
        {
            txbDocument.Text = "";
            txbPassword.Text = "";
            pnlError.Visible = false;

            this.Show();
        }
    }
}
