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
            //necesito conectarme a la base de datos
            DB_Manager.AbrirConexion("127.0.0.1", "pruebas", "mebrito", "Garumon1996");
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            String insertChain = "INSERT INTO pacientes_2 (fechaR, nombre, edad, sexo, EC, fechaN, direccion, telefono, descripcion) VALUES" +
                " ('" + txtNombre.Text + "', '" + txtNombre.Text + "', '" + txtNombre.Text + "', '" + txtNombre.Text + "', '" + txtNombre.Text +
                "', '" + int.Parse(txtEdad.Text) + "','" + txtSex.Text + "','" + txtAddress.Text + "', '" + txtDescription.Text + "')";

            DB_Manager.ConsultaAccion(insertChain);
            ereaserFiels();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //I use the library itext 7 from NuGet
            String nombre = "Nombre: " + txtNombre.Text + "\n";
            String edad = "Edad: " + txtEdad.Text + "\n";
            String sex = "Sexo: " + txtSex.Text + "\n";
            String adds = "Dirección: " + txtAddress.Text + "\n";
            String description = "Causa de cita: " + txtDescription.Text + "\n";
            String fechaR = "Fecha de registo: " + txtFechaR.Text + "\n";
            String fechaN = "Fecha de nacimiento: " + txtFechaN.Text + "\n";
            String estadoCivil = "Estado Civil: " + txtEstadoCivil.Text + "\n";
            String telefono = "Telefono: " + txtTelefono.Text + "\n";
            PDFCreator(nombre, edad, sex, adds, description, fechaR, fechaN, estadoCivil, telefono);
        }
        private void PDFCreator(String name, String age, String sex, String adds, String description, String fR, String fN, String eC, String tel)
        {
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
            var Title = new Paragraph("Historia Clinica. \n").SetFont(fontTitle);
            var pxInformation = new Paragraph(fR + name + age + fN + sex + eC + adds + tel + description).SetFont(fontContend); 
            //Add Sections
            documento.Add(Title);
            documento.Add(pxInformation);
            documento.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form2 ventana;
            ventana = new Form2(this);
            ventana.Show();
            this.Hide();
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
