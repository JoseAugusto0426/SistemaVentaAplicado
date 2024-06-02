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
    public partial class ReporteCompra : Form
    {
        public ReporteCompra()
        {
            InitializeComponent();
        }

        public object DocumentoProveedor { get; internal set; }
        public object FechaRegistro { get; internal set; }
        public object TipoDocumento { get; internal set; }
        public object NumeroDocumento { get; internal set; }
        public object UsuarioRegistro { get; internal set; }
        public object MontoTotal { get; internal set; }
        public object CodigoProducto { get; internal set; }
        public object RazonSocial { get; internal set; }
        public object NombreProducto { get; internal set; }
        public object PrecioCompra { get; internal set; }
        public object Categoria { get; internal set; }
        public object PrecioVenta { get; internal set; }
        public object Cantidad { get; internal set; }
        public object SubTotal { get; internal set; }

        private void ReporteCompra_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'celularesDataSet.DETALLE_COMPRA' Puede moverla o quitarla según sea necesario.
            this.dETALLE_COMPRATableAdapter.Fill(this.celularesDataSet.DETALLE_COMPRA);

            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
