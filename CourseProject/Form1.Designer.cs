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
            this.insertValues = new System.Windows.Forms.Button();
            this.out1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.sameSign = new System.Windows.Forms.CheckBox();
            this.numCols = new System.Windows.Forms.TextBox();
            this.numRows = new System.Windows.Forms.TextBox();
            this.buildMatrix = new System.Windows.Forms.Button();
            this.Solve = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.вихідToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AnswerBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // insertValues
            // 
            this.insertValues.Location = new System.Drawing.Point(836, 94);
            this.insertValues.Name = "insertValues";
            this.insertValues.Size = new System.Drawing.Size(181, 23);
            this.insertValues.TabIndex = 0;
            this.insertValues.Text = "Заповнити матрицю";
            this.insertValues.UseVisualStyleBackColor = true;
            this.insertValues.Click += new System.EventHandler(this.insertValues_Click);
            // 
            // out1
            // 
            this.out1.AutoSize = true;
            this.out1.Location = new System.Drawing.Point(849, 562);
            this.out1.Name = "out1";
            this.out1.Size = new System.Drawing.Size(0, 13);
            this.out1.TabIndex = 6;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 27);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(824, 61);
            this.dataGridView1.TabIndex = 7;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(11, 94);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(824, 425);
            this.dataGridView2.TabIndex = 8;
            // 
            // sameSign
            // 
            this.sameSign.AutoSize = true;
            this.sameSign.Location = new System.Drawing.Point(909, 27);
            this.sameSign.Name = "sameSign";
            this.sameSign.Size = new System.Drawing.Size(109, 17);
            this.sameSign.TabIndex = 9;
            this.sameSign.Text = "Однаковий знак";
            this.sameSign.UseVisualStyleBackColor = true;
            this.sameSign.Click += new System.EventHandler(this.sameSign_Click);
            // 
            // numCols
            // 
            this.numCols.Location = new System.Drawing.Point(841, 27);
            this.numCols.Name = "numCols";
            this.numCols.Size = new System.Drawing.Size(23, 20);
            this.numCols.TabIndex = 10;
            this.numCols.Text = "3";
            // 
            // numRows
            // 
            this.numRows.Location = new System.Drawing.Point(870, 27);
            this.numRows.Name = "numRows";
            this.numRows.Size = new System.Drawing.Size(23, 20);
            this.numRows.TabIndex = 11;
            this.numRows.Text = "3";
            // 
            // buildMatrix
            // 
            this.buildMatrix.Location = new System.Drawing.Point(836, 53);
            this.buildMatrix.Name = "buildMatrix";
            this.buildMatrix.Size = new System.Drawing.Size(181, 23);
            this.buildMatrix.TabIndex = 12;
            this.buildMatrix.Text = "Будувати матрицю";
            this.buildMatrix.UseVisualStyleBackColor = true;
            this.buildMatrix.Click += new System.EventHandler(this.buildMatrix_Click);
            // 
            // Solve
            // 
            this.Solve.Location = new System.Drawing.Point(836, 123);
            this.Solve.Name = "Solve";
            this.Solve.Size = new System.Drawing.Size(181, 23);
            this.Solve.TabIndex = 13;
            this.Solve.Text = "Розв\'язати";
            this.Solve.UseVisualStyleBackColor = true;
            this.Solve.Click += new System.EventHandler(this.Solve_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вихідToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1018, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // вихідToolStripMenuItem
            // 
            this.вихідToolStripMenuItem.Name = "вихідToolStripMenuItem";
            this.вихідToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.вихідToolStripMenuItem.Text = "Вихід";
            this.вихідToolStripMenuItem.Click += new System.EventHandler(this.exit_Click);
            // 
            // AnswerBox
            // 
            this.AnswerBox.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.AnswerBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AnswerBox.Location = new System.Drawing.Point(836, 152);
            this.AnswerBox.Multiline = true;
            this.AnswerBox.Name = "AnswerBox";
            this.AnswerBox.ReadOnly = true;
            this.AnswerBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.AnswerBox.Size = new System.Drawing.Size(181, 367);
            this.AnswerBox.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 599);
            this.Controls.Add(this.AnswerBox);
            this.Controls.Add(this.Solve);
            this.Controls.Add(this.buildMatrix);
            this.Controls.Add(this.numRows);
            this.Controls.Add(this.numCols);
            this.Controls.Add(this.sameSign);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.out1);
            this.Controls.Add(this.insertValues);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button insertValues;
        private System.Windows.Forms.Label out1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.CheckBox sameSign;
        private System.Windows.Forms.TextBox numCols;
        private System.Windows.Forms.TextBox numRows;
        private System.Windows.Forms.Button buildMatrix;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button Solve;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem вихідToolStripMenuItem;
        private System.Windows.Forms.TextBox AnswerBox;
    }
}

