using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class ReportProducto : Form
    {
        public ReportProducto()
        {
            InitializeComponent();
        }

        private void ReportProducto_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'celularesDataSet2.PRODUCTO' Puede moverla o quitarla según sea necesario.
            this.pRODUCTOTableAdapter.Fill(this.celularesDataSet2.PRODUCTO);

            this.reportViewer1.RefreshReport();
        }
    }
}
