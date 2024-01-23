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
    public partial class mdfrmClientes : Form
    {
        public Cliente propCliente { get; set; }
        public mdfrmClientes()
        {
            InitializeComponent();
        }

        private void mdfrmClientes_Load(object sender, EventArgs e)
        {
            List<Cliente> ListCliente = new CN_Cliente().ToList();

            foreach (Cliente item in ListCliente) // Este foreach se encarga de MOSTRAR los CLIENTES GUARDADOS en el DataGridView.
            {
                dgvClientes.Rows.Add(new object[] {
                    item.IdCliente,
                    item.NombreCompleto,
                    item.Documento
                });                                                                                                                                                                                                                           
            }
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            propCliente = new Cliente()
            {
                IdCliente = Convert.ToInt32(dgvClientes.CurrentRow.Cells["IdCliente"].Value.ToString()),
                NombreCompleto = dgvClientes.CurrentRow.Cells["ClientName"].Value.ToString(),
                Documento = dgvClientes.CurrentRow.Cells["Document"].Value.ToString(),
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
