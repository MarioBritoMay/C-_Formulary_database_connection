//#define debugVersion
#define realeseVersion
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
#if debugVersion
            //DB_Manager.AbrirConexion("127.0.0.1", "ClinicaDental", "mebrito", "Garumon1996");
#elif realeseVersion
            DB_Manager.AbrirConexion("127.0.0.1", "ClinicaDental", "DentalMotul", "contraseña");
#endif
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
        //return to form1 button click code
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            formulary_1.Show();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        //display database in datagridview function
        public void displayData()
        {
#if debugVersion
            DB_Manager.ConsultaSeleccion("select  id as 'ID', fechaR as 'Fecha Registro', Nombre as 'Nombre', Edad as 'Edad', Sexo as 'Sexo', Estado_Civil as 'Estado Civil', FechaN as 'Fecha de nacimiento', Direccion as 'Dirección', Telefono as 'Tel', Alergias as 'Alergias', Padecimientos as 'Padecimientos', Motivo_Consulta as 'Motivo Consulta', Nombre_Tutor as 'Tutor', Telefono_Tutor as 'Telefono Tutor' from ClinicaDental where Borrado=0", dataGridView1);
#elif realeseVersion
            DB_Manager.ConsultaSeleccion("select  id as 'ID', fechaR as 'Fecha Registro', Nombre as 'Nombre', Edad as 'Edad', Sexo as 'Sexo', Estado_Civil as 'Estado Civil', FechaN as 'Fecha de nacimiento', Direccion as 'Dirección', Telefono as 'Tel', Alergias as 'Alergias', Padecimientos as 'Padecimientos', descripcion as 'Motivo Consulta', Nombre_Tutor as 'Tutor', Telefono_Tutor as 'Telefono Tutor' from pacientes where Borrado=0", dataGridView1);
#endif
            //Reset Checkbox
            checkBox1.Checked = false;
        }

       
        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentRow;
            int age_1 = (int)row.Cells[3].Value;

            String registerDate = "Fecha de registo: " + row.Cells[1].Value.ToString() + "\n";
            String name = "Nombre: " + row.Cells[2].Value.ToString() + "\n";
            String age = "Edad:  " + row.Cells[3].Value.ToString() + "\n";
            String sex = "sexo: " + row.Cells[4].Value.ToString() + "\n";
            String marriegeState = "Estado civil: " + row.Cells[5].Value.ToString() + "\n";
            String birthDay = "Fecha de nacimiento: " + row.Cells[6].Value.ToString() + "\n";
            String adds = "Direccion: " + row.Cells[7].Value.ToString() + "\n";
            String tel = "Telefono: " + row.Cells[8].Value.ToString() + "\n";
            String allergies = "Alergias: " + row.Cells[9].Value.ToString() + "\n";
            String ailnes = "Padecimientos: " + row.Cells[10].Value.ToString() + "\n";
            String description = "Descripción: " + row.Cells[11].Value.ToString() + "\n";
            String tutor = "Tutor: " + row.Cells[12].Value.ToString() + "\n";
            String tutor_Tel = "Tutor Tel: " + row.Cells[13].Value.ToString() + "\n";
            String file_Name = row.Cells[2].Value.ToString() + row.Cells[3].Value.ToString();

               //PDFCreator(age_1, name, age, sex, adds, allergies, ailnes, description, registerDate, birthDay, marriegeState, tel, tutor, tutor_Tel, file_Name);
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            Form3 ventana_3 = new Form3(this);
            ventana_3.label9.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();//id
            ventana_3.textBox1_F3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();//name
            ventana_3.textBox7_F3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();//Age
            ventana_3.textBox3_F3.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();//sex
            ventana_3.textBox4_F3.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();//marriege Status
            ventana_3.textBox5_F3.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();//birthday
            ventana_3.textBox2_F3.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();//address
            ventana_3.textBox6_F3.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();//Tel   
            ventana_3.textBox10_F3.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();//allergies
            ventana_3.textBox11_F3.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();//ailnes
            ventana_3.richTextBox1_F3.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();//description
            ventana_3.textBox8_F3.Text = dataGridView1.CurrentRow.Cells[12].Value.ToString();//Tutor
            ventana_3.textBox9_F3.Text = dataGridView1.CurrentRow.Cells[13].Value.ToString();//Tutor cel

            ventana_3.Show();
            this.Hide();
        }

        private void button5_Click_1(object sender, EventArgs e)
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
#if debugVersion
                    string orden = "UPDATE ClinicaDental SET  borrado = '0' where id = '" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
#elif realeseVersion
                    string orden = "UPDATE pacientes SET  borrado = '0' where id = '" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
