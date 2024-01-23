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
using FontAwesome.Sharp;
using System.IO;
using System.Diagnostics;
using CapaInterfaz.Translator;
using System.Threading;

namespace CapaInterfaz
{
    public partial class frmMenu : Form
    {
  

        // FORMULARIO MENU --------------------------------------------------------------------------------------------------------------------------------------------------
        // Este formulario MENU sera el que se abra luego de que algun USUARIO se haya registrado en el formulario de LOGIN. Su funcion es ofrecer las opciones que brinda la
        // aplicacion para aquel que se haya ingresado, dependiendo de su ROL perteneciente de la clase ROL en la CAPA ENTIDAD tendra mas o menos opciones. Aquel que ingrese
        // con el ROL ADMINISTRADOR podra ver las opciones de USUARIO, MANTENEDOR y REPORTES mientras que el ROL EMPLEADO no podra, para esto se usara la clase PERMISO que
        // tiene el atributo de NombreMenu en la base de datos SQL, este atributo marca a que opciones puede acceder dependiendo de su ROL.
        // ------------------------------------------------------------------------------------------------------------------------------------------------------------------


        private static Usuario IngresoUsuario;
        private static Negocio IngresoNegocio;

        private bool MaximizeClicked = false;

        public frmMenu(Usuario usuario_login, Negocio negocio_login)
        {
            IngresoUsuario = usuario_login;
            IngresoNegocio = negocio_login;

            InitializeComponent();
            CustomizeDesign();
        }


        private void Menu_Load(object sender, EventArgs e)
        {
            List<Permiso> ListPermiso = new CN_Permiso().ToList(IngresoUsuario.IdUsuario);

            foreach (Control icobtn in pnlMenu1.Controls) // Este foreach se encarga de MOSTRAR los botones del menu dependiendo del Rol del que ingreso.
            {
                if (icobtn is IconButton)
                {
                    bool findBtn = ListPermiso.Any(menu => menu.NombreMenu == icobtn.Name);

                    if (findBtn == false)
                    {
                        icobtn.Visible = false;
                    }
                }
            }

            if (btnMantenedor.Visible == false)
            {
                btnNegocioMenu.Visible = false;
            }

            // Detalle de icono y nombre del USUARIO que ingreso.

            lblUsuario.Text = IngresoUsuario.NombreCompleto;
            lblId.Text = Convert.ToString(IngresoUsuario.IdUsuario);
            lblRol.Text = IngresoUsuario.objRol.Descripcion;
            lblDocument.Text = IngresoUsuario.Documento;

            if (IngresoUsuario.Estado)
            {
                lblEstado.Text = "Activo";
            }

            else
            {
                lblEstado.Text = "No Activo";
            }

            if(IngresoUsuario.objRol.IdRol == 1)
            {
                icoUsuario.IconChar = IconChar.UserTie;
            }

            if (IngresoUsuario.objRol.IdRol == 2)
            {
                icoUsuario.IconChar = IconChar.User;
            }

            // Logo y nombre del NEGOCIO

            bool registrado = true;
            byte[] logoBytes = new CN_Negocio().Registrar_Logo(out registrado);

            if (registrado)
            {
                pcbLogo.Image = Byte2Image(logoBytes);
            }

            lblBuisnessName.Text = IngresoNegocio.Nombre;
            lblAddress.Text = IngresoNegocio.Direccion;

            List<Proveedor> ListProveedor = new CN_Proveedor().ToList();

            foreach (Proveedor item in ListProveedor) // Este foreach se encarga de MOSTRAR los PROVEEDORES GUARDADOS en el DataGridView.
            {
                dgvProveedores.Rows.Add(new object[] {
                    item.RazonSocial,
                    item.Documento,
                    item.Correo,
                    item.Telefono,
                });
            }

            List<Cliente> ListCliente = new CN_Cliente().ToList();

            foreach (Cliente item in ListCliente) // Este foreach se encarga de MOSTRAR los CLIENTES GUARDADOS en el DataGridView.
            {
                dgvClientes.Rows.Add(new object[] {
                    item.NombreCompleto,
                    item.Documento,
                    item.Correo,
                    item.Telefono,
                });
            }

            frmLogin login = new frmLogin();

            if (login.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Maximized;
            }

            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }


