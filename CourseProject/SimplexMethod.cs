using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Метод последовательного улучшения плана (1 алгоритм)
/// </summary>
namespace CourseProject {
    class SimplexMethod {

     /*   public void buildTable(double[,] A, double[] C, double[,] fs) {
            double[,] X = newTableCoeffsCount(A, fs);
        }
    */


        /// <summary>
        /// Метод нахождения коэффициентов симплекс таблицы
        /// </summary>
        /// <param name="A">Вектор ограничений</param>
        /// <param name="fs">Начальный базис</param>
        /// <returns>Новые коэффициенты симплекс таблицы</returns>
        public static double[,] newTableCoeffsCount(double[,] A, double[,] fs) {
            int x = fs.GetLength(0);
            int y = A.GetLength(1);
            double[,] X = new double[x, y];
                double[,] Afs = new double[fs.GetLength(0), fs.GetLength(1)];
                Afs = inverseMatrix(fs);
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
