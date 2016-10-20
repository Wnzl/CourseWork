using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CourseProject {

    /// <summary>
    /// Метод последовательного улучшения плана (1 алгоритм)
    /// </summary>
    class SimplexMethod {
        /// <summary>
        /// Метод нахождения оптимального плана методом последовательного улучшения плана (1 алгоритм)
        /// </summary>
        /// <param name="AFirst">Матрица ограничений</param>
        /// <param name="CFirst">Вектор коэффициентов целевой функции</param>
        public static SimplexTable[] solve(double[,] AFirst, double[] CFirst) {
            
            //матрица ограничений с учётом добавленных переменных 
            double[,] A = modifyA(AFirst);
            //коэффициенты целевой функции с учетом добавленных переменных
            double[] C = modifyC(CFirst, AFirst);
            //коэффициенты базиса
            int[] fs = referenceBasis(AFirst);
            int rows = A.GetLength(0);                               //Получаем количество строк матрицы ограничений
            for (int i = 0; i < rows; i++)
                A[i, 0] = fs[i];                                     //Записываем в каждый 0-ой элемент строки индекс А
            SimplexTable[] resultMassive = new SimplexTable[0];      //Создаем массив симплекс таблиц
            resultMassive = optimalityCheck(A, C, fs, resultMassive);//Определяем массив симплекс таблиц для всех итераций
            return resultMassive;
        }

        /// <summary>
        /// Метод проверки текущего базиса на оптимальность, выполняется до тех пор пока не 
        /// будет найдено решение либо не будет определено, что задача не имеет решения
        /// </summary>
        /// <param name="X">Матрица ограничений, где [i,0] это индекс А, а [i,1] значение вектора b</param>
        /// <param name="C">Вектор коэффициентов целевой функции</param>
        /// <param name="fs">Коэффициенты начального базиса</param>
        private static SimplexTable[] optimalityCheck(double[,] X, double[] C, int[] fs, SimplexTable[] oldMassive) {
            SimplexTable[] resultMassive = new SimplexTable[oldMassive.GetLength(0) + 1]; //Создаем новую симпл таблицу для новой итерации
            Array.Copy(oldMassive, resultMassive, oldMassive.GetLength(0));
            int lastTable = resultMassive.GetLength(0) - 1;                               //Выбираем индекс последней таблицы
            resultMassive[lastTable] = new SimplexTable();
            double[] delta = deltaCount(C, X, fs);
            int k = 0; //Направляющий столбец
            int r = 0; //Направляющая строка
            int situation = situationCheck(delta, X);
            //Записываем значения текущей симплекс таблицы
            resultMassive[lastTable].delta = delta;
            resultMassive[lastTable].L = delta[0];
            resultMassive[lastTable].C = C;
            resultMassive[lastTable].X = X;
            resultMassive[lastTable].delta = delta;
            resultMassive[lastTable].situation = situation;
            switch (situation) {
                case 1:
                    Console.WriteLine("Решение найдено");
                    return resultMassive;
                case 2:
                    Console.WriteLine("Задача не имеет решения");
                    return resultMassive;
                case 3:
                    k = findDirectiveColumn(delta);
                    r = findDirectiveRow(k, X, fs);
                    //Записываем значения текущей симплекс таблицы
                    resultMassive[lastTable].k = k;
                    resultMassive[lastTable].r = r;
                    resultMassive[lastTable].teta = tetaCount(k, X, fs);
                    fs[Array.IndexOf(fs, r)] = k;                       //Делаем замену вектора условий с индексом r на вектор с индексом k 
                    double[,] Xnew = new double[X.GetLength(0), X.GetLength(1)]; 
                    Xnew = newPlanFormation(X, C, fs, k, r);            //Расчитываем новый опорный план для следующей итерации
                    return optimalityCheck(Xnew, C, fs, resultMassive); //Делаем новую итерацию до тех пор пока не будет найден результат
            }
            return resultMassive;
        }

        /// <summary>
        /// Метод проверки погрешностей вычисления
        /// </summary>
        /// <param name="X">Опорный план</param>
        /// <param name="C">Коэффециенты линейной функции</param>
        /// <param name="fs">Коэффициенты базиса</param>
        /// <returns>Возвращает true, если погрешность допустима</returns>
        private static Boolean calculationsCheckIsOk(double[,] X, double[] C, int[] fs) {
            int row = X.GetLength(0);
            int col = X.GetLength(1) - 1;
            double[] delta = deltaCount(C, X, fs);
            double[] checkDelta = new double[col]; //Дельта расчитываемая для сравнения и проверки погрешностей
            for (int i = 1; i < col; i++) {
                for (int j = 0; j < row; j++) {
                    checkDelta[i] += C[fs[j] - 1] * X[j, i];
                }
                checkDelta[i] -= C[i - 1]; 
            }
            double epsilon = 0.000001;
            int checkedValues = 0;
            for (int i = 0; i < col; i++) {
                if(Math.Abs(checkDelta[i]-delta[i]) <= epsilon)
                    checkedValues++;
            }
            if (checkedValues == col)
                return true;
            else
                return false;
        } 

        /// <summary>
        /// Метод формирования нового опорного плана
        /// </summary>
        /// <param name="X">Начальный опорный план</param>
        /// <param name="C">Вектор коэффициентов линейной функции</param>
        /// <param name="fs">Коэффециенты начального базиса</param>
        /// <param name="k">Направляющий столбец</param>
        /// <param name="r">Направляющая строка</param>
        /// <returns>Новый опорный план</returns>
        private static double[,] newPlanFormation(double[,] X, double[] C, int[] fs, int k, int r) {
            int row = X.GetLength(0);
            int col = X.GetLength(1) - 1;
            double[,] Xnew = new double[row,col + 1];
            int rIndexRow = -1;
            for(int i = 0; i < row; i++) {
                if (X[i, 0] == r) {
                    rIndexRow = i; //Находим какая строка имеет индекс r
                    break;
                }
            }
            for(int i = 0; i < row; i++) {
               for(int j = 0; j < col; j++) {
                    if (X[i, 0] != r) { //Является ли текущая строка ведущей строкой (r)
                        Xnew[i, j + 1] = Math.Round((X[i, j + 1] - ((X[rIndexRow, j + 1] * X[i, k + 1]) / X[rIndexRow, k + 1])), 14);
                    } else {
                        Xnew[i, j + 1] = Math.Round((X[rIndexRow, j + 1] / X[rIndexRow, k + 1]), 14);
                    }
                }
            }
            for (int i = 0; i < row; i++)
                Xnew[i, 0] = fs[i];

            return Xnew;
        }

        /// <summary>
        /// Метод нахождения направляющей строки
        /// </summary>
        /// <param name="k">Направляющий столбец</param>
        /// <param name="X">Опорный план задачи</param>
        /// <param name="fs">Коэффициенты начального базиса</param>
        /// <returns>Направляющая строка</returns>
        private static int findDirectiveRow(int k, double[,] X, int[] fs) {
            int r = fs[0]; //Направляющая строка
            int length = X.GetLength(0);
            double[] teta = tetaCount(k, X, fs);
            int minVal = Array.IndexOf(teta, teta.Min());
            r = fs[minVal];
            return r;
        }

        /// <summary>
        /// Метод расчета тета
        /// </summary>
        /// <param name="k">Направляющий столбец</param>
        /// <param name="X">Опорный план</param>
        /// <param name="fs">Коэффициенты базиса</param>
        /// <returns>Тета</returns>
        private static double[] tetaCount(int k, double[,] X, int[] fs) {
            int length = X.GetLength(0);
            double[] teta = new double[length];
            for (int i = 0; i < length; i++) {
                if (X[i, k + 1] > 0)
                    teta[i] = (X[i, 1] / X[i, k + 1]);
                else
                    teta[i] = 99999999999999; //Неадекватно большое значение, которое показывает, что элемент не рассматривается
            }
            return teta;
        }

        /// <summary>
        /// Метод нахождения направляющего столбца
        /// </summary>
        /// <param name="delta">Вектор дельта</param>
        /// <returns>Направляющий столбец</returns>
        private static int findDirectiveColumn(double[] delta) {
            int k = 0; //Направляющий столбец
            int length = delta.GetLength(0);
            double min = delta[0];
            for(int i = 1; i < length; i++) {
                if (min > delta[i]) {
                    min = delta[i];
                    k = i;
                }
            }
            return k;
        }
        
        /// <summary>
        /// Метод определения ситуации
        /// </summary>
        /// <param name="delta">Вектор дельта</param>
        /// <returns>Номер ситуации</returns>
        private static int situationCheck(double[] delta, double[,] X) {
            int length = delta.GetLength(0);
            int height = X.GetLength(0);
            int situation = 1;
            for (int j = 1; j < length; j++) {
                if (delta[j-1] < 0) {
                    int omega = 0;
                    situation = 3;
                    for (int i = 0; i < height; i++) {
                        if (X[i, j] >= 0) omega++;
                    }
                    if (omega == 0) {
                        situation = 2;
                        break;
                    }
                }
            }
            return situation;
        }
    
        /// <summary>
        /// Метод нахождения коэффициентов дельта
        /// </summary>
        /// <param name="C">Вектор целевой функции</param>
        /// <param name="X">Опорный план задачи</param>
        /// <param name="fs">Коэффициенты базиса</param>
        /// <returns>Вектор дельта</returns>
        private static double[] deltaCount(double[] C, double[,] X, int[] fs) {
            int coefCount = X.GetLength(1) - 1; //Потому что [i,0] это не коэффициент, а индекс А
            int leng = fs.GetLength(0);
            double[] Cs = new double[leng]; //Коэффициенты целевой функции начального базиса
            for(int i = 0; i < leng; i++) {
                Cs[i] = C[fs[i] - 1];
            }
            double[] delta = new double[coefCount];
            double[] z = new double[coefCount];
            for(int i = 0; i < coefCount; i++) {
                for (int j = 0; j < leng; j++)
                    z[i] += Cs[j] * X[j,i + 1];
            }
            delta[0] = z[0];
            for (int i = 1; i < coefCount; i++)
                delta[i] = Math.Round(z[i] - C[i-1], 6); //округляем до 6 знаков после запятой
            return delta;
        }

        /// <summary>
        /// Метод изменения матрицы А с учетом дополнительных переменных
        /// </summary>
        /// <param name="AFirst">Исходная матрица АFirst</param>
        /// <returns>Матрица А</returns>
        private static double[,] modifyA(double[,] AFirst)
        {
            int m = AFirst.GetLength(0);
            int n = AFirst.GetLength(1);
            double[,] A = new double[m, m + n + 1]; // Каждый 0-ой элемент строки это индекс А, каждый 1-ый это значение вектора b
            //добавление элементов в матрицу А
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m + n; j++)
                {
                    //если переменная не новая, т.е. уже была в АFirst
                    if (j < n) A[i, j + 1] = AFirst[i,j]; //переписываем
                    //если переменная новая
                    else
                    {
                        //единицы должны быть по диагонали
                        if (j == i + n) A[i, j + 1] = 1;//диагональный элемент - 1
                        else A[i, j + 1] = 0;// пишем 0
                    }
                }
            }
            return A;
        }

        /// <summary>
        /// Метод изменения вектора С с учетом дополнительных переменных
        /// </summary>
        /// <param name="AFirst">Исходная матрица АFirst</param>
        /// <param name="СFirst">Исходный вектор СFirst</param>
        /// <returns>вектор С</returns>
        private static double[] modifyC(double[] CFirst, double[,]AFirst)
        {
            int m = AFirst.GetLength(0);
            int n = AFirst.GetLength(1);
            double[] C = new double[m + n - 1];
            //добавление элементов в вектор С
            for (int i = 0; i < m + n - 1; i++)
            {
                //если переменная не новая, т.е. уже была в СFirst
                if (i < n - 1) C[i] = CFirst[i];//переписываем
                else C[i] = 0;  //иначе коэффициент при переменной, которой по условию не было
                                //в целевой функции, будет = 0
            }
            return C;
        }

        /// <summary>
        /// Метод изменения матрицы А и вектора С с учетом дополнительных переменных
        /// </summary>
        /// <param name="А">Матрица А с учетом дополнительных переменных</param>
        /// <returns>Базис fs</returns>
        private static int[] referenceBasis(double[,] AFirst)
        {
            int m = AFirst.GetLength(0);
            int n = AFirst.GetLength(1);
            int[] fs = new int[m]; //коэффициенты базиса
            //записываем базис с учетом новых переменных
            for (int i = 0; i < m; i++)
            {
                fs[i] = n + i;
            }
            return fs;
        }

        /* Методы связанные с обработкой начальной матрицы, из-за того, 
         * что у нас метод нахождения начального базиса это все не требует они теряют актуальность, но на всякий случай оставлю.
         * 
        /// <summary>
        /// Метод нахождения коэффициентов симплекс таблицы
        /// </summary>
        /// <param name="A">Вектор ограничений</param>
        /// <param name="fs">Коэффициенты начального базиса</param>
        /// <returns>Новые коэффициенты симплекс таблицы</returns>
        private static double[,] basicPlanFormation(double[,] A, int[] fs) {
            int x = fs.GetLength(0);
            int y = A.GetLength(1);
            double[,] fsMatrix = new double[x, x]; //Создаем матрицу начального базиса по коэффициентам
            int counter = 0;
            foreach(int row in fs) {
                for (int i = 0; i < x; i++)
                    fsMatrix[i, counter] = A[i, row];
                counter++;
            }
            double[,] X = new double[x, y];
                double[,] Afs = new double[x, x];
                Afs = inverseMatrix(fsMatrix);
                for (int i = 0; i < x; i++) {
                    for (int j = 0; j < y; j++) {
                        for (int z = 0; z < x; z++)
                            X[i, j] += Afs[i, z] * A[z, j];
                    
                }
            }
            return X;
        }

        /// <summary>
        /// Метод транспонирования матрицы 
        /// </summary>
        /// <param name="matrix">Транспонируемая матрица</param>
        /// <returns>Транспонированная матрица</returns>
        private static double[,] transposition(double[,] matrix) {
            int N = matrix.GetLength(0);
            double tmp;
            for (int i = 0; i < N; i++) {
                for (int j = 0; j < i; j++) {
                    tmp = matrix[i, j];
                    matrix[i, j] = matrix[j, i];
                    matrix[j, i] = tmp;
                }
            }
            return matrix;
        }

        /// <summary>
        /// Метод нахождения алгебраического дополнения матрицы
        /// </summary>
        /// <param name="matrix">Исходная матрица</param>
        /// <returns>Алгебраическое дополнение матрицы</returns>
        private static double[,] cofactor(double[,] matrix) {
            int x = matrix.GetLength(0);
            int y = matrix.GetLength(1);
            double[,] cofactor = new double[matrix.GetLength(0), matrix.GetLength(1)];
            if (x != 2) {
                for (int i = 0; i < x; i++) {
                    for (int j = 0; j < y; j++) {
                        cofactor[i, j] = Math.Pow((-1), (i + j)) * determinant(x - 1, minorMatrix(i, j, x, matrix));
                    }
                }
            } else {
                for (int i = 0; i < x; i++) {
                    for (int j = 0; j < y; j++) {
                        cofactor[i, j] = Math.Pow((-1), (j + 2 + i)) * matrix[(x - 1) - i, (y - 1) - j];
                    }
                }
            }
            return cofactor;
        }

        /// <summary>
        /// Метод нахождения обратной матрицы
        /// </summary>
        /// <param name="determ">Определитель матрицы</param>
        /// <param name="matrix">Исходная матрица</param>
        /// <returns>Обратная матрица</returns>
        private static double[,] inverseMatrix(double[,] matrix) {
            int x = matrix.GetLength(0);
            int y = matrix.GetLength(1);
            double determ = determinant(x, matrix);
            double[,] inversed = new double[x, y];
            matrix = cofactor(matrix);
            inversed = transposition(matrix);
            for (int i = 0; i < x; i++) {
                for (int j = 0; j < y; j++)
                    inversed[i, j] *= 1 / determ;
            }
            return inversed;
        }

        /// <summary>
        /// Метод нахождения определителя матрицы
        /// </summary>
        /// <param name="dimensh">Размерность</param>
        /// <param name="matrix">Исходная матрица</param>
        /// <returns>Определитель матрицы</returns>
        private static double determinant(int dimensh, double[,] matrix) {
            double sum = 0;
            if (dimensh != 2) {
                for (int i = 0; i < dimensh; i++) {
                    sum += Math.Pow((-1), (i + 2)) * matrix[0, i] * determinant(dimensh - 1, minorMatrix(0, i, dimensh, matrix));
                }
            } else
                sum = matrix[0, 0] * matrix[dimensh - 1, dimensh - 1] - matrix[dimensh - 1, 0] * matrix[0, dimensh - 1];
            return sum;
        }

        /// <summary>
        /// Метод нахождения минора матрицы
        /// </summary>
        /// <param name="x">Строка элемента</param>
        /// <param name="y">Столбец элемента</param>
        /// <param name="dimensh">Размерность матрицы </param>
        /// <param name="matrix">Исходная матрица</param>
        /// <returns>Минор матрицы</returns>
        private static double[,] minorMatrix(int x, int y, int dimensh, double[,] matrix) {
            double[,] C = new double[dimensh - 1, dimensh - 1];
            for (int h = 0, i = 0; i < dimensh - 1; i++, h++) {
                if (i == x) h++;
                for (int k = 0, j = 0; j < dimensh - 1; j++, k++) {
                    if (k == y) k++;
                    C[i, j] = matrix[h, k];
                }
            }
            return C;
        }
        */
    }
}
