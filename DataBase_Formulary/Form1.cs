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
            //DB_Manager.AbrirConexion("127.0.0.1", "pruebas", "mebrito", "Garumon1996");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Connect to database
            DB_Manager.AbrirConexion("127.0.0.1", "pruebas", "mebrito", "Garumon1996");
            String insertChain = "INSERT INTO pacientes_2 (fechaR, nombre, edad, sexo, EC, fechaN, direccion, telefono, descripcion, Borrado) VALUES" +
                " ('" + txtFechaR.Text + "', '" + txtNombre.Text + "', '" + int.Parse(txtEdad.Text) + "', '" + txtSex.Text + "', '" + txtEstadoCivil.Text +
                "', '" + txtFechaN.Text + "','" + txtAddress.Text + "','" + txtTelefono.Text + "', '" + txtDescription.Text +  "', 0)";

            DB_Manager.ConsultaAccion(insertChain);
            PDFCreator();
            ereaserFields();
        }
        private void PDFCreator()
        {
            //I use the library itext 7 from NuGet
            String name = "Nombre: " + txtNombre.Text + "\n";
            String age = "Edad: " + txtEdad.Text + "\n";
            String sex = "Sexo: " + txtSex.Text + "\n";
            String adds = "Dirección: " + txtAddress.Text + "\n";
            String description = "Causa de cita: " + txtDescription.Text + "\n";
            String registerDate = "Fecha de registo: " + txtFechaR.Text + "\n";
            String birthDate = "Fecha de nacimiento: " + txtFechaN.Text + "\n";
            String maritalStatus = "Estado Civil: " + txtEstadoCivil.Text + "\n";
            String tel = "Telefono: " + txtTelefono.Text + "\n";
            //Initialize PDF writer and set the file address
            PdfWriter pdfwriter = new PdfWriter("C:/Users/mebri/Documents/GitHub/C#_Formulary_database_connection/C-_" +
                                  "Formulary_database_connection/DataBase_Formulary/Reportes/Reporte.pdf");
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(pdfwriter); 
            Document documento = new Document(pdf, PageSize.LETTER); //I give size to the document
            documento.SetMargins(60, 20, 55, 20);//I put a margin to the document (top, rigth, button, left)
            //Document Fonts
            PdfFont fontTitle = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
            PdfFont fontContend = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            //Document Sections
            var Title = new Paragraph("Historia Clinica de: " + txtNombre.Text + "\n").SetFont(fontTitle);
            var pxInformation = new Paragraph(registerDate + name + age + birthDate + sex + maritalStatus + adds + tel + description).SetFont(fontContend); 
            //Add Sections
            documento.Add(Title);
            documento.Add(pxInformation);
            documento.Close();
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
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form2 ventana;
            ventana = new Form2(this);
            ventana.Show();
            this.Hide();
        }
    }
}
