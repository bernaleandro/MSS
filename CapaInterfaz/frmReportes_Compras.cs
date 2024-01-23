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
    public partial class frmReportes_Compras : Form
    {
        public frmReportes_Compras()
        {
            InitializeComponent();
        }

        private void frmReportes_Compras_Load(object sender, EventArgs e)
        {
            List<Proveedor> ListProveedor = new CN_Proveedor().ToList();

            cmbProveedor.Items.Add(new OptionsComboBox() { Value = 0, Text = "Todos los proveedores" });

            foreach (Proveedor item in ListProveedor)
            {
                cmbProveedor.Items.Add(new OptionsComboBox() { Value = item.IdProveedor, Text = item.RazonSocial });
            }

            cmbProveedor.DisplayMember = "Text";
            cmbProveedor.ValueMember = "Value";
            cmbProveedor.SelectedIndex = 0;
        }


        /* ---------------------------------------------------------------------- APLICACION DE BUTTON ---------------------------------------------------------------------- */



        private void btnBuscar_Click(object sender, EventArgs e)  // Este Button se encarga de MOSTRAR los datos de la COMPRA que se desea buscar
        {
            int idproveedor = Convert.ToInt32(((OptionsComboBox)cmbProveedor.SelectedItem).Value.ToString());

            List<Reporte_Compra> list = new List<Reporte_Compra>();

            list = new CN_Reporte().Reportar_Compra
            (
                dtpFechaInicio.Value.ToString(),
                dtpFechaFin.Value.ToString(),
                idproveedor
            );

            dgvCompras.Rows.Clear();

            foreach(Reporte_Compra rc in list)
            {
                dgvCompras.Rows.Add(new object[]
                {
                    rc.FechaRegistro,
                    rc.TipoDocumento,
                    rc.NumeroDocumento,
                    rc.MontoTotal,
                    rc.UsuarioRegistro,
                    rc.DocumentoProveedor,
                    rc.RazonSocial,
                    rc.CodigoProducto,
                    rc.NombreProducto,
                    rc.Categoria,
                    rc.PrecioCompra,
                    rc.PrecioVenta,
                    rc.Cantidad,
                    rc.SubTotal
                });
            }
        }

        /*private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvCompras.Rows.Count < 1)
            {
                pnlError.Visible = true;
            }

            else
            {
                DataTable d_table = new DataTable();

                foreach(DataGridViewColumn column in dgvCompras.Columns)
                {
                    if(column.HeaderText != "" && column.Visible)
                    {
                        d_table.Columns.Add(column.HeaderText, typeof(string));
                    }
                }

                foreach(DataGridViewRow row in dgvCompras.Rows)
                {
                    if (row.Visible)
                    {
                        d_table.Rows.Add(new object[]
                        {
                            row.Cells[0].Value.ToString(),
                            row.Cells[1].Value.ToString(),
                            row.Cells[2].Value.ToString(),
                            row.Cells[3].Value.ToString(),
                            row.Cells[4].Value.ToString(),
                            row.Cells[5].Value.ToString(),
                            row.Cells[6].Value.ToString(),
                            row.Cells[7].Value.ToString(),
                            row.Cells[8].Value.ToString(),
                            row.Cells[9].Value.ToString(),
                            row.Cells[10].Value.ToString(),
                            row.Cells[11].Value.ToString(),
                            row.Cells[12].Value.ToString(),
                            row.Cells[13].Value.ToString()
                        });
                    }

                    SaveFileDialog saveFile = new SaveFileDialog();
                    saveFile.FileName = string.Format("MSS_Reporte_Compras_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss"));
                    saveFile.Filter = "Excel files | *.xlsx";

                    if(saveFile.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            XLWorkBook w_book
                        }
                    }
                }
            }
        }*/
    }
}
