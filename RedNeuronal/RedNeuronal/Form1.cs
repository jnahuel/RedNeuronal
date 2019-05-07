using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Directivas para leer el archivo de Excel
using System.IO;
using System.Runtime.InteropServices;

// Alias para manejar el objeto de excel de una forma mas legible
using Excel = Microsoft.Office.Interop.Excel;

namespace RedNeuronal
{
    public partial class Form1 : Form
    {
        // Variables globales
        PerceptronMulticapa perceptron = new PerceptronMulticapa();

        // La forma de acceder a las entradas y salidas es: Variable[ fila o par de datos; numero de entrada o salida ]
        // Variables para almacenar los datos de entradas y salidas para el entrenamiento
        private double[,]   entradas;
        private double[,]   salidas;

        // Variables para la verificacion
        private double[,] entradasVerificacion;
        private double[,] salidasVerificacion;

        // Total de filas de la tabla de pares entradas-salidas
        private int totalDeParesDeDatos = 7;

        // Para la verificacion se permiten solo 2 filas de entradas
        private int totalDeParesDeDatosVerificacion = 2;

        // Parametros de la red
        private int totalDeEntradasExternas = 3;
        private int totalDeSalidas = 2;
        private int totalDeCapasOcultas = 2;
        private int totalDeNeuronasEnCapasOcultas = 4;

        // Indicadores de la forma de entrenamiento
        private int cantidadDePasadasParaEntrenar = 1500;
        private double coeficienteDeAprendizaje = 0.4;

        // Se crean los objetos para el archivo de Excel
        private Excel.Application xlApp = new Excel.Application();
        private Excel.Workbook xlWorkbook;          // Para indicarle la direccion del archivo
        private Excel._Worksheet xlWorksheet;       // Para indicarle la hoja de la que se leeran los datos
        private Excel.Range xlRange;                // Para dimensionar la cantidad de datos que contiene la hoja

        // String para almacenar el nombre del archivo Excel
        String ruta = "";

        // Flag para indicar si se abrio el archivo de Excel, para cerrarlo correctamente al finalizar el programa de no haberlo hecho antes
        private bool ExcelAbierto;

