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
using System.IO;

namespace CapaInterfaz
{
    public partial class frmNegocio : Form
    {
        public frmNegocio()
        {
            InitializeComponent();
        }

        public Image Byte2Image(byte[] imageBytes) // Este METODO nos permite convertir un array de BYTES a una IMAGEN.
        {
            MemoryStream m_stream = new MemoryStream();
            m_stream.Write(imageBytes, 0, imageBytes.Length);

            Image image = new Bitmap(m_stream);

            return image;
        }

        private void frmNegocio_Load(object sender, EventArgs e)
        {
            bool registrado = true;
            byte[] logoBytes = new CN_Negocio().Registrar_Logo(out registrado);

            if (registrado)
            {
                pcbLogo.Image = Byte2Image(logoBytes);
            }

            Negocio datos = new CN_Negocio().Datos_Negocio();

            txbBusinessName.Text = datos.Nombre;
            txbRUC.Text = datos.RUC;
            txbAddress.Text = datos.Direccion;
        }

        private void btnSubirLogo_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            OpenFileDialog objOpenFile = new OpenFileDialog();
            objOpenFile.FileName = "Files|*.jpg;*.jpeg;*.png";

            if (objOpenFile.ShowDialog() == DialogResult.OK)
            {
                byte[] imageByte = File.ReadAllBytes(objOpenFile.FileName);
                bool respuesta = new CN_Negocio().Actualizar_Logo(imageByte, out mensaje);

                if (respuesta)
                {
                    pcbLogo.Image = Byte2Image(imageByte);
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Negocio objNeg = new Negocio()
            {
                Nombre = txbBusinessName.Text,
                RUC = txbRUC.Text,
                Direccion = txbAddress.Text,
            };

            bool respuesta = new CN_Negocio().Registrar_Negocio(objNeg, out mensaje);

            pnlSave.Visible = false;
            pnlError.Visible = false;

            if (respuesta)
            {
                pnlSave.Visible = true;
            }

            else
            {
                pnlError.Visible = true;
            }
        }

        private void btnSaveOK_Click(object sender, EventArgs e)
        {
            pnlSave.Visible = false;
        }

        private void btnErrorOK_Click(object sender, EventArgs e)
        {
            pnlError.Visible = false;
        }
    }
}
