using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBase_Formulary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //necesito conectarme a la base de datos
            DB_Manager.AbrirConexion("127.0.0.1", "pruebas", "mebrito", "Garumon1996");
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            //String fullName = txtNombre.Text;
            //int edad = int.Parse(txtEdad.Text);
            //String sex = txtSex.Text;
            //String address = txtAddress.Text;
            //String description = txtDescription.Text;

            String insertChain = "INSERT INTO pacientes_2 (nombre, edad, sexo, direccion, descripcion) VALUES ('"+ txtNombre.Text +
                "', '" + int.Parse(txtEdad.Text) + "','" + txtSex.Text + "','" + txtAddress.Text + "', '" + txtDescription.Text + "')";

            DB_Manager.ConsultaAccion(insertChain);
            ereaserFiels();

        }

        public void  ereaserFiels()
        {
            txtNombre.Text = "";
            txtEdad.Text = "";
            txtSex.Text = "";
            txtAddress.Text = "";
            txtDescription.Text = "";
        }

    }
}
