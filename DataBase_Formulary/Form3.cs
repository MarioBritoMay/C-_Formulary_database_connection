using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataBase_Formulary
{
    public partial class Form3 : Form
    {
        private Form2 formulary_2;
        public Form3()
        {
            InitializeComponent();
        }
        public Form3(Form2 _formulary2)
        {
            InitializeComponent();
            formulary_2 = _formulary2;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            //Connecting to dataBase
            //DB_Manager.AbrirConexion("127.0.0.1", "ClinicaDental", "mebrito", "Garumon1996");
            DB_Manager.AbrirConexion("127.0.0.1", "ClinicaDental", "Root", "contraseña");
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            formulary_2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string orden = "UPDATE Pacientes SET nombre= '" + textBox1_F3.Text + "', edad = '" + textBox7_F3.Text + "', sexo = '" + textBox3_F3.Text + "', EC= '" + textBox4_F3.Text + "', fechaN = '" + textBox5_F3.Text + "', direccion= '" + textBox2_F3.Text + "', telefono= '" + textBox6_F3.Text + "', descripcion= '" + richTextBox1_F3.Text + "' where id = '" + label9.Text + "'";
            DB_Manager.ConsultaAccion(orden);
            this.Close();
            formulary_2.Show();
        }
    }
}
