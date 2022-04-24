#define debugVersion
//#define realeseVersion
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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;

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
            SaveFileDialog saveFile = new SaveFileDialog();
            //le damos nombre al documento y en el añadimos el tipo de documento añadiendole la extension .pdf
            saveFile.FileName = txtNombre.Text + txtFechaN.Text + ".pdf";

            String HtmlPage_content = "<html><head><title>Historia clinica</title><style type='text/css'>body {font-family: Georgia, 'Times New Roman'," +
            "font-family: Georgia, 'Times New Roman', Times, serif; color: black; background-color: white; border:  red 1px solid;" +
            "margin:  25px 35px 25px 35px; padding: 15px;}" +
            "h1 { margin: 10px; color: black; font-size: 18px; font-weight: 400; line-height: 1.5; text-transform: uppercase; text-align:center;}" +
            "  		h2 { margin: 10px;color: black; font-size: 18px; font-weight: normal; line-height: 1.5;    text-align: center; }" +
            "tr { margin:  5px; border: black 1px solid; } " +
            "td { margin: 5px; border: black 1px solid; } </style>" +
            "</head><body><header><div><h1>Consultorio Dental Motul</h1><h2>Historia Clinica.</h2></div></header>"+
            "<section><div><table class='default'><tr><td><strong>"+name+"</strong></td><td><strong>"+ age + "</strong></td><td><strong>"+sex+" </strong></td>"+
            "</tr><tr><td><strong>"+adds+"</strong></td><td><strong>"+fN+"</strong></td><td><strong>"+tel+"</strong></td>"+
            "</tr></table></div></section></body></html>";

                // contenido del PDF

            if (saveFile.ShowDialog() == DialogResult.OK) // mostrar ventana de dialogo si la respuesta es correcta
            {
                using (FileStream stream = new FileStream(saveFile.FileName, FileMode.Create))// crea el archivo de memoria
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25); //da las caracteristicas al documento
                    PdfWriter writter = PdfWriter.GetInstance(pdfDoc, stream);     // nos va permitir escribir el contenido        
                    pdfDoc.Open();// abrimos el documento
                    pdfDoc.Add(new Phrase(""));// añadimos el contenido

                    using (StringReader sr = new StringReader(HtmlPage_content))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writter, pdfDoc, sr);
                    }

                    pdfDoc.Close();// cerramos documento
                    stream.Close();// cerramos espacio de memoria
                }
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