        /* --------------------------------------------------------------------- APLICACION DE BUTTONS --------------------------------------------------------------------- */


        private void btnUsuarios_Click(object sender, EventArgs e) // Este Button se encarga de ABRIR el formulario de USUARIOS.
        {           
            btnUsuarios.BackColor = ColorTranslator.FromHtml("#D08F60");
            btnMantenedor.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnVentas.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnCompras.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnClientes.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnProveedores.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnReportes.BackColor = ColorTranslator.FromHtml("#2B2B2B");

            btnUsuarios.ForeColor = ColorTranslator.FromHtml("#2B2B2B");
            btnMantenedor.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnVentas.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnCompras.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnClientes.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnProveedores.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnReportes.ForeColor = ColorTranslator.FromHtml("#D08F60");

            btnMantenedor.IconColor = ColorTranslator.FromHtml("#D08F60");
            btnVentas.IconColor = ColorTranslator.FromHtml("#D08F60");
            btnCompras.IconColor = ColorTranslator.FromHtml("#D08F60");       
            btnReportes.IconColor = ColorTranslator.FromHtml("#D08F60");   
            
            OpenChildForm(new frmUsuarios());

            HideSubMenu();
        }

        private void btnMantenedor_Click(object sender, EventArgs e) // Este Button se encarga de ABRIR el submenu de MANTENEDOR.
        {
            btnUsuarios.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnMantenedor.BackColor = ColorTranslator.FromHtml("#D08F60");
            btnVentas.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnCompras.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnClientes.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnProveedores.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnReportes.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnCategoria.BackColor = ColorTranslator.FromHtml("#404040");
            btnProducto.BackColor = ColorTranslator.FromHtml("#404040");
            btnNegocio.BackColor = ColorTranslator.FromHtml("#404040");

            btnUsuarios.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnMantenedor.ForeColor = ColorTranslator.FromHtml("#2B2B2B");
            btnVentas.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnCompras.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnClientes.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnProveedores.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnReportes.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnCategoria.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnProducto.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnNegocio.ForeColor = ColorTranslator.FromHtml("#D08F60");

            btnMantenedor.IconColor = ColorTranslator.FromHtml("#2B2B2B");
            btnVentas.IconColor = ColorTranslator.FromHtml("#D08F60");
            btnCompras.IconColor = ColorTranslator.FromHtml("#D08F60");
            btnReportes.IconColor = ColorTranslator.FromHtml("#D08F60");

            ShowSubMenu(subpnlMantenedor);
        }

        private void btnCategoria_Click(object sender, EventArgs e) // Este Button se encarga de ABRIR el formulario de CATEGORIAS.
        {
            btnCategoria.BackColor = ColorTranslator.FromHtml("#D08F60");
            btnProducto.BackColor = ColorTranslator.FromHtml("#404040");
            btnNegocio.BackColor = ColorTranslator.FromHtml("#404040");

            btnCategoria.ForeColor = ColorTranslator.FromHtml("#404040");
            btnProducto.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnNegocio.ForeColor = ColorTranslator.FromHtml("#D08F60");

            OpenChildForm(new frmCategoria());  
        }

        private void btnProducto_Click(object sender, EventArgs e) // Este Button se encarga de ABRIR el formulario de PRODUCTOS.
        {
            btnCategoria.BackColor = ColorTranslator.FromHtml("#404040");
            btnProducto.BackColor = ColorTranslator.FromHtml("#D08F60");
            btnNegocio.BackColor = ColorTranslator.FromHtml("#404040");

            btnCategoria.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnProducto.ForeColor = ColorTranslator.FromHtml("#404040");
            btnNegocio.ForeColor = ColorTranslator.FromHtml("#D08F60");

            OpenChildForm(new frmProducto());       
        }

