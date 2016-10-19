using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseProject
{
    class IO
    {
        /// <summary>
        /// Очищення вказаного об'єкту dataGridView
        /// </summary>
        /// <param name="dataGridView">dataGridView для очищення</param>
        private static void clearGrid(object sender, EventArgs e, DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            dataGridView.Refresh();
        }

        /// <summary>
        /// Розмітка таблиць для введення даних
        /// </summary>
        public static void buildMatrix(object sender, EventArgs e, DataGridView dataGridView1, DataGridView dataGridView2, TextBox numCols, TextBox numRows)
        {
            //Очистимо Grid
            clearGrid(sender, e, dataGridView1);
            clearGrid(sender, e, dataGridView2);

            //Кількість колонок і рядків
            int colsNum = Convert.ToInt16(numCols.Text);
            int rowsNum = Convert.ToInt16(numRows.Text);

            //Створимо таблицю для введення вектора цільової функції
            for (int i = 0; i < colsNum; i++)
            {
                int colIndex = i + 1;
                string colName = "x" + colIndex.ToString();
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
                { Name = colName, HeaderText = colName, Width = 50 });
            }
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            { HeaderText = "→", Width = 50 });
            DataGridViewComboBoxColumn comboboxColumnMaxMin = new DataGridViewComboBoxColumn();
            comboboxColumnMaxMin.Items.AddRange("max", "min");
            comboboxColumnMaxMin.HeaderText = "MaxMin";
            comboboxColumnMaxMin.Name = "MaxMin";
            comboboxColumnMaxMin.Width = 50;
            dataGridView1.Columns.Add(comboboxColumnMaxMin);

            //Створимо таблицю для введення матриці обмежень
            //Додаємо колонки для Ax
            for (int i = 0; i < colsNum; i++)
            {
                int colIndex = i + 1;
                string colName = "a" + colIndex.ToString();
                dataGridView2.Columns.Add(new DataGridViewTextBoxColumn()
                { Name = colName, HeaderText = colName, Width = 50 });
            }
            //Колонка з comboBox для знаку
            DataGridViewComboBoxColumn comboboxColumnsign = new DataGridViewComboBoxColumn();
            comboboxColumnsign.Items.AddRange("≤", "≥");
            comboboxColumnsign.HeaderText = "sign";
            comboboxColumnsign.Name = "sign";
            comboboxColumnsign.Width = 34;
            dataGridView2.Columns.Add(comboboxColumnsign);
            //Колонка для b
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn()
            { Name = "b", HeaderText = "b", Width = 50 });

            //Додаємо рядки
            dataGridView1.RowCount = 1;
            dataGridView2.RowCount = rowsNum;
        }

        /// <summary>
        /// Задання знаків у матриці обмежень
        /// </summary>
        public static void sameSign_Check(object sender, EventArgs e, DataGridView dataGridView, CheckBox sameSign)
        {
            int colsNum = dataGridView.ColumnCount;
            int rowsNum = dataGridView.RowCount;
            if (colsNum > 1)
            {
                int cellNum = dataGridView.ColumnCount - 2;
                for (int i = 1; i < rowsNum; i++)
                {
                    string signValue = null;
                    if (dataGridView.Rows[0].Cells[cellNum].Value != null)
                        signValue = dataGridView.Rows[0].Cells[cellNum].Value.ToString();
                    if (sameSign.Checked)
                    {
                        if (signValue != null)
                            dataGridView.Rows[i].Cells[cellNum].Value = signValue;
                        dataGridView.Rows[i].Cells[cellNum].ReadOnly = true;
                    }
                    else
                    {
                        dataGridView.Rows[i].Cells[cellNum].ReadOnly = false;
                    }
                }
            }
        }

        /// <summary>
        /// Отримання вектора цільової функції (масив)
        /// </summary>
        public static double[] getTargetFunction(object sender, EventArgs e, TextBox numCols, DataGridView dataGridView1)
        {
            int colsNum = Convert.ToInt16(numCols.Text);
            int colsOfFunction = colsNum - 2;
            double[] targetFunction = new double[colsOfFunction];
            for (int i = 0; i < colsOfFunction; i++)
                targetFunction[i] = Convert.ToDouble(dataGridView1.Rows[0].Cells[i].Value);
            return targetFunction;
        }

        /// <summary>
        /// Отримання матриці обмежень (масив)
        /// </summary>
        public static double[,] getLimitationMatrix(object sender, EventArgs e, TextBox numCols, TextBox numRows, DataGridView dataGridView1)
        {
            int colsNum = Convert.ToInt16(numCols.Text);
            int rowOfFunction = Convert.ToInt16(numRows.Text);
            int colsOfFunction = colsNum - 1;
            double[,] limitationMatrix = new double[rowOfFunction, colsOfFunction];
            int bIndex = rowOfFunction - 1;
            for (int i = 0; i < rowOfFunction; i++)
                limitationMatrix[i, 0] = Convert.ToDouble(dataGridView1.Rows[i].Cells[bIndex].Value);

            int jj; //для виводу значень A в масив береться індекс на 1 більше, перший займає b
            for (int i = 0; i < rowOfFunction; i++)
                for (int j = 0; j < colsOfFunction; j++)
                {
                    jj = j + 1;
                    limitationMatrix[i, jj] = Convert.ToDouble(dataGridView1.Rows[i].Cells[bIndex].Value);
                }
            return limitationMatrix;
        }

        /// <summary>
        /// Отримання значення куди прямує функція MIN/MAX
        /// </summary>
        public static bool getMaxMin(object sender, EventArgs e, DataGridView dataGridView)
        {
            //отримати індекс comboBox з dataGridView не вдалося, тому беремо його зачення
            if (dataGridView.Rows[0].Cells[dataGridView.ColumnCount - 1].Value.ToString() == "max")
                return true;
            else
                return false;
        }
    }
}
