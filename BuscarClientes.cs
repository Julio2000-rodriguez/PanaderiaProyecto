using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Panaderia
{
    public partial class BuscarClientes : Form
    {
        Conexion_DB objConexion = new Conexion_DB();
        public int _idCliente;

        public BuscarClientes()
        {
            InitializeComponent();
        }
        private void Btnseleccionar_Click(object sender, EventArgs e)
        {
            if (grdBusquedaClientes.RowCount > 0)
            {
                _idCliente = int.Parse(grdBusquedaClientes.CurrentRow.Cells[0].Value.ToString());
                Close();
            }
            else
            {
                MessageBox.Show("NO hay datos que seleccionar", "Busqueda de Clientes",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void Busqueda_clientes_Load(object sender, EventArgs e)
        {
            grdBusquedaClientes.DataSource =
               objConexion.Obtener_datos().Tables["clientes"].DefaultView;
        }
        void Filtrar_datos(String valor)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = grdBusquedaClientes.DataSource;
            bs.Filter = "nombre like '%" + valor + "%'";
            grdBusquedaClientes.DataSource = bs;
        }
        private void Txtbuscar_TextChanged(object sender, EventArgs e)
        {
            Filtrar_datos(txtbuscar.Text);
        }
        private void Btncancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