#endif
                    DB_Manager.ConsultaAccion(orden);
                    displayData();
                    break;
                case DialogResult.No:
                    break;
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
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
#if debugVersion
                    string orden = "UPDATE ClinicaDental SET  Borrado = '1' where id = '" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
#elif realeseVersion
                    string orden = "UPDATE pacientes SET  Borrado = '1' where id = '" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
#endif
                    DB_Manager.ConsultaAccion(orden);
                    displayData();
                    break;
                case DialogResult.No:
                    break;
            }
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
#if debugVersion
                DB_Manager.ConsultaSeleccion("select  id as 'ID', fechaR as 'Fecha Registro', Nombre as 'Nombre', Edad as 'Edad', Sexo as 'Sexo', Estado_Civil as 'Estado Civil', FechaN as 'Fecha de nacimiento', Direccion as 'Dirección', Telefono as 'Tel', Alergias as 'Alergias', Padecimientos as 'Padecimientos', Motivo_Consulta as 'Motivo Consulta', Nombre_Tutor as 'Tutor', Telefono_Tutor as 'Telefono Tutor' from ClinicaDental where Borrado=1", dataGridView1);
#elif realeseVersion
                DB_Manager.ConsultaSeleccion("select  id as 'ID', fechaR as 'Fecha Registro', Nombre as 'Nombre', Edad as 'Edad', Sexo as 'Sexo', Estado_Civil as 'Estado Civil', FechaN as 'Fecha de nacimiento', Direccion as 'Dirección', Telefono as 'Tel', Alergias as 'Alergias', Padecimientos as 'Padecimientos', descripcion as 'Motivo Consulta', Nombre_Tutor as 'Tutor', Telefono_Tutor as 'Telefono Tutor' from pacientes where Borrado=1", dataGridView1);
#endif
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

        private void button1_Click(object sender, EventArgs e)
        {
#if debugVersion
            string buscar = "select  id as 'ID', fechaR as 'Fecha Registro', Nombre as 'Nombre', Edad as 'Edad', Sexo as 'Sexo',  Estado_Civil as 'Estado Civil', fechaN as 'Fecha de nacimiento', Direccion as 'Dirección', telefono as 'Tel',  Alergias as 'Alergias', Padecimientos as 'Padecimientos', Motivo_Consulta as 'Descripcion' from ClinicaDental where nombre like '%" + txtBusqueda.Text + "%' and borrado =0";
#elif realeseVersion
            string buscar = "select  id as 'ID', fechaR as 'Fecha Registro', Nombre as 'Nombre', Edad as 'Edad', Sexo as 'Sexo',  Estado_Civil as 'Estado Civil', fechaN as 'Fecha de nacimiento', Direccion as 'Dirección', telefono as 'Tel',  Alergias as 'Alergias', Padecimientos as 'Padecimientos', descripcion as 'Descripcion' from Pacientes where nombre like '%" + txtBusqueda.Text + "%' and borrado =0";
#endif
            DB_Manager.ConsultaSeleccion(buscar, dataGridView1);
        }

        private void Form2_Click(object sender, EventArgs e)
        {
            displayData();
        }

        //Hay que crear una clase para generar PDF de manera mas eficiente
        //private void PDFCreator_1(String name, String age, String sex, String adds, String allergies, String ailments, String description, String fR, String fN, String eC, String tel, String file_Name)
        //{
        //    //Initialize PDF writer and set the file address
        //    //PdfWriter pdfwriter = new PdfWriter("C:/Users/Francisco/Desktop/Reportes/Reporte_" +file_Name+ ".pdf");
        //    PdfWriter pdfwriter = new PdfWriter("C:/Users/mebri/Desktop/Reportes/Reporte_" + file_Name + ".pdf");
        //    //Initialize PDF document
        //    PdfDocument pdf = new PdfDocument(pdfwriter);
        //    Document documento = new Document(pdf, PageSize.LETTER); //I give size to the document
        //    documento.SetMargins(60, 20, 55, 20);//I put a margin to the document (top, rigth, button, left)
        //    //Document Fonts
        //    PdfFont fontTitle = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
        //    PdfFont fontContend = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
        //    //Document Sections
        //    var Title = new Paragraph("Historia Clinica. \n").SetFont(fontTitle);
        //    var pxInformation = new Paragraph(fR + name + age + fN + sex + eC + adds + tel + allergies + ailments + description).SetFont(fontContend);
        //    //Add Sections
        //    documento.Add(Title);
        //    documento.Add(pxInformation);
        //    documento.Close();
        //}
    }
}
