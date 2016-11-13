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
            this.components = new System.ComponentModel.Container();
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
            this.getDetailSolveButton = new System.Windows.Forms.ToolStripMenuItem();
            this.перевіркаОптимальностіToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.дослідженняСтійкостіToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.допомогаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.проПрограмуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вихідToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AnswerBox = new System.Windows.Forms.TextBox();
            this.getDetailSolve = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // insertValues
            // 
            this.insertValues.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.insertValues.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.insertValues.Location = new System.Drawing.Point(836, 82);
            this.insertValues.Name = "insertValues";
            this.insertValues.Size = new System.Drawing.Size(181, 23);
            this.insertValues.TabIndex = 0;
            this.insertValues.Text = "Заповнити: тестові значення";
            this.toolTip1.SetToolTip(this.insertValues, "Заповнити тестовими значеннями");
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
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 27);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(829, 61);
            this.dataGridView1.TabIndex = 7;
            this.toolTip1.SetToolTip(this.dataGridView1, "Введіть цільову функцію");
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(6, 94);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(829, 493);
            this.dataGridView2.TabIndex = 8;
            this.toolTip1.SetToolTip(this.dataGridView2, "Введіть матрицю обмежень");
            // 
            // sameSign
            // 
            this.sameSign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sameSign.AutoSize = true;
            this.sameSign.Location = new System.Drawing.Point(909, 27);
            this.sameSign.Name = "sameSign";
            this.sameSign.Size = new System.Drawing.Size(109, 17);
            this.sameSign.TabIndex = 9;
            this.sameSign.Text = "Однаковий знак";
            this.toolTip1.SetToolTip(this.sameSign, "Задає однаковий знак в матриці обмежень");
            this.sameSign.UseVisualStyleBackColor = true;
            this.sameSign.Click += new System.EventHandler(this.sameSign_Click);
            // 
            // numCols
            // 
            this.numCols.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numCols.Location = new System.Drawing.Point(841, 27);
            this.numCols.Name = "numCols";
            this.numCols.Size = new System.Drawing.Size(23, 20);
            this.numCols.TabIndex = 10;
            this.numCols.Text = "3";
            this.toolTip1.SetToolTip(this.numCols, "Кількість колонок");
            // 
            // numRows
            // 
            this.numRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numRows.Location = new System.Drawing.Point(870, 27);
            this.numRows.Name = "numRows";
            this.numRows.Size = new System.Drawing.Size(23, 20);
            this.numRows.TabIndex = 11;
            this.numRows.Text = "3";
            this.toolTip1.SetToolTip(this.numRows, "Кількість рядків");
            // 
            // buildMatrix
            // 
            this.buildMatrix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buildMatrix.Location = new System.Drawing.Point(836, 53);
            this.buildMatrix.Name = "buildMatrix";
            this.buildMatrix.Size = new System.Drawing.Size(181, 23);
            this.buildMatrix.TabIndex = 12;
            this.buildMatrix.Text = "Будувати матрицю";
            this.toolTip1.SetToolTip(this.buildMatrix, "Збудувати матриці використовуючи задані параметри");
            this.buildMatrix.UseVisualStyleBackColor = true;
            this.buildMatrix.Click += new System.EventHandler(this.buildMatrix_Click);
            // 
            // Solve
            // 
            this.Solve.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Solve.Location = new System.Drawing.Point(836, 111);
            this.Solve.Name = "Solve";
            this.Solve.Size = new System.Drawing.Size(181, 23);
            this.Solve.TabIndex = 13;
            this.Solve.Text = "Розв\'язати";
            this.toolTip1.SetToolTip(this.Solve, "Отримати розв\'язок");
            this.Solve.UseVisualStyleBackColor = true;
            this.Solve.Click += new System.EventHandler(this.Solve_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getDetailSolveButton,
            this.перевіркаОптимальностіToolStripMenuItem,
            this.дослідженняСтійкостіToolStripMenuItem,
            this.допомогаToolStripMenuItem,
            this.проПрограмуToolStripMenuItem,
            this.вихідToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1018, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // getDetailSolveButton
            // 
            this.getDetailSolveButton.Enabled = false;
            this.getDetailSolveButton.Name = "getDetailSolveButton";
            this.getDetailSolveButton.Size = new System.Drawing.Size(91, 20);
            this.getDetailSolveButton.Text = "Табуляграма";
            this.getDetailSolveButton.Click += new System.EventHandler(this.getDetailSolve_Click);
            // 
            // перевіркаОптимальностіToolStripMenuItem
            // 
            this.перевіркаОптимальностіToolStripMenuItem.Name = "перевіркаОптимальностіToolStripMenuItem";
            this.перевіркаОптимальностіToolStripMenuItem.Size = new System.Drawing.Size(160, 20);
            this.перевіркаОптимальностіToolStripMenuItem.Text = "Перевірка оптимальності";
            this.перевіркаОптимальностіToolStripMenuItem.Click += new System.EventHandler(this.перевіркаОптимальностіToolStripMenuItem_Click_1);
            // 
            // дослідженняСтійкостіToolStripMenuItem
            // 
            this.дослідженняСтійкостіToolStripMenuItem.Name = "дослідженняСтійкостіToolStripMenuItem";
            this.дослідженняСтійкостіToolStripMenuItem.Size = new System.Drawing.Size(142, 20);
            this.дослідженняСтійкостіToolStripMenuItem.Text = "Дослідження стійкості";
            // 
            // допомогаToolStripMenuItem
            // 
            this.допомогаToolStripMenuItem.Name = "допомогаToolStripMenuItem";
            this.допомогаToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.допомогаToolStripMenuItem.Text = "Довідка";
            // 
            // проПрограмуToolStripMenuItem
            // 
            this.проПрограмуToolStripMenuItem.Name = "проПрограмуToolStripMenuItem";
            this.проПрограмуToolStripMenuItem.Size = new System.Drawing.Size(99, 20);
            this.проПрограмуToolStripMenuItem.Text = "Про програму";
            this.проПрограмуToolStripMenuItem.Click += new System.EventHandler(this.проПрограмуToolStripMenuItem_Click);
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
            this.AnswerBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AnswerBox.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.AnswerBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AnswerBox.Location = new System.Drawing.Point(836, 163);
            this.AnswerBox.Multiline = true;
            this.AnswerBox.Name = "AnswerBox";
            this.AnswerBox.ReadOnly = true;
            this.AnswerBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.AnswerBox.Size = new System.Drawing.Size(181, 424);
            this.AnswerBox.TabIndex = 15;
            this.toolTip1.SetToolTip(this.AnswerBox, "Так, тут має бути відповідь");
            // 
            // getDetailSolve
            // 
            this.getDetailSolve.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.getDetailSolve.AutoSize = true;
            this.getDetailSolve.Checked = true;
            this.getDetailSolve.CheckState = System.Windows.Forms.CheckState.Checked;
            this.getDetailSolve.Location = new System.Drawing.Point(840, 140);
            this.getDetailSolve.Name = "getDetailSolve";
            this.getDetailSolve.Size = new System.Drawing.Size(177, 17);
            this.getDetailSolve.TabIndex = 9;
            this.getDetailSolve.Text = "Вивести детальний розв\'язок";
            this.toolTip1.SetToolTip(this.getDetailSolve, "Виводить детальний покроковий розв\'язок задачі");
            this.getDetailSolve.UseVisualStyleBackColor = true;
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
            this.Controls.Add(this.getDetailSolve);
            this.Controls.Add(this.sameSign);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.out1);
            this.Controls.Add(this.insertValues);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.ShowIcon = false;
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
        private System.Windows.Forms.ToolStripMenuItem getDetailSolveButton;
        private System.Windows.Forms.CheckBox getDetailSolve;
        private System.Windows.Forms.ToolStripMenuItem допомогаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem проПрограмуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem перевіркаОптимальностіToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem дослідженняСтійкостіToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

