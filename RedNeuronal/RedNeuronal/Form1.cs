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
        private double[] entradas;

        private int totalDeEntradasExternas = 3;
        private int totalDeSalidas = 2;
        private int totalDeCapasOcultas = 2;
        private int totalDeNeuronasEnCapasOcultas = 4;

        public Form1()
        {
            // Codigo de inicializacion interno del formulario
            InitializeComponent();

            // Instanciacion del perceptron
            perceptron = new PerceptronMulticapa();

            perceptron.ConfigurarPerceptron( totalDeEntradasExternas, totalDeSalidas, totalDeCapasOcultas, totalDeNeuronasEnCapasOcultas );

            entradas = new double[totalDeEntradasExternas+1];

            entradas[1] = 1;
            entradas[2] = 0;
            entradas[3] = 0;

            perceptron.CalcularSalida( entradas );

            MessageBox.Show( "Ok" );
        }
    }
}
