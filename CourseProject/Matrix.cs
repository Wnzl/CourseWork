using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject {
    public class Matrix
    {
        public int E { set; get; }// точность
        public int Row { set; get; }//кол-во строк
        public int Col { set; get; }//кол-во столбцов
        public decimal[][] A { set; get; } //главный масив

        #region Конструкторы
        public Matrix(int Length)
        {
            this.Row = this.Col = Length;
            A = new decimal[Length][];
            for (int i = 0; i < Length; i++)
                A[i] = new decimal[Length];
            E = 5;
        }
        public Matrix(int Row, int Col)
        {
            this.Row = Row;
            this.Col = Col;
            A = new decimal[Row][];
            for (int i = 0; i < Row; i++)
                A[i] = new decimal[Col];
            E = 5;
        }
        public Matrix()
        {
            this.Row = this.Col = 0;
            A = new decimal[][]{};
            E = 5;
        }

        public Matrix(decimal[][] A)
        {
            Row = A.Length;
            Col = A[0].Length;
            this.A = new decimal[Row][];
            for (int i = 0; i < Row; i++)
                this[i] = new decimal[Col];
            for (int i = 0; i < Row; i++)
                for (int j = 0; j < Col; j++)
                    this[i, j] = A[i][j];
            E = 5;
        }

        /// <summary>
        /// Создание матрицы из строки/столбца с значениями
        /// </summary>
        /// <param name="a">Направление Vertical - солбец; Horizontal - строка</param>
        /// <param name="A">Значения помещаймые в строку/столбец в зависимости от направления</param>
        public Matrix(Directions a, decimal[] A)
        {
            if (a.Equals(Directions.Horizontal))
            {
                Row = 1;
                Col = A.Length;
            }
            else
            {
                Row = A.Length;
                Col = 1;
            }
            this.A = new decimal[Row][];
            for (int i = 0; i < Row; i++)
                this[i] = new decimal[Col];
            for (int i = 0; i < Row; i++)
                for (int j = 0; j < Col; j++)
                    this[i, j] = A[Math.Max(i, j)];
            E = 5;
        }
        /// <summary>
        /// Создание пустой матрицы из одного столбца/строки
        /// </summary>
        /// <param name="a">Направление Vertical - солбец; Horizontal - строка</param>
        /// <param name="Length">Значение Row или Col в зависимости от направления</param>
        /// <param name="StartValue">Чему равны все елементы в столбце/строке</param>
        public Matrix(Directions a, int Length, decimal StartValue = 0)
        {
            if (a.Equals(Directions.Horizontal))
            {
                Row = 1;
                Col = Length;
            }
            else
            {
                Row = Length;
                Col = 1;
            }
            this.A = new decimal[Row][];
            for (int i = 0; i < Row; i++)
            {
                this[i] = new decimal[Col];
                if (StartValue != 0)
                    for (int j = 0; j < Col; j++)
                        A[i][j] = StartValue;
            }
            E = 5;
        }
        /// <summary>
        /// создание новой мартрицы с такими же составляющими
        /// </summary>
        public Matrix(Matrix A)
        {
            Matrix C = new Matrix(A.A);
            this.Col = C.Col;
            this.Row = C.Row;
            this.A = C.A;
            E = 5;
        }
        #endregion

        public decimal this[int index1, int index2]
        {
            get
            {
                return A[index1][index2];
            }
            set
            {
                A[index1][index2] = value;
            }
        }
        public decimal[] this[int index]
        {
            get
            {
                return A[index];
            }
            set
            {
                A[index] = value;
            }
        }
        /// <summary>
        ///удаление строки Row и столбца Col для дополнения
        /// </summary>
        public Matrix Delete(int Row, int Col) 
        {
            Matrix C = new Matrix(this.Row - 1, this.Col - 1);
            int k = 0, n = 0;
            for (int i = 0; i < C.Row; i++)
            {
                if (i < Row) k = i; else k = i + 1;
                for (int j = 0; j < C.Col; j++)
                {
                    if (j < Col) n = j; else n = j + 1;
                    C[i, j] = this[k, n];
                }
            }
            return C;
        }
        /// <summary>
        /// Возвращает единичную матрицу размерности Length*Length
        /// </summary>
        public static Matrix GetE(int Length) 
        {
            Matrix E = new Matrix(Length);
            for (int i = 0; i < Length; i++)
                for (int j = 0; j < Length; j++)
                    if (i == j) E[i, j] = 1; else E[i, j] = 0;
            return E;
        }
        /// <summary>
        /// путь матрицы
        /// </summary>
        public decimal? Trace() 
        {
            if (this.Row != this.Col) return null;
            decimal Sum = 0;
            for (int i = 0; i < this.Row; i++) Sum += this[i, i];
            return Sum;
        }
        /// <summary>
        /// this^n
        /// </summary>
        public Matrix Pow(int n) 
        {
            if (!this.CheckDemension(this) && n < -1) return null;
            if (n == -1) return (decimal)(1 / this.Det()) * Transpose(this.Addition());
            if (n == 0) return Matrix.GetE(this.Row);
            Matrix B = this;
            for (int i = 1; i < n; i++)
            {
                B *= this;
            }
            return B;
        }
        /// <summary>
        /// транспонирует A
        /// </summary>
        public static Matrix Transpose(Matrix A) 
        {
            Matrix C = new Matrix(A.Col, A.Row);
            for (int i = 0; i < C.Row; i++)
                for (int j = 0; j < C.Col; j++)
                    C[i, j] = A[j, i];
            return C;
        }
        /// <summary>
        /// дополнение к this, для this^(-1)
        /// </summary>
        public Matrix Addition() 
        {
            Matrix C = new Matrix(this.A);
            for (int i = 0; i < this.Row; i++)
                for (int j = 0; j < this.Col; j++)
                {
                    Matrix a = this.Delete(i, j);
                    decimal det = (decimal)a.Det();
                    C[i, j] = (decimal)Math.Pow(-1, i + j) * det;
                }
            return C;
        }
        
        /// <summary>
        /// возвращает из this новую матрицу с номерами столбцов из array
        /// </summary>
        public Matrix GetPart(params int[] array) 
        {
            Matrix C= Transpose(this);
            Matrix Result=new Matrix(Directions.Vertical, C[array[0]]);
            for (int i = 1; i < array.Length; i++)
                Result = Result.Concatenation(new Matrix(Directions.Vertical, C[array[i]]));
            return Result;
        }
        
        /// <summary>
        /// возвращает из this новую матрицу с номерами строк из array
        /// </summary>
        public Matrix GetRows(params int[] array) 
        {
            Matrix C = new Matrix (this);
            Matrix Result = new Matrix(Directions.Horizontal, C[array[0]]);
            for (int i = 1; i < array.Length; i++)
                Result = Result.Add(new Matrix(Directions.Horizontal, C[array[i]]));
            return Result;
        }
        /// <summary>
        /// Смена местами строки i с строкой j
        /// </summary>
        private void swap(int i, int j)
        {
            for (int k = 0; k < this.Col; k++)
            {
                A[i][k] += A[j][k];
                A[j][k] = A[i][k] - A[j][k];
                A[i][k] = A[i][k] - A[j][k];
            }
        }
        /// <summary>
        /// Приведение матрицы к треугольному виду для гаусса
        /// </summary>
        public Matrix ToTriangle(ref int swaps)
        {
            if (!this.CheckDemension(this)) return null;
            Matrix C = new Matrix(this);
            for (int k = 0; k < C.Row - 1; k++)
            {
                int q = k + 1;
                while (C[k][k] < 0.00000000001m && C[k][k] > - 0.00000000001m)
                {
                    C.swap(k, q);
                    q++;
                    swaps++;
                }
                for (int i = k + 1; i < C.Row; i++)
                {
                    decimal p = C[i, k] / C[k, k];
                    for (int j = k; j < C.Col; j++)
                        C[i, j] = C[i, j] - C[k, j] * p;
                }
            }
            return C;
        }
        /// <summary>
        /// Решение системы уравнений гауссом, this - кофценты, b - ответы
        /// </summary>
        public Matrix Reshenie(Matrix b)
        {
            int swaps = 0;
            Matrix C = this.Concatenation(b).ToTriangle(ref swaps);
            Matrix Resh = new Matrix(1, this.Col);
            for (int i = C.Row - 1; i >= 0; i--)
            {
                decimal Sum = 0;
                for (int j = i + 1; j < C.Col - 1; j++)
                    Sum += C[i, j] * Resh[0][j];
                Resh[0][i] = (C[i][C.Col - 1] - Sum) / C[i][i];
            }
            return Resh;
        }
        /// <summary>
        /// Решение системы уравнений гауссом, this - система уравнений, последний столбец - ответы
        /// </summary>
        public Matrix Reshenie()
        {
            int swaps = 0;
            Matrix C = this.ToTriangle(ref swaps);
            Matrix Resh = new Matrix(1, this.Row);
            for (int i = C.Row - 1; i >= 0; i--)
            {
                decimal Sum = 0;
                for (int j = i + 1; j < C.Col - 1; j++)
                    Sum += C[i, j] * Resh[0][j];
                Resh[0][i] = (C[i][C.Col - 1] - Sum) / C[i][i];
            }
            return Resh;
        }
        /// <summary>
        /// Присоеденяет к матрице this справа матрицу В
        /// </summary>
        public Matrix Concatenation(Matrix B)
        {
            Matrix C = new Matrix(this.Row, this.Col + B.Col);
            for (int i = 0; i < this.Row; i++)
                for (int j = 0; j < this.Col + B.Col; j++)
                    if (j < this.Col) C[i][j] = this[i, j];
                    else C[i, j] = B[i][j - this.Col];
            return C;
        }
        /// <summary>
        /// Присоеденяет к матрице this снизу матрицу В
        /// </summary>
        public Matrix Add(Matrix B)
        {
            Matrix C = new Matrix(this.Row + 1, this.Col);
            for (int i = 0; i < this.Row + B.Row; i++)
                for (int j = 0; j < this.Col; j++)
                    if (i < this.Row) C[i][j] = this[i, j];
                    else C[i, j] = B[i - this.Row][j];
            return C;
        }
        /// <summary>
        /// Возвращает детерминант
        /// </summary>
        /// 
        public decimal? Det() 
        {
            decimal Sum = 1;
            int swaps = 0;
            Matrix C = this.ToTriangle(ref swaps);
            for (int i = 0; i < C.Row; i++)
                Sum *= C[i, i];
            return swaps % 2 == 0 ? Sum : -Sum;
        }
        /// <summary>
        ///Проверка является ли матрица квадратной
        /// </summary>
        public bool CheckDemension(Matrix A) 
        {
            return A.Col == this.Col && this.Row == A.Row;
        }
        #region Операторы
        public static Matrix operator -(Matrix A, Matrix B)
        {
            if (!A.CheckDemension(B)) return null;
            Matrix C = new Matrix(A.Row, B.Col);
            for (int i = 0; i < A.Row; i++)
                for (int j = 0; j < A.Col; j++)
                    C[i, j] = A[i, j] - B[i, j];
            return C;
        }
        public static Matrix operator +(Matrix A, Matrix B)
        {
            if (!A.CheckDemension(B)) return null;
            Matrix C = new Matrix(A.Row, B.Col);
            for (int i = 0; i < A.Row; i++)
                for (int j = 0; j < A.Col; j++)
                    C[i, j] = A[i, j] + B[i, j];
            return C;
        }
        public static Matrix operator *(Matrix A, Matrix B)
        {
            if (A.Col != B.Row) return null;
            Matrix C = new Matrix(A.Row, B.Col);
            for (int i = 0; i < A.Row; i++)
                for (int j = 0; j < B.Col; j++)
                {
                    decimal Sum = 0;
                    for (int m = 0; m < A.Col; m++)
                        Sum += A[i, m] * B[m, j];
                    C[i, j] = Sum;
                }
            return C;
        }
        public static Matrix operator *(decimal a, Matrix A)
        {
            for (int i = 0; i < A.Row; i++)
                for (int j = 0; j < A.Col; j++)
                    A[i, j] *= a;
            return A;
        }
        public static decimal[] operator *(Matrix A, decimal[] y)
        {
            if (A.Col != y.Length) return null;
            decimal[] ar = new decimal[A.Row];
            for (int i = 0; i < ar.Length; i++)
            {
                ar[i] = 0;
            }
            for (int i = 0; i < A.Row; i++)
            {
                for (int j = 0; j < A.Col; j++)
                {
                    ar[i] += A[i, j] * y[j];
                }
            }
            return ar;
        }
        #endregion
        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                    result += Math.Round(this[i, j], E).ToString() + "\t";
                result += "\n";
            }
            return result;
        }
    
        /// <summary>
        /// вывод как системы уравнений b - матрица ответов, n - номера выводимых строк
        /// </summary>
    /*    public string ToString(int[] n, Matrix b, PrintVariants variant = PrintVariants.Normal) 
        {
            string result = "";
            Matrix C = this.Concatenation(b);
            int j = 0, k = 0;
            for (int i = 0; i < C.Row; i++)
            {
                j = 0;
                k = 0;
                while (k < C.Col - 1)
                {   
                    if (variant == PrintVariants.Cutted)
                    {
                        while (j < C.Col && C[i][j] == 0) j++;
                    }                 
                    k = j;
                    if (variant == PrintVariants.Cutted)
                    {
                        while (j + 1 < C.Col && C[i][j + 1] == 0) j++;
                        if (j != k) j++;
                    }
                    if (j == k && j < C.Col - 1) j++;
                    result += (C[i, k] == 1 ? "" : C[i, k] == -1 ? "-" : MainWindow.Rounding(C[i, k], E).ToString()) +
                        (k < C.Col - 1 ? "x" + (k + 1) : "") + ((k != C.Col - 1)?
                        (j > C.Col - 2  ? "=" : C[i, j] < 0 ? " " : " +") : "");                    
                }
                result += "\n";
            }
            return result;
        }*/
    }
}

