#define debugVersion
//#define realeseVersion
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
#if debugVersion
            DB_Manager.AbrirConexion("127.0.0.1", "pruebas", "root", "garumon1996"); 
#elif realeseVersion
            DB_Manager.AbrirConexion("127.0.0.1", "ClinicaDental", "DentalMotul", "contraseña");
#endif
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
#if debugVersion
            String insertChain = "INSERT INTO ClinicaDental (fechaR, Nombre, Edad, Sexo, Estado_Civil, FechaN, Direccion, Telefono, Motivo_Consulta, Nombre_Tutor, Telefono_Tutor, alergias, padecimientos, Borrado) VALUES" +
                " ('" + txtFechaR.Text + "', '" + txtNombre.Text + "', '" + int.Parse(txtEdad.Text) + "', '" + txtSex.Text + "', '" + txtEstadoCivil.Text +
                "', '" + txtFechaN.Text + "','" + txtAddress.Text + "','" + txtTelefono.Text + "', '" + txtDescription.Text + "','" + tutorTxtBox.Text + "', '" + tutorTelTxtBox.Text + "', '" + txtAllergies.Text + "', '" + ailments + "', 0)";
#elif realeseVersion
            String insertChain = "INSERT INTO pacientes (fechaR, Nombre, Edad, Sexo, Estado_Civil, FechaN, Direccion, Telefono, descripcion, Nombre_Tutor, Telefono_Tutor, alergias, padecimientos, Borrado) VALUES" +
                " ('" + txtFechaR.Text + "', '" + txtNombre.Text + "', '" + int.Parse(txtEdad.Text) + "', '" + txtSex.Text + "', '" + txtEstadoCivil.Text +
                "', '" + txtFechaN.Text + "','" + txtAddress.Text + "','" + txtTelefono.Text + "', '" + txtDescription.Text + "','" + tutorTxtBox.Text + "', '" + tutorTelTxtBox.Text + "', '" + txtAllergies.Text + "', '" + ailments + "', 0)";
#endif

            DB_Manager.ConsultaAccion(insertChain);
            ailments = "";

            int age_1 = int.Parse(txtEdad.Text);

            //String registerDate = "Fecha de registo: " + row.Cells[1].Value.ToString() + "\n";
            String registerDate = "Fecha de registro; "+txtFechaR.Text;
            String name = "Nombre: " + txtNombre.Text + "\n";
            String age = "Edad:  " + int.Parse(txtEdad.Text) + "\n";
            String sex = "sexo: " + txtSex.Text + "\n";
            String marriegeState = "Estado civil: " + txtEstadoCivil.Text + "\n";
            String birthDay = "Fecha de nacimiento: " + txtFechaN.Text + "\n";
            String adds = "Direccion: " + txtAddress.Text + "\n";
            String tel = "Telefono: " + txtTelefono.Text + "\n";
            String allergies = "Alergias: " + txtAllergies.Text  + "\n";
            String ailnes = "Padecimientos: " + ailments + "\n";
            String description = "Descripción: " + txtDescription.Text + "\n";
            String tutor = "Tutor: " + tutorTxtBox.Text + "\n";
            String tutor_Tel = "Tutor Tel: " + tutorTelTxtBox.Text + "\n";
            String file_Name = txtNombre.Text + txtFechaN.Text;

            PDFCreator(age_1, name, age, sex, adds, allergies, ailnes, description, registerDate, birthDay, marriegeState, tel, tutor, tutor_Tel, file_Name);

            ereaserFields();
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

        private void PDFCreator(int age_1, String name, String age, String sex, String adds, String allergies, String ailments, String description, String fR, String fN, String eC, String tel, String Tutor, String T_Cel, String file_Name)
        {
            //Initialize PDF writer and set the file address
#if debugVersion
            PdfWriter pdfwriter = new PdfWriter("C:/Users/Lenovo/Documents/Mario_Brito/Codes/C-_Formulary_database_connection/DataBase_Formulary/Reportes/Reporte_" + file_Name + ".pdf");

#elif realeseVersion
                        PdfWriter pdfwriter = new PdfWriter("C:/Users/Francisco/Desktop/Reportes/Reporte_" + file_Name + ".pdf");
#endif
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(pdfwriter);
            Document documento = new Document(pdf, PageSize.LETTER); //I give size to the document
            documento.SetMargins(60, 20, 55, 20);//I put a margin to the document (top, rigth, button, left)
            //Document Fonts
            PdfFont fontTitle = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
            PdfFont fontContend = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            //Document Sections
            var Title = new Paragraph("Historia Clinica. \n").SetFont(fontTitle);
            if (age_1 >= 18)
            {
                var pxInformation = new Paragraph(fR + name + age + fN + sex + eC + adds + tel + allergies + ailments + description).SetFont(fontContend);                //Add Sections
                documento.Add(Title);
                documento.Add(pxInformation);
                documento.Close();
            }
            else
            {
                var pxInformation = new Paragraph(fR + name + age + fN + Tutor + T_Cel + sex + eC + adds + tel + allergies + ailments + description).SetFont(fontContend);
                //Add Sections
                documento.Add(Title);
                documento.Add(pxInformation);
                documento.Close();
            }



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
    }
}
