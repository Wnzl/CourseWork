namespace CourseProject
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
            this.button1 = new System.Windows.Forms.Button();
            this.out1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.sameSign = new System.Windows.Forms.CheckBox();
            this.numCols = new System.Windows.Forms.TextBox();
            this.numRows = new System.Windows.Forms.TextBox();
            this.buildMatrix = new System.Windows.Forms.Button();
            this.tryBTN = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 381);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(190, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "НАЙТИ КОЭФФИЦИЕНТЫ!1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // out1
            // 
            this.out1.AutoSize = true;
            this.out1.Location = new System.Drawing.Point(9, 407);
            this.out1.Name = "out1";
            this.out1.Size = new System.Drawing.Size(0, 13);
            this.out1.TabIndex = 6;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 13);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(658, 61);
            this.dataGridView1.TabIndex = 7;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(12, 80);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(658, 159);
            this.dataGridView2.TabIndex = 8;
            // 
            // sameSign
            // 
            this.sameSign.AutoSize = true;
            this.sameSign.Location = new System.Drawing.Point(749, 13);
            this.sameSign.Name = "sameSign";
            this.sameSign.Size = new System.Drawing.Size(109, 17);
            this.sameSign.TabIndex = 9;
            this.sameSign.Text = "Однаковий знак";
            this.sameSign.UseVisualStyleBackColor = true;
            this.sameSign.Click += new System.EventHandler(this.sameSign_Click);
            // 
            // numCols
            // 
            this.numCols.Location = new System.Drawing.Point(681, 13);
            this.numCols.Name = "numCols";
            this.numCols.Size = new System.Drawing.Size(23, 20);
            this.numCols.TabIndex = 10;
            this.numCols.Text = "3";
            // 
            // numRows
            // 
            this.numRows.Location = new System.Drawing.Point(710, 13);
            this.numRows.Name = "numRows";
            this.numRows.Size = new System.Drawing.Size(23, 20);
            this.numRows.TabIndex = 11;
            this.numRows.Text = "3";
            // 
            // buildMatrix
            // 
            this.buildMatrix.Location = new System.Drawing.Point(681, 39);
            this.buildMatrix.Name = "buildMatrix";
            this.buildMatrix.Size = new System.Drawing.Size(177, 23);
            this.buildMatrix.TabIndex = 12;
            this.buildMatrix.Text = "Будувати матрицю";
            this.buildMatrix.UseVisualStyleBackColor = true;
            this.buildMatrix.Click += new System.EventHandler(this.buildMatrix_Click);
            // 
            // tryBTN
            // 
            this.tryBTN.Location = new System.Drawing.Point(681, 68);
            this.tryBTN.Name = "tryBTN";
            this.tryBTN.Size = new System.Drawing.Size(52, 23);
            this.tryBTN.TabIndex = 13;
            this.tryBTN.Text = "try";
            this.tryBTN.UseVisualStyleBackColor = true;
            this.tryBTN.Click += new System.EventHandler(this.tryBTN_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 473);
            this.Controls.Add(this.tryBTN);
            this.Controls.Add(this.buildMatrix);
            this.Controls.Add(this.numRows);
            this.Controls.Add(this.numCols);
            this.Controls.Add(this.sameSign);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.out1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label out1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.CheckBox sameSign;
        private System.Windows.Forms.TextBox numCols;
        private System.Windows.Forms.TextBox numRows;
        private System.Windows.Forms.Button buildMatrix;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button tryBTN;
    }
}

