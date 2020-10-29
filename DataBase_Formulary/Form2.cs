using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
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
        private Form1 formulary_1;

        public Form3 form3;
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(Form1 _formulary1)
        {
            InitializeComponent();
            formulary_1 = _formulary1;
        }
        //formLoad code
        private void Form2_Load(object sender, EventArgs e)
        {
            //Connecting to dataBase
            DB_Manager.AbrirConexion("127.0.0.1", "pruebas", "mebrito", "Garumon1996");
            //Show database info into datagridview1
            displayData();

        }
        private void Form2_Shown(object sender, EventArgs e)
        {
            displayData();
        }
        //Form closed code
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            formulary_1.Show();
        }
        //search button click code
        private void button1_Click(object sender, EventArgs e)
        {
            string buscar = "select  id as 'ID', fechaR as 'Fecha Registro', nombre as 'Nombre', edad as 'Edad', sexo as 'Sexo', EC as 'Estado Civil', fechaN as 'Fecha de nacimiento', direccion as 'Dirección', telefono as 'Tel', descripcion as 'Descripcion' from pacientes_2 where nombre like '%" + txtBusqueda.Text + "%' and borrado =0";
            DB_Manager.ConsultaSeleccion(buscar, dataGridView1);
        }
        //PDF generator button click code
        private void button2_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentRow;
            String registerDate = "Fecha de registo: " + row.Cells[1].Value.ToString() + "\n";
            String name = "Nombre: " + row.Cells[2].Value.ToString() + "\n";
            String age = "Edad:  " + row.Cells[3].Value.ToString() + "\n";
            String sex = "sexo: " + row.Cells[4].Value.ToString() + "\n";
            String marriegeState = "Estado civil: " + row.Cells[5].Value.ToString() + "\n";
            String birthDay = "Fecha de nacimiento: " + row.Cells[6].Value.ToString() + "\n";
            String adds = "Direccion: " + row.Cells[7].Value.ToString() + "\n";
            String tel = "Telefono: " + row.Cells[8].Value.ToString() + "\n";
            String description = "Descripción: " + row.Cells[9].Value.ToString() + "\n";
            PDFCreator(name, age, sex, adds, description, registerDate, birthDay, marriegeState, tel);
        }
        //return to form1 button click code
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            formulary_1.Show();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            //Styled messagebox code
            string messageBoxText = "¿Esta seguro que quiere borrar la informacion?";
            string caption = "Word Processor";
            MessageBoxButtons button = MessageBoxButtons.YesNoCancel;
            MessageBoxIcon icon = MessageBoxIcon.Warning;

            DialogResult result = MessageBox.Show(messageBoxText, caption, button, icon);
            switch (result)
            {
                case DialogResult.Yes:
                    string orden = "UPDATE pacientes_2 SET  borrado = '1' where id = '" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
                    DB_Manager.ConsultaAccion(orden);
                    displayData();
                    break;
                case DialogResult.No:
                    break;
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            //Styled messagebox code
            string messageBoxText = "¿Esta seguro que quiere restaurar la informacion?";
            string caption = "Word Processor";
            MessageBoxButtons button = MessageBoxButtons.YesNoCancel;
            MessageBoxIcon icon = MessageBoxIcon.Warning;

            DialogResult result = MessageBox.Show(messageBoxText, caption, button, icon);
            switch (result)
            {
                case DialogResult.Yes:
                    string orden = "UPDATE pacientes_2 SET  borrado = '0' where id = '" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
                    DB_Manager.ConsultaAccion(orden);
                    displayData();
                    break;
                case DialogResult.No:
                    break;
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Form3 ventana_3 = new Form3(this);
            ventana_3.label9.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();//id
            ventana_3.textBox1_F3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();//name
            ventana_3.textBox2_F3.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();//address
            ventana_3.textBox3_F3.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();//sex
            ventana_3.textBox4_F3.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();//marriege Status
            ventana_3.textBox5_F3.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();//birthday
            ventana_3.textBox6_F3.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();//Tel
            ventana_3.textBox7_F3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();//Age
            ventana_3.richTextBox1_F3.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();//description
            ventana_3.Show();
            this.Hide();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                DB_Manager.ConsultaSeleccion("select  id as 'ID', fechaR as 'Fecha Registro', nombre as 'Nombre', edad as 'Edad', sexo as 'Sexo', EC as 'Estado Civil', fechaN as 'Fecha de nacimiento', direccion as 'Dirección', telefono as 'Tel', descripcion as 'Descripcion' from pacientes_2 where borrado=1", dataGridView1);
                button4.Enabled = false;
                button5.Visible = true;
                button5.Enabled = true;
            }
            else
            {
                displayData();
                button4.Enabled = true;
                button5.Visible = false;
                button5.Enabled = false;
            }
                
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        //display database in datagridview function
        public void displayData()
        {

            DB_Manager.ConsultaSeleccion("select  id as 'ID', fechaR as 'Fecha Registro', nombre as 'Nombre', edad as 'Edad', sexo as 'Sexo', EC as 'Estado Civil', fechaN as 'Fecha de nacimiento', direccion as 'Dirección', telefono as 'Tel', descripcion as 'Descripcion' from pacientes_2 where borrado=0", dataGridView1);
            checkBox1.Checked = false;
        }

        //Hay que crear una clase para generar PDF de manera mas eficiente
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

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
