using Parcial2.Datos;
using Parcial2.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//CURSO:1w3 – LEGAJO:114080 – APELLIDO:Rossi – NOMBRE:Braian Alexis

namespace Parcial2
{
    public partial class frmCandidatos : Form
    {   
        private AccesoDatos oBD;
        private List<Candidato> lCandidato;
        public frmCandidatos()
        {
            InitializeComponent();
            oBD = new AccesoDatos();
            lCandidato = new List<Candidato>();
        }

        private void frmCandidatos_Load(object sender, EventArgs e)
        {
            cargarCombo();
            cargarLista();
            habilitar(false);
        }
        private void cargarLista()
        {
            lCandidato.Clear();
            lstCandidatos.Items.Clear();
            DataTable tabla = oBD.consultarBD("SELECT * FROM Candidatos");
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                Candidato c = new Candidato();
                c.IdCandidato = Convert.ToInt32(tabla.Rows[i][0]);
                c.Nombre = tabla.Rows[i][1].ToString();
                c.Partido = Convert.ToInt32(tabla.Rows[i][2]);
                c.Cargo = Convert.ToInt32(tabla.Rows[i][3]);
                c.FechaNacimiento = Convert.ToDateTime(tabla.Rows[i][4]);

                lCandidato.Add(c);
                lstCandidatos.Items.Add(c.ToString());
            }
        }

        private void cargarCombo()
        {
            DataTable tabla = oBD.consultarBD("SELECT * FROM Partidos order by 2");
            cboPartido.DataSource = tabla;
            cboPartido.DisplayMember = "nombrePartido";
            cboPartido.ValueMember = "idPartido";
        }

        private void habilitar(bool v)
        {
            btnNuevo.Enabled = !v;
            cboPartido.Enabled = v;
            dtpFechaNacimiento.Enabled = v;
            lstCandidatos.Enabled = v;
            rbtPresidente.Enabled = v;
            txtNombre.Enabled = v;
            rbtGobernador.Enabled = v;
            rbtIntendente.Enabled = v;
            btnSalir.Enabled = !v;
            btnGrabar.Enabled = v;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (btnSalir.Text == "Salir")
            {
                if (MessageBox.Show("Confirmar si usted desa salir?", "Salir", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Close();
                }
            }
            else
            {
                btnGrabar.Enabled = false;
                btnSalir.Text = "Salir";
            }   
        }      

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            habilitar(true);
            txtNombre.Focus();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (validarDatos() == true)
            {
                Candidato c = new Candidato();
                c.Nombre = txtNombre.Text;
                c.Partido = Convert.ToInt16(cboPartido.SelectedValue);
                if (rbtGobernador.Checked) c.Cargo = 2;
                if (rbtIntendente.Checked) c.Cargo = 3;
                else c.Cargo = 1;
                c.FechaNacimiento = Convert.ToDateTime(dtpFechaNacimiento.Value);

                string insertSQL = "INSERT INTO Candidatos(nombre,partido,cargo,fechaNacimiento) VALUES(@nombre,@partido,@cargo,@fechaNacimiento)";

                List<Parametro> lCandidato = new List<Parametro>();

                lCandidato.Add(new Parametro("@nombre", c.Nombre));
                lCandidato.Add(new Parametro("@partido", c.Partido));
                lCandidato.Add(new Parametro("@cargo", c.Cargo));
                lCandidato.Add(new Parametro("@fechaNacimiento", c.FechaNacimiento));

                if (oBD.actualizarBD(insertSQL, lCandidato) > 0)
                {
                    MessageBox.Show("Se cargaron los datos correctamente!");
                    cargarLista();
                }
            }
            else
            {
                MessageBox.Show("Por favor vuelva a cargar correctamente los datos!");
            }
            habilitar(false);
        }
        

        private bool validarDatos()
        {
            bool v = true;
            if (string.IsNullOrEmpty(txtNombre.Text)) v = false;
            if (cboPartido.SelectedIndex == -1) v = false;
            if (!rbtGobernador.Checked && !rbtIntendente.Checked && !rbtPresidente.Checked) v = false;
            //if (dtpFechaNacimiento.Value.Year > 64 && rbtPresidente.Checked) v = false;
            //No supe como hacerlo\ 
            return v;
        }
    }
}