        public Form1()
        {
            // Codigo de inicializacion interno del formulario
            InitializeComponent();

            // Se configura el Perceptron
            perceptron.ConfigurarPerceptron( totalDeEntradasExternas, totalDeSalidas, totalDeCapasOcultas, totalDeNeuronasEnCapasOcultas );

            // El "+1" en las definiciones es para evitar el subindice 0. Ergo, los vectores tendran N datos, desde 1 hasta N
            entradas = new double[ totalDeParesDeDatos + 1, totalDeEntradasExternas + 1 ];
            salidas  = new double[ totalDeParesDeDatos + 1, totalDeSalidas + 1];

            entradasVerificacion = new double[ totalDeParesDeDatosVerificacion + 1, totalDeEntradasExternas + 1];
            salidasVerificacion = new double[ totalDeParesDeDatosVerificacion + 1, totalDeSalidas + 1];


            /*   Se inicializan los controles graficos  */
            
            // Tablas
            for (int columnaEntradas = 0; columnaEntradas < totalDeEntradasExternas; columnaEntradas++)
            {
                tablaEntradas.Columns.Add( "E" + (columnaEntradas + 1).ToString(), "E" + (columnaEntradas + 1).ToString() );
                tablaVerificacionEntradas.Columns.Add("E" + (columnaEntradas + 1).ToString(), "E" + (columnaEntradas + 1).ToString() );
            }
            for (int columnaSalidas = 0; columnaSalidas < totalDeSalidas; columnaSalidas++)
            {
                tablaSalidas.Columns.Add( "S" + (columnaSalidas + 1).ToString(), "S" + (columnaSalidas + 1).ToString() );
                tablaSalidasEntrenadas.Columns.Add("S" + (columnaSalidas + 1).ToString(), "S" + (columnaSalidas + 1).ToString());
                tablaVerificacionSalidas.Columns.Add("S" + (columnaSalidas + 1).ToString(), "S" + (columnaSalidas + 1).ToString());
            }
            
            // Contadores numericos
            numEntradas.Value = totalDeEntradasExternas;
            numSalidas.Value = totalDeSalidas;
            numCoeficiente.Value = Convert.ToDecimal( coeficienteDeAprendizaje );
            numIteraciones.Value = cantidadDePasadasParaEntrenar;
            numCantidadDePares.Value = totalDeParesDeDatos;

            // Rellenar las tablas con datos por default
            tablaEntradas.Rows.Add( totalDeParesDeDatos );
            for (int filaEntradas = 0; filaEntradas < totalDeParesDeDatos; filaEntradas++)
            {
                for (int columnaEntradas = 0; columnaEntradas < totalDeEntradasExternas; columnaEntradas++)
                {
                    // Se podria igualar a 0 en lugar de "entradas[filaEntradas + 1, columnaEntradas + 1];", ya que la variable "entradas" no esta inicializada y vale 0
                    tablaEntradas.Rows[filaEntradas].Cells[columnaEntradas].Value = entradas[filaEntradas + 1, columnaEntradas + 1];
                }
            }

            tablaSalidas.Rows.Add( totalDeParesDeDatos );
            tablaSalidasEntrenadas.Rows.Add( totalDeParesDeDatos );
            for (int filaSalidas = 0; filaSalidas < totalDeParesDeDatos; filaSalidas++)
            {
                for (int columnaSalidas = 0; columnaSalidas < totalDeSalidas; columnaSalidas++)
                {
                    // Idem tabla de entradas
                    tablaSalidas.Rows[filaSalidas].Cells[columnaSalidas].Value = salidas[filaSalidas + 1, columnaSalidas + 1];
                    tablaSalidasEntrenadas.Rows[filaSalidas].Cells[columnaSalidas].Value = 0;
                }
            }

            // Para las tablas de verificacion, solo se permiten agregar 2 filas
            tablaVerificacionEntradas.Rows.Add(totalDeParesDeDatosVerificacion);
            tablaVerificacionSalidas.Rows.Add(totalDeParesDeDatosVerificacion);
            for (int fila = 0; fila < totalDeParesDeDatosVerificacion; fila++)
            {
                for (int columnaEntradas = 0; columnaEntradas < totalDeEntradasExternas; columnaEntradas++)
                    tablaVerificacionEntradas.Rows[fila].Cells[columnaEntradas].Value = 0;
                for (int columnaSalidas = 0; columnaSalidas < totalDeSalidas; columnaSalidas++)
                    tablaVerificacionSalidas.Rows[fila].Cells[columnaSalidas].Value = 0;
            }
        }


        /* *** CONTADORES NUMERICOS *** */

        // Agregar o quitar cantidad de entradas externas
        private void numEntradas_ValueChanged(object sender, EventArgs e)
        {
            // Si el contador indica un numero mayor a la cantidad de entradas en uso, se debe incrementar el contador y agregar una columna mas a la tabla de entradas
            if (numEntradas.Value > totalDeEntradasExternas )
            {
                // Entradas externas
                totalDeEntradasExternas++;
                tablaEntradas.Columns.Add( "E" + totalDeEntradasExternas.ToString(), "E" + totalDeEntradasExternas.ToString() );
                for (int filaEntradas = 0; filaEntradas < totalDeParesDeDatos; filaEntradas++)
                {
                    tablaEntradas.Rows[filaEntradas].Cells[totalDeEntradasExternas - 1].Value = 0;
                }

                // Entradas para las verificaciones
                tablaVerificacionEntradas.Columns.Add("E" + totalDeEntradasExternas.ToString(), "E" + totalDeEntradasExternas.ToString());
                for (int filaEntradas = 0; filaEntradas < totalDeParesDeDatosVerificacion; filaEntradas++)
                {
                    tablaVerificacionEntradas.Rows[filaEntradas].Cells[totalDeEntradasExternas - 1].Value = 0;
                }
            }
            else
            {
                // Si indica un numero menor al actual, se debe decrementar el contador y retirar la ultima columna de la tabla de entradas
                if (numEntradas.Value < totalDeEntradasExternas)
                {
                    tablaEntradas.Columns.Remove("E" + totalDeEntradasExternas.ToString());
                    tablaVerificacionEntradas.Columns.Remove("E" + totalDeEntradasExternas.ToString());
                    totalDeEntradasExternas--;
                }
            }

            // Finalmente, se redimensiona la variable segun la cantidad total de entradas que se hayan elegido
            entradas = new double[totalDeParesDeDatos + 1, totalDeEntradasExternas + 1];

            // Se configura el Perceptron
            perceptron.ConfigurarPerceptron(totalDeEntradasExternas, totalDeSalidas, totalDeCapasOcultas, totalDeNeuronasEnCapasOcultas);
        }

