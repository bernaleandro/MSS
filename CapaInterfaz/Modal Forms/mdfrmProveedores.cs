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

namespace CapaInterfaz.Modal_Forms
{
    public partial class mdfrmProveedores : Form
    {
        public Proveedor propProveedor { get; set; }

        public mdfrmProveedores()
        {
            InitializeComponent();
        }

        private void mdfrmProveedores_Load(object sender, EventArgs e)
        {
            List<Proveedor> ListProveedor = new CN_Proveedor().ToList();

            foreach (Proveedor item in ListProveedor) // Este foreach se encarga de MOSTRAR los PROVEEDORES GUARDADOS en el DataGridView.
            {
                dgvProveedores.Rows.Add(new object[] {
                    item.IdProveedor,
                    item.RazonSocial,
                    item.Documento,
                });
            }
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            propProveedor = new Proveedor()
            {
                IdProveedor = Convert.ToInt32(dgvProveedores.CurrentRow.Cells["IdProveedor"].Value.ToString()),
                Documento = dgvProveedores.CurrentRow.Cells["Document"].Value.ToString(),
                RazonSocial = dgvProveedores.CurrentRow.Cells["RazonSocial"].Value.ToString(),
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
