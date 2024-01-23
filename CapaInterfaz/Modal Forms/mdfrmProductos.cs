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
    public partial class mdfrmProductos : Form
    {
        public Producto propProducto { get; set; }
        public mdfrmProductos()
        {
            InitializeComponent();
        }

        private void mdfrmProductos_Load(object sender, EventArgs e)
        {
            List<Producto> ListProducto = new CN_Producto().ToList();

            foreach (Producto item in ListProducto) // Este foreach se encarga de MOSTRAR los Productos GUARDADOS en el DataGridView.
            {
                dgvProductos.Rows.Add(new object[] {
                    item.IdProducto,
                    item.Codigo,
                    item.Nombre,
                    item.objCategoria.Descripcion,
                    item.Stock,
                    item.PrecioCompra,
                    item.PrecioVenta,
                });
            }
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            propProducto = new Producto()
            {
                IdProducto = Convert.ToInt32(dgvProductos.CurrentRow.Cells["IdProducto"].Value.ToString()),
                Codigo = dgvProductos.CurrentRow.Cells["Code"].Value.ToString(),
                Nombre = dgvProductos.CurrentRow.Cells["ProductName"].Value.ToString(),
                Stock = Convert.ToInt32(dgvProductos.CurrentRow.Cells["Stock"].Value.ToString()),
                PrecioCompra = Convert.ToDecimal(dgvProductos.CurrentRow.Cells["PrecioCompra"].Value.ToString()),
                PrecioVenta = Convert.ToDecimal(dgvProductos.CurrentRow.Cells["PrecioVenta"].Value.ToString()),
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
