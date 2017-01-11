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
        /// <param name="isMax">Стремится ли функция к максимуму</param>
        public static SimplexTable[] solve(decimal[,] AFirst, decimal[] CFirst, Boolean isMax) {

            //матрица ограничений с учётом добавленных переменных 
            decimal[,] A = modifyA(AFirst);
            //коэффициенты целевой функции с учетом добавленных переменных
            decimal[] C = modifyC(CFirst, AFirst);
            //коэффициенты базиса
            int[] fs = referenceBasis(AFirst);
            int rows = A.GetLength(0);                               //Получаем количество строк матрицы ограничений
            for (int i = 0; i < rows; i++)
                A[i, 0] = fs[i];                                     //Записываем в каждый 0-ой элемент строки индекс А
            SimplexTable[] resultMassive = new SimplexTable[0];      //Создаем массив симплекс таблиц
            resultMassive = newTableCalculation(A, C, fs, resultMassive, isMax);//Определяем массив симплекс таблиц для всех итераций
            return resultMassive;
        }

        /// <summary>
        /// Метод проверки текущего базиса на оптимальность, выполняется до тех пор пока не 
        /// будет найдено решение либо не будет определено, что задача не имеет решения
        /// </summary>
        /// <param name="X">Матрица ограничений, где [i,0] это индекс А, а [i,1] значение вектора b</param>
        /// <param name="C">Вектор коэффициентов целевой функции</param>
        /// <param name="fs">Коэффициенты начального базиса</param>
        /// <param name="oldMassive">Предыдущий массив симплекс таблиц</param>
        /// <param name="isMax">Стремится ли функция к максимуму</param>
        private static SimplexTable[] newTableCalculation(decimal[,] X, decimal[] C, int[] fs, SimplexTable[] oldMassive, Boolean isMax) {
            SimplexTable[] resultMassive =
                new SimplexTable[oldMassive.GetLength(0) + 1];                  //Создаем новую симпл таблицу для новой итерации
            Array.Copy(oldMassive, resultMassive, oldMassive.GetLength(0));
            int lastTable = resultMassive.GetLength(0) - 1;                     //Выбираем индекс последней таблицы
            resultMassive[lastTable] = new SimplexTable();

            resultMassive[lastTable].delta = deltaCount(C, X, fs);
            resultMassive[lastTable].situation =
                situationCheck(resultMassive[lastTable].delta, X, isMax);
            resultMassive[lastTable].L = resultMassive[lastTable].delta[0];
            resultMassive[lastTable].C = C;
            resultMassive[lastTable].X = X;

            switch (resultMassive[lastTable].situation) {
                case 1:
                    Console.WriteLine("Решение найдено");
                    return resultMassive;
                case 2:
                    Console.WriteLine("Задача не имеет решения");
                    return resultMassive;
                case 3:
                    resultMassive[lastTable].k =
                          findDirectiveColumn(resultMassive[lastTable].delta, isMax);    //Приближенный метод нахождения столбца
                    /*    resultMassive[lastTable].k =
                             findDirectiveColimnTrue(resultMassive[lastTable].C,        //Точный метод нахождения столбца
                    resultMassive[lastTable].X, resultMassive[lastTable].fs, isMax); */
                    resultMassive[lastTable].r =
                        findDirectiveRow(resultMassive[lastTable].k, X, fs);     //Направляющая строка
                    resultMassive[lastTable].teta =
                        tetaCount(resultMassive[lastTable].k, X);                //Находим тету
                    fs[Array.IndexOf(fs, resultMassive[lastTable].r)] =
                        resultMassive[lastTable].k;                              //Делаем замену вектора условий с индексом r на вектор с индексом k 
                    decimal[,] Xnew = newPlanFormation(X, C, fs,
                        resultMassive[lastTable].k, resultMassive[lastTable].r); //Расчитываем новый опорный план для следующей итерации
                    resultMassive[lastTable].checkRow = checkRowCalculation(resultMassive[lastTable].delta,
                        resultMassive[lastTable].X, resultMassive[lastTable].r, resultMassive[lastTable].k);
                    return newTableCalculation(Xnew, C, fs, resultMassive, isMax);
                    /*          if (calculationsCheckIsOk(Xnew, resultMassive[lastTable].C, fs, resultMassive[lastTable].checkRow)) 
                                  return optimalityCheck(Xnew, C, fs, resultMassive, isMax);          //Делаем новую итерацию до тех пор пока не будет найден результат
                              else {
                                  Console.WriteLine("Все сломалось");
                                  return resultMassive; 
                              }*/

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
        private static Boolean calculationsCheckIsOk(decimal[,] X, decimal[] C, int[] fs, decimal[] checkRow) {
            int col = X.GetLength(1) - 1;
            decimal[] delta = deltaCount(C, X, fs);
            decimal epsilon = 0.000001m;
            int checkedValues = 0;
            for (int i = 0; i < col; i++) {
                if (Math.Abs(delta[i] - checkRow[i]) <= epsilon)
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
        /// <param name="fs">Коэффециенты базиса</param>
        /// <param name="k">Направляющий столбец</param>
        /// <param name="r">Направляющая строка</param>
        /// <returns>Новый опорный план</returns>
        private static decimal[,] newPlanFormation(decimal[,] X, decimal[] C, int[] fs, int k, int r) {
            int row = X.GetLength(0);
            int col = X.GetLength(1) - 1;
            decimal[,] Xnew = new decimal[row, col + 1];
            int rIndexRow = -1;
            for (int i = 0; i < row; i++) {
                if (X[i, 0] == r) {
                    rIndexRow = i; //Находим какая строка имеет индекс r
                    break;
                }
            }
            for (int i = 0; i < row; i++) {
                for (int j = 0; j < col; j++) {
                    if (X[i, 0] != r) { //Является ли текущая строка ведущей строкой (r)
                        Xnew[i, j + 1] = (X[i, j + 1] - ((X[rIndexRow, j + 1] * X[i, k + 1]) / X[rIndexRow, k + 1]));
                    } else {
                        Xnew[i, j + 1] = (X[rIndexRow, j + 1] / X[rIndexRow, k + 1]);
                    }
                }
            }
            for (int i = 0; i < row; i++)
                Xnew[i, 0] = fs[i];

            return Xnew;
        }

        /// <summary>
        /// Метод нахождения новой строки для проверки вычислений
        /// </summary>
        /// <param name="delta">Массив дельта</param>
        /// <param name="X">Опорный план</param>
        /// <param name="r">Направляющая строка</param>
        /// <param name="k">Направляющий столбец</param>
        /// <returns>Строка для проверки вычислений</returns>
        private static decimal[] checkRowCalculation(decimal[] delta, decimal[,] X, int r, int k) {
            int row = X.GetLength(0);
            int col = X.GetLength(1) - 1;
            decimal[] checkRow = new decimal[col];

            int rIndexRow = -1;
            for (int i = 0; i < row; i++) {
                if (X[i, 0] == r) {
                    rIndexRow = i; //Находим какая строка имеет индекс r
                    break;
                }
            }

            for (int i = 0; i < col; i++) {
                checkRow[i] = delta[i] - ((X[rIndexRow, i + 1] * delta[k]) / X[rIndexRow, k + 1]);
            }
            return checkRow;
        }

        /// <summary>
        /// Метод нахождения направляющей строки
        /// </summary>
        /// <param name="k">Направляющий столбец</param>
        /// <param name="X">Опорный план задачи</param>
        /// <param name="fs">Коэффициенты базиса</param>
        /// <returns>Направляющая строка</returns>
        private static int findDirectiveRow(int k, decimal[,] X, int[] fs) {
            int r = fs[0]; //Направляющая строка
            int length = X.GetLength(0);
            decimal[] teta = tetaCount(k, X);
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
        private static decimal[] tetaCount(int k, decimal[,] X) {
            int length = X.GetLength(0);
            decimal[] teta = new decimal[length];
            for (int i = 0; i < length; i++) {
                if (Math.Round(X[i, k + 1], 24) > 0)
                    teta[i] = (X[i, 1] / X[i, k + 1]);
                else
                    teta[i] = 99999999999999; //Неадекватно большое значение, которое показывает, что элемент не рассматривается
            }
            return teta;
        }

        /// <summary>
        /// Приближенный метод нахождения направляющего столбца
        /// </summary>
        /// <param name="delta">Вектор дельта</param>
        /// <returns>Направляющий столбец</returns>
        private static int findDirectiveColumn(decimal[] delta, Boolean isMax) {
            int k = 0; //Направляющий столбец
            int length = delta.GetLength(0);
            decimal current = delta[0];
            if (isMax == true) {
                for (int i = 1; i < length; i++) {
                    if (current > delta[i]) {
                        current = delta[i];
                        k = i;
                    }
                }
            } else {
                for (int i = 1; i < length; i++) {
                    if (current < delta[i]) {
                        current = delta[i];
                        k = i;
                    }
                }
            }
            return k;
        }

        /// <summary>
        /// Точный метод нахождения направляющего столбца
        /// </summary>
        /// <param name="C">Вектор коэффициентов линейной функции </param>
        /// <param name="X">Опорный план</param>
        /// <param name="fs">Коэффициенты базиса</param>
        /// <returns></returns>
        private static int findDirectiveColimnTrue(decimal[] C, decimal[,] X, int[] fs, Boolean isMax) {
            int row = X.GetLength(0);
            int col = X.GetLength(1) - 1;
            decimal[] arrayL = new decimal[col];
            int k = 0;
            decimal[] delta = deltaCount(C, X, fs);
            for (int j = 0; j < col; j++) {
                if (delta[j] < 0) {
                    arrayL[j] = -1 * tetaCount(j, X).Min() * delta[j];
                }
            }
            /*     if(isMax == true) { 
                 for (int i = 0; i < col; i++) {
                     if (arrayL[i] > k) {
                         k = i;
                     }
                 }
                }else {
                     for (int i = 0; i < col; i++) {
                         if (arrayL[i] < k) {
                             k = i;
                         }
                     }
                 }
                 */
            for (int i = 0; i < col; i++) {
                if (arrayL[i] > k) {
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
        private static int situationCheck(decimal[] delta, decimal[,] X, Boolean isMax) {
            int length = delta.GetLength(0);
            int height = X.GetLength(0);
            int situation = 1;
            if (isMax == true) {
                for (int j = 1; j < length; j++) {
                    if (delta[j - 1] < 0) {
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
            } else {
                for (int j = 1; j < length; j++) {
                    if (delta[j - 1] > 0) {
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
        private static decimal[] deltaCount(decimal[] C, decimal[,] X, int[] fs) {
            int coefCount = X.GetLength(1) - 1; //Потому что [i,0] это не коэффициент, а индекс А
            int leng = fs.GetLength(0);
            decimal[] Cs = new decimal[leng]; //Коэффициенты целевой функции начального базиса
            for (int i = 0; i < leng; i++) {
                Cs[i] = C[fs[i] - 1];
            }
            decimal[] delta = new decimal[coefCount];
            decimal[] z = new decimal[coefCount];
            for (int i = 0; i < coefCount; i++) {
                for (int j = 0; j < leng; j++)
                    z[i] += Cs[j] * X[j, i + 1];
            }
            delta[0] = z[0];
            for (int i = 1; i < coefCount; i++)
                delta[i] = z[i] - C[i - 1];
            return delta;
        }

        /// <summary>
        /// Метод изменения матрицы А с учетом дополнительных переменных
        /// </summary>
        /// <param name="AFirst">Исходная матрица АFirst</param>
        /// <returns>Матрица А</returns>
        private static decimal[,] modifyA(decimal[,] AFirst) {
            int m = AFirst.GetLength(0);
            int n = AFirst.GetLength(1);
            decimal[,] A = new decimal[m, m + n + 1]; // Каждый 0-ой элемент строки это индекс А, каждый 1-ый это значение вектора b
            //добавление элементов в матрицу А
            for (int i = 0; i < m; i++) {
                for (int j = 0; j < m + n; j++) {
                    //если переменная не новая, т.е. уже была в АFirst
                    if (j < n) A[i, j + 1] = AFirst[i, j]; //переписываем
                    //если переменная новая
                    else {
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
        private static decimal[] modifyC(decimal[] CFirst, decimal[,] AFirst) {
            int m = AFirst.GetLength(0);
            int n = AFirst.GetLength(1);
            decimal[] C = new decimal[m + n - 1];
            //добавление элементов в вектор С
            for (int i = 0; i < m + n - 1; i++) {
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
        private static int[] referenceBasis(decimal[,] AFirst) {
            int m = AFirst.GetLength(0);
            int n = AFirst.GetLength(1);
            int[] fs = new int[m]; //коэффициенты базиса
            //записываем базис с учетом новых переменных
            for (int i = 0; i < m; i++) {
                fs[i] = n + i;
            }
            return fs;
        }

        /// <summary>
        /// Метод транспонирования матрицы 
        /// </summary>
        /// <param name="matrix">Транспонируемая матрица</param>
        /// <returns>Транспонированная матрица</returns>
        public static decimal[,] transposition(decimal[,] matrix) {
            int N = matrix.GetLength(0);
            decimal tmp;
            for (int i = 0; i < N; i++) {
                for (int j = 0; j < i; j++) {
                    tmp = matrix[i, j];
                    matrix[i, j] = matrix[j, i];
                    matrix[j, i] = tmp;
                }
            }
            return matrix;
        }

        /*      Решение нашей задачи ток к минимуму
                public static SimplexTable[] inverseSolve(decimal[,] A, decimal[] C, Boolean isMax) {
                    decimal[,] inverseA = createInverseA(A, C);
                    decimal[] inverseC = createInverseC(A);
                    return solve(inverseA, inverseC, !isMax);
                }

                private static decimal[,] createInverseA(decimal[,] A, decimal[] C) {
                    int row = A.GetLength(0);
                    int col = A.GetLength(1);
                    decimal[,] inverseA = new decimal[row, col];
                    //Заполняем первый столбец значениями C 
                    for (int i = 0; i < row; i++)
                        inverseA[i, 0] = C[i];

                    //Обрезаем первый столбец из исходной матрицы ограничений
                    decimal[,] cuttedA = new decimal[row, col - 1];
                    for (int i = 0; i < row; i++) {
                        for (int j = 0; j < col - 1; j++)
                            cuttedA[i, j] = A[i, j + 1];
                    }

                    //Транспонируем обрезанную матрицу
                    decimal[,] transposited = transposition(cuttedA);

                    //Записываем значения транспонированной матрицы в новую матрицу
                    for (int i = 0; i < row; i++) {
                        for (int j = 1; j < col; j++)
                            inverseA[i, j] = transposited[i, j - 1];
                    }
                    return inverseA;
                }

                private static decimal[] createInverseC(decimal[,] A) {
                    int vars = A.GetLength(0);
                    decimal[] inverseC = new decimal[vars];
                    for (int i = 0; i < vars; i++) {
                        inverseC[i] = A[i, 0];
                    }
                    return inverseC;
                }

            */

        /// <summary>
        /// Метод проверки опорности
        /// </summary>
        /// <param name="tablesMassive">Массив симплекс-таблиц</param>
        /// <returns>true or false</returns>
        public static bool referenceCheck(SimplexTable[] tablesMassive) {
            decimal[,] Afs = formAfs(tablesMassive);
            if (determinant(Afs) != 0) {
                return true;
            } else {
                return false;
            }
        }

        /// <summary>
        /// Метод проверки оптимальности
        /// </summary>
        /// <param name="tablesMassive"></param>
        /// <returns></returns>
        public static bool optimalityCheck(SimplexTable[] tablesMassive) {
            int lastTable = tablesMassive.GetLength(0) - 1;
            decimal[] Cs = tablesMassive[lastTable].Cs;
            int xCount = Cs.GetLength(0);
            decimal[,] Afs = formAfs(tablesMassive);
            decimal[,] inversed = inverseMatrix(Afs);
            inverseCount(inversed);
            decimal[] y = new decimal[xCount];
            int rows = inversed.GetLength(0);

            for (int i = 0; i < xCount; i++) {
                for (int j = 0; j < rows; j++) {
                    y[i] += Cs[j] * inversed[j, i];

                }
                Console.WriteLine("y{0} = {1}", i, y[i]);
            }

            decimal[,] CHECK = transposition(tablesMassive[0].A);
            decimal count = 0;
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < xCount; j++) {
                    count += y[j] * CHECK[i, j];
                }
                Console.WriteLine("Cs = {0} , count = {1}", tablesMassive[0].C[i], count);
                count = 0;
            }

            decimal L = 0;
            for (int i = 0; i < xCount; i++) {
                L += y[i] * tablesMassive[0].X[i, 1];
            }
            Console.WriteLine(L);
            return true;
        }



        /// <summary>
        /// Метод формирования матрицы Afs для проверкм опорности
        /// </summary>
        /// <param name="tablesMassive">Массив симплекс-таблиц</param>
        /// <returns>Матрица Afs</returns>
        public static decimal[,] formAfs(SimplexTable[] tablesMassive) {
            int lastTable = tablesMassive.GetLength(0) - 1;
            int m = tablesMassive[0].X.GetLength(0);
            int n = tablesMassive[0].X.GetLength(1);
            decimal[,] Afs = new decimal[m, m];
            for (int j = 2; j < n; j++) {

                for (int k = 0; k < m; k++) {
                    if (j - 1 == tablesMassive[lastTable].fs[k]) {
                        for (int i = 0; i < m; i++) {
                            Afs[i, k] = tablesMassive[0].X[i, j];
                        }
                    }
                }
            }
            return Afs;
        }


        /// <summary>
        /// Метод нахождения определителя матрицы методом Монтанте (Барейса)
        /// </summary>
        /// <param name="matrix">Исходная матрица</param>
        /// <returns>Определитель матрицы</returns>
        private static decimal determinant(decimal[,] matrix) {
            int xCount = matrix.GetLength(0);
            decimal prevI = matrix[0, 0];
            decimal temp = 0;
            for (int i = 0; i < xCount; i++) {
                for (int row = 0; row < xCount; row++) {
                    temp = matrix[row, i];
                    if (row != i) {
                        for (int col = 0; col < xCount; col++) {
                            matrix[row, col] = ((matrix[i, i] * matrix[row, col]) - (temp * matrix[i, col])) / prevI;
                        }
                    }
                }
                prevI = matrix[i, i];
            }
            return prevI;
        }

        public static decimal[,] inverseMatrix(decimal[,] matrix) {
            int xCount = matrix.GetLength(0);
            decimal[,] inversed = new decimal[xCount, xCount * 2];
            //добавление элементов в матрицу А
            for (int i = 0; i < xCount; i++) {
                for (int j = 0; j < xCount * 2; j++) {
                    //если переменная не новая, т.е. уже была в АFirst
                    if (j < xCount) inversed[i, j] = matrix[i, j]; //переписываем
                    //если переменная новая
                    else {
                        //единицы должны быть по диагонали
                        if (j == i + xCount) inversed[i, j] = 1;//диагональный элемент - 1
                        else inversed[i, j] = 0;// пишем 0
                    }
                }
            }
            decimal prevI = inversed[0, 0];
            decimal temp = 0;
            for (int i = 0; i < xCount; i++) {
                for (int row = 0; row < xCount; row++) {
                    temp = inversed[row, i];
                    if (row != i) {
                        for (int col = 0; col < xCount * 2; col++) {
                            inversed[row, col] = ((inversed[i, i] * inversed[row, col]) - (temp * inversed[i, col])) / prevI;
                        }
                    }
                }
                prevI = inversed[i, i];
            }
            decimal[,] finalInversed = new decimal[xCount, xCount];
            for (int i = 0; i < xCount; i++) {
                for (int j = 0; j < xCount; j++)
                    finalInversed[i, j] = inversed[i, j + xCount] / inversed[0, 0];
            }
            return finalInversed;
        }


        //удалить нафиг
        public static decimal[] getY(SimplexTable[] tablesMassive) {
            int lastTable = tablesMassive.GetLength(0) - 1;
            decimal[] Cs = tablesMassive[lastTable].Cs;
            int xCount = Cs.GetLength(0);
            decimal[,] Afs = formAfs(tablesMassive);
            decimal[,] inversed = inverseMatrix(Afs);
            decimal[] y = new decimal[xCount];
            int rows = inversed.GetLength(0);

            for (int i = 0; i < xCount; i++) {
                for (int j = 0; j < rows; j++) {
                    y[i] += Cs[j] * inversed[j, i];

                }
            }

            return y;
        }


        public static void inverseCount(decimal[,] matrix) {
            decimal[] b = new decimal[] { 1300, 9100, 12400, 11240, 12402, 41240, 12405, 21240, 12407, 51240, 12408, 12410 };
            int xCount = b.GetLength(0);
            decimal[] X = new decimal[xCount];
            
            for(int i = 0; i < xCount; i++) {
                for(int j = 0; j < xCount; j++) 
                    X[i] += matrix[i,j] * b[j];
            }

            foreach (decimal el in X)
                Console.WriteLine(el + ", ");
        }
    }
}
