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
    public partial class FormularioClientes : Form
    {

        Conexion_DB objConexion = new Conexion_DB();
        int posicion = 0;
        string accion = "nuevo";
        DataTable tbl = new DataTable();

        public FormularioClientes()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ActualizarDs();
            MostrarDatos();
        }

        void ActualizarDs()
        {
            tbl = objConexion.Obtener_datos().Tables["clientes"];
            tbl.PrimaryKey = new DataColumn[] { tbl.Columns["idCliente"] };
        }
        void MostrarDatos()
        {
            try
            {
                lblidcliente.Text = tbl.Rows[posicion].ItemArray[0].ToString();
                txtidcliente.Text = tbl.Rows[posicion].ItemArray[1].ToString();
                txtnombre.Text = tbl.Rows[posicion].ItemArray[2].ToString();
                txtapellido.Text = tbl.Rows[posicion].ItemArray[3].ToString();
                txtdireccion.Text = tbl.Rows[posicion].ItemArray[4].ToString();
                txtdui.Text = tbl.Rows[posicion].ItemArray[5].ToString();
                txttelefono.Text = tbl.Rows[posicion].ItemArray[4].ToString(); 

                lblnregistros.Text = (posicion + 1) + " de " + tbl.Rows.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No hay Datos que mostrar", "Registros de Cliente",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar_cajas();
            }
        }
        void Limpiar_cajas()
        {
            txtidcliente.Text = "";
            txtnombre.Text = "";
            txtapellido.Text = "";
            txtdireccion.Text = "";
            txtdui.Text = "";
            txttelefono.Text = "";
        }
        void Controles(Boolean valor)
        {
            grbNavegacion.Enabled = valor;
            btneliminar.Enabled = valor;
            btnBuscar.Enabled = valor;
            grbDatosClientes.Enabled = !valor;
        }
        private void BtnVolver_Click(object sender, EventArgs e)
        {
            Inicio Cambio = new Inicio();
            Cambio.Show();
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            if (btnNuevo.Text == "Nuevo")
            {//boton de nuevo
                btnNuevo.Text = "Guardar";
                btnModificar.Text = "Cancelar";
                accion = "nuevo";

                Limpiar_cajas();
                Controles(false);
            }
            else
            { //boton de guardar
                String[] valores = {
                    lblidcliente.Text,
                    txtnombre.Text,
                    txtapellido.Text,
                    txtdireccion.Text,
                    txtdui.Text,
                    txttelefono.Text,
                    
                   
                };
                objConexion.Mantenimiento_datos(valores, accion);
                ActualizarDs();
                posicion = tbl.Rows.Count - 1;
                MostrarDatos();

                Controles(true);

                btnNuevo.Text = "Nuevo";
                btnModificar.Text = "Modificar";
            }
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {

            if (btnModificar.Text == "Modificar")
            {//boton de modificar
                btnNuevo.Text = "Guardar";
                btnModificar.Text = "Cancelar";
                accion = "modificar";

                Controles(false);

                btnNuevo.Text = "Guardar";
                btnModificar.Text = "Cancelar";

            }
            else
            { //boton de cancelar
                Controles(true);
                MostrarDatos();

                btnNuevo.Text = "Nuevo";
                btnModificar.Text = "Modificar";
            }

        }

        private void Btneliminar_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Esta seguro de elimina a " + txtnombre.Text, "Registro de Clientes",
               MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                String[] valores = { lblidcliente.Text };
                objConexion.Mantenimiento_datos(valores, "eliminar");

                ActualizarDs();
                posicion = posicion > 0 ? posicion - 1 : 0;
                MostrarDatos();
            }

        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {

            BuscarClientes frmBusquedaClientes = new BuscarClientes();
            frmBusquedaClientes.ShowDialog();

            if (frmBusquedaClientes._idCliente > 0)
            {
                posicion = tbl.Rows.IndexOf(tbl.Rows.Find(frmBusquedaClientes._idCliente));
                MostrarDatos();
            }

        }

        private void Btnsiguiente_Click(object sender, EventArgs e)
        {

            if (posicion < tbl.Rows.Count - 1)
            {
                posicion++;
                MostrarDatos();
            }
            else
            {
                MessageBox.Show("Ultimo Registro...", "Registros de Cliente",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void Btnultimo_Click(object sender, EventArgs e)
        {
            posicion = tbl.Rows.Count - 1;
            MostrarDatos();
        }

        private void Btnanterior_Click(object sender, EventArgs e)
        {
            if (posicion > 0)
            {
                posicion--;
                MostrarDatos();
            }
            else
            {
                MessageBox.Show("Primer Registro...", "Registros de Cliente",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Btnprimero_Click(object sender, EventArgs e)
        {

            posicion = 0;
            MostrarDatos();

        }

        private void FormularioClientes_Load(object sender, EventArgs e)
        {

        }

        private void GrbDatosClientes_Enter(object sender, EventArgs e)
        {

        }
    }
}