        private void btnNegocio_Click(object sender, EventArgs e) // Este Button se encarga de ABRIR el formulario de NEGOCIOS.
        {
            btnCategoria.BackColor = ColorTranslator.FromHtml("#404040");
            btnProducto.BackColor = ColorTranslator.FromHtml("#404040");
            btnNegocio.BackColor = ColorTranslator.FromHtml("#D08F60");

            btnCategoria.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnProducto.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnNegocio.ForeColor = ColorTranslator.FromHtml("#404040");

            OpenChildForm(new frmNegocio());    
        }

        private void btnVentas_Click(object sender, EventArgs e) // Este Button se encarga de ABRIR el submenu de VENTAS.
        {
            btnUsuarios.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnMantenedor.BackColor = ColorTranslator.FromHtml("#2B2B2B");            
            btnVentas.BackColor = ColorTranslator.FromHtml("#D08F60");           
            btnCompras.BackColor = ColorTranslator.FromHtml("#2B2B2B");          
            btnClientes.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnProveedores.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnReportes.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnRegistrarVenta.BackColor = ColorTranslator.FromHtml("#404040");
            btnDetalleVenta.BackColor = ColorTranslator.FromHtml("#404040");

            btnUsuarios.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnMantenedor.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnVentas.ForeColor = ColorTranslator.FromHtml("#2B2B2B");
            btnCompras.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnClientes.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnProveedores.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnReportes.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnRegistrarVenta.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnDetalleVenta.ForeColor = ColorTranslator.FromHtml("#D08F60");

            btnMantenedor.IconColor = ColorTranslator.FromHtml("#D08F60");
            btnVentas.IconColor = ColorTranslator.FromHtml("#2B2B2B");
            btnCompras.IconColor = ColorTranslator.FromHtml("#D08F60");
            btnReportes.IconColor = ColorTranslator.FromHtml("#D08F60");

            ShowSubMenu(subpnlVentas);
        }

        private void btnRegistrarVenta_Click(object sender, EventArgs e) // Este Button se encarga de ABRIR el formulario de REGISTRAR VENTAS.
        {
            btnRegistrarVenta.BackColor = ColorTranslator.FromHtml("#D08F60");
            btnDetalleVenta.BackColor = ColorTranslator.FromHtml("#404040");

            btnRegistrarVenta.ForeColor = ColorTranslator.FromHtml("#404040");
            btnDetalleVenta.ForeColor = ColorTranslator.FromHtml("#D08F60");

            OpenChildForm(new frmRegistrar_Venta(IngresoUsuario));
        }

        private void btnDetalleVenta_Click(object sender, EventArgs e) // Este Button se encarga de ABRIR el formulario de DETALLE VENTAS.
        {
            btnRegistrarVenta.BackColor = ColorTranslator.FromHtml("#404040");
            btnDetalleVenta.BackColor = ColorTranslator.FromHtml("#D08F60");

            btnRegistrarVenta.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnDetalleVenta.ForeColor = ColorTranslator.FromHtml("#404040");

            OpenChildForm(new frmDetalle_Venta());   
        }

        private void btnCompras_Click(object sender, EventArgs e) // Este Button se encarga de ABRIR el submenu de COMPRAS.
        {
            btnUsuarios.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnMantenedor.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnCategoria.BackColor = ColorTranslator.FromHtml("#404040");
            btnProducto.BackColor = ColorTranslator.FromHtml("#404040");
            btnNegocio.BackColor = ColorTranslator.FromHtml("#404040");
            btnVentas.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnCompras.BackColor = ColorTranslator.FromHtml("#D08F60");
            btnClientes.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnProveedores.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnReportes.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnDetalleCompra.BackColor = ColorTranslator.FromHtml("#404040");
            btnRegistrarCompra.BackColor = ColorTranslator.FromHtml("#404040");

            btnUsuarios.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnMantenedor.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnVentas.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnCompras.ForeColor = ColorTranslator.FromHtml("#2B2B2B");
            btnClientes.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnProveedores.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnReportes.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnDetalleCompra.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnRegistrarCompra.ForeColor = ColorTranslator.FromHtml("#D08F60");

            btnMantenedor.IconColor = ColorTranslator.FromHtml("#D08F60");
            btnVentas.IconColor = ColorTranslator.FromHtml("#D08F60");
            btnCompras.IconColor = ColorTranslator.FromHtml("#2B2B2B");
            btnReportes.IconColor = ColorTranslator.FromHtml("#D08F60");

            ShowSubMenu(subpnlCompras);
        }

