using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
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
        public Form2 form2;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Connect to database
            //DB_Manager.AbrirConexion("127.0.0.1", "ClinicaDental", "mebrito", "Garumon1996");
            DB_Manager.AbrirConexion("127.0.0.1", "ClinicaDental", "DentalMotul", "contraseña");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String ailments = "";
            //ailments (Embarazo, hipeertension o diabetico)
            if (checkBox1.Checked == true)
            {
                ailments = "Diabetes ";
            }
            if (checkBox2.Checked == true)
            {
                ailments += "Hipertenso ";
            }
            if (checkBox3.Checked == true)
            {
                ailments += "Embarazo";
            }

            //Connect to database
            String insertChain = "INSERT INTO ClinicaDental (fechaR, Nombre, Edad, Sexo, Estado_Civil, FechaN, Direccion, Telefono, Motivo_Consulta, Nombre_Tutor, Telefono_Tutor, alergias, padecimientos, Borrado) VALUES" +
                " ('" + txtFechaR.Text + "', '" + txtNombre.Text + "', '" + int.Parse(txtEdad.Text) + "', '" + txtSex.Text + "', '" + txtEstadoCivil.Text +
                "', '" + txtFechaN.Text + "','" + txtAddress.Text + "','" + txtTelefono.Text + "', '" + txtDescription.Text + "','" + tutorTxtBox.Text + "', '" + tutorTelTxtBox.Text + "', '" + txtAllergies.Text + "', '" + ailments + "', 0)";

            //String insertChain = "INSERT INTO pacientes (fechaR, Nombre, Edad, Sexo, Estado_Civil, FechaN, Direccion, Telefono, descripcion, Nombre_Tutor, Telefono_Tutor, alergias, padecimientos, Borrado) VALUES" +
            //    " ('" + txtFechaR.Text + "', '" + txtNombre.Text + "', '" + int.Parse(txtEdad.Text) + "', '" + txtSex.Text + "', '" + txtEstadoCivil.Text +
            //    "', '" + txtFechaN.Text + "','" + txtAddress.Text + "','" + txtTelefono.Text + "', '" + txtDescription.Text + "','" + tutorTxtBox.Text + "', '" + tutorTelTxtBox.Text + "', '" + txtAllergies.Text + "', '" + ailments + "', 0)";

            DB_Manager.ConsultaAccion(insertChain);
            ailments = "";
            ereaserFields();
        }
        public void ereaserFields()
        {
            txtNombre.Text = "";
            txtEdad.Text = "";
            txtSex.Text = "";
            txtAddress.Text = "";
            txtDescription.Text = "";
            txtFechaR.Text = "";
            txtFechaN.Text = "";
            txtEstadoCivil.Text = "";
            txtTelefono.Text = "";
            tutorTxtBox.Text = "";
            tutorTelTxtBox.Text = "";
            txtAllergies.Text = "";
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form2 ventana;
            ventana = new Form2(this);
            ventana.Show();
            this.Hide();
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEdad_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
