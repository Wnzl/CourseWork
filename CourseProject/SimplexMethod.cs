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

        public static void solve(double[,] AFirst, double[] CFirst) {
            
            //матрица ограничений с учётом добавленных переменных
            double[,] A = new double[AFirst.GetLength(0), AFirst.GetLength(0) + AFirst.GetLength(1)];
            //коэффициенты целевой функции с учетом добавленных переменных
            double[] C = new double[AFirst.GetLength(0) + AFirst.GetLength(1) - 1];
            A=modifyA(AFirst);
            C = modifyC(CFirst, AFirst);
            int[] fs = new int[A.GetLength(0)]; //коэффициенты базиса
            fs = referenceBasis(A);
            double[,] X = basicPlanFormation(A, fs);
            buildTable(X, C, fs);
        }

        private static void buildTable(double[,] X, double[] C, int[] fs) {
            double[] delta = deltaCount(C, X, fs);
            int k = 0; //Направляющий столбец
            int r = 0; //Направляющая строка
            int situation = situationCheck(delta);
            switch (situation) {
                case 1:
                    //Безобразный вывод в консоль исключительно для теста как выполняется эта вся кухня
                    Console.WriteLine("Решение найдено");
                    int row = X.GetLength(0);
                    int col = X.GetLength(1);
                    Console.WriteLine("L = {0}", delta[0]);
                    Console.Write("\nFs = {");
                    for (int i = 0; i < fs.GetLength(0); i++)
                        Console.Write("A" + fs[i] + " ");
                    Console.Write("}");
                    //тут нужно еще вывод вектора значений переменных вывести, но мне уже влом
                  /*int l = C.GetLength(0);
                    double[] result = new double[l];
                    for(int i = 0; i < l; i++) {
                        if(i == )
                        result[i] 
                    }*/
                        break;
                case 2:
                    Console.WriteLine("Задача не имеет решения");
                    break;
                case 3:
                    k = findDirectiveColumn(delta);
                    // Не работает, нужно допилить
                    //   r = findDirectiveRow(k, X, fs);
                    r = 1;
                    fs[Array.IndexOf(fs, r)] = k;   //Делаем замену вектора условий с индексом r на вектор с индексом k 
                    double[,] Xnew = new double[X.GetLength(0), X.GetLength(1)];
                    Xnew = newBasicPlanFormation(X, C, fs, k, r);
                     if (calculationsCheckIsOk(Xnew,C,fs)) {
                        buildTable(Xnew, C, fs);
                    } else {
                          Console.WriteLine("Чет пошло не так");
                      }
                    break;
            }
        }

        //Не понимаю смысла этой  проверки, думаю удалить этот метод
        /// <summary>
        /// Метод проверки погрешностей вычисления
        /// </summary>
        /// <param name="X">Опорный план</param>
        /// <param name="C">Коэффециенты линейной функции</param>
        /// <param name="fs">Коэффициенты базиса</param>
        /// <returns>Возвращает true, если погрешность допустима</returns>
        private static Boolean calculationsCheckIsOk(double[,] X, double[] C, int[] fs) {
            int row = X.GetLength(0);
            int col = X.GetLength(1);
            double[] delta = deltaCount(C, X, fs);
            double[] checkDelta = new double[col]; //Дельта расчитываемая для сравнения и проверки погрешностей
            checkDelta[0] = C[fs[0] - 1] * X[0, 0] + C[fs[1] - 1] * X[1, 0];
            for (int i = 1; i < col; i++) {
                for (int j = 0; j < row; j++) {
                    checkDelta[i] += C[fs[j] - 1] * X[j, i];
                }
                checkDelta[i] -= C[i - 1]; 
            }
            double epsilon = 0.0000001;
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

        //Не работает т.к. неправильно сделан выбор i != r, надо допилить
        /// <summary>
        /// Метод формирования нового опорного плана
        /// </summary>
        /// <param name="X">Начальный опорный план</param>
        /// <param name="C">Вектор коэффициентов линейной функции</param>
        /// <param name="fs">Коэффециенты начального базиса</param>
        /// <param name="k">Направляющий столбец</param>
        /// <param name="r">Направляющая строка</param>
        /// <returns>Новый опорный план</returns>
        private static double[,] newBasicPlanFormation(double[,] X, double[] C, int[] fs, int k, int r) {
            int row = X.GetLength(0);
            int col = X.GetLength(1);
            double[,] Xnew = new double[row,col];
            for(int i = 0; i < row; i++) {
               for(int j = 0; j < col; j++) {
                    if (i != r - 1) {
                        //Ошибка из-за того, что в массиве нумерация идёт 0, 1, 2..., а в матрице это может
                        //быть что-то вроде 3,4,6 или 1, 2, 4 и всякое такое
                        Xnew[i, j] = Math.Round(X[i, j] - ((X[r - 1, j] * X[i, k]) / X[r - 1, k]),14);
                    }else {
                        Xnew[i, j] = Math.Round(X[r-1, j] / X[r-1, k],14);
                    }
                }
            }
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
            double[] teta = new double[length];
            teta[0] = X[0, 0] / X[0, k];
            for (int i = 1; i < length; i++) {
                teta[i] = (X[i,0]/X[i,k]);
                if (teta[i] < teta[i-1]) {
                    r = fs[i];
                }
            }
            return r;
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
        private static int situationCheck(double[] delta) {
            int situation = 0;
            int length = delta.GetLength(0);
            int negativeCount = 0;
            for(int i = 0; i < length; i++) {
                if (delta[i] < 0)
                    negativeCount++;
            }
            if(negativeCount == 0) {
                situation = 1;
            }else {
                //тут еще омегу нужно сделать
                if(negativeCount == length) {
                    situation = 2;
                }else {
                    situation = 3;
                }
            }
            return situation;
        }
    
        /// <summary>
        /// Метод нахождения коэффициентов дельта
        /// </summary>
        /// <param name="C">Вектор целевой функции</param>
        /// <param name="X">Опорный план задачи</param>
        /// <param name="fs">Коэффициенты начального базиса</param>
        /// <returns>Вектор дельта</returns>
        private static double[] deltaCount(double[] C, double[,] X, int[] fs) {
            int coefCount = X.GetLength(1);
            int leng = fs.GetLength(0);
            double[] Cs = new double[leng]; //Коэффициенты целевой функции начального базиса
            for(int i = 0; i < leng; i++) {
                Cs[i] = C[fs[i] - 1];
            }
            double[] delta = new double[coefCount];
            double[] z = new double[coefCount];
            for(int i = 0; i < coefCount; i++) {
                for (int j = 0; j < leng; j++)
                    z[i] += Cs[j] * X[j,i];
            }
            delta[0] = z[0];
            for (int i = 1; i < coefCount; i++)
                delta[i] = z[i] - C[i-1];
                       
            return delta;
        }

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
                        cofactor[i, j] = Math.Pow((-1), (j + 2 + i)) * matrix[(x-1)-i, (y-1)-j];
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
                    inversed[i, j] *= 1/determ;
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
        /// <summary>
        /// Метод изменения матрицы А с учетом дополнительных переменных
        /// </summary>
        /// <param name="AFirst">Исходная матрица АFirst</param>
        /// <returns>Матрица А</returns>
        private static double[,] modifyA(double[,] AFirst)
        {
            double[,] A = new double[AFirst.GetLength(0), AFirst.GetLength(0) + AFirst.GetLength(1)];
            //добавление элементов в матрицу А
            for (int i = 0; i < AFirst.GetLength(0); i++)
            {
                for (int j = 0; j < AFirst.GetLength(0) + AFirst.GetLength(1); j++)
                {
                    //если переменная не новая, т.е. уже была в АFirst
                    if (j < AFirst.GetLength(1)) A[i, j] = AFirst[i, j]; //переписываем
                    //если переменная новая
                    else
                    {
                        //единицы должны быть по диагонали
                        if (j == i + A.GetLength(1) - 2) A[i, j] = 1;//диагональный элемент - 1
                        else A[i, j] = 0;// пишем 0
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
            double[] C = new double[AFirst.GetLength(0) + AFirst.GetLength(1) - 1];
            //добавление элементов в вектор С
            for (int i = 0; i < AFirst.GetLength(0) + AFirst.GetLength(1) - 1; i++)
            {
                //если переменная не новая, т.е. уже была в СFirst
                if (i < AFirst.GetLength(1) - 1) C[i] = CFirst[i];//переписываем
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
        private static int[] referenceBasis(double[,] A)
        {
            int[] fs = new int[A.GetLength(0)]; //коэффициенты базиса
            //записываем базис с учетом новых переменных
            for (int i = 0; i < A.GetLength(0); i++)
            {
                fs[i] = A.GetLength(0) + i + 1;
            }
            return fs;
        }
    }
}