        private void btnRegistrarCompra_Click(object sender, EventArgs e) // Este Button se encarga de ABRIR el formulario de REGISTRAR COMPRAS.
        {
            btnRegistrarCompra.BackColor = ColorTranslator.FromHtml("#D08F60");
            btnDetalleCompra.BackColor = ColorTranslator.FromHtml("#404040");

            btnRegistrarCompra.ForeColor = ColorTranslator.FromHtml("#404040");
            btnDetalleCompra.ForeColor = ColorTranslator.FromHtml("#D08F60");

            OpenChildForm(new frmRegistrar_Compra(IngresoUsuario));   
        }

        private void btnDetalleCompra_Click(object sender, EventArgs e) // Este Button se encarga de ABRIR el formulario de DETALLE COMPRAS.
        {
            btnRegistrarCompra.BackColor = ColorTranslator.FromHtml("#404040");
            btnDetalleCompra.BackColor = ColorTranslator.FromHtml("#D08F60");

            btnRegistrarCompra.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnDetalleCompra.ForeColor = ColorTranslator.FromHtml("#404040");

            OpenChildForm(new frmDetalle_Compra());           
        }

        private void btnClientes_Click(object sender, EventArgs e) // Este Button se encarga de ABRIR el formulario de CLIENTES.
        {
            btnUsuarios.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnMantenedor.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnVentas.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnCompras.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnClientes.BackColor = ColorTranslator.FromHtml("#D08F60");
            btnProveedores.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnReportes.BackColor = ColorTranslator.FromHtml("#2B2B2B");

            btnUsuarios.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnMantenedor.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnVentas.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnCompras.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnClientes.ForeColor = ColorTranslator.FromHtml("#2B2B2B");
            btnProveedores.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnReportes.ForeColor = ColorTranslator.FromHtml("#D08F60");

            btnMantenedor.IconColor = ColorTranslator.FromHtml("#D08F60");
            btnVentas.IconColor = ColorTranslator.FromHtml("#D08F60");
            btnCompras.IconColor = ColorTranslator.FromHtml("#D08F60");
            btnReportes.IconColor = ColorTranslator.FromHtml("#D08F60");

            OpenChildForm(new frmClientes());

            HideSubMenu();
        }

        private void btnProveedores_Click(object sender, EventArgs e) // Este Button se encarga de ABRIR el formulario de PROVEEDORES.
        {
            btnUsuarios.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnMantenedor.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnVentas.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnCompras.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnClientes.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnProveedores.BackColor = ColorTranslator.FromHtml("#D08F60");
            btnReportes.BackColor = ColorTranslator.FromHtml("#2B2B2B");

            btnUsuarios.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnMantenedor.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnVentas.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnCompras.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnClientes.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnProveedores.ForeColor = ColorTranslator.FromHtml("#2B2B2B");
            btnReportes.ForeColor = ColorTranslator.FromHtml("#D08F60");

            btnMantenedor.IconColor = ColorTranslator.FromHtml("#D08F60");
            btnVentas.IconColor = ColorTranslator.FromHtml("#D08F60");
            btnCompras.IconColor = ColorTranslator.FromHtml("#D08F60");
            btnReportes.IconColor = ColorTranslator.FromHtml("#D08F60");

            OpenChildForm(new frmProveedores());

            HideSubMenu();
        }

