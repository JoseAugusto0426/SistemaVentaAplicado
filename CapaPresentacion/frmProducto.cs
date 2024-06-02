using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using ClosedXML.Excel;
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
    public partial class frmProducto : Form
    {
        public frmProducto()
        {
            InitializeComponent();
        }
        private void frmProducto_Load(object sender, EventArgs e)
        {
            // Agregar elementos al ComboBox de estado
            cboestado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Disponible" });
            cboestado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Disponible" });
            cboestado.DisplayMember = "Texto";
            cboestado.ValueMember = "Valor";

            // Obtener y cargar la lista de categorías
            List<Categoria> listacategoria = new CN_Categoria().Listar();
            foreach (Categoria item in listacategoria)
            {
                cbocategoria.Items.Add(new OpcionCombo() { Valor = item.IdCategoria, Texto = item.Descripcion });
            }
            cbocategoria.DisplayMember = "Texto";
            cbocategoria.ValueMember = "Valor";

            // Agregar elementos al ComboBox de búsqueda
            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {
                if (columna.Visible && columna.Name != "btnseleccionar")
                {
                    cbobusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            cbobusqueda.DisplayMember = "Texto";
            cbobusqueda.ValueMember = "Valor";
            cbobusqueda.SelectedIndex = 0;

            // Cargar la lista de productos en el DataGridView
            List<Producto> lista = new CN__Producto().Listar();
            foreach (Producto item in lista)
            {
                dgvdata.Rows.Add(new object[] {
            "",
            item.IdProducto,
            item.Codigo,
            item.Nombre,
            item.Descripcion,
            item.oCategoria.IdCategoria,
            item.oCategoria.Descripcion,
            item.Stock,
            item.PrecioCompra,
            item.PrecioVenta,
            item.Estado ? 1 : 0,
            item.Estado ? "Disponible" : "No Disponible"
        });
            }
        }


        private void btnguardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbocategoria.Text))
            {
                MessageBox.Show("Debe elegir la categoría en la descripción. Modelos, Marcas o Color.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Detener la ejecución del método si cbocategoria está vacío
            }

            // Verificar si los campos obligatorios están completos
            if (string.IsNullOrEmpty(txtcodigo.Text) || string.IsNullOrEmpty(txtnombre.Text) ||
                cbocategoria.SelectedItem == null || cboestado.SelectedItem == null)
            {
                MessageBox.Show("Por favor, complete todos los campos requeridos.");
                return; // Salir del método si algún campo está vacío o no seleccionado
            }

            // Continuar con el proceso de guardado o edición del producto
            string mensaje = string.Empty;
            bool esNuevo = string.IsNullOrEmpty(txtid.Text) || txtid.Text == "";

            bool estadoSeleccionado = ((OpcionCombo)cboestado.SelectedItem).Texto.ToString().Equals("Disponible");

            Producto obj = new Producto()
            {
                IdProducto = esNuevo ? 0 : Convert.ToInt32(txtid.Text),
                Codigo = txtcodigo.Text,
                Nombre = txtnombre.Text,
                Descripcion = txtdescripcion.Text,
                oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(((OpcionCombo)cbocategoria.SelectedItem).Valor) },
                Estado = estadoSeleccionado
            };
            

            if (obj.IdProducto == 0)
            {
                int idgenerado = new CN__Producto().Registrar(obj, out mensaje);

                if (idgenerado != 0)
                {
                    dgvdata.Rows.Add(new object[] {
                "",
                idgenerado,
                txtcodigo.Text,
                txtnombre.Text,
                txtdescripcion.Text,
                ((OpcionCombo)cbocategoria.SelectedItem).Valor.ToString(),
                ((OpcionCombo)cbocategoria.SelectedItem).Texto.ToString(),
                "0",
                "0.00",
                "0.00",
                ((OpcionCombo)cboestado.SelectedItem).Valor.ToString(),
                ((OpcionCombo)cboestado.SelectedItem).Texto.ToString()
            });

                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
            else
            {
                bool resultado = new CN__Producto().Editar(obj, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dgvdata.Rows[Convert.ToInt32(txtindice.Text)];
                    row.Cells["Id"].Value = txtid.Text;
                    row.Cells["Codigo"].Value = txtcodigo.Text;
                    row.Cells["Nombre"].Value = txtnombre.Text;
                    row.Cells["Descripcion"].Value = txtdescripcion.Text;
                    row.Cells["IdCategoria"].Value = ((OpcionCombo)cbocategoria.SelectedItem).Valor.ToString();
                    row.Cells["Categoria"].Value = ((OpcionCombo)cbocategoria.SelectedItem).Texto.ToString();
                    row.Cells["EstadoValor"].Value = ((OpcionCombo)cboestado.SelectedItem).Valor.ToString();
                    row.Cells["Estado"].Value = ((OpcionCombo)cboestado.SelectedItem).Texto.ToString();

                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
        }




        private void Limpiar()
        {

            txtindice.Text = "-1";
            txtid.Text = "0";
            txtcodigo.Text = "";
            txtnombre.Text = "";
            txtdescripcion.Text = "";
            cbocategoria.SelectedIndex = 0;
            cboestado.SelectedIndex = 0;

            txtcodigo.Select();

        }

        private void dgvdata_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 0)
            {

                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.check20.Width;
                var h = Properties.Resources.check20.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.check20, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvdata.Columns[e.ColumnIndex].Name == "btnseleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtindice.Text = indice.ToString();
                    txtid.Text = dgvdata.Rows[indice].Cells["Id"].Value.ToString();
                    txtcodigo.Text = dgvdata.Rows[indice].Cells["Codigo"].Value.ToString();
                    txtnombre.Text = dgvdata.Rows[indice].Cells["Nombre"].Value.ToString();
                    txtdescripcion.Text = dgvdata.Rows[indice].Cells["Descripcion"].Value.ToString();

                    foreach (object item in cbocategoria.Items)
                    {
                        if (item is OpcionCombo && Convert.ToInt32(((OpcionCombo)item).Valor) == Convert.ToInt32(dgvdata.Rows[indice].Cells["IdCategoria"].Value))
                        {
                            cbocategoria.SelectedItem = item;
                            break;
                        }
                    }

                    foreach (object item in cboestado.Items)
                    {
                        if (item is OpcionCombo && Convert.ToInt32(((OpcionCombo)item).Valor) == Convert.ToInt32(dgvdata.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            cboestado.SelectedItem = item;
                            break;
                        }
                    }
                }
            }
        }


        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtid.Text) || Convert.ToInt32(txtid.Text) == 0)
            {
                MessageBox.Show("Asegúrate de que haya datos registrados para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Detener la ejecución del método si txtid está vacío o es 0
            }

            if (MessageBox.Show("¿Desea eliminar el producto?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string mensaje = string.Empty;
                Producto obj = new Producto()
                {
                    IdProducto = Convert.ToInt32(txtid.Text)
                };

                bool respuesta = new CN__Producto().Eliminar(obj, out mensaje);

                if (respuesta)
                {
                    dgvdata.Rows.RemoveAt(Convert.ToInt32(txtindice.Text));
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }


        private void btnbuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cbobusqueda.SelectedItem).Valor.ToString();

            if (dgvdata.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvdata.Rows)
                {

                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtbusqueda.Text.Trim().ToUpper()))
                        row.Visible = true;
                    else
                        row.Visible = false;
                }
            }
        }

        private void btnlimpiarbuscador_Click(object sender, EventArgs e)
        {
            txtbusqueda.Text = "";

            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                row.Visible = true;
            }
        }
        private void btnlimpiar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtcodigo.Text + txtnombre.Text + txtdescripcion.Text))
            {
                MessageBox.Show("No hay Datos Para Limpiar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Detener la ejecución del método si cbocategoria está vacío
            }

            if (string.IsNullOrEmpty(txtindice.Text) &&
                string.IsNullOrEmpty(txtid.Text) &&
                string.IsNullOrEmpty(txtcodigo.Text) &&
                string.IsNullOrEmpty(txtnombre.Text))
            {
                MessageBox.Show("No hay Datos Para Limpiar en los campos de texto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Detener la ejecución del método si todos los campos de texto están vacíos
            }


            // Limpiar los ComboBox
            cbocategoria.SelectedIndex = -1; // Establecer el índice a -1 para deseleccionar cualquier elemento
            cboestado.SelectedIndex = -1;

            // Limpiar la DataGridView
            dgvdata.ClearSelection();

            // Llamar al método Limpiar
            Limpiar();
        }



        private void btnexportar_Click(object sender, EventArgs e)
        {
            Form prod = new ReportProducto();
            prod.Show();
        }

        private void cbocategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void btnexportar_Click_1(object sender, EventArgs e)
        {
            Form form = new ReportProducto();
            form.Show();
        }
    }
}
