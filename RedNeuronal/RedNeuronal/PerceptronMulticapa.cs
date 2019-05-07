using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNeuronal
{
    class PerceptronMulticapa
    {
        // Definicion de los tipos de funcion de activacion para poder hacer mas flexible el codigo
        public enum             funcionesActivacion { FUNCION_SIGMOIDEA, FUNCION_TANGENCIAL, FUNCION_PROPIA };

        // Variables para el kernel de la red
        public double[, ,]      pesos;              // Matriz de pesos.     Se usa asi =>   pesos[capa,neuronaInicial,neuronaFinal]
        public double[,]        seniales;           // Matriz de seniales.  Se usa asi =>   salidas[capa,neuronaInicial]
        public double[,]        umbrales;           // Matriz de umbrales.  Se usa asi =>   umbrales[capa,neuronaInicial]
        private int[]           neuronasPorCapa;    // Vector para almacenar la cantidad de neuronas en cada capa
        private int             totalDeCapas;       // Indicador de la cantidad de capas que tiene la red
        private int             totalDeEntradasExternas;
        private int             totalDeSalidasExternas;
        funcionesActivacion     tipoDeActivacion = funcionesActivacion.FUNCION_SIGMOIDEA;

        // Variables para el entrenamiento
        private double[]        entradas;           // Vector para almacenar el juego de valores de las entradas
        private double[]        salidasEsperadas;   // Vector para almacenar los valores esperados de las salidas, contra los cuales se comparan los obtenidos
        private double[, ,]     derivadasDeLosPesos;
        private double[,]       derivadasDeLosUmbrales;
        private double[]        entradasAuxiliares;
        private double[]        salidasAuxiliares;

        // Variables para almacenar los conjuntos de pares entradas-salidas reales de la red para armar una tabla y obtener el porcentaje de acierto
        public double[,]        salidasTabla;

        // Variables auxiliares para llevar el control del funcionamiento de la red
        private string          error;              // Para almacenar el texto descriptivo del error que pueda ocurrir





        /********************************************************************************************************************************************/
        /*                                                         CONSTRUCTOR DE LA CLASE                                                          */
        /********************************************************************************************************************************************/

        public PerceptronMulticapa(  ) { }        // Constructor sin parametros



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
            entradasAuxiliares = new double[numeroDeEntradas + 1];
            salidasEsperadas = new double[ numeroDeSalidas + 1 ];
            salidasAuxiliares = new double[numeroDeSalidas + 1];
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
            derivadasDeLosPesos = new double[ totalDeCapas, maximoDeNeuronas, maximoDeNeuronas ];
            derivadasDeLosUmbrales = new double[ totalDeCapas + 1, maximoDeNeuronas ];
            seniales = new double[totalDeCapas + 1, maximoDeNeuronas];
            umbrales = new double[totalDeCapas + 1, maximoDeNeuronas];
            neuronasPorCapa = new int[totalDeCapas + 1];
            neuronasPorCapa[1] = numeroDeEntradas;
            neuronasPorCapa[totalDeCapas] = numeroDeSalidas;
            totalDeEntradasExternas = numeroDeEntradas;
            totalDeSalidasExternas = numeroDeSalidas;
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



        /****************************************************************/
        /* ObtenerTablaEntradasSalidas                                  */
        /*   Recibe una matriz con las entradas externas y calcula las  */
        /*   salidas de la red para ver el porcentaje de acierto.       */
        /*   Para hacerlo mas facil de ver, se utiliza una funcion para */
        /*   redondear los valores a 0 y 1.                             */
        /*                                                              */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        public void ObtenerTablaEntradasSalidas(double[,] entradasExternas, bool salidasRedondeadas)
        {
            // Se obtiene el numero de pares de entradas-salidas, representado por la cantidad de filas que tenga la matriz de datos
            int totalDePares = entradasExternas.GetLength(0);

            salidasTabla = new double[totalDePares, totalDeSalidasExternas + 1];

            for (int indiceParDeDatos = 1; indiceParDeDatos < totalDePares; indiceParDeDatos++)
            {
                // Se copian los valores a utilizar en variables auxiliares
                for (int indiceEntrada = 1; indiceEntrada <= totalDeEntradasExternas; indiceEntrada++)
                    entradasAuxiliares[indiceEntrada] = entradasExternas[indiceParDeDatos, indiceEntrada];

                // Luego se procesan los valores de las seniales de entrada para obtener las salidas reales
                CalcularSalidas(entradasAuxiliares);

                // Posteriomente se redondean hacia 0 y 1 para hacer mas facil las comparaciones, si es que asi se lo requiere
                if (salidasRedondeadas == true)
                    redondearSalidas(indiceParDeDatos);
                else
                {
                    // Se recorren las salidas reales de la red redondeando de a una
                    for (int indiceSalida = 1; indiceSalida <= totalDeSalidasExternas; indiceSalida++)
                    {
                        salidasTabla[indiceParDeDatos, indiceSalida] = seniales[totalDeCapas, indiceSalida];
                    }
                }
            }
        }



        /****************************************************************/
        /* CalcularSalida                                               */
        /*   Recibe un vector con los datos de las entradas externas de */
        /*   la red y calcula las salidas de la red.                    */
        /*   Para tener mayor versatilidad, la salida de cada neurona   */
        /*   se calcula mediante "evaluarFuncionDeActivacion", para     */
        /*   poder elegir facilmente que tipo de funcion de activacion  */
        /*   usar y comparar resultados.                                */
        /*                                                              */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        public void CalcularSalidas(double[] valoresEntradasExternas)
        {
            // Variables auxiliares
            double sumatoria;

            // La primer capa no realiza procesamiento, solo retiene los valores de las entradas externas
            for (int indiceEntrada = 1; indiceEntrada <= neuronasPorCapa[1]; indiceEntrada++)
                seniales[1, indiceEntrada] = valoresEntradasExternas[indiceEntrada];

            // Se calculan las demas capas
            for (int capa = 2; capa <= totalDeCapas; capa++)
                for (int neuronaLlegada = 1; neuronaLlegada <= neuronasPorCapa[capa]; neuronaLlegada++)
                {
                    sumatoria = 0;
                    for (int neuronaSalida = 1; neuronaSalida <= neuronasPorCapa[capa - 1]; neuronaSalida++)
                        sumatoria += seniales[capa - 1, neuronaSalida] * pesos[capa - 1, neuronaSalida, neuronaLlegada];
                    sumatoria += umbrales[capa, neuronaLlegada];
                    seniales[capa, neuronaLlegada] = evaluarFuncionDeActivacion(sumatoria);
                }

        }



        /****************************************************************/
        /* EntrenarRed                                                  */
        /*   Recibe una matriz con las entradas externas y otra matriz  */
        /*   con las salidas esperadas para dichas entradas.            */
        /*   Tambien el coeficiente de aprendizaje y la cantidad de     */
        /*   iteraciones para el entrenamiento.                         */
        /*                                                              */
        /*   Se plantea que a futuro reciba tambien un valor indicando  */
        /*   el porcentaje minimo aceptable de aciertos de la red a     */
        /*   partir del cual se podria cortar el loop de iteraciones.   */
        /*   Con esto se busca optimizar el tiempo de entrenamiento, ya */
        /*   que si se alcanza un porcentaje de aciertos aceptables, no */
        /*   seria necesario seguir iterando el entrenamiento.          */
        /*                                                              */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        public void EntrenarRed(double[,] valoresEntradasExternas, double[,] salidasCorrectas, int totalDeParesDeDatos, double coeficienteDeAprendizaje, int cantidadDePasadasParaEntrenar)
        {
            for (int indicePasadaEntrenamiento = 1; indicePasadaEntrenamiento <= cantidadDePasadasParaEntrenar; indicePasadaEntrenamiento++)
            {
                // En cada pasada se tienen que recorrer todos los pares de datos entradas-salidas que se tengan
                for (int indiceParDeDatos = 1; indiceParDeDatos <= totalDeParesDeDatos; indiceParDeDatos++)
                {
                    // Se copian los valores a utilizar en variables auxiliares
                    for (int indiceEntrada = 1; indiceEntrada <= totalDeEntradasExternas; indiceEntrada++)
                        entradasAuxiliares[indiceEntrada] = valoresEntradasExternas[indiceParDeDatos, indiceEntrada];
                    for (int indiceSalida = 1; indiceSalida <= totalDeSalidasExternas; indiceSalida++)
                        salidasAuxiliares[indiceSalida] = salidasCorrectas[indiceParDeDatos, indiceSalida];

                    actualizarPesosUmbrales(entradasAuxiliares, salidasAuxiliares, coeficienteDeAprendizaje);
                }
            }
        }





        /********************************************************************************************************************************************/
        /*                                                                 PRIVADOS                                                                 */
        /********************************************************************************************************************************************/




                        /*******************************************************************************************************/
                        /*************** *************** SECCION PARA EL ENTRENAMIENTO DE LA RED *************** ***************/
                        /*******************************************************************************************************/

        /****************************************************************/
        /* actualizarPesosUmbrales                                      */
        /*   Recibe un par de entradas-salidas esperadas para la red y  */
        /*   el coeficiente de aprendizaje para corregir los pesos.     */
        /*                                                              */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        private void actualizarPesosUmbrales(double[] valoresEntradasExternas, double[] salidasCorrectas, double coeficienteDeAprendizaje)
        {
            // Primero se copian los valores de las seniales de salida que se deberina obtener
            salidasEsperadas = salidasCorrectas;

            // Luego se procesan los valores de las seniales de entrada para obtener las salidas reales
            CalcularSalidas( valoresEntradasExternas );

            // Luego se debe realizar un loop para hallar la derivada del error respecto de todos los pesos y los umbrales
            for (int capa = totalDeCapas; capa >= 1; capa--)                                                      // Alpha
                for (int indiceLlegada = 1; indiceLlegada <= neuronasPorCapa[capa]; indiceLlegada++)              // Gamma
                {
                    // Bucle para calcular la derivada de los pesos
                    for (int indiceSalida = 1; indiceSalida <= neuronasPorCapa[capa - 1]; indiceSalida++)         // Beta
                        derivadasDeLosPesos[capa - 1, indiceSalida, indiceLlegada] = derivadaDelErrorRespectoDelPeso(capa - 1, indiceSalida, indiceLlegada);

                    // Calculo de la derivada de los umbrales
                    derivadasDeLosUmbrales[capa, indiceLlegada] = derivadaDelErrorRespectoDelUmbral( capa, indiceLlegada );
                }

            // Se actualizan los valores de los pesos y los umbrales
            for (int capa = totalDeCapas; capa >= 1; capa--)
                for (int indiceLlegada = 1; indiceLlegada <= neuronasPorCapa[capa]; indiceLlegada++)
                {
                    // Correccion de los pesos
                    for (int indiceSalida = 1; indiceSalida <= neuronasPorCapa[capa - 1]; indiceSalida++)
                        pesos[capa - 1, indiceSalida, indiceLlegada] -= coeficienteDeAprendizaje * derivadasDeLosPesos[capa - 1, indiceSalida, indiceLlegada];

                    // Correccion de los umbrales
                    umbrales[capa, indiceLlegada] -= coeficienteDeAprendizaje * derivadasDeLosUmbrales[capa, indiceLlegada];
                }
        }



                            /*************** *************** DERIVADAS RESPECTO DE LOS PESOS *************** ***************/

        /****************************************************************/
        /* derivadaDelErrorRespectoDelPeso                              */
        /*   Es el calculo de la derivada del error respecto de un peso */
        /*   especifico para corregir su valor.                         */
        /*   La ecuacion es igual a:                                    */
        /*      Sum( [Yi - Si] * derivada( Yi ) / derivada del peso )   */
        /*   Donde:                                                     */
        /*      Yi: Salida real de la red.                              */
        /*      Si: Salida esperada de la red.                          */
        /*      der(Yi): Es la derivada de la salida Yi respecto del    */
        /*          peso en cuestion.                                   */
        /*                                                              */
        /*   La diferencia entra las salidas se calcula directamente.   */
        /*   Para hacerlo mas sencillo de entender, la derivada de las  */
        /*   salidas respecto de los diferentes pesos de realiza en una */
        /*   funcion especifica.                                        */
        /*                                                              */
        /* Recibe:                                                      */
        /*   Wcapa - La capa del peso solicitado.                       */
        /*   Wsalida - La neurona de salida que conecta el peso.        */
        /*   Wllegada - La neurona de llegada que conecto el peso.      */
        /* Devuelve:                                                    */
        /*   El resultado numerico del calculo.                         */
        /****************************************************************/
        private double derivadaDelErrorRespectoDelPeso(int Wcapa, int Wsalida, int Wllegada)
        {
            // Variables auxiliares para hacer mas legible el codigo
            double primerTermino;
            double segundoTermino;
            double sumatoria;
            int ultimaCapa = totalDeCapas;

            // Inicializacion de las variables criticas, por las dudas
            sumatoria = 0;

            // Bucle para realizar la sumatoria
            for (int indiceSalida = 1; indiceSalida <= neuronasPorCapa[ ultimaCapa ]; indiceSalida++)
            {
                primerTermino = seniales[ultimaCapa, indiceSalida] - salidasEsperadas[indiceSalida];
                segundoTermino = derivadaDeLaSalidaRespectoDelPeso(indiceSalida, Wcapa, Wsalida, Wllegada);
                sumatoria += primerTermino * segundoTermino;
            }

            // Luego del loop, solo resta devolver el valor calculado
            return( sumatoria );
        }



        /****************************************************************/
        /* derivadaDeLaSalidaRespectoDelPeso                            */
        /*   Calcula la derivada de una salida especifica respecto del  */
        /*   peso solicitado.                                           */
        /*   Es llamada desde "derivadaDelErrorRespectoDelPeso" para    */
        /*   obtener el segundo termino.                                */
        /*                                                              */
        /*   Cada salida se calcula como:                               */
        /*      Yi = Fact( U + sum( a * W ) )                           */
        /*   Donde:                                                     */
        /*      U: Es el umbral de la neurona.                          */
        /*      a: Son las salidas de las neuronas de la capa anterior. */
        /*      W: Son los pesos que ponderan las salidas de las        */
        /*          neuronas de la capa anterior.                       */
        /*      Fact(): Es la funcion de activacion de la neurona.      */
        /*          Normalmente sera una Sigmoidea o una Tangente.      */
        /*          Esto hace que se tengan que realizar las derivadas  */
        /*          mediante la regla de la cadena, debiendo derivar la */
        /*          funcion de activacion y luego las derivadas         */
        /*          parciales del argumento de la funcion de activacion.*/
        /*                                                              */
        /*   Al igual que en la funcion precedente, se opta por dividir */
        /*   el procedimiento en 2 calculos y obtener las derivadas     */
        /*   parciales mediante una funcion especifica para una mayor   */
        /*   comprension del codigo.                                    */
        /*   A su vez, tambien se plantea una funcion especifica para   */
        /*   la derivada de la funcion de activacion. Mediante esto, el */
        /*   procedimiento general sera el mismo independientemente de  */
        /*   que funcion se emplee, y se podran comparar resultados con */
        /*   mayor facilidad a futuro.                                  */
        /*                                                              */
        /* Recibe:                                                      */
        /*   salida - El numero de la salida requerida                  */
        /*   Wcapa - La capa del peso solicitado.                       */
        /*   Wsalida - La neurona de salida que conecta el peso.        */
        /*   Wllegada - La neurona de llegada que conecto el peso.      */
        /* Devuelve:                                                    */
        /*   El resultado numerico del calculo.                         */
        /****************************************************************/
        private double derivadaDeLaSalidaRespectoDelPeso( int salidaIndice, int Wcapa, int Wsalida, int Wllegada )
        {
            // Variables auxiliares para hacer mas legible el codigo
            double primerTermino;
            double segundoTermino;
            double sumatoria;
            int salidaCapa = totalDeCapas;

            // Inicializacion de las variables criticas, por las dudas
            sumatoria = 0;

            // Primero se debe revisar si la capa del peso es la anterior a la de la salida a derivar.
            if (salidaCapa == (Wcapa + 1))
            {
                // Si coinciden el indice de la salida con el Wllegada, entonces la derivada no es nula
                if (salidaIndice == Wllegada)
                {
                    primerTermino = derivadaDeLaFuncionActivacion(salidaCapa, salidaIndice);
                    segundoTermino = seniales[Wcapa, Wsalida];
                    return (primerTermino * segundoTermino);
                }
                // Si no coinciden, la derivada es cero porque la senial no depende del peso en cuestion
                else
                {
                    return (0);
                }
            }

            // Si no coinciden, se debe realizar la derivada de la funcion de activacion por la sumatoria de las derivadas parciales de las seniales de la capa anterior
            primerTermino = derivadaDeLaFuncionActivacion(salidaCapa, salidaIndice);
            for (int indiceSenial = 1; indiceSenial <= neuronasPorCapa[salidaCapa-1]; indiceSenial++)
            {
                segundoTermino = derivadaParcialRespectoDelPeso(salidaCapa - 1, indiceSenial, Wcapa, Wsalida, Wllegada);
                sumatoria += pesos[salidaCapa - 1, indiceSenial, salidaIndice] * segundoTermino;
            }

            return ( primerTermino * sumatoria );
        }



        /****************************************************************/
        /* derivadaParcialRespectoDelPeso                               */
        /*   Realiza la derivada parcial de cualquier senial de salida  */
        /*   de una neurona respecto de cualquier peso.                 */
        /*   Es practicamente el mismo procedimiento que el de la       */
        /*   funcion "derivadaDeLaSalidaRespectoDelPeso", salvo que     */
        /*   esa funcion se aplica solo a las salidas de las neuronas   */
        /*   de la ultima capa de la red, o sea, las salidas externas   */
        /*   de la red, mientras que esta funcion es para todas las     */
        /*   seniales internas de la red.                               */
        /*                                                              */
        /* Recibe:                                                      */
        /*   senialCapa - La capa de la senial a derivar.               */
        /*   senialNeurona - El numero de la neurona a derivar.         */
        /*   Wcapa - La capa del peso solicitado.                       */
        /*   Wsalida - La neurona de salida que conecta el peso.        */
        /*   Wllegada - La neurona de llegada que conecto el peso.      */
        /* Devuelve:                                                    */
        /*   El resultado numerico del calculo.                         */
        /****************************************************************/
        private double derivadaParcialRespectoDelPeso( int senialCapa, int senialNeurona, int Wcapa, int Wsalida, int Wllegada )
        {
            // Variables auxiliares
            double primerTermino;
            double segundoTermino;
            double sumatoria;

            // Primero se debe revisar si la capa del peso es la anterior a la de la senial a derivar.
            if (senialCapa == (Wcapa + 1))
            {
                // Si coinciden el indice de la senial con el Wllegada, entonces la derivada no es nula
                if (senialNeurona == Wllegada)
                {
                    primerTermino = derivadaDeLaFuncionActivacion( senialCapa, senialNeurona );
                    segundoTermino = seniales[ Wcapa, Wsalida ];
                    return( primerTermino * segundoTermino );
                }
                // Si no coinciden, la derivada es cero porque la senial no depende del peso en cuestion
                else
                {
                    return (0);
                }
            }

            // Si no coinciden, se debe realizar la derivada de la funcion de activacion por la sumatoria de las derivadas parciales de las seniales de la capa anterior
            primerTermino = derivadaDeLaFuncionActivacion(senialCapa, senialNeurona);
            sumatoria = 0;
            for (int indiceSenialCapaAnterior = 1; indiceSenialCapaAnterior <= neuronasPorCapa[senialCapa - 1]; indiceSenialCapaAnterior++)
            {
                segundoTermino = derivadaParcialRespectoDelPeso( senialCapa-1, indiceSenialCapaAnterior, Wcapa, Wsalida, Wllegada );
                sumatoria += pesos[senialCapa - 1, indiceSenialCapaAnterior, senialNeurona] * segundoTermino;
            }

            // Luego del loop, resta multiplicar los terminos
            return (primerTermino * sumatoria);
        }



                            /*************** *************** DERIVADAS RESPECTO DE LOS UMBRALES *************** ***************/

        /****************************************************************/
        /* derivadaDelErrorRespectoDelUmbral                            */
        /*   Es el calculo de la derivada del error respecto de un      */
        /*   umbral especifico para corregir su valor.                  */
        /*   La ecuacion es igual a:                                    */
        /*      Sum( [Yi - Si] * derivada( Yi ) / derivada del umbral ) */
        /*   Donde:                                                     */
        /*      Yi: Salida real de la red.                              */
        /*      Si: Salida esperada de la red.                          */
        /*      der(Yi): Es la derivada de la salida Yi respecto del    */
        /*          umbral en cuestion.                                 */
        /*                                                              */
        /*   La diferencia entra las salidas se calcula directamente.   */
        /*   Para hacerlo mas sencillo de entender, la derivada de las  */
        /*   salidas respecto de los diferentes umbrales de realiza en  */
        /*   una funcion especifica.                                    */
        /*                                                              */
        /* Recibe:                                                      */
        /*   Ucapa - La capa del umbral solicitado.                     */
        /*   Ullegada - El numero de la neurona a la cual ingresa el    */
        /*              umbral.                                         */
        /* Devuelve:                                                    */
        /*   El resultado numerico del calculo.                         */
        /****************************************************************/
        private double derivadaDelErrorRespectoDelUmbral(int Ucapa, int Ullegada)
        {
            // Variables auxiliares para hacer mas legible el codigo
            double primerTermino;
            double segundoTermino;
            double sumatoria;
            int ultimaCapa = totalDeCapas;

            // Inicializacion de las variables criticas, por las dudas
            sumatoria = 0;

            // Bucle para realizar la sumatoria
            for (int indiceSalida = 1; indiceSalida <= neuronasPorCapa[ultimaCapa]; indiceSalida++)
            {
                primerTermino = seniales[ultimaCapa, indiceSalida] - salidasEsperadas[indiceSalida];
                segundoTermino = derivadaDeLaSalidaRespectoDelUmbral(indiceSalida, Ucapa, Ullegada);
                sumatoria += primerTermino * segundoTermino;
            }

            // Luego del loop, solo resta devolver el valor calculado
            return (sumatoria);
        }



        /****************************************************************/
        /* derivadaDeLaSalidaRespectoDelUmbral                          */
        /*   Calcula la derivada de una salida especifica respecto del  */
        /*   umbral solicitado.                                         */
        /*   Es llamada desde "derivadaDelErrorRespectoDelUmbral" para  */
        /*   obtener el segundo termino.                                */
        /*                                                              */
        /*   Cada salida se calcula como:                               */
        /*      Yi = Fact( U + sum( a * W ) )                           */
        /*   Donde:                                                     */
        /*      U: Es el umbral de la neurona.                          */
        /*      a: Son las salidas de las neuronas de la capa anterior. */
        /*      W: Son los pesos que ponderan las salidas de las        */
        /*          neuronas de la capa anterior.                       */
        /*      Fact(): Es la funcion de activacion de la neurona.      */
        /*          Normalmente sera una Sigmoidea o una Tangente.      */
        /*          Esto hace que se tengan que realizar las derivadas  */
        /*          mediante la regla de la cadena, debiendo derivar la */
        /*          funcion de activacion y luego las derivadas         */
        /*          parciales del argumento de la funcion de activacion.*/
        /*                                                              */
        /*   Al igual que en la funcion precedente, se opta por dividir */
        /*   el procedimiento en 2 calculos y obtener las derivadas     */
        /*   parciales mediante una funcion especifica para una mayor   */
        /*   comprension del codigo.                                    */
        /*   A su vez, tambien se plantea una funcion especifica para   */
        /*   la derivada de la funcion de activacion. Mediante esto, el */
        /*   procedimiento general sera el mismo independientemente de  */
        /*   que funcion se emplee, y se podran comparar resultados con */
        /*   mayor facilidad a futuro.                                  */
        /*                                                              */
        /* Recibe:                                                      */
        /*   salida - El numero de la salida requerida                  */
        /*   Ucapa - La capa del umbral solicitado.                     */
        /*   Ullegada - El numero de la neurona a la cual ingresa el    */
        /*              umbral.                                         */
        /* Devuelve:                                                    */
        /*   El resultado numerico del calculo.                         */
        /****************************************************************/
        private double derivadaDeLaSalidaRespectoDelUmbral(int salidaIndice, int Ucapa, int Uindice)
        {
            // Variables auxiliares para hacer mas legible el codigo
            double primerTermino;
            double segundoTermino;
            double sumatoria;
            int salidaCapa = totalDeCapas;

            // Inicializacion de las variables criticas, por las dudas
            sumatoria = 0;

            // Primero se debe revisar si el umbral es de una neurona de la ultima capa
            if ( salidaCapa == Ucapa )
            {
                // Si coinciden el indice de la salida con el del umbral, entonces la derivada no es nula
                if (salidaIndice == Uindice)
                {
                    return ( derivadaDeLaFuncionActivacion(salidaCapa, salidaIndice) );
                }
                // Si no coinciden, la derivada es cero porque la senial no depende del umbral en cuestion
                else
                {
                    return (0);
                }
            }

            // Si no coinciden, se debe realizar la derivada de la funcion de activacion por la sumatoria de las derivadas parciales de las seniales de la capa anterior
            primerTermino = derivadaDeLaFuncionActivacion(salidaCapa, salidaIndice);
            for (int indiceUmbral = 1; indiceUmbral <= neuronasPorCapa[salidaCapa - 1]; indiceUmbral++)
            {
                segundoTermino = derivadaParcialRespectoDelUmbral(salidaCapa - 1, indiceUmbral, Ucapa, Uindice);
                sumatoria += pesos[salidaCapa - 1, indiceUmbral, salidaIndice] * segundoTermino;
            }

            return (primerTermino * sumatoria);
        }



        /****************************************************************/
        /* derivadaParcialRespectoDelUmbral                             */
        /*   Realiza la derivada parcial de cualquier senial de salida  */
        /*   de una neurona respecto de cualquier umbral.               */
        /*   Es practicamente el mismo procedimiento que el de la       */
        /*   funcion "derivadaDeLaSalidaRespectoDelUmbral", salvo que   */
        /*   esa funcion se aplica solo a las salidas de las neuronas   */
        /*   de la ultima capa de la red, o sea, las salidas externas   */
        /*   de la red, mientras que esta funcion es para todas las     */
        /*   seniales internas de la red.                               */
        /*                                                              */
        /* Recibe:                                                      */
        /*   senialCapa - La capa de la senial a derivar.               */
        /*   senialNeurona - El numero de la neurona a derivar.         */
        /*   Ucapa - La capa del umbral solicitado.                     */
        /*   Ullegada - El numero de la neurona a la cual ingresa el    */
        /*              umbral.                                         */
        /* Devuelve:                                                    */
        /*   El resultado numerico del calculo.                         */
        /****************************************************************/
        private double derivadaParcialRespectoDelUmbral( int senialCapa, int senialNeurona, int Ucapa, int Uindice)
        {
            // Variables auxiliares
            double primerTermino;
            double segundoTermino;
            double sumatoria;

            // Primero se debe revisar si el umbral es de una neurona de la ultima capa
            if (senialCapa == Ucapa)
            {
                // Si coinciden el indice de la salida con el del umbral, entonces la derivada no es nula
                if (senialNeurona == Uindice)
                {
                    primerTermino = derivadaDeLaFuncionActivacion(senialCapa, senialNeurona);
                    return ( primerTermino );
                }
                // Si no coinciden, la derivada es cero porque la senial no depende del umbral en cuestion
                else
                {
                    return (0);
                }
            }

            // Si no coinciden, se debe realizar la derivada de la funcion de activacion por la sumatoria de las derivadas parciales de las seniales de la capa anterior
            primerTermino = derivadaDeLaFuncionActivacion(senialCapa, senialNeurona);
            sumatoria = 0;
            for (int indiceSenialCapaAnterior = 1; indiceSenialCapaAnterior <= neuronasPorCapa[senialCapa - 1]; indiceSenialCapaAnterior++)
            {
                segundoTermino = derivadaParcialRespectoDelUmbral(senialCapa - 1, indiceSenialCapaAnterior, Ucapa, Uindice);
                sumatoria += pesos[senialCapa - 1, indiceSenialCapaAnterior, senialNeurona] * segundoTermino;
            }

            // Luego del loop, resta multiplicar los terminos
            return (primerTermino * sumatoria);
        }



                            /*************** *************** DERIVADA DE LA FUNCION DE ACTIVACION *************** ***************/

        /****************************************************************/
        /* derivadaDeLaFuncionActivacion                                */
        /*   Realiza la derivada de la funcion de activacion para la    */
        /*   senial solicitada. Para modificar el tipo de funcion de    */
        /*   activacion utilizada se debe usar el metodo especifico.    */
        /*                                                              */
        /* Recibe:                                                      */
        /*   senialCapa - La capa de la senial a derivar.               */
        /*   senialNeurona - El numero de la neurona a derivar.         */
        /* Devuelve:                                                    */
        /*   El resultado numerico del calculo.                         */
        /****************************************************************/
        private double derivadaDeLaFuncionActivacion(int senialCapa, int senialNeurona)
        {
            switch (tipoDeActivacion)
            {
                case funcionesActivacion.FUNCION_SIGMOIDEA:
                default:
                    return ( seniales[senialCapa, senialNeurona] * ( 1 - seniales[senialCapa, senialNeurona] ) );
            }
        }



                        /*************** *************** FIN DE LA SECCION PARA EL ENTRENAMIENTO DE LA RED *************** ***************/



        /****************************************************************/
        /* evaluarFuncionDeActivacion                                   */
        /*   Recibe el resultado de la sumatoria de valores de entrada  */
        /*   a una neurona y obtiene su salida, segun el tipo de        */
        /*   funcion de activacion que se use para la red.              */
        /*                                                              */
        /* Devuelve:                                                    */
        /*   El resultado numerico del calculo.                         */
        /****************************************************************/
        private double evaluarFuncionDeActivacion(double valor)
        {
            switch (tipoDeActivacion)
            {
                case funcionesActivacion.FUNCION_SIGMOIDEA:
                default:
                    return (1 / (1 + Math.Exp(-valor)));
            }
        }



        /****************************************************************/
        /* redondearSalidas                                             */
        /*   Genera la tabla de salidas en formato 0 y 1 redondeando    */
        /*   los verdaderos valores de la red.                          */
        /*                                                              */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        private void redondearSalidas(int indiceParDeDatos)
        {
            // Se recorren las salidas reales de la red redondeando de a una
            for( int indiceSalida = 1; indiceSalida <= totalDeSalidasExternas; indiceSalida++ )
            {
                if (seniales[totalDeCapas, indiceSalida] >= 0.5)
                    salidasTabla[indiceParDeDatos,indiceSalida] = 1;
                else
                    salidasTabla[indiceParDeDatos,indiceSalida] = 0;
            }
        }





        // Metodos publicos para acceder a las variables
        string Error { get { return(error); } }

    }
}