        private void btnReportes_Click(object sender, EventArgs e) // Este Button se encarga de ABRIR el submenu de REPORTES.
        {
            btnUsuarios.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnMantenedor.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnVentas.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnCompras.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnClientes.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnProveedores.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnReportes.BackColor = ColorTranslator.FromHtml("#D08F60");
            btnReporteVenta.BackColor = ColorTranslator.FromHtml("#404040");
            btnReporteCompra.BackColor = ColorTranslator.FromHtml("#404040");

            btnUsuarios.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnMantenedor.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnVentas.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnCompras.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnClientes.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnProveedores.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnReportes.ForeColor = ColorTranslator.FromHtml("#2B2B2B");
            btnReporteVenta.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnReporteCompra.ForeColor = ColorTranslator.FromHtml("#D08F60");

            btnMantenedor.IconColor = ColorTranslator.FromHtml("#D08F60");
            btnVentas.IconColor = ColorTranslator.FromHtml("#D08F60");
            btnCompras.IconColor = ColorTranslator.FromHtml("#D08F60");
            btnReportes.IconColor = ColorTranslator.FromHtml("#2B2B2B");

            ShowSubMenu(subpnlReportes);
        }

        private void btnReporteVenta_Click(object sender, EventArgs e) // Este Button se encarga de ABRIR el formulario de REPORTES VENTAS.
        {
            btnReporteVenta.BackColor = ColorTranslator.FromHtml("#D08F60");
            btnReporteCompra.BackColor = ColorTranslator.FromHtml("#404040");

            btnReporteVenta.ForeColor = ColorTranslator.FromHtml("#404040");
            btnReporteCompra.ForeColor = ColorTranslator.FromHtml("#D08F60");
         
            OpenChildForm(new frmReportes_Ventas());     
        }

        private void btnReporteCompra_Click(object sender, EventArgs e) // Este Button se encarga de ABRIR el formulario de REPORTES COMPRAS.
        {
            btnReporteVenta.BackColor = ColorTranslator.FromHtml("#404040");
            btnReporteCompra.BackColor = ColorTranslator.FromHtml("#D08F60");

            btnReporteVenta.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnReporteCompra.ForeColor = ColorTranslator.FromHtml("#404040");

            OpenChildForm(new frmReportes_Compras());           
        }