        // Agregar o quitar cantidad de salidas
        private void numSalidas_ValueChanged(object sender, EventArgs e)
        {
            if (numSalidas.Value > totalDeSalidas)
            {
                totalDeSalidas++;
                tablaSalidas.Columns.Add("S" + totalDeSalidas.ToString(), "S" + totalDeSalidas.ToString());
                tablaSalidasEntrenadas.Columns.Add("S" + totalDeSalidas.ToString(), "S" + totalDeSalidas.ToString());
                tablaVerificacionSalidas.Columns.Add("S" + totalDeSalidas.ToString(), "S" + totalDeSalidas.ToString());
                for (int filaSalidas = 0; filaSalidas < totalDeParesDeDatos; filaSalidas++)
                {
                    tablaSalidas.Rows[filaSalidas].Cells[totalDeSalidas - 1].Value = 0;
                    tablaSalidasEntrenadas.Rows[filaSalidas].Cells[totalDeSalidas - 1].Value = 0;
                    tablaVerificacionSalidas.Rows[filaSalidas].Cells[totalDeSalidas - 1].Value = 0;
                }
            }
            else
            {
                if (numSalidas.Value < totalDeSalidas)
                {
                    tablaSalidas.Columns.Remove("S" + totalDeSalidas.ToString());
                    tablaSalidasEntrenadas.Columns.Remove("S" + totalDeSalidas.ToString());
                    tablaVerificacionSalidas.Columns.Remove("S" + totalDeSalidas.ToString());
                    totalDeSalidas--;
                }
            }

            salidas = new double[totalDeParesDeDatos + 1, totalDeSalidas + 1];

            // Se configura el Perceptron
            perceptron.ConfigurarPerceptron(totalDeEntradasExternas, totalDeSalidas, totalDeCapasOcultas, totalDeNeuronasEnCapasOcultas);
        }

        // Contador de la cantidad de neuronas por capa
        private void numNeuronasPorCapa_ValueChanged(object sender, EventArgs e)
        {
            // Se toma el valor indicado
            totalDeNeuronasEnCapasOcultas = Int16.Parse(numNeuronasPorCapa.Value.ToString());

            // Se configura el Perceptron
            perceptron.ConfigurarPerceptron(totalDeEntradasExternas, totalDeSalidas, totalDeCapasOcultas, totalDeNeuronasEnCapasOcultas);
        }

        // Contador de la cantidad de capas ocultas
        private void numCapasOcultas_ValueChanged(object sender, EventArgs e)
        {
            // Se toma el valor indicado
            totalDeCapasOcultas = Int16.Parse(numCapasOcultas.Value.ToString());

            // Se configura el Perceptron
            perceptron.ConfigurarPerceptron(totalDeEntradasExternas, totalDeSalidas, totalDeCapasOcultas, totalDeNeuronasEnCapasOcultas);
        }

