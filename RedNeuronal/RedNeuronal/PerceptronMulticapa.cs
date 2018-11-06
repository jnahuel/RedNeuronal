using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNeuronal
{
    class PerceptronMulticapa
    {
        // Variables para el kernel de la red
        private double[, ,]     pesos;              // Matriz de pesos.     Se usa asi =>   pesos[capa,neuronaInicial,neuronaFinal]
        private double[,]       salidas;            // Matriz de salidas.   Se usa asi =>   salidas[capa,neuronaInicial]
        private double[,]       umbrales;           // Matriz de umbrales.  Se usa asi =>   umbrales[capa,neuronaInicial]
        private double[]        entradas;           // Vector para almacenar el juego de valores de las entradas
        private int[]           neuronasPorCapa;    // Vector para almacenar la cantidad de neuronas en cada capa
        private int             totalDeCapas;       // Indicador de la cantidad de capas que tiene la red

        // Variables auxiliares para llevar el control del funcionamiento de la red
        private string          error;              // Para almacenar el texto descriptivo del error que pueda ocurrir





        /********************************************************************************************************************************************/
        /*                                                         CONSTRUCTOR DE LA CLASE                                                          */
        /********************************************************************************************************************************************/

        public PerceptronMulticapa() { }        // Constructor sin parametros



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                           MÉTODOS Y PROPIEDADES                                                          */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/



        /********************************************************************************************************************************************/
        /*                                                                 PUBLICOS                                                                 */
        /********************************************************************************************************************************************/


        /****************************************************************/
        /* ConfigurarPerceptron                                         */
        /*   Configura el puerto serie, lo abre y recibe el mensaje     */
        /*   inicial del casco                                          */
        /*                                                              */
        /* Recibe:                                                      */
        /*   nombrePuerto - string con el nombre del puerto a usar      */
        /*   mostrarInformacion - Un indicador para mostrar o no el     */
        /*      mensaje inicial                                         */
        /* Devuelve:                                                    */
        /*   True - Si se configuro y abrio correctamente               */
        /*   False - Si hay algun problema                              */
        /****************************************************************/
        public bool ConfigurarPerceptron( int numeroDeEntradas, int numeroDeSalidas, int numeroDeCapasOcultas, int numeroDeNeuronasEnCapasOcultas )
        {
            // Verificacion de los parametros de entrada
            if (numeroDeEntradas < 1)               { error = "El numero de entradas tiene que ser mayor o igual a uno";  return (false); }
            if (numeroDeSalidas < 1)                { error = "El numero de salidas tiene que ser mayor o igual a uno"; return (false); }
            if (numeroDeCapasOcultas < 1)           { error = "El numero de capas ocultas tiene que ser mayor o igual a uno"; return (false); }
            if (numeroDeNeuronasEnCapasOcultas < 1) { error = "El numero de neuronas en las capas ocultas tiene que ser mayor o igual a uno"; return (false); }

            // Verificados los parametros de entrada, se establece la estructura de la red
            totalDeCapas = numeroDeCapasOcultas + 2;        // Total = Entrada + Capas ocultas + Salida
            entradas = new double[ numeroDeEntradas + 1 ];  // Se dimensiona el vector de entradas. El +1 es para evitar el indice cero
            neuronasPorCapa = new int[ totalDeCapas ];      // Se dimensiona el vector de neuronas por capa

            // Para hacerlo mas sencillo, se calcula en que capa se tienen mas neuronas y se dimensionan todos con ese valor
            int maximoDeNeuronas;
            if (numeroDeEntradas > numeroDeSalidas )
            {
                if (numeroDeEntradas > numeroDeNeuronasEnCapasOcultas)
                    maximoDeNeuronas = numeroDeEntradas;
                else
                    maximoDeNeuronas = numeroDeNeuronasEnCapasOcultas;
            }
            else
            {
                if (numeroDeSalidas > numeroDeNeuronasEnCapasOcultas)
                    maximoDeNeuronas = numeroDeSalidas;
                else
                    maximoDeNeuronas = numeroDeNeuronasEnCapasOcultas;
            }
            maximoDeNeuronas++;                             // Para evitar el indice cero

            // Conociendo el maximo de neuronas en una capa, se dimensionan el resto de las variables
            pesos = new double[ totalDeCapas, maximoDeNeuronas, maximoDeNeuronas ];
            salidas = new double[totalDeCapas + 1, maximoDeNeuronas];
            umbrales = new double[totalDeCapas + 1, maximoDeNeuronas];
            neuronasPorCapa = new int[totalDeCapas + 1];
            neuronasPorCapa[1] = numeroDeEntradas;
            neuronasPorCapa[totalDeCapas] = numeroDeSalidas;
            for (int indice = 2; indice < totalDeCapas; indice++)
                neuronasPorCapa[indice] = numeroDeNeuronasEnCapasOcultas;


            // Inicializacion de las variables con numeros aleatorios
            Random azar = new Random(23);                   // Variable para calcular los numeros aleatorios
            for (int capa = 1; capa < totalDeCapas; capa++)
            {
                // Bucle para los pesos
                for( int neuronaInicial = 1; neuronaInicial <= neuronasPorCapa[capa]; neuronaInicial++ )
                    for( int neuronaFinal = 1; neuronaFinal <= neuronasPorCapa[capa+1]; neuronaFinal++ )
                        pesos[ capa, neuronaInicial, neuronaFinal ] = azar.NextDouble();

                // Bucle para los umbrales
                for (int neuronaUmbral = 1; neuronaUmbral <= neuronasPorCapa[capa + 1]; neuronaUmbral++)
                    umbrales[capa + 1, neuronaUmbral] = azar.NextDouble();
            }

           return (true);
        }


        public void CalcularSalida( double[] valoresEntradasExternas )
        {
            // Variables auxiliares
            double sumatoria;

            // La primer capa no realiza procesamiento, solo retiene los valores de las entradas externas
            for (int indiceEntrada = 1; indiceEntrada <= neuronasPorCapa[1]; indiceEntrada++)
                salidas[1, indiceEntrada] = valoresEntradasExternas[indiceEntrada];

            // Se calculan las demas capas
            for( int capa = 2; capa <= totalDeCapas; capa++ )
                for( int neuronaLlegada = 1; neuronaLlegada <= neuronasPorCapa[capa]; neuronaLlegada++ )
                {
                    sumatoria = 0;
                    for (int neuronaSalida = 1; neuronaSalida <= neuronasPorCapa[capa - 1]; neuronaSalida++)
                        sumatoria += salidas[capa - 1, neuronaSalida] * pesos[capa - 1, neuronaSalida, neuronaLlegada];
                    sumatoria += umbrales[ capa, neuronaLlegada ];
                    salidas[capa, neuronaLlegada] = Sigmoidea(sumatoria);
                }

        }






        // Funcion de activacion de la neurona. 
        private double Sigmoidea(double valor)
        {
            return( 1 / ( 1 + Math.Exp( -valor ) ) );
        }



        // Metodos publicos para acceder a las variables
        string Error { get { return(error); } }

    }
}
