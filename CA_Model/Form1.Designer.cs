namespace CATest
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.startButton = new System.Windows.Forms.Button();
            this.grainBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveBitMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTxtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importBitMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importTxtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.numericUpDownX = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownY = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.inclustionType = new System.Windows.Forms.ComboBox();
            this.numericUpDownIncl = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownSize = new System.Windows.Forms.NumericUpDown();
            this.inclusionCheckbox = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDownProb = new System.Windows.Forms.NumericUpDown();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.borderLabel = new System.Windows.Forms.Label();
            this.methodBox = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.grainCheckbox = new System.Windows.Forms.CheckBox();
            this.grainInc = new System.Windows.Forms.NumericUpDown();
            this.grainBegin = new System.Windows.Forms.NumericUpDown();
            this.grainEnergy = new System.Windows.Forms.NumericUpDown();
            this.boundaryEnergy = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIncl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grainInc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grainBegin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grainEnergy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.boundaryEnergy)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(300, 300);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(710, 389);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "nucleating";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.Button1_Click);
            // 
            // grainBox
            // 
            this.grainBox.Location = new System.Drawing.Point(711, 169);
            this.grainBox.Name = "grainBox";
            this.grainBox.Size = new System.Drawing.Size(120, 20);
            this.grainBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(708, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "No. of grains/states";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(791, 389);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 52);
            this.button1.TabIndex = 8;
            this.button1.Text = "clear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(710, 418);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "growth";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1096, 25);
            this.toolStrip1.TabIndex = 12;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveBitMapToolStripMenuItem,
            this.saveTxtToolStripMenuItem,
            this.importBitMapToolStripMenuItem,
            this.importTxtToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveBitMapToolStripMenuItem
            // 
            this.saveBitMapToolStripMenuItem.Name = "saveBitMapToolStripMenuItem";
            this.saveBitMapToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.saveBitMapToolStripMenuItem.Text = "Save BitMap";
            this.saveBitMapToolStripMenuItem.Click += new System.EventHandler(this.saveBitMapToolStripMenuItem_Click);
            // 
            // saveTxtToolStripMenuItem
            // 
            this.saveTxtToolStripMenuItem.Name = "saveTxtToolStripMenuItem";
            this.saveTxtToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.saveTxtToolStripMenuItem.Text = "Save Txt";
            this.saveTxtToolStripMenuItem.Click += new System.EventHandler(this.saveTxtToolStripMenuItem_Click);
            // 
            // importBitMapToolStripMenuItem
            // 
            this.importBitMapToolStripMenuItem.Name = "importBitMapToolStripMenuItem";
            this.importBitMapToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.importBitMapToolStripMenuItem.Text = "Import BitMap";
            this.importBitMapToolStripMenuItem.Click += new System.EventHandler(this.importBitMapToolStripMenuItem_Click);
            // 
            // importTxtToolStripMenuItem
            // 
            this.importTxtToolStripMenuItem.Name = "importTxtToolStripMenuItem";
            this.importTxtToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.importTxtToolStripMenuItem.Text = "Import Txt";
            this.importTxtToolStripMenuItem.Click += new System.EventHandler(this.importTxtToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(708, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Grid size X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(708, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Grid size Y";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(842, 57);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(103, 89);
            this.button3.TabIndex = 17;
            this.button3.Text = "set size";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // numericUpDownX
            // 
            this.numericUpDownX.Location = new System.Drawing.Point(711, 57);
            this.numericUpDownX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownX.Name = "numericUpDownX";
            this.numericUpDownX.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownX.TabIndex = 18;
            this.numericUpDownX.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            // 
            // numericUpDownY
            // 
            this.numericUpDownY.Location = new System.Drawing.Point(711, 126);
            this.numericUpDownY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownY.Name = "numericUpDownY";
            this.numericUpDownY.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownY.TabIndex = 19;
            this.numericUpDownY.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(12, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(693, 401);
            this.panel1.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(843, 325);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(708, 238);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Inclusion type";
            // 
            // inclustionType
            // 
            this.inclustionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.inclustionType.FormattingEnabled = true;
            this.inclustionType.Items.AddRange(new object[] {
            "square",
            "circular"});
            this.inclustionType.Location = new System.Drawing.Point(711, 254);
            this.inclustionType.Name = "inclustionType";
            this.inclustionType.Size = new System.Drawing.Size(121, 21);
            this.inclustionType.TabIndex = 23;
            // 
            // numericUpDownIncl
            // 
            this.numericUpDownIncl.Location = new System.Drawing.Point(712, 306);
            this.numericUpDownIncl.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownIncl.Name = "numericUpDownIncl";
            this.numericUpDownIncl.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownIncl.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(708, 290);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "No. of inclusions";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(841, 290);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "Radius / Square size";
            // 
            // numericUpDownSize
            // 
            this.numericUpDownSize.Location = new System.Drawing.Point(843, 306);
            this.numericUpDownSize.Name = "numericUpDownSize";
            this.numericUpDownSize.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownSize.TabIndex = 27;
            // 
            // inclusionCheckbox
            // 
            this.inclusionCheckbox.Location = new System.Drawing.Point(842, 254);
            this.inclusionCheckbox.Name = "inclusionCheckbox";
            this.inclusionCheckbox.Size = new System.Drawing.Size(145, 17);
            this.inclusionCheckbox.TabIndex = 28;
            this.inclusionCheckbox.Text = "Inclusion after generation";
            this.inclusionCheckbox.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(708, 193);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Moore Probability [%]";
            // 
            // numericUpDownProb
            // 
            this.numericUpDownProb.Location = new System.Drawing.Point(711, 210);
            this.numericUpDownProb.Name = "numericUpDownProb";
            this.numericUpDownProb.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownProb.TabIndex = 30;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(880, 389);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(81, 52);
            this.button4.TabIndex = 31;
            this.button4.Text = "set phase 1";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(967, 389);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(89, 23);
            this.button5.TabIndex = 32;
            this.button5.Text = "draw all borders";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(967, 418);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(89, 23);
            this.button6.TabIndex = 33;
            this.button6.Text = "clear space";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(957, 57);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 13);
            this.label9.TabIndex = 34;
            this.label9.Text = "Border %:";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // borderLabel
            // 
            this.borderLabel.AutoSize = true;
            this.borderLabel.Location = new System.Drawing.Point(1020, 57);
            this.borderLabel.Name = "borderLabel";
            this.borderLabel.Size = new System.Drawing.Size(0, 13);
            this.borderLabel.TabIndex = 35;
            // 
            // methodBox
            // 
            this.methodBox.FormattingEnabled = true;
            this.methodBox.Items.AddRange(new object[] {
            "CA",
            "Monte Carlo"});
            this.methodBox.Location = new System.Drawing.Point(842, 168);
            this.methodBox.Name = "methodBox";
            this.methodBox.Size = new System.Drawing.Size(121, 21);
            this.methodBox.TabIndex = 36;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(839, 152);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 13);
            this.label10.TabIndex = 37;
            this.label10.Text = "Generation Method";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(711, 456);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(345, 37);
            this.button7.TabIndex = 39;
            this.button7.Text = "show energy";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(711, 527);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(345, 39);
            this.button8.TabIndex = 40;
            this.button8.Text = "recrystalyze";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 508);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 13);
            this.label11.TabIndex = 41;
            this.label11.Text = "Beginning grain no.";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(201, 508);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(99, 13);
            this.label12.TabIndex = 42;
            this.label12.Text = "Grain no. increment";
            // 
            // grainCheckbox
            // 
            this.grainCheckbox.AutoSize = true;
            this.grainCheckbox.Location = new System.Drawing.Point(373, 520);
            this.grainCheckbox.Name = "grainCheckbox";
            this.grainCheckbox.Size = new System.Drawing.Size(118, 17);
            this.grainCheckbox.TabIndex = 43;
            this.grainCheckbox.Text = "Constant Increment";
            this.grainCheckbox.UseVisualStyleBackColor = true;
            // 
            // grainInc
            // 
            this.grainInc.Location = new System.Drawing.Point(204, 527);
            this.grainInc.Name = "grainInc";
            this.grainInc.Size = new System.Drawing.Size(120, 20);
            this.grainInc.TabIndex = 44;
            // 
            // grainBegin
            // 
            this.grainBegin.Location = new System.Drawing.Point(16, 527);
            this.grainBegin.Name = "grainBegin";
            this.grainBegin.Size = new System.Drawing.Size(120, 20);
            this.grainBegin.TabIndex = 45;
            // 
            // grainEnergy
            // 
            this.grainEnergy.DecimalPlaces = 2;
            this.grainEnergy.Location = new System.Drawing.Point(712, 363);
            this.grainEnergy.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.grainEnergy.Name = "grainEnergy";
            this.grainEnergy.Size = new System.Drawing.Size(120, 20);
            this.grainEnergy.TabIndex = 46;
            this.grainEnergy.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // boundaryEnergy
            // 
            this.boundaryEnergy.DecimalPlaces = 2;
            this.boundaryEnergy.Location = new System.Drawing.Point(846, 363);
            this.boundaryEnergy.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.boundaryEnergy.Name = "boundaryEnergy";
            this.boundaryEnergy.Size = new System.Drawing.Size(120, 20);
            this.boundaryEnergy.TabIndex = 1;
            this.boundaryEnergy.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(711, 344);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 13);
            this.label13.TabIndex = 47;
            this.label13.Text = "Grain Energy";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(843, 344);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(88, 13);
            this.label14.TabIndex = 48;
            this.label14.Text = "Boundary Energy";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 580);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.boundaryEnergy);
            this.Controls.Add(this.grainEnergy);
            this.Controls.Add(this.grainBegin);
            this.Controls.Add(this.grainInc);
            this.Controls.Add(this.grainCheckbox);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.methodBox);
            this.Controls.Add(this.borderLabel);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.numericUpDownProb);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.inclusionCheckbox);
            this.Controls.Add(this.numericUpDownSize);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numericUpDownIncl);
            this.Controls.Add(this.inclustionType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.numericUpDownY);
            this.Controls.Add(this.numericUpDownX);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.grainBox);
            this.Controls.Add(this.startButton);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIncl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grainInc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grainBegin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grainEnergy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.boundaryEnergy)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.TextBox grainBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveBitMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveTxtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importBitMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importTxtToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.NumericUpDown numericUpDownX;
        private System.Windows.Forms.NumericUpDown numericUpDownY;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox inclustionType;
        private System.Windows.Forms.NumericUpDown numericUpDownIncl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDownSize;
        private System.Windows.Forms.CheckBox inclusionCheckbox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDownProb;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label borderLabel;
        private System.Windows.Forms.ComboBox methodBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox grainCheckbox;
        private System.Windows.Forms.NumericUpDown grainInc;
        private System.Windows.Forms.NumericUpDown grainBegin;
        private System.Windows.Forms.NumericUpDown grainEnergy;
        private System.Windows.Forms.NumericUpDown boundaryEnergy;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
    }
}

