using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedNeuronal
{
    public partial class Form1 : Form
    {
        // Variables globales
        PerceptronMulticapa perceptron;

        // La forma de acceder a las entradas y salidas es: Variable[ fila o par de datos; numero de entrada o salida ]
        // Variables para almacenar los datos de entradas y salidas para el entrenamiento
        private double[,]   entradas;
        private double[,]   salidas;
        private double[]    entradasAuxiliares;
        private double[]    salidasAuxiliares;

        // Total de filas de la tabla de pares entradas-salidas
        private int totalDeParesDeDatos = 8;

        // Parametros de la red
        private int totalDeEntradasExternas = 3;
        private int totalDeSalidas = 2;
        private int totalDeCapasOcultas = 1;
        private int totalDeNeuronasEnCapasOcultas = 2;

        // Indicadores de la forma de entrenamiento
        private int cantidadDePasadasParaEntrenar = 1000;
        private double coeficienteDeAprendizaje = 0.4;



        public Form1()
        {
            // Codigo de inicializacion interno del formulario
            InitializeComponent();

            // Instanciacion del perceptron
            perceptron = new PerceptronMulticapa();

            perceptron.ConfigurarPerceptron( totalDeEntradasExternas, totalDeSalidas, totalDeCapasOcultas, totalDeNeuronasEnCapasOcultas );

            entradas = new double[ totalDeParesDeDatos + 1, totalDeEntradasExternas + 1 ];
            salidas  = new double[ totalDeParesDeDatos + 1, totalDeSalidas + 1];

            /* **** Pasaje manual de los datos para las pruebas **** */

            /*  Tabla propuesta  */
            /*-------------------*/
            /*     000 - 10      */
            /*     001 - 11      */
            /*     010 - 10      */
            /*     011 - 00      */
            /*     100 - 11      */
            /*     101 - 00      */
            /*     110 - 00      */
            /*     111 - 10      */
            /*-------------------*/

            // Primer par de datos
            entradas[1, 1]  = 0;
            entradas[1, 2]  = 0;
            entradas[1, 3]  = 0;
            salidas[1, 1]   = 1;
            salidas[1, 2]   = 0;
            // Segundo par de datos
            entradas[2, 1]  = 0;
            entradas[2, 2]  = 0;
            entradas[2, 3]  = 1;
            salidas[2, 1]   = 1;
            salidas[2, 2]   = 1;
            // Tercer par de datos
            entradas[3, 1]  = 0;
            entradas[3, 2]  = 1;
            entradas[3, 3]  = 0;
            salidas[3, 1]   = 1;
            salidas[3, 2]   = 0;
            // Cuarto par de datos
            entradas[4, 1]  = 0;
            entradas[4, 2]  = 1;
            entradas[4, 3]  = 1;
            salidas[4, 1]   = 0;
            salidas[4, 2]   = 0;
            // Quinto par de datos
            entradas[5, 1]  = 1;
            entradas[5, 2]  = 0;
            entradas[5, 3]  = 0;
            salidas[5, 1]   = 1;
            salidas[5, 2]   = 1;
            // Sexto par de datos
            entradas[6, 1]  = 1;
            entradas[6, 2]  = 0;
            entradas[6, 3]  = 1;
            salidas[6, 1]   = 0;
            salidas[6, 2]   = 0;
            // Septimo par de datos
            entradas[7, 1]  = 1;
            entradas[7, 2]  = 1;
            entradas[7, 3]  = 0;
            salidas[7, 1]   = 0;
            salidas[7, 2]   = 0;
            // Octavo par de datos
            entradas[8, 1]  = 1;
            entradas[8, 2]  = 1;
            entradas[8, 3]  = 1;
            salidas[8, 1]   = 1;
            salidas[8, 2]   = 0;
            /* **** */

            entradasAuxiliares = new double[ totalDeEntradasExternas + 1 ];
            salidasAuxiliares = new double[ totalDeSalidas + 1 ];

            perceptron.EntrenarRed(entradas, salidas, totalDeParesDeDatos, coeficienteDeAprendizaje, cantidadDePasadasParaEntrenar);

            perceptron.ObtenerTablaEntradasSalidas(entradas);

            MessageBox.Show("Ok");
        }
    }
}
