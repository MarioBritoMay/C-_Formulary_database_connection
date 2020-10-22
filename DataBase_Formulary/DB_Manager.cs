////Hay que incluir la referencia a MySql.Data al proyecto
////Clic derechoerecho al proyecto > Agregar Referencia

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

////Espacios de nombre para MySQL
using MySql.Data;
using MySql.Data.MySqlClient;

namespace DataBase_Formulary
{
    public class DB_Manager
    {
        //Objetos global para gestionar la base de datos
        public static MySqlConnection myConn;


        public static bool AbrirConexion(String servidor, String base_de_datos, String usuario, String password)
        {
            //Este método se encarga de abrir la conexión con la base de datos. 
            //Devuelve un boolean(true=éxito al abrir la base de datos / false=fracaso)
            //recibe como parámetros: servidor,nombre de la base de datos,usuario y password

            //capturamos las excepciones producidas al momento de generar la conexión
            try
            {
                //La CADENA DE CONEXIÓN para MySQL. Se proporcionan los datos necesarios para lograr la conexión entre C# y el servidor de base de datos
                string strConn = "server=" + servidor + ";database=" + base_de_datos + ";uid=" + usuario + ";pwd=" + password;

                myConn = new MySqlConnection(strConn);//creamos la conexión
                myConn.Open(); //abrir la conexión
                return true; //conexión exitosa
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "ERROR AL CONECTAR A LA BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false; //ocurrió un error. La conexión no se ha llevado a cabo
            }
        }

        public static bool CerrarConexion()
        {
            //Este método cierra la conexión con la base de datos. 
            //Devuelve true=conexión cerrada / false=no se puede cerrar la conexión

            try
            {
                myConn.Close(); //se cierra la conexión
                return true;//conexión cerrada con éxito
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "ERROR AL CERRAR LA CONEXIÓN CON LA BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;//la conexión no se ha podido cerrar
            }
        }

        public static bool ConsultaAccion(string strQuery)
        {
            //Este método nos permite ejecutar una consulta de acción (INSERT,DELETE,UPDATE). 
            //Estas consultas modifican la base de datos
            //Recibe como parametro la sentencia en lenguaje SQl. 
            //Devuelve true si se realiza con éxito; false en caso contrario

            try
            {
                MySqlCommand myCmmd = new MySqlCommand(strQuery, myConn);//creacion,y asignacion del objeto MySqlCommand. Recibe como parámetros la sentencia y la conexión
                myCmmd.ExecuteNonQuery();//ejecuta la sentencia
                return true;//consulta exitosa
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "ERROR AL PROCESAR LA CONSULTA DE ACCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;//consulta falló
            }
        }

        public static string ConsultaEscalar(string strQuery)
        {
            //Este método ejecuta una consulta que devuelve un único valor 
            //Recibe la consulta

            string resultado = "";
            try
            {
                MySqlCommand cmd = new MySqlCommand(strQuery, myConn);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    resultado = Convert.ToString(result);
                }
                return resultado;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "ERROR AL PROCESAR LA CONSULTA ESCALAR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return ""; //la consulta falló
            }
        }

        public static bool ConsultaSeleccion(string strQuery, DataGridView dgv)
        {
            //Este método ejecuta la consultas de selección (SELECT) y pone el resultado en el objeto DataGridView. 
            //Devuelve true si la consulta se ejecuta con éxito; en caso contrario se devuelve un false

            try
            {
                DataSet dataSet = new DataSet(); //creación del objeto DataSet. Este objeto representa en memoria los datos de la consulta: bases de datos, tablas, relaciones, etc...
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(strQuery, myConn); //creación del objeto MySqlDataAdapter. Recibe como parámetros la consulta y el objeto de conexión. Se encarga de "traducir" los tipos de datos entre el manejador de base de datos y el C#. Por ejemplo: hace las traducciones entre el tipo VARCHAR (usado en el SGBD) y el tipo STRING (usado en el lenguaje de programación)
                myAdapter.Fill(dataSet, "tabla"); //este método del objeto MySqlDataAdapter añade o refresca las filas en el objeto DataSet para que coincida con el origen de datos. En este caso "tabla" es el nombre de referencia para el mapeo de datos
                dgv.DataSource = dataSet.Tables[0];//Se inserta los resultados de la consulta en el DataGridView. Como el dataset puede representar VARIAS tablas, se indica que tome la primera. Lo mas probable es que la consulta SELECT devuelva una sola tabla, entonces ha de tomarse la primera que corresponde al índice 0                                
                return true; //la consulta fue un éxito
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "ERROR AL PROCESAR LA CONSULTA DE SELECCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false; //la consulta falló
            }
        }

        public static bool ConsultaSeleccion(string strQuery, ComboBox combo, string campo)
        {
            //Este método ejecuta la consultas de selección (SELECT) y pone el resultado en el objeto combobox. 
            //Se le debe pasar el nombre del campo de la tabla que se quiere mostrar. Es decir, se le puede pasar una consulta compleja que involucre varios campos al estilo SELECT CAMPO1, CAMPO2, CAMPO3 FROM TABLA WHERE CONDICION. En este caso el método debe recibir el campo de interés que será considerado para el combobox
            //Devuelve true si la consulta se ejecuta con éxito; en caso contrario se devuelve un false

            try
            {
                DataSet dataSet = new DataSet();
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(strQuery, myConn);
                myAdapter.Fill(dataSet, "tabla");
                combo.DataSource = dataSet.Tables[0];
                combo.DisplayMember = campo;
                return true; //la consulta fue un éxito
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "ERROR AL PROCESAR LA CONSULTA DE SELECCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false; //la consulta falló
            }
        }


        public static string ConsultaSeleccion(string strQuery)
        {
            //Este método ejecuta la consultas de selección (SELECT) y pone el resultado en un string

            try
            {
                DataTable datatable = new DataTable(); //Representación de UNA SOLA tabla. En cambio el dataset es la representación de la BD completa (tablas, stored procedures, relaciones...)
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(strQuery, myConn); //Es el traductor de tipos de datos entre la BD y el C#. Por ejemplo de varchar -> string y viceversa.
                myAdapter.Fill(datatable); //llenar la tabla. datatable esta originalmente vacío

                string temporal = "";
                for (int fila = 0; fila < datatable.Rows.Count; fila++)
                {
                    for (int columna = 0; columna < datatable.Columns.Count; columna++)
                    {
                        temporal += datatable.Rows[fila].ItemArray[columna].ToString() + " | ";
                    }
                    temporal += "\n\r";
                }

                return temporal; //la consulta fue un éxito
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "ERROR AL PROCESAR LA CONSULTA DE SELECCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return ""; //la consulta falló
            }
        }
    }
}
//DATASET
// * =======
// * 
// * Incluye bases de datos, tablas, relaciones, etc. En general, contiene muchos datos.
// * Cuando se tienen varias tablas hay que referenciar cual se va a utilizar. Por ejemplo la primera con índice 0.
// * 
// * 
// * DATATABLE
// * =========
// * 
// * Representa solo a una tabla. En los casos de consulta que solo se tiene como resultado una tabla es preferible utilizar uno de estos.
// * 
// * 
// * DATAADAPTER
// * ===========
// * Es el encargado de traducir las diferencias que hay entre los tipos de datos entre el manejador de base de datos y el lenguaje de programación
// */
