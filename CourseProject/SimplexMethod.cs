﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CourseProject {

    /// <summary>
    /// Метод последовательного улучшения плана (1 алгоритм)
    /// </summary>
    class SimplexMethod {

        public static void buildTable(double[,] A, double[] C, int[] fs) {
            double[,] X = newTableCoeffsCount(A, fs);
            double[] delta = deltaCount(C, X, fs);
            int situation = situationCheck(delta);
            int k = 0; //Направляющий столбец
            int r = 0; //Направляющая строка
            switch (situation) {
                case 1:
                    Console.WriteLine("Решение найдено");
                    // тут нужно вывести результаты
                    break;
                case 2:
                    Console.WriteLine("Задача не имеет решения");
                    break;
                case 3:
                    k = findDirectiveColumn(delta);
                    r = findDirectiveRow(k, X, fs);
                    fs[Array.IndexOf(fs, r)] = k;   //Делаем замену вектора условий с индексом r на вектор с индексом k 
                    //тут должны делаться всякие штуки которые я еще не дописал
                    break;
            }
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
        private static double[,] newTableCoeffsCount(double[,] A, int[] fs) {
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
    }
}
