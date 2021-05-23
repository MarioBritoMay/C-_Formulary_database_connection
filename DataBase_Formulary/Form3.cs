//#define debugVersion
#define realeseVersion
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
#if debugVersion
            DB_Manager.AbrirConexion("127.0.0.1", "ClinicaDental", "mebrito", "Garumon1996");
#elif realeseVersion
            DB_Manager.AbrirConexion("127.0.0.1", "ClinicaDental", "DentalMotul", "contraseña");
#endif
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            formulary_2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
#if debugVersion
            string orden = "UPDATE ClinicaDental SET Nombre= '" + textBox1_F3.Text + "', Edad = '" + textBox7_F3.Text + "', Sexo = '" + textBox3_F3.Text + "', Estado_Civil= '" + textBox4_F3.Text + "', FechaN = '" + textBox5_F3.Text + "', Direccion= '" + textBox2_F3.Text + "', Telefono= '" + textBox6_F3.Text + "', alergias= '" + textBox10_F3.Text + "', Padecimientos= '" + textBox11_F3.Text + "', Motivo_Consulta= '" + richTextBox1_F3.Text + "', Nombre_Tutor= '" + textBox8_F3.Text + "', Telefono_Tutor= '" + textBox9_F3.Text + "' where id = '" + label9.Text + "'";
#elif realeseVersion
            string orden = "UPDATE pacientes SET Nombre= '" + textBox1_F3.Text + "', Edad = '" + textBox7_F3.Text + "', Sexo = '" + textBox3_F3.Text + "', Estado_Civil= '" + textBox4_F3.Text + "', FechaN = '" + textBox5_F3.Text + "', Direccion= '" + textBox2_F3.Text + "', Telefono= '" + textBox6_F3.Text + "', alergias= '" + textBox10_F3.Text + "', Padecimientos= '" + textBox11_F3.Text + "', descripcion= '" + richTextBox1_F3.Text + "', Nombre_Tutor= '" + textBox8_F3.Text + "', Telefono_Tutor= '" + textBox9_F3.Text + "' where id = '" + label9.Text + "'";
#endif
            DB_Manager.ConsultaAccion(orden);
            this.Close();
            formulary_2.Show();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_F3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
}
