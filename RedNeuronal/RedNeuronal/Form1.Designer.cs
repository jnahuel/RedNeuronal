namespace RedNeuronal
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tablaEntradas = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnNuevaRed = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnTomarPares = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numSalidas = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numCantidadDePares = new System.Windows.Forms.NumericUpDown();
            this.numEntradas = new System.Windows.Forms.NumericUpDown();
            this.tablaSalidas = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.numIteraciones = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numCoeficiente = new System.Windows.Forms.NumericUpDown();
            this.btnEntrenar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chekImponer = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnGuardarRed = new System.Windows.Forms.Button();
            this.tbxPath = new System.Windows.Forms.TextBox();
            this.btnAbrir = new System.Windows.Forms.Button();
            this.gbxSalidasEntrenadas = new System.Windows.Forms.GroupBox();
            this.chekSalidasReales = new System.Windows.Forms.CheckBox();
            this.lblAciertos = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnBorrar = new System.Windows.Forms.Button();
            this.tablaSalidasEntrenadas = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numNeuronasPorCapa = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numCapasOcultas = new System.Windows.Forms.NumericUpDown();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnVerificacionBorrar = new System.Windows.Forms.Button();
            this.btnVerificacionCalcular = new System.Windows.Forms.Button();
            this.tablaVerificacionSalidas = new System.Windows.Forms.DataGridView();
            this.tablaVerificacionEntradas = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.tablaEntradas)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSalidas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidadDePares)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEntradas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablaSalidas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIteraciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCoeficiente)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.gbxSalidasEntrenadas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablaSalidasEntrenadas)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNeuronasPorCapa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCapasOcultas)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablaVerificacionSalidas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablaVerificacionEntradas)).BeginInit();
            this.SuspendLayout();
            // 
            // tablaEntradas
            // 
            this.tablaEntradas.AllowUserToAddRows = false;
            this.tablaEntradas.AllowUserToDeleteRows = false;
            this.tablaEntradas.AllowUserToResizeColumns = false;
            this.tablaEntradas.AllowUserToResizeRows = false;
            this.tablaEntradas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tablaEntradas.Location = new System.Drawing.Point(15, 19);
            this.tablaEntradas.Name = "tablaEntradas";
            this.tablaEntradas.Size = new System.Drawing.Size(361, 176);
            this.tablaEntradas.TabIndex = 0;
            this.tablaEntradas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tablaEntradas_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnNuevaRed);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnTomarPares);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numSalidas);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numCantidadDePares);
            this.groupBox1.Controls.Add(this.numEntradas);
            this.groupBox1.Controls.Add(this.tablaSalidas);
            this.groupBox1.Controls.Add(this.tablaEntradas);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(657, 278);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Entradas y salidas esperadas";
            // 
            // btnNuevaRed
            // 
            this.btnNuevaRed.Location = new System.Drawing.Point(402, 241);
            this.btnNuevaRed.Name = "btnNuevaRed";
            this.btnNuevaRed.Size = new System.Drawing.Size(154, 23);
            this.btnNuevaRed.TabIndex = 14;
            this.btnNuevaRed.Text = "Nueva red";
            this.btnNuevaRed.UseVisualStyleBackColor = true;
            this.btnNuevaRed.Click += new System.EventHandler(this.btnNuevaRed_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(463, 213);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Cantidad de pares";
            // 
            // btnTomarPares
            // 
            this.btnTomarPares.Location = new System.Drawing.Point(150, 241);
            this.btnTomarPares.Name = "btnTomarPares";
            this.btnTomarPares.Size = new System.Drawing.Size(154, 23);
            this.btnTomarPares.TabIndex = 11;
            this.btnTomarPares.Text = "Tomar pares";
            this.btnTomarPares.UseVisualStyleBackColor = true;
            this.btnTomarPares.Click += new System.EventHandler(this.btnTomarPares_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(230, 213);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Cantidad de salidas";
            // 
            // numSalidas
            // 
            this.numSalidas.Location = new System.Drawing.Point(353, 211);
            this.numSalidas.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numSalidas.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSalidas.Name = "numSalidas";
            this.numSalidas.Size = new System.Drawing.Size(58, 20);
            this.numSalidas.TabIndex = 5;
            this.numSalidas.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSalidas.ValueChanged += new System.EventHandler(this.numSalidas_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 213);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Cantidad de entradas";
            // 
            // numCantidadDePares
            // 
            this.numCantidadDePares.Location = new System.Drawing.Point(562, 211);
            this.numCantidadDePares.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numCantidadDePares.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCantidadDePares.Name = "numCantidadDePares";
            this.numCantidadDePares.Size = new System.Drawing.Size(58, 20);
            this.numCantidadDePares.TabIndex = 12;
            this.numCantidadDePares.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCantidadDePares.ValueChanged += new System.EventHandler(this.numCantidadDePares_ValueChanged);
            // 
            // numEntradas
            // 
            this.numEntradas.Location = new System.Drawing.Point(135, 211);
            this.numEntradas.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numEntradas.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numEntradas.Name = "numEntradas";
            this.numEntradas.Size = new System.Drawing.Size(58, 20);
            this.numEntradas.TabIndex = 3;
            this.numEntradas.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numEntradas.ValueChanged += new System.EventHandler(this.numEntradas_ValueChanged);
            // 
            // tablaSalidas
            // 
            this.tablaSalidas.AllowUserToAddRows = false;
            this.tablaSalidas.AllowUserToDeleteRows = false;
            this.tablaSalidas.AllowUserToResizeColumns = false;
            this.tablaSalidas.AllowUserToResizeRows = false;
            this.tablaSalidas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tablaSalidas.Location = new System.Drawing.Point(382, 19);
            this.tablaSalidas.Name = "tablaSalidas";
            this.tablaSalidas.Size = new System.Drawing.Size(262, 176);
            this.tablaSalidas.TabIndex = 1;
            this.tablaSalidas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tablaSalidas_CellClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Cantidad de iteraciones";
            // 
            // numIteraciones
            // 
            this.numIteraciones.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numIteraciones.Location = new System.Drawing.Point(150, 22);
            this.numIteraciones.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numIteraciones.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numIteraciones.Name = "numIteraciones";
            this.numIteraciones.Size = new System.Drawing.Size(58, 20);
            this.numIteraciones.TabIndex = 9;
            this.numIteraciones.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Coeficiente de aprendizaje";
            // 
            // numCoeficiente
            // 
            this.numCoeficiente.DecimalPlaces = 1;
            this.numCoeficiente.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numCoeficiente.Location = new System.Drawing.Point(150, 56);
            this.numCoeficiente.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCoeficiente.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numCoeficiente.Name = "numCoeficiente";
            this.numCoeficiente.Size = new System.Drawing.Size(58, 20);
            this.numCoeficiente.TabIndex = 7;
            this.numCoeficiente.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnEntrenar
            // 
            this.btnEntrenar.Location = new System.Drawing.Point(6, 208);
            this.btnEntrenar.Name = "btnEntrenar";
            this.btnEntrenar.Size = new System.Drawing.Size(85, 23);
            this.btnEntrenar.TabIndex = 2;
            this.btnEntrenar.Text = "Entrenar";
            this.btnEntrenar.UseVisualStyleBackColor = true;
            this.btnEntrenar.Click += new System.EventHandler(this.btnEntrenar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chekImponer);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.btnGuardarRed);
            this.groupBox2.Controls.Add(this.tbxPath);
            this.groupBox2.Controls.Add(this.btnAbrir);
            this.groupBox2.Location = new System.Drawing.Point(515, 296);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(436, 94);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Archivo";
            // 
            // chekImponer
            // 
            this.chekImponer.AutoSize = true;
            this.chekImponer.Location = new System.Drawing.Point(300, 23);
            this.chekImponer.Name = "chekImponer";
            this.chekImponer.Size = new System.Drawing.Size(119, 17);
            this.chekImponer.TabIndex = 4;
            this.chekImponer.Text = "Imponer parametros";
            this.chekImponer.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(96, 55);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(331, 20);
            this.textBox1.TabIndex = 3;
            // 
            // btnGuardarRed
            // 
            this.btnGuardarRed.Location = new System.Drawing.Point(15, 53);
            this.btnGuardarRed.Name = "btnGuardarRed";
            this.btnGuardarRed.Size = new System.Drawing.Size(75, 23);
            this.btnGuardarRed.TabIndex = 2;
            this.btnGuardarRed.Text = "Guardar";
            this.btnGuardarRed.UseVisualStyleBackColor = true;
            this.btnGuardarRed.Click += new System.EventHandler(this.btnGuardarRed_Click);
            // 
            // tbxPath
            // 
            this.tbxPath.Location = new System.Drawing.Point(96, 21);
            this.tbxPath.Name = "tbxPath";
            this.tbxPath.Size = new System.Drawing.Size(180, 20);
            this.tbxPath.TabIndex = 1;
            // 
            // btnAbrir
            // 
            this.btnAbrir.Location = new System.Drawing.Point(15, 19);
            this.btnAbrir.Name = "btnAbrir";
            this.btnAbrir.Size = new System.Drawing.Size(75, 23);
            this.btnAbrir.TabIndex = 0;
            this.btnAbrir.Text = "Abrir";
            this.btnAbrir.UseVisualStyleBackColor = true;
            this.btnAbrir.Click += new System.EventHandler(this.btnAbrir_Click);
            // 
            // gbxSalidasEntrenadas
            // 
            this.gbxSalidasEntrenadas.Controls.Add(this.chekSalidasReales);
            this.gbxSalidasEntrenadas.Controls.Add(this.lblAciertos);
            this.gbxSalidasEntrenadas.Controls.Add(this.label6);
            this.gbxSalidasEntrenadas.Controls.Add(this.btnBorrar);
            this.gbxSalidasEntrenadas.Controls.Add(this.tablaSalidasEntrenadas);
            this.gbxSalidasEntrenadas.Controls.Add(this.btnEntrenar);
            this.gbxSalidasEntrenadas.Location = new System.Drawing.Point(675, 12);
            this.gbxSalidasEntrenadas.Name = "gbxSalidasEntrenadas";
            this.gbxSalidasEntrenadas.Size = new System.Drawing.Size(276, 278);
            this.gbxSalidasEntrenadas.TabIndex = 4;
            this.gbxSalidasEntrenadas.TabStop = false;
            this.gbxSalidasEntrenadas.Text = "Salidas entrenadas";
            // 
            // chekSalidasReales
            // 
            this.chekSalidasReales.AutoSize = true;
            this.chekSalidasReales.Location = new System.Drawing.Point(59, 245);
            this.chekSalidasReales.Name = "chekSalidasReales";
            this.chekSalidasReales.Size = new System.Drawing.Size(91, 17);
            this.chekSalidasReales.TabIndex = 6;
            this.chekSalidasReales.Text = "Salidas reales";
            this.chekSalidasReales.UseVisualStyleBackColor = true;
            this.chekSalidasReales.CheckedChanged += new System.EventHandler(this.chekSalidasReales_CheckedChanged);
            // 
            // lblAciertos
            // 
            this.lblAciertos.AutoSize = true;
            this.lblAciertos.Location = new System.Drawing.Point(228, 241);
            this.lblAciertos.Name = "lblAciertos";
            this.lblAciertos.Size = new System.Drawing.Size(10, 13);
            this.lblAciertos.TabIndex = 5;
            this.lblAciertos.Text = "-";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(214, 213);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Aciertos";
            // 
            // btnBorrar
            // 
            this.btnBorrar.Location = new System.Drawing.Point(104, 208);
            this.btnBorrar.Name = "btnBorrar";
            this.btnBorrar.Size = new System.Drawing.Size(85, 23);
            this.btnBorrar.TabIndex = 3;
            this.btnBorrar.Text = "Borrar";
            this.btnBorrar.UseVisualStyleBackColor = true;
            this.btnBorrar.Click += new System.EventHandler(this.btnBorrar_Click);
            // 
            // tablaSalidasEntrenadas
            // 
            this.tablaSalidasEntrenadas.AllowUserToAddRows = false;
            this.tablaSalidasEntrenadas.AllowUserToDeleteRows = false;
            this.tablaSalidasEntrenadas.AllowUserToResizeColumns = false;
            this.tablaSalidasEntrenadas.AllowUserToResizeRows = false;
            this.tablaSalidasEntrenadas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tablaSalidasEntrenadas.Location = new System.Drawing.Point(6, 19);
            this.tablaSalidasEntrenadas.Name = "tablaSalidasEntrenadas";
            dataGridViewCellStyle1.Format = "N4";
            dataGridViewCellStyle1.NullValue = null;
            this.tablaSalidasEntrenadas.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.tablaSalidasEntrenadas.Size = new System.Drawing.Size(262, 176);
            this.tablaSalidasEntrenadas.TabIndex = 2;
            this.tablaSalidasEntrenadas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tablaSalidasEntrenadas_CellClick);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.numNeuronasPorCapa);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.numCapasOcultas);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.numCoeficiente);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.numIteraciones);
            this.groupBox4.Location = new System.Drawing.Point(12, 296);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(479, 94);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Parametros de la red";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(254, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(122, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Coeficiente de neuronas";
            // 
            // numNeuronasPorCapa
            // 
            this.numNeuronasPorCapa.Location = new System.Drawing.Point(392, 56);
            this.numNeuronasPorCapa.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numNeuronasPorCapa.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numNeuronasPorCapa.Name = "numNeuronasPorCapa";
            this.numNeuronasPorCapa.Size = new System.Drawing.Size(58, 20);
            this.numNeuronasPorCapa.TabIndex = 11;
            this.numNeuronasPorCapa.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numNeuronasPorCapa.ValueChanged += new System.EventHandler(this.numNeuronasPorCapa_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(254, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(133, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Cantidad de capas ocultas";
            // 
            // numCapasOcultas
            // 
            this.numCapasOcultas.Location = new System.Drawing.Point(392, 22);
            this.numCapasOcultas.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numCapasOcultas.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCapasOcultas.Name = "numCapasOcultas";
            this.numCapasOcultas.Size = new System.Drawing.Size(58, 20);
            this.numCapasOcultas.TabIndex = 13;
            this.numCapasOcultas.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numCapasOcultas.ValueChanged += new System.EventHandler(this.numCapasOcultas_ValueChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnVerificacionBorrar);
            this.groupBox5.Controls.Add(this.btnVerificacionCalcular);
            this.groupBox5.Controls.Add(this.tablaVerificacionSalidas);
            this.groupBox5.Controls.Add(this.tablaVerificacionEntradas);
            this.groupBox5.Location = new System.Drawing.Point(12, 396);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(939, 114);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Verificacion";
            // 
            // btnVerificacionBorrar
            // 
            this.btnVerificacionBorrar.Location = new System.Drawing.Point(816, 60);
            this.btnVerificacionBorrar.Name = "btnVerificacionBorrar";
            this.btnVerificacionBorrar.Size = new System.Drawing.Size(85, 23);
            this.btnVerificacionBorrar.TabIndex = 5;
            this.btnVerificacionBorrar.Text = "Borrar";
            this.btnVerificacionBorrar.UseVisualStyleBackColor = true;
            this.btnVerificacionBorrar.Click += new System.EventHandler(this.btnVerificacionBorrar_Click);
            // 
            // btnVerificacionCalcular
            // 
            this.btnVerificacionCalcular.Location = new System.Drawing.Point(701, 60);
            this.btnVerificacionCalcular.Name = "btnVerificacionCalcular";
            this.btnVerificacionCalcular.Size = new System.Drawing.Size(85, 23);
            this.btnVerificacionCalcular.TabIndex = 4;
            this.btnVerificacionCalcular.Text = "Calcular";
            this.btnVerificacionCalcular.UseVisualStyleBackColor = true;
            this.btnVerificacionCalcular.Click += new System.EventHandler(this.btnVerificacionCalcular_Click);
            // 
            // tablaVerificacionSalidas
            // 
            this.tablaVerificacionSalidas.AllowUserToAddRows = false;
            this.tablaVerificacionSalidas.AllowUserToDeleteRows = false;
            this.tablaVerificacionSalidas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tablaVerificacionSalidas.Location = new System.Drawing.Point(392, 29);
            this.tablaVerificacionSalidas.Name = "tablaVerificacionSalidas";
            this.tablaVerificacionSalidas.Size = new System.Drawing.Size(252, 76);
            this.tablaVerificacionSalidas.TabIndex = 1;
            // 
            // tablaVerificacionEntradas
            // 
            this.tablaVerificacionEntradas.AllowUserToAddRows = false;
            this.tablaVerificacionEntradas.AllowUserToDeleteRows = false;
            this.tablaVerificacionEntradas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tablaVerificacionEntradas.Location = new System.Drawing.Point(15, 29);
            this.tablaVerificacionEntradas.Name = "tablaVerificacionEntradas";
            this.tablaVerificacionEntradas.Size = new System.Drawing.Size(361, 76);
            this.tablaVerificacionEntradas.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 521);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.gbxSalidasEntrenadas);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Red neuronal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.tablaEntradas)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSalidas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidadDePares)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEntradas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablaSalidas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIteraciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCoeficiente)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbxSalidasEntrenadas.ResumeLayout(false);
            this.gbxSalidasEntrenadas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablaSalidasEntrenadas)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNeuronasPorCapa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCapasOcultas)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablaVerificacionSalidas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablaVerificacionEntradas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView tablaEntradas;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numIteraciones;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numCoeficiente;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numSalidas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numEntradas;
        private System.Windows.Forms.Button btnEntrenar;
        private System.Windows.Forms.DataGridView tablaSalidas;
        private System.Windows.Forms.Button btnTomarPares;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numCantidadDePares;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbxPath;
        private System.Windows.Forms.Button btnAbrir;
        private System.Windows.Forms.GroupBox gbxSalidasEntrenadas;
        private System.Windows.Forms.DataGridView tablaSalidasEntrenadas;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnGuardarRed;
        private System.Windows.Forms.Label lblAciertos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnBorrar;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numNeuronasPorCapa;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numCapasOcultas;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView tablaVerificacionSalidas;
        private System.Windows.Forms.DataGridView tablaVerificacionEntradas;
        private System.Windows.Forms.Button btnVerificacionBorrar;
        private System.Windows.Forms.Button btnVerificacionCalcular;
        private System.Windows.Forms.Button btnNuevaRed;
        private System.Windows.Forms.CheckBox chekImponer;
        private System.Windows.Forms.CheckBox chekSalidasReales;

    }
}

