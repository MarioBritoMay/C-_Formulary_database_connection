using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataBase_Formulary
{
    public partial class Form2 : Form
    {
        private Form1 form_1;
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(Form1 form1)
        {
            InitializeComponent();
            form_1 = form1;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //Connecting to dataBase
            DB_Manager.AbrirConexion("127.0.0.1", "pruebas", "mebrito", "Garumon1996");


        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            form_1.Show();
        }

        public void displayData()
        {
            DB_Manager.ConsultaSeleccion("select  id as 'ID', fechaR as 'Fecha Registro' nombre as 'Nombre', edad as 'Edad', sexo as 'Sexo', EC as 'Estado Civil', fechaN as 'Fecha de nacimiento', direccion as 'Dirección', telefono as 'Tel' descripcion as 'Descripcion' from pacientes_2 where borrado=0", dataGridView1);
        }
    }
}