        // Agregar o quitar cantidad de pares de datos entradas-salidas
        private void numCantidadDePares_ValueChanged(object sender, EventArgs e)
        {
            if (numCantidadDePares.Value > totalDeParesDeDatos)
            {
                totalDeParesDeDatos++;
                tablaEntradas.Rows.Add();
                tablaSalidas.Rows.Add();
                tablaSalidasEntrenadas.Rows.Add();
                for (int columnaEntradas = 0; columnaEntradas < totalDeEntradasExternas; columnaEntradas++)
                    tablaEntradas.Rows[totalDeParesDeDatos - 1].Cells[columnaEntradas].Value = 0;
                for (int columnaSalidas = 0; columnaSalidas < totalDeSalidas; columnaSalidas++)
                {
                    tablaSalidas.Rows[totalDeParesDeDatos - 1].Cells[columnaSalidas].Value = 0;
                    tablaSalidasEntrenadas.Rows[totalDeParesDeDatos - 1].Cells[columnaSalidas].Value = 0;
                }
            }
            else
            {
                if (numCantidadDePares.Value < totalDeParesDeDatos)
                {
                    tablaSalidas.Rows.Remove(tablaSalidas.Rows[tablaSalidas.Rows.Count - 1]);
                    tablaSalidasEntrenadas.Rows.Remove(tablaSalidasEntrenadas.Rows[tablaSalidasEntrenadas.Rows.Count - 1]);
                    tablaEntradas.Rows.Remove(tablaEntradas.Rows[tablaEntradas.Rows.Count - 1]);
                    totalDeParesDeDatos--;
                }
            }

            entradas = new double[totalDeParesDeDatos + 1, totalDeEntradasExternas + 1];
            salidas = new double[totalDeParesDeDatos + 1, totalDeSalidas + 1];
        }


        /* *** TABLAS *** */

        // Cambia el foco entre las tablas para seleccionar la misma fila en todas y hacer mas facil el seguimiento
        private void tablaEntradas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Se quitan los handlers de las demas tablas
            tablaSalidas.CellClick -= tablaSalidas_CellClick;
            tablaSalidasEntrenadas.CellClick -= tablaSalidasEntrenadas_CellClick;

            // Se modifica el foco
            tablaSalidas.CurrentCell = tablaSalidas.Rows[ tablaEntradas.CurrentRow.Index ].Cells[0];
            tablaSalidasEntrenadas.CurrentCell = tablaSalidasEntrenadas.Rows[tablaEntradas.CurrentRow.Index].Cells[0];

            // Se reestablecen los handlers de las demas tablas
            tablaSalidas.CellClick += tablaSalidas_CellClick;
            tablaSalidasEntrenadas.CellClick += tablaSalidasEntrenadas_CellClick;
        }

        // Cambia el foco entre las tablas para seleccionar la misma fila en todas y hacer mas facil el seguimiento
        private void tablaSalidas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Se quitan los handlers de las demas tablas
            tablaEntradas.CellClick -= tablaEntradas_CellClick;
            tablaSalidasEntrenadas.CellClick -= tablaSalidasEntrenadas_CellClick;

            // Se modifica el foco
            tablaEntradas.CurrentCell = tablaEntradas.Rows[tablaSalidas.CurrentRow.Index].Cells[0];
            tablaSalidasEntrenadas.CurrentCell = tablaSalidasEntrenadas.Rows[tablaSalidas.CurrentRow.Index].Cells[0];

