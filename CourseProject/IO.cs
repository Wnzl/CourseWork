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
        public static void buildMatrix(object sender, EventArgs e, DataGridView dataGridView1, DataGridView dataGridView2, int colsNum, int rowsNum)
        {
            //Очистимо Grid
            clearGrid(sender, e, dataGridView1);
            clearGrid(sender, e, dataGridView2);
            
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
        public static double[] getTargetFunction(object sender, EventArgs e, DataGridView dataGridView1)
        {
            int colsNum = dataGridView1.ColumnCount;
            int colsOfFunction = colsNum - 2;
            double[] targetFunction = new double[colsOfFunction];
            for (int i = 0; i < colsOfFunction; i++)
                targetFunction[i] = Convert.ToDouble(dataGridView1.Rows[0].Cells[i].Value);
            return targetFunction;
        }

        /// <summary>
        /// Отримання матриці обмежень (масив)
        /// </summary>
        public static double[,] getLimitationMatrix(object sender, EventArgs e, DataGridView dataGridView)
        {
            int colsNum = Convert.ToInt16(dataGridView.ColumnCount);
            int rowsNum = Convert.ToInt16(dataGridView.RowCount);
            int colsOfMassive = colsNum - 1;
            double[,] limitationMatrix = new double[rowsNum, colsOfMassive];
            //Індекс b = кількість колонок - 1
            int bIndex = colsNum - 1;

            //забираємо значення b
            for (int rowIndex = 0; rowIndex < rowsNum; rowIndex++)
                limitationMatrix[rowIndex, 0] = Convert.ToDouble(dataGridView.Rows[rowIndex].Cells[bIndex].Value);
            MessageBox.Show("0: " +limitationMatrix.GetLength(0)+ " 1: " + limitationMatrix.GetLength(1));
            //забираємо значення A
            //colMassive для виводу значень A в масив береться індекс на 1 більше, перший займає b
            //colsOfFunction - 2, адже беремо лише значення А
            for (int rowIndex = 0; rowIndex < rowsNum; rowIndex++) 
                for (int colGridIndex = 0, colIndexMassive = 1; colGridIndex < colsNum - 2; colGridIndex++, colIndexMassive++)
                {
                    limitationMatrix[rowIndex, colIndexMassive] = Convert.ToDouble(dataGridView.Rows[rowIndex].Cells[colGridIndex].Value);
                }

            /*string a = ""; //це було для перевірки масиву
            for (int row = 0; row < limitationMatrix.GetLength(0); row++)
            {
                for (int col = 0; col < limitationMatrix.GetLength(1); col++)
                    a += limitationMatrix[row, col].ToString() + " ";
                a += "\n";
            }
            MessageBox.Show(a);*/
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