        private void pcbMSS_Click(object sender, EventArgs e) // Esta PictureBox se encarga de CERRAR el formulario de pnlChildForm que se este viendo .
        {
            if (activeForm != null)
            {
                btnUsuarios.BackColor = ColorTranslator.FromHtml("#2B2B2B");
                btnMantenedor.BackColor = ColorTranslator.FromHtml("#2B2B2B");
                btnVentas.BackColor = ColorTranslator.FromHtml("#2B2B2B");
                btnCompras.BackColor = ColorTranslator.FromHtml("#2B2B2B");
                btnClientes.BackColor = ColorTranslator.FromHtml("#2B2B2B");
                btnProveedores.BackColor = ColorTranslator.FromHtml("#2B2B2B");
                btnReportes.BackColor = ColorTranslator.FromHtml("#2B2B2B");

                btnUsuarios.ForeColor = ColorTranslator.FromHtml("#D08F60");
                btnMantenedor.ForeColor = ColorTranslator.FromHtml("#D08F60");
                btnVentas.ForeColor = ColorTranslator.FromHtml("#D08F60");
                btnCompras.ForeColor = ColorTranslator.FromHtml("#D08F60");
                btnClientes.ForeColor = ColorTranslator.FromHtml("#D08F60");
                btnProveedores.ForeColor = ColorTranslator.FromHtml("#D08F60");
                btnReportes.ForeColor = ColorTranslator.FromHtml("#D08F60");

                btnMantenedor.IconColor = ColorTranslator.FromHtml("#D08F60");
                btnVentas.IconColor = ColorTranslator.FromHtml("#D08F60");
                btnCompras.IconColor = ColorTranslator.FromHtml("#D08F60");
                btnReportes.IconColor = ColorTranslator.FromHtml("#D08F60");

                HideSubMenu();
                activeForm.Close();
            }
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

        private void btnVolverLogin_Click(object sender, EventArgs e) // Este Button se encarga de VOLVER al LOGIN.
        {
            this.Close();
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

        private void btnProveedoresMenu_Click(object sender, EventArgs e)
        {
            btnUsuarios.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnMantenedor.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnVentas.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnCompras.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnClientes.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnProveedores.BackColor = ColorTranslator.FromHtml("#D08F60");
            btnReportes.BackColor = ColorTranslator.FromHtml("#2B2B2B");

            btnUsuarios.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnMantenedor.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnVentas.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnCompras.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnClientes.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnProveedores.ForeColor = ColorTranslator.FromHtml("#2B2B2B");
            btnReportes.ForeColor = ColorTranslator.FromHtml("#D08F60");

            btnMantenedor.IconColor = ColorTranslator.FromHtml("#D08F60");
            btnVentas.IconColor = ColorTranslator.FromHtml("#D08F60");
            btnCompras.IconColor = ColorTranslator.FromHtml("#D08F60");
            btnReportes.IconColor = ColorTranslator.FromHtml("#D08F60");

            OpenChildForm(new frmProveedores());

            HideSubMenu();
        }

        private void btnClientesMenu_Click(object sender, EventArgs e)
        {
            btnUsuarios.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnMantenedor.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnVentas.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnCompras.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnClientes.BackColor = ColorTranslator.FromHtml("#D08F60");
            btnProveedores.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnReportes.BackColor = ColorTranslator.FromHtml("#2B2B2B");

            btnUsuarios.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnMantenedor.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnVentas.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnCompras.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnClientes.ForeColor = ColorTranslator.FromHtml("#2B2B2B");
            btnProveedores.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnReportes.ForeColor = ColorTranslator.FromHtml("#D08F60");

            btnMantenedor.IconColor = ColorTranslator.FromHtml("#D08F60");
            btnVentas.IconColor = ColorTranslator.FromHtml("#D08F60");
            btnCompras.IconColor = ColorTranslator.FromHtml("#D08F60");
            btnReportes.IconColor = ColorTranslator.FromHtml("#D08F60");

            OpenChildForm(new frmClientes());

            HideSubMenu();
        }

        private void btnNegocioMenu_Click(object sender, EventArgs e)
        {
            btnUsuarios.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnMantenedor.BackColor = ColorTranslator.FromHtml("#D08F60");
            btnVentas.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnCompras.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnClientes.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnProveedores.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnReportes.BackColor = ColorTranslator.FromHtml("#2B2B2B");
            btnCategoria.BackColor = ColorTranslator.FromHtml("#404040");
            btnProducto.BackColor = ColorTranslator.FromHtml("#404040");
            btnNegocio.BackColor = ColorTranslator.FromHtml("#404040");

            btnUsuarios.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnMantenedor.ForeColor = ColorTranslator.FromHtml("#2B2B2B");
            btnVentas.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnCompras.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnClientes.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnProveedores.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnReportes.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnCategoria.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnProducto.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnNegocio.ForeColor = ColorTranslator.FromHtml("#D08F60");

            btnMantenedor.IconColor = ColorTranslator.FromHtml("#2B2B2B");
            btnVentas.IconColor = ColorTranslator.FromHtml("#D08F60");
            btnCompras.IconColor = ColorTranslator.FromHtml("#D08F60");
            btnReportes.IconColor = ColorTranslator.FromHtml("#D08F60");

            ShowSubMenu(subpnlMantenedor);

            btnCategoria.BackColor = ColorTranslator.FromHtml("#404040");
            btnProducto.BackColor = ColorTranslator.FromHtml("#404040");
            btnNegocio.BackColor = ColorTranslator.FromHtml("#D08F60");

            btnCategoria.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnProducto.ForeColor = ColorTranslator.FromHtml("#D08F60");
            btnNegocio.ForeColor = ColorTranslator.FromHtml("#404040");

            OpenChildForm(new frmNegocio());

        }

        private void icoInfo_Click(object sender, EventArgs e) // Process
        {
            Process process = new Process();
            process.StartInfo.FileName = "notepad.exe";
            process.StartInfo.Arguments = @"C:\Users\MSI10\OneDrive\Documentos\UAI Lomas\2do año\Segundo Cuatrimestre\Lenguaje de Ultima Generacion (LUG)\Trabajo de asignatura\MSS\CapaInterfaz\PROCESS.txt";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            process.Start();
        }

        private void btnLenguajeIngles_Click(object sender, EventArgs e)
        {
            LoadLenguage();
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
        }


        /* --------------------------------------------------------------------- APLICACION DE EVENTOS --------------------------------------------------------------------- */


        private void CustomizeDesign() // Este evento le quita la VISIBILIDAD a los subpaneles al momento de cargar el programa.
        {
            subpnlMantenedor.Visible = false;
            subpnlVentas.Visible = false;
            subpnlCompras.Visible = false;
            subpnlReportes.Visible = false;
        }

        private void HideSubMenu() // Este evento se encarga de OCULTAR los submenues de los Button cuando otro Button esta seleccionado.
        {
            if (subpnlMantenedor.Visible == true)
                subpnlMantenedor.Visible = false;
            if (subpnlVentas.Visible == true)
                subpnlVentas.Visible = false;
            if (subpnlCompras.Visible == true)
                subpnlCompras.Visible = false;
            if (subpnlReportes.Visible == true)
                subpnlReportes.Visible = false;
        }

        private void ShowSubMenu(Panel subMenu) // Este evento se encarga de MOSTRAR los submenues cuando el Button de este es seleccionado.
        {
            if (subMenu.Visible == false)
            {
                HideSubMenu();
                subMenu.Visible = true;
            }

            else
            {
                subMenu.Visible = false;
            }
        }

        private static Form activeForm = null;

        private void OpenChildForm(Form childForm) // Este evento se encarga de ABRIR el formulario de los Button mas pequeño en un panel.
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnlChildForm.Controls.Add(childForm);
            pnlChildForm.Tag = childForm;

            childForm.BringToFront();
            childForm.Show();
        }

        public Image Byte2Image(byte[] imageBytes) // Este METODO nos permite convertir un array de BYTES a una IMAGEN.
        {
            MemoryStream m_stream = new MemoryStream();
            m_stream.Write(imageBytes, 0, imageBytes.Length);

            Image image = new Bitmap(m_stream);

            return image;
        }

        public void LoadLenguage()
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            btnUsuarios.Text = Strings.btnUsuarios;
            btnMantenedor.Text = Strings.btnMantenedor;
            btnCategoria.Text = Strings.btnCategoria;
            btnProducto.Text = Strings.btnProducto;
            btnNegocio.Text = Strings.btnNegocio;
            btnVentas.Text = Strings.btnVentas;
            btnRegistrarVenta.Text = Strings.btnRegistrarVentas;
            btnDetalleVenta.Text = Strings.btnDetalleVenta;
            btnCompras.Text = Strings.btnCompras;
            btnRegistrarCompra.Text = Strings.btnRegistrarCompras;
            btnDetalleCompra.Text = Strings.btnDetalleCompra;
            btnClientes.Text = Strings.btnClientes;
            btnProveedores.Text = Strings.btnProveedores;
            btnReportes.Text = Strings.btnReportes;
            btnReporteVenta.Text = Strings.btnReporteVenta;
            btnReporteCompra.Text = Strings.btnReporteCompra;

            lblMensaje.Text = Strings.lblMensaje;
            btnVolverLogin.Text = Strings.btnVolverLogin;
            btnSalir.Text = Strings.btnSalir;
        }

        // FileSystemWatcher
        private void fswWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            MessageBox.Show(String.Format("File_Changed {0} {1}", e.FullPath, e.Name));
        }

        private void fswWatcher_Created(object sender, FileSystemEventArgs e)
        {
            MessageBox.Show(String.Format("File_Created {0} {1}", e.FullPath, e.Name));
        }

        private void fswWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            MessageBox.Show(String.Format("File_Deleted {0} {1}", e.FullPath, e.Name));
        }

        private void fswWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            MessageBox.Show(String.Format("File_Renamed {0} {1}", e.FullPath, e.Name));
        }
    }   
}