            // Se reestablecen los handlers de las demas tablas
            tablaEntradas.CellClick += tablaEntradas_CellClick;
            tablaSalidasEntrenadas.CellClick += tablaSalidasEntrenadas_CellClick;
        }

        // Cambia el foco entre las tablas para seleccionar la misma fila en todas y hacer mas facil el seguimiento
        private void tablaSalidasEntrenadas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Se quitan los handlers de las demas tablas
            tablaSalidas.CellClick -= tablaSalidas_CellClick;
            tablaEntradas.CellClick -= tablaEntradas_CellClick;

            // Se modifica el foco
            tablaSalidas.CurrentCell = tablaSalidas.Rows[tablaSalidasEntrenadas.CurrentRow.Index].Cells[0];
            tablaEntradas.CurrentCell = tablaEntradas.Rows[tablaSalidasEntrenadas.CurrentRow.Index].Cells[0];

            // Se reestablecen los handlers de las demas tablas
            tablaSalidas.CellClick += tablaSalidas_CellClick;
            tablaEntradas.CellClick += tablaEntradas_CellClick;
        }


        /* *** BOTONES *** */

        // Almacenar en las variables los datos de las tablas
        private void btnTomarPares_Click(object sender, EventArgs e)
        {
            // Se copian las entradas
            for (int filasEntradas = 0; filasEntradas < tablaEntradas.Rows.Count; filasEntradas++)
            {
                for (int columnasEntradas = 0; columnasEntradas < tablaEntradas.Columns.Count; columnasEntradas++)
                {
                    try
                    { entradas[filasEntradas + 1, columnasEntradas + 1] = Convert.ToDouble(tablaEntradas.Rows[filasEntradas].Cells[columnasEntradas].Value); }
                    catch (FormatException)
                    { MessageBox.Show("Por favor, verifique la fila " + (filasEntradas + 1).ToString() + ", columna" + (columnasEntradas + 1).ToString() + " de la tabla de entradas"); }
                }
            }

            // Se copian las salidas
            for (int filasSalidas = 0; filasSalidas < tablaSalidas.Rows.Count; filasSalidas++)
            {
                for (int columnasSalidas = 0; columnasSalidas < tablaSalidas.Columns.Count; columnasSalidas++)
                {
                    try
                    { salidas[filasSalidas + 1, columnasSalidas + 1] = Convert.ToDouble(tablaSalidas.Rows[filasSalidas].Cells[columnasSalidas].Value); }
                    catch (FormatException)
                    { MessageBox.Show("Por favor, verifique la fila " + (filasSalidas + 1).ToString() + ", columna" + (columnasSalidas + 1).ToString() + " de la tabla de salidas"); }
                }
            }
        }

        // Entrena la red con los datos almacenados
        private void btnEntrenar_Click(object sender, EventArgs e)
        {
            // Se deshabilita el boton hasta que se finalice el entrenamiento
            btnEntrenar.Enabled = false;

            // Se entrena la red
            perceptron.EntrenarRed(entradas, salidas, totalDeParesDeDatos, coeficienteDeAprendizaje, cantidadDePasadasParaEntrenar);

            if( chekSalidasReales.Checked == true )
                perceptron.ObtenerTablaEntradasSalidas(entradas, false);
            else
                perceptron.ObtenerTablaEntradasSalidas(entradas, true);

            // Se declara una variable para contabilizar la cantidad de aciertos
            int aciertos = 0;

            // Variables auxiliares
            int valorEsperado;
            int valorEntrenado;

            // Se vuelcan los datos del entrenamiento a la tabla para verlos
            for (int filaSalidas = 0; filaSalidas < totalDeParesDeDatos; filaSalidas++)
            {
                for (int columnaSalidas = 0; columnaSalidas < totalDeSalidas; columnaSalidas++)
                {
                    tablaSalidasEntrenadas.Rows[filaSalidas].Cells[columnaSalidas].Value = perceptron.salidasTabla[filaSalidas + 1, columnaSalidas + 1];

//                    valorEsperado = Int32.Parse(tablaSalidas.Rows[filaSalidas].Cells[columnaSalidas].Value.ToString());
//                    valorEntrenado = Int32.Parse(tablaSalidasEntrenadas.Rows[filaSalidas].Cells[columnaSalidas].Value.ToString());
//                    if (valorEsperado == valorEntrenado)
//                        aciertos++;
                }
            }

            // Se calcula el porcentaje de aciertos de la red, comparando los valores de las salidas esperadas con las obtenidas
//            lblAciertos.Text = (100 * aciertos / (totalDeParesDeDatos * totalDeSalidas)).ToString() + "%";

            // Se habilita para un nuevo entrenamiento
            btnEntrenar.Enabled = true;
        }

        // Borra los datos del entrenamiento
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            for (int filaSalidas = 0; filaSalidas < totalDeParesDeDatos; filaSalidas++)
                for (int columnaSalidas = 0; columnaSalidas < totalDeSalidas; columnaSalidas++)
                    tablaSalidasEntrenadas.Rows[filaSalidas].Cells[columnaSalidas].Value = 0;

            // Se borra el label indicador de los aciertos
            lblAciertos.Text = "-";
        }

        // Boton para desplegar la interfaz de windows para seleccionar el archivo excel con los datos
        private void btnAbrir_Click(object sender, EventArgs e)
        {
            // Se crea el objeto para seleccionar el archivo
            OpenFileDialog dialogoAbrirArchivo = new OpenFileDialog();

            // Para manejar los rangos de los datos en el archivo
            int totalDeColumnas;
            int totalDeFilas;

            // Se definen los filtros y los textos para limitar las opciones de seleccion
            dialogoAbrirArchivo.Filter = "Excel files |*.xlsx";
            dialogoAbrirArchivo.Title = "Seleccione el archivo de excel con los datos";

            // Se muestra el dialogo para seleccionar el archivo de datos
            if (dialogoAbrirArchivo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (dialogoAbrirArchivo.FileName.Equals("") == false)
                {
                    // Si se abre correctamente el archivo, se muestra la ruta del mismo, para saber correctamente cual se esta utilizando
                    ruta = dialogoAbrirArchivo.FileName;
                    tbxPath.Text = ruta;

                    // Se abre el archivo de Excel
                    xlWorkbook = xlApp.Workbooks.Open( @"" + ruta );

                    // Se marca el flag para indicar que se abrio el archivo Excel, para luego liberar los recursos
                    ExcelAbierto = true;

                    /* ****                  IMPORTANTE                     **** */
                    /*  LOS INDICES EN EL ARCHIVO DE EXCEL ARRANCAN SIEMPRE EN 1 */
                    /* ****                  IMPORTANTE                     **** */

                    /* Se leen los datos de la hoja "Entradas" */
                    xlWorksheet = xlWorkbook.Sheets[1];             // Se selecciona la hoja 1
                    xlRange = xlWorksheet.UsedRange;                // Se obtiene la dimension del total de datos contenidos ( filas x columnas )

                    // Se revisa el estado del checkBox "Imponer parametros" => Si esta activado, se redimensionan las variables segun el archivo
                    if (chekImponer.Checked == true)
                    {
                        // Se redimensionan las variables segun la cantidad de datos del archivo
                        totalDeColumnas = xlRange.Columns.Count;        // El total de columnas no se modifica, respecto del archivo ya que no hay una columna de encabezados
                        totalDeFilas = xlRange.Rows.Count - 1;          // Se resta 1 para contemplar el encabezado

                        // Se deben eliminar columnas
                        if (totalDeColumnas < totalDeEntradasExternas)
                            for (int columnasParaEliminar = totalDeEntradasExternas - totalDeColumnas; columnasParaEliminar > 0; columnasParaEliminar--)
                                numEntradas.Value--;

                        else
                        {
                            // Se deben agregar columnas
                            if (totalDeColumnas > totalDeEntradasExternas)
                                for (int columnasParaAgregar = totalDeColumnas - totalDeEntradasExternas; columnasParaAgregar > 0; columnasParaAgregar--)
                                    numEntradas.Value++;
                        }

                        // Se deben eliminar pares de datos
                        if (totalDeFilas < totalDeParesDeDatos)
                            for (int filasParaEliminar = totalDeParesDeDatos - totalDeFilas; filasParaEliminar > 0; filasParaEliminar--)
                                numCantidadDePares.Value--;

                        else
                        {
                            // Se deben agregar pares de datos
                            if (totalDeFilas > totalDeParesDeDatos)
                                for (int filasParaAgregar = totalDeFilas - totalDeParesDeDatos; filasParaAgregar > 0; filasParaAgregar--)
                                    numCantidadDePares.Value++;
                        }


                    }

                    // Sino, se toman solo los datos como indique la red actualmente
                    else
                    {
                        totalDeColumnas = totalDeEntradasExternas;
                        totalDeFilas = totalDeParesDeDatos;
                    }

                    // Se vuelcan los datos en la tabla
                    for (int indiceFila = 1; indiceFila < totalDeFilas; indiceFila++)
                        for (int indiceColumna = 1; indiceColumna <= totalDeColumnas; indiceColumna++)
                            if (xlRange.Cells[indiceFila + 1, indiceColumna] != null && xlRange.Cells[indiceFila + 1, indiceColumna].Value2 != null)
                                tablaEntradas.Rows[indiceFila - 1].Cells[indiceColumna - 1].Value = xlRange.Cells[indiceFila + 1, indiceColumna].Value2.ToString();


                    /* Se leen los datos de la hoja "Salidas" */
                    xlWorksheet = xlWorkbook.Sheets[2];             // Se selecciona la hoja 2
                    xlRange = xlWorksheet.UsedRange;                // Se obtiene la dimension del total de datos contenidos ( filas x columnas )

                    // Se revisa el estado del checkBox "Imponer parametros" => Si esta activado, se redimensionan las variables segun el archivo
                    if (chekImponer.Checked == true)
                    {
                        // Se redimensionan las variables segun la cantidad de datos del archivo
                        totalDeColumnas = xlRange.Columns.Count;        // El total de columnas no se modifica
                        totalDeFilas = xlRange.Rows.Count - 1;          // Se resta 1 para contemplar el encabezado

                        // Se deben eliminar columnas
                        if (totalDeColumnas < totalDeSalidas)
                            for (int columnasParaEliminar = totalDeSalidas - totalDeColumnas; columnasParaEliminar > 0; columnasParaEliminar--)
                                numSalidas.Value--;

                        else
                        {
                            // Se deben agregar columnas
                            if (totalDeColumnas > totalDeSalidas)
                                for (int columnasParaAgregar = totalDeColumnas - totalDeSalidas; columnasParaAgregar > 0; columnasParaAgregar--)
                                    numSalidas.Value++;
                        }
                    }

                    // Sino, se toman solo los datos como indique la red actualmente
                    else
                    {
                        totalDeColumnas = totalDeSalidas;
                        totalDeFilas = totalDeParesDeDatos;
                    }

                    // Se vuelcan los datos en la tabla
                    for (int indiceFila = 1; indiceFila < totalDeFilas; indiceFila++)
                        for (int indiceColumna = 1; indiceColumna <= totalDeColumnas; indiceColumna++)
                            if (xlRange.Cells[indiceFila + 1, indiceColumna] != null && xlRange.Cells[indiceFila + 1, indiceColumna].Value2 != null)
                                tablaSalidas.Rows[indiceFila - 1].Cells[indiceColumna - 1].Value = xlRange.Cells[indiceFila + 1, indiceColumna].Value2.ToString();

                    // Luego de volcar todos los datos a las tablas, se liberan los recursos
                    liberarRecursosArchivoExcel();
                }
            }



        }

        // Se borra la red actual y se genera una nueva
        private void btnNuevaRed_Click(object sender, EventArgs e)
        {
            // Se configura el Perceptron
            perceptron.ConfigurarPerceptron(totalDeEntradasExternas, totalDeSalidas, totalDeCapasOcultas, totalDeNeuronasEnCapasOcultas);
        }


        /* *** DEMAS COSAS *** */

        // Se deben liberar los recursos del archivo Excel cuando se termine el programa
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if( ExcelAbierto == true )
                liberarRecursosArchivoExcel();
        }

        // Funcion especifica para liberar los recursos utilizados para leer el archivo de Excel con los datos
        public void liberarRecursosArchivoExcel()
        {
            // Limpieza general
            GC.Collect();
            GC.WaitForPendingFinalizers();

            // Cierre de las conexiones con el archivo especifico de Excel
            Marshal.ReleaseComObject( xlRange );
            Marshal.ReleaseComObject( xlWorksheet );

            // Se cierra la conexion y se liberan los recursos restantes
            xlWorkbook.Close();
            Marshal.ReleaseComObject( xlWorkbook );
            xlApp.Quit();
            Marshal.ReleaseComObject( xlApp );

            // Se marca el flag indicando que ya se liberaron los recursos
            ExcelAbierto = false;
        }




        // Luego de entrenar la red, se pueden ingresar datos para la verificacion
        private void btnVerificacionCalcular_Click(object sender, EventArgs e)
        {
            // Se deben tomar los datos de la tabla de entradas de verificacion y calcular sus salidas
            for (int filasEntradas = 0; filasEntradas < totalDeParesDeDatosVerificacion; filasEntradas++)
            {
                for (int columnasEntradas = 0; columnasEntradas < tablaVerificacionEntradas.Columns.Count; columnasEntradas++)
                {
                    try
                    { entradasVerificacion[filasEntradas + 1, columnasEntradas + 1] = Convert.ToDouble(tablaVerificacionEntradas.Rows[filasEntradas].Cells[columnasEntradas].Value); }
                    catch (FormatException)
                    { MessageBox.Show("Por favor, verifique la fila " + (filasEntradas + 1).ToString() + ", columna" + (columnasEntradas + 1).ToString() + " de la tabla de entradas para la verificacion"); }
                }
            }

            // Se calculan las salidas
            perceptron.ObtenerTablaEntradasSalidas(entradasVerificacion, true);

            // Se vuelcan los datos a la tabla
            for (int filaSalidas = 0; filaSalidas < totalDeParesDeDatosVerificacion; filaSalidas++)
                for (int columnaSalidas = 0; columnaSalidas < totalDeSalidas; columnaSalidas++)
                    tablaVerificacionSalidas.Rows[filaSalidas].Cells[columnaSalidas].Value = perceptron.salidasTabla[filaSalidas + 1, columnaSalidas + 1];

        }

        // Borra los datos de la verificacion
        private void btnVerificacionBorrar_Click(object sender, EventArgs e)
        {
            for (int filaSalidas = 0; filaSalidas < totalDeParesDeDatosVerificacion; filaSalidas++)
                for (int columnaSalidas = 0; columnaSalidas < totalDeSalidas; columnaSalidas++)
                    tablaVerificacionSalidas.Rows[filaSalidas].Cells[columnaSalidas].Value = 0;
        }

        // Se almacenan los parametros de la red obtenida en un archivo de excel para su posterior uso en otras plataformas
        private void btnGuardarRed_Click(object sender, EventArgs e)
        {
            DateTime fechaHora = DateTime.Now;
            Microsoft.Office.Interop.Excel.Application aplicacion;
            Microsoft.Office.Interop.Excel.Workbook libros_trabajo;
            Microsoft.Office.Interop.Excel.Worksheet hoja_trabajo;
            aplicacion = new Microsoft.Office.Interop.Excel.Application();
            libros_trabajo = aplicacion.Workbooks.Add();
            hoja_trabajo = (Microsoft.Office.Interop.Excel.Worksheet)libros_trabajo.Worksheets.get_Item(1);

            // Primero se escriben los encabezados
            for (int indiceCapa = 0; indiceCapa < perceptron.pesos.GetLength(0); indiceCapa++)
            {
                hoja_trabajo.Cells[1 + indiceCapa * 10, 1] = "Pesos de la capa " + indiceCapa.ToString();
                hoja_trabajo.Cells[1 + indiceCapa * 10, 2] = "Indice de Llegada";
                hoja_trabajo.Cells[2 + indiceCapa * 10, 1] = "Indice de Salida";
                for (int indiceSalida = 1; indiceSalida < perceptron.pesos.GetLength(1); indiceSalida++)
                    for (int indiceLlegada = 1; indiceLlegada < perceptron.pesos.GetLength(2); indiceLlegada++)
                        hoja_trabajo.Cells[indiceCapa * 10 + indiceSalida + 1, indiceLlegada + 1] = perceptron.pesos[indiceCapa, indiceSalida, indiceLlegada];
            }


            // Definiciones para hacer más legible el nombre del archivo en excel
            string extension = @".xls";
            string nombre = fechaHora.ToString().Replace(" ", "-").Replace(":", "-").Replace("/", "-");
            string ubicacionArchivo = "-" + nombre + extension;

            libros_trabajo.SaveAs(Directory.GetCurrentDirectory() + ubicacionArchivo, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);    
            
            libros_trabajo.Close(true);
            aplicacion.Quit();
        }

        // Check box para indicar si se quieren mostrar las salidas reales o las salidas convertidas a binario
        private void chekSalidasReales_CheckedChanged(object sender, EventArgs e)
        {
            // Se cambia el titulo del groupBox para indicar que se esta mostrando
            if( chekSalidasReales.Checked == true )
                gbxSalidasEntrenadas.Text = "Salidas Reales";
            else
                gbxSalidasEntrenadas.Text = "Salidas Entrenadas";
        }




        /* *** EXTRAS *** */


    }
}
