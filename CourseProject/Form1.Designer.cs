﻿namespace CourseProject
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
            this.out1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.numCols = new System.Windows.Forms.TextBox();
            this.numRows = new System.Windows.Forms.TextBox();
            this.buildMatrix = new System.Windows.Forms.Button();
            this.Solve = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.заповнитиТестовіЗначенняToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMatrix = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMatrix = new System.Windows.Forms.ToolStripMenuItem();
            this.saveResult = new System.Windows.Forms.ToolStripMenuItem();
            this.getDetailSolveButton = new System.Windows.Forms.ToolStripMenuItem();
            this.GraphsButton = new System.Windows.Forms.ToolStripMenuItem();
            this.TargetFunctionGraphButton = new System.Windows.Forms.ToolStripMenuItem();
            this.областьДопустимостіОптимальногоПлануToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkingButton = new System.Windows.Forms.ToolStripMenuItem();
            this.допустимостіToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оптимавльностіToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ефективностіToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.стійкостіToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpButton = new System.Windows.Forms.ToolStripMenuItem();
            this.проПрограмуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вихідToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AnswerBox = new System.Windows.Forms.TextBox();
            this.getDetailSolve = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.MaxMinBox = new System.Windows.Forms.ComboBox();
            this.AnswerRoundBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.dataGridView1.Size = new System.Drawing.Size(740, 61);
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
            this.dataGridView2.Size = new System.Drawing.Size(824, 493);
            this.dataGridView2.TabIndex = 8;
            this.toolTip1.SetToolTip(this.dataGridView2, "Введіть матрицю обмежень");
            // 
            // numCols
            // 
            this.numCols.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numCols.Location = new System.Drawing.Point(950, 27);
            this.numCols.Name = "numCols";
            this.numCols.Size = new System.Drawing.Size(23, 20);
            this.numCols.TabIndex = 10;
            this.numCols.Text = "3";
            this.toolTip1.SetToolTip(this.numCols, "Кількість колонок");
            // 
            // numRows
            // 
            this.numRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numRows.Location = new System.Drawing.Point(983, 27);
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
            this.Solve.Location = new System.Drawing.Point(836, 102);
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
            this.файлToolStripMenuItem,
            this.getDetailSolveButton,
            this.GraphsButton,
            this.checkingButton,
            this.HelpButton,
            this.проПрограмуToolStripMenuItem,
            this.вихідToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1018, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.заповнитиТестовіЗначенняToolStripMenuItem,
            this.loadMatrix,
            this.saveMatrix,
            this.saveResult});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // заповнитиТестовіЗначенняToolStripMenuItem
            // 
            this.заповнитиТестовіЗначенняToolStripMenuItem.Name = "заповнитиТестовіЗначенняToolStripMenuItem";
            this.заповнитиТестовіЗначенняToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.заповнитиТестовіЗначенняToolStripMenuItem.Text = "Заповнити тестові значення";
            this.заповнитиТестовіЗначенняToolStripMenuItem.Click += new System.EventHandler(this.insertValues_Click);
            // 
            // loadMatrix
            // 
            this.loadMatrix.Name = "loadMatrix";
            this.loadMatrix.Size = new System.Drawing.Size(250, 22);
            this.loadMatrix.Text = "Завантажити матрицю з файлу";
            this.loadMatrix.Click += new System.EventHandler(this.loadMatrix_Click);
            // 
            // saveMatrix
            // 
            this.saveMatrix.Name = "saveMatrix";
            this.saveMatrix.Size = new System.Drawing.Size(250, 22);
            this.saveMatrix.Text = "Зберегти матрицю у файл";
            this.saveMatrix.Click += new System.EventHandler(this.saveMatrix_Click);
            // 
            // saveResult
            // 
            this.saveResult.Enabled = false;
            this.saveResult.Name = "saveResult";
            this.saveResult.Size = new System.Drawing.Size(250, 22);
            this.saveResult.Text = "Зберегти результати обчислень";
            this.saveResult.Click += new System.EventHandler(this.saveResult_Click);
            // 
            // getDetailSolveButton
            // 
            this.getDetailSolveButton.Enabled = false;
            this.getDetailSolveButton.Name = "getDetailSolveButton";
            this.getDetailSolveButton.Size = new System.Drawing.Size(91, 20);
            this.getDetailSolveButton.Text = "Табуляграма";
            this.getDetailSolveButton.Click += new System.EventHandler(this.getDetailSolve_Click);
            // 
            // GraphsButton
            // 
            this.GraphsButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TargetFunctionGraphButton,
            this.областьДопустимостіОптимальногоПлануToolStripMenuItem});
            this.GraphsButton.Enabled = false;
            this.GraphsButton.Name = "GraphsButton";
            this.GraphsButton.Size = new System.Drawing.Size(63, 20);
            this.GraphsButton.Text = "Графіки";
            // 
            // TargetFunctionGraphButton
            // 
            this.TargetFunctionGraphButton.Name = "TargetFunctionGraphButton";
            this.TargetFunctionGraphButton.Size = new System.Drawing.Size(316, 22);
            this.TargetFunctionGraphButton.Text = "Зміна цільової функції";
            this.TargetFunctionGraphButton.Click += new System.EventHandler(this.TargetFunctionGraphButton_Click);
            // 
            // областьДопустимостіОптимальногоПлануToolStripMenuItem
            // 
            this.областьДопустимостіОптимальногоПлануToolStripMenuItem.Name = "областьДопустимостіОптимальногоПлануToolStripMenuItem";
            this.областьДопустимостіОптимальногоПлануToolStripMenuItem.Size = new System.Drawing.Size(316, 22);
            this.областьДопустимостіОптимальногоПлануToolStripMenuItem.Text = "Область допустимості оптимального плану";
            // 
            // checkingButton
            // 
            this.checkingButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.допустимостіToolStripMenuItem,
            this.оптимавльностіToolStripMenuItem,
            this.ефективностіToolStripMenuItem,
            this.стійкостіToolStripMenuItem});
            this.checkingButton.Enabled = false;
            this.checkingButton.Name = "checkingButton";
            this.checkingButton.Size = new System.Drawing.Size(84, 20);
            this.checkingButton.Text = "Перевірка...";
            // 
            // допустимостіToolStripMenuItem
            // 
            this.допустимостіToolStripMenuItem.Name = "допустимостіToolStripMenuItem";
            this.допустимостіToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.допустимостіToolStripMenuItem.Text = "Допустимості";
            this.допустимостіToolStripMenuItem.Click += new System.EventHandler(this.admissibilityCheck_Click);
            // 
            // оптимавльностіToolStripMenuItem
            // 
            this.оптимавльностіToolStripMenuItem.Name = "оптимавльностіToolStripMenuItem";
            this.оптимавльностіToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.оптимавльностіToolStripMenuItem.Text = "Оптимальності";
            // 
            // ефективностіToolStripMenuItem
            // 
            this.ефективностіToolStripMenuItem.Name = "ефективностіToolStripMenuItem";
            this.ефективностіToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.ефективностіToolStripMenuItem.Text = "Ефективності";
            // 
            // стійкостіToolStripMenuItem
            // 
            this.стійкостіToolStripMenuItem.Name = "стійкостіToolStripMenuItem";
            this.стійкостіToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.стійкостіToolStripMenuItem.Text = "Стійкості";
            // 
            // HelpButton
            // 
            this.HelpButton.Name = "HelpButton";
            this.HelpButton.Size = new System.Drawing.Size(61, 20);
            this.HelpButton.Text = "Довідка";
            this.HelpButton.Click += new System.EventHandler(this.HelpButton_Click);
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
            this.AnswerBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AnswerBox.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.AnswerBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AnswerBox.Location = new System.Drawing.Point(836, 151);
            this.AnswerBox.Multiline = true;
            this.AnswerBox.Name = "AnswerBox";
            this.AnswerBox.ReadOnly = true;
            this.AnswerBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.AnswerBox.Size = new System.Drawing.Size(181, 436);
            this.AnswerBox.TabIndex = 15;
            this.toolTip1.SetToolTip(this.AnswerBox, "Так, тут має бути відповідь");
            // 
            // getDetailSolve
            // 
            this.getDetailSolve.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.getDetailSolve.AutoSize = true;
            this.getDetailSolve.Checked = true;
            this.getDetailSolve.CheckState = System.Windows.Forms.CheckState.Checked;
            this.getDetailSolve.Location = new System.Drawing.Point(840, 128);
            this.getDetailSolve.Name = "getDetailSolve";
            this.getDetailSolve.Size = new System.Drawing.Size(177, 17);
            this.getDetailSolve.TabIndex = 9;
            this.getDetailSolve.Text = "Вивести детальний розв\'язок";
            this.toolTip1.SetToolTip(this.getDetailSolve, "Виводить детальний покроковий розв\'язок задачі");
            this.getDetailSolve.UseVisualStyleBackColor = true;
            // 
            // MaxMinBox
            // 
            this.MaxMinBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MaxMinBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MaxMinBox.FormattingEnabled = true;
            this.MaxMinBox.Items.AddRange(new object[] {
            "Max",
            "Min"});
            this.MaxMinBox.Location = new System.Drawing.Point(776, 50);
            this.MaxMinBox.Name = "MaxMinBox";
            this.MaxMinBox.Size = new System.Drawing.Size(54, 21);
            this.MaxMinBox.TabIndex = 17;
            this.toolTip1.SetToolTip(this.MaxMinBox, "Куди прямує функція");
            // 
            // AnswerRoundBox
            // 
            this.AnswerRoundBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AnswerRoundBox.Location = new System.Drawing.Point(965, 79);
            this.AnswerRoundBox.Name = "AnswerRoundBox";
            this.AnswerRoundBox.Size = new System.Drawing.Size(19, 20);
            this.AnswerRoundBox.TabIndex = 19;
            this.AnswerRoundBox.Text = "8";
            this.toolTip1.SetToolTip(this.AnswerRoundBox, "Точність при відображенні розв\'язку. Від 1 до 28 знаків після коми");
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(863, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Точність виводу:";
            this.toolTip1.SetToolTip(this.label3, "Точність при відображенні розв\'язку. Від 1 до 28 знаків після коми");
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(752, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "→";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(840, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Змінних/обмежень";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 599);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.AnswerRoundBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MaxMinBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AnswerBox);
            this.Controls.Add(this.Solve);
            this.Controls.Add(this.buildMatrix);
            this.Controls.Add(this.numRows);
            this.Controls.Add(this.numCols);
            this.Controls.Add(this.getDetailSolve);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.out1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Курсова";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label out1;
        private System.Windows.Forms.DataGridView dataGridView2;
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
        private System.Windows.Forms.ToolStripMenuItem HelpButton;
        private System.Windows.Forms.ToolStripMenuItem проПрограмуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkingButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox MaxMinBox;
        private System.Windows.Forms.ToolStripMenuItem оптимавльностіToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ефективностіToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem стійкостіToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem допустимостіToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadMatrix;
        private System.Windows.Forms.ToolStripMenuItem saveMatrix;
        private System.Windows.Forms.ToolStripMenuItem saveResult;
        private System.Windows.Forms.ToolStripMenuItem GraphsButton;
        private System.Windows.Forms.ToolStripMenuItem TargetFunctionGraphButton;
        private System.Windows.Forms.ToolStripMenuItem областьДопустимостіОптимальногоПлануToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem заповнитиТестовіЗначенняToolStripMenuItem;
        private System.Windows.Forms.TextBox AnswerRoundBox;
        private System.Windows.Forms.Label label3;
    }
}

